using System;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BLINK.RPGBuilder.Managers
{
    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager Instance { get; private set; }

        public class ActiveDungeon
        {
            public int dungeonID;
            public int difficultyIndex;
            public int currentStage;
            public float remainingTime;
            public int deathCount;
            public bool isActive;
            public DateTime startTime;
            public Dictionary<int, bool> stageCompleted = new Dictionary<int, bool>();
            public List<int> killedBosses = new List<int>();
        }

        private ActiveDungeon currentDungeon;
        private Dictionary<int, DateTime> dungeonLockouts = new Dictionary<int, DateTime>();

        public Action<RPGDungeon> OnDungeonEntered;
        public Action<RPGDungeon> OnDungeonCompleted;
        public Action<RPGDungeon> OnDungeonFailed;
        public Action<RPGDungeon, int> OnStageCompleted;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadLockouts();
        }

        private void LoadLockouts()
        {
            if (Character.Instance?.CharacterData?.DungeonLockouts != null)
            {
                foreach (var lockout in Character.Instance.CharacterData.DungeonLockouts)
                {
                    dungeonLockouts[lockout.dungeonID] = DateTime.FromBinary(lockout.lockoutEndTicks);
                }
            }
        }

        public bool CanEnterDungeon(int dungeonID, int difficultyIndex = 0)
        {
            var all = GameDatabase.Instance.GetDungeons();
            if (!all.ContainsKey(dungeonID)) return false;

            var dungeon = all[dungeonID];
            if (difficultyIndex >= dungeon.difficulties.Count) return false;

            // Lockout check
            if (dungeonLockouts.ContainsKey(dungeonID))
            {
                if (DateTime.Now < dungeonLockouts[dungeonID] && dungeon.difficulties[difficultyIndex].hasLockout)
                    return false;
            }

            // Requirements
            if (!RequirementsManager.Instance.CheckRequirements(dungeon.Requirements, dungeon.RequirementsTemplate))
                return false;

            // Entry cost
            if (dungeon.entryCurrencyID != -1 && dungeon.entryCost > 0)
            {
                var currency = Character.Instance.getCurrencyDATA(dungeon.entryCurrencyID);
                if (currency == null || currency.count < dungeon.entryCost) return false;
            }

            // Item level
            if (dungeon.checkItemLevel && GetAverageItemLevel() < dungeon.requiredItemLevel)
                return false;

            return true;
        }

        public void EnterDungeon(int dungeonID, int difficultyIndex = 0)
        {
            if (!CanEnterDungeon(dungeonID, difficultyIndex)) return;

            var dungeon = GameDatabase.Instance.GetDungeons()[dungeonID];

            // Pay cost
            if (dungeon.entryCurrencyID != -1 && dungeon.entryCost > 0 && dungeon.consumeEntryCost)
                InventoryManager.Instance.RemoveCurrency(dungeon.entryCurrencyID, dungeon.entryCost);

            currentDungeon = new ActiveDungeon
            {
                dungeonID = dungeonID,
                difficultyIndex = difficultyIndex,
                currentStage = 0,
                remainingTime = dungeon.timeLimit,
                deathCount = 0,
                isActive = true,
                startTime = DateTime.Now
            };

            // Game Actions OnEnter
            GameActionsManager.Instance.ExecuteGameActions(dungeon.OnEnterActions, null, null);

            OnDungeonEntered?.Invoke(dungeon);
            Debug.Log($"Entered Dungeon: {dungeon.entryDisplayName} - {dungeon.difficulties[difficultyIndex].difficultyName}");

            // Load scene
            if (dungeon.dungeonGameSceneID != -1)
            {
                var scene = GameDatabase.Instance.GetGameScenes()[dungeon.dungeonGameSceneID];
                LoadingScreenManager.Instance?.LoadScene(scene.entryName);
            }
        }

        public void CompleteStage(int stageIndex)
        {
            if (currentDungeon == null) return;
            var dungeon = GameDatabase.Instance.GetDungeons()[currentDungeon.dungeonID];
            if (stageIndex >= dungeon.stages.Count) return;

            currentDungeon.stageCompleted[stageIndex] = true;
            currentDungeon.currentStage = stageIndex + 1;

            var stage = dungeon.stages[stageIndex];
            GameActionsManager.Instance.ExecuteGameActions(stage.OnStageCompleteActions, null, null);

            OnStageCompleted?.Invoke(dungeon, stageIndex);

            if (stageIndex == dungeon.stages.Count - 1)
                CompleteDungeon();
        }

        public void CompleteDungeon()
        {
            if (currentDungeon == null) return;
            var dungeon = GameDatabase.Instance.GetDungeons()[currentDungeon.dungeonID];
            var difficulty = dungeon.difficulties[currentDungeon.difficultyIndex];

            // Rewards
            foreach (var reward in dungeon.completionRewards)
            {
                if (UnityEngine.Random.Range(0f, 100f) <= reward.chance)
                    GrantReward(reward);
            }

            // Loot table
            if (difficulty.lootTableID != -1)
            {
                // Roll loot table
                var lootTable = GameDatabase.Instance.GetLootTables()[difficulty.lootTableID];
                // Simplified
            }

            GameActionsManager.Instance.ExecuteGameActions(dungeon.OnCompleteActions, null, null);

            // Lockout
            if (difficulty.hasLockout)
                dungeonLockouts[dungeon.ID] = DateTime.Now.AddSeconds(difficulty.lockoutTime);

            OnDungeonCompleted?.Invoke(dungeon);
            Debug.Log($"Dungeon Completed: {dungeon.entryDisplayName}");

            SaveLockouts();
            currentDungeon = null;
        }

        public void FailDungeon()
        {
            if (currentDungeon == null) return;
            var dungeon = GameDatabase.Instance.GetDungeons()[currentDungeon.dungeonID];
            GameActionsManager.Instance.ExecuteGameActions(dungeon.OnFailActions, null, null);
            OnDungeonFailed?.Invoke(dungeon);
            currentDungeon = null;
        }

        public void HandlePlayerDeath()
        {
            if (currentDungeon == null) return;
            currentDungeon.deathCount++;

            var dungeon = GameDatabase.Instance.GetDungeons()[currentDungeon.dungeonID];
            if (dungeon.deathLimit > 0 && currentDungeon.deathCount >= dungeon.deathLimit)
                FailDungeon();
        }

        public void HandleBossKill(int npcID)
        {
            if (currentDungeon == null) return;
            if (!currentDungeon.killedBosses.Contains(npcID))
                currentDungeon.killedBosses.Add(npcID);

            var dungeon = GameDatabase.Instance.GetDungeons()[currentDungeon.dungeonID];
            foreach (var boss in dungeon.bosses)
            {
                if (boss.npcID == npcID)
                {
                    // Boss loot
                    if (boss.lootTableID != -1)
                    {
                        // Roll
                    }
                }
            }
        }

        private void GrantReward(RPGDungeon.DungeonReward reward)
        {
            switch (reward.rewardType)
            {
                case RPGDungeon.DungeonReward.RewardType.Item:
                    InventoryManager.Instance.AddItem(reward.itemID, reward.amount);
                    break;
                case RPGDungeon.DungeonReward.RewardType.Currency:
                    InventoryManager.Instance.AddCurrency(reward.currencyID, reward.amount);
                    break;
                case RPGDungeon.DungeonReward.RewardType.Experience:
                    LevelingManager.Instance.AddExperience(reward.amount);
                    break;
                case RPGDungeon.DungeonReward.RewardType.Title:
                    TitleManager.Instance?.UnlockTitle(reward.titleID);
                    break;
                case RPGDungeon.DungeonReward.RewardType.Achievement:
                    AchievementManager.Instance?.CompleteAchievement(reward.achievementID);
                    break;
                case RPGDungeon.DungeonReward.RewardType.Mount:
                    MountManager.Instance?.UnlockMount(reward.mountID);
                    break;
                case RPGDungeon.DungeonReward.RewardType.Pet:
                    PetManager.Instance?.UnlockPet(reward.petID);
                    break;
            }
        }

        private int GetAverageItemLevel()
        {
            // Simplified - would calculate from equipped items
            return Character.Instance?.Level ?? 1;
        }

        private void SaveLockouts()
        {
            if (Character.Instance?.CharacterData == null) return;
            var list = new List<CharacterEntries.DungeonLockoutEntry>();
            foreach (var kvp in dungeonLockouts)
            {
                list.Add(new CharacterEntries.DungeonLockoutEntry
                {
                    dungeonID = kvp.Key,
                    lockoutEndTicks = kvp.Value.ToBinary()
                });
            }
            Character.Instance.CharacterData.DungeonLockouts = list;
        }

        public bool IsInDungeon() => currentDungeon != null && currentDungeon.isActive;
        public ActiveDungeon GetCurrentDungeon() => currentDungeon;
        public bool IsDungeonOnLockout(int dungeonID)
        {
            return dungeonLockouts.ContainsKey(dungeonID) && DateTime.Now < dungeonLockouts[dungeonID];
        }
        public TimeSpan GetLockoutRemaining(int dungeonID)
        {
            if (!dungeonLockouts.ContainsKey(dungeonID)) return TimeSpan.Zero;
            return dungeonLockouts[dungeonID] - DateTime.Now;
        }

        private void Update()
        {
            if (currentDungeon == null || !currentDungeon.isActive) return;
            var dungeon = GameDatabase.Instance.GetDungeons()[currentDungeon.dungeonID];
            if (dungeon.timeLimit > 0)
            {
                currentDungeon.remainingTime -= Time.deltaTime;
                if (currentDungeon.remainingTime <= 0) FailDungeon();
            }
        }
    }
}
