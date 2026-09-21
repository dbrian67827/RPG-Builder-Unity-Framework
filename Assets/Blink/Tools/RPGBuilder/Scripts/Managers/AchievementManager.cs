using System;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Characters;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager Instance { get; private set; }

        public class AchievementProgress
        {
            public int achievementID;
            public Dictionary<int, int> objectiveProgress = new Dictionary<int, int>(); // objectiveIndex -> currentAmount
            public bool isCompleted;
            public bool isClaimed;
            public DateTime completedTime;
            public int currentCount;
        }

        private Dictionary<int, AchievementProgress> achievementProgress = new Dictionary<int, AchievementProgress>();
        public Action<RPGAchievement> OnAchievementCompleted;
        public Action<RPGAchievement> OnAchievementProgress;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            InitializeAchievements();
        }

        public void InitializeAchievements()
        {
            var allAchievements = GameDatabase.Instance.GetAchievements();
            if (allAchievements == null) return;

            foreach (var ach in allAchievements.Values)
            {
                if (!achievementProgress.ContainsKey(ach.ID))
                {
                    var prog = new AchievementProgress { achievementID = ach.ID };
                    for (int i = 0; i < ach.objectives.Count; i++) prog.objectiveProgress[i] = 0;
                    achievementProgress[ach.ID] = prog;
                }
            }

            // Load from save
            if (Character.Instance != null && Character.Instance.CharacterData != null)
            {
                var saved = Character.Instance.CharacterData.Achievements;
                if (saved != null)
                {
                    foreach (var savedAch in saved)
                    {
                        if (achievementProgress.ContainsKey(savedAch.achievementID))
                        {
                            achievementProgress[savedAch.achievementID].isCompleted = savedAch.isCompleted;
                            achievementProgress[savedAch.achievementID].isClaimed = savedAch.isClaimed;
                            achievementProgress[savedAch.achievementID].currentCount = savedAch.progress;
                        }
                    }
                }
            }
        }

        public void UpdateObjective(RPGAchievement.AchievementObjective.ObjectiveType type, int id, int amount = 1)
        {
            var allAchievements = GameDatabase.Instance.GetAchievements();
            if (allAchievements == null) return;

            foreach (var kvp in allAchievements)
            {
                var ach = kvp.Value;
                if (IsAchievementCompleted(ach.ID)) continue;

                for (int i = 0; i < ach.objectives.Count; i++)
                {
                    var obj = ach.objectives[i];
                    if (obj.objectiveType != type) continue;

                    bool matches = false;
                    switch (type)
                    {
                        case RPGAchievement.AchievementObjective.ObjectiveType.KillNPC:
                            matches = obj.npcID == id || obj.npcID == -1;
                            break;
                        case RPGAchievement.AchievementObjective.ObjectiveType.CompleteQuest:
                            matches = obj.questID == id;
                            break;
                        case RPGAchievement.AchievementObjective.ObjectiveType.GainItem:
                            matches = obj.itemID == id;
                            break;
                        case RPGAchievement.AchievementObjective.ObjectiveType.CraftItem:
                            matches = obj.recipeID == id;
                            break;
                        case RPGAchievement.AchievementObjective.ObjectiveType.GatherResource:
                            matches = obj.resourceID == id;
                            break;
                        case RPGAchievement.AchievementObjective.ObjectiveType.LearnAbility:
                            matches = obj.abilityID == id;
                            break;
                        case RPGAchievement.AchievementObjective.ObjectiveType.EarnCurrency:
                            matches = obj.currencyID == id;
                            break;
                        case RPGAchievement.AchievementObjective.ObjectiveType.CompleteDungeon:
                            matches = obj.dungeonID == id;
                            break;
                        default:
                            matches = true;
                            break;
                    }

                    if (matches)
                    {
                        AddProgress(ach.ID, i, amount);
                    }
                }
            }
        }

        public void AddProgress(int achievementID, int objectiveIndex, int amount)
        {
            if (!achievementProgress.ContainsKey(achievementID)) return;
            var prog = achievementProgress[achievementID];
            if (prog.isCompleted) return;

            if (!prog.objectiveProgress.ContainsKey(objectiveIndex)) prog.objectiveProgress[objectiveIndex] = 0;
            prog.objectiveProgress[objectiveIndex] += amount;

            var ach = GameDatabase.Instance.GetAchievements()[achievementID];
            if (objectiveIndex < ach.objectives.Count)
            {
                var obj = ach.objectives[objectiveIndex];
                if (prog.objectiveProgress[objectiveIndex] >= obj.requiredAmount)
                {
                    // Check if all objectives complete
                    bool allComplete = true;
                    for (int i = 0; i < ach.objectives.Count; i++)
                    {
                        int cur = prog.objectiveProgress.ContainsKey(i) ? prog.objectiveProgress[i] : 0;
                        if (cur < ach.objectives[i].requiredAmount)
                        {
                            allComplete = false;
                            break;
                        }
                    }
                    if (allComplete)
                    {
                        CompleteAchievement(achievementID);
                    }
                }
            }

            OnAchievementProgress?.Invoke(ach);
            SaveProgress();
        }

        public void CompleteAchievement(int achievementID)
        {
            if (!achievementProgress.ContainsKey(achievementID)) return;
            var prog = achievementProgress[achievementID];
            if (prog.isCompleted) return;

            prog.isCompleted = true;
            prog.completedTime = DateTime.Now;

            var ach = GameDatabase.Instance.GetAchievements()[achievementID];
            Debug.Log($"Achievement Completed: {ach.entryDisplayName}");

            // Rewards
            foreach (var reward in ach.rewards)
            {
                GrantReward(reward);
            }

            // Game Actions
            if (ach.UseGameActionsTemplate && ach.GameActionsTemplate != null)
            {
                // Execute via GameActionsManager
                GameActionsManager.Instance.ExecuteGameActions(ach.GameActionsTemplate.GameActions, null, null);
            }
            else
            {
                GameActionsManager.Instance.ExecuteGameActions(ach.GameActions, null, null);
            }

            OnAchievementCompleted?.Invoke(ach);

            // Visual/Sound
            if (ach.visualEffectOnComplete != null)
            {
                Instantiate(ach.visualEffectOnComplete, Character.Instance.transform.position, Quaternion.identity);
            }

            // Save
            SaveProgress();

            // Check chain
            if (ach.chain.isPartOfChain && ach.chain.nextAchievementID != -1)
            {
                // Auto-start next?
            }
        }

        private void GrantReward(RPGAchievement.AchievementReward reward)
        {
            switch (reward.rewardType)
            {
                case RPGAchievement.AchievementReward.RewardType.Item:
                    InventoryManager.Instance.AddItem(reward.itemID, reward.amount);
                    break;
                case RPGAchievement.AchievementReward.RewardType.Currency:
                    InventoryManager.Instance.AddCurrency(reward.currencyID, reward.amount);
                    break;
                case RPGAchievement.AchievementReward.RewardType.Title:
                    TitleManager.Instance?.UnlockTitle(reward.titleID);
                    break;
                case RPGAchievement.AchievementReward.RewardType.Mount:
                    MountManager.Instance?.UnlockMount(reward.mountID);
                    break;
                case RPGAchievement.AchievementReward.RewardType.Pet:
                    PetManager.Instance?.UnlockPet(reward.petID);
                    break;
                case RPGAchievement.AchievementReward.RewardType.Experience:
                    LevelingManager.Instance?.AddExperience(reward.amount);
                    break;
            }
        }

        public bool IsAchievementCompleted(int achievementID)
        {
            return achievementProgress.ContainsKey(achievementID) && achievementProgress[achievementID].isCompleted;
        }

        public int GetProgress(int achievementID, int objectiveIndex)
        {
            if (!achievementProgress.ContainsKey(achievementID)) return 0;
            var prog = achievementProgress[achievementID];
            return prog.objectiveProgress.ContainsKey(objectiveIndex) ? prog.objectiveProgress[objectiveIndex] : 0;
        }

        public float GetCompletionPercent(int achievementID)
        {
            if (!achievementProgress.ContainsKey(achievementID)) return 0f;
            var ach = GameDatabase.Instance.GetAchievements()[achievementID];
            var prog = achievementProgress[achievementID];
            if (ach.objectives.Count == 0) return prog.isCompleted ? 1f : 0f;

            int totalRequired = 0;
            int totalCurrent = 0;
            for (int i = 0; i < ach.objectives.Count; i++)
            {
                totalRequired += ach.objectives[i].requiredAmount;
                totalCurrent += prog.objectiveProgress.ContainsKey(i) ? Math.Min(prog.objectiveProgress[i], ach.objectives[i].requiredAmount) : 0;
            }
            return totalRequired > 0 ? (float)totalCurrent / totalRequired : 0f;
        }

        private void SaveProgress()
        {
            if (Character.Instance == null || Character.Instance.CharacterData == null) return;
            var list = new List<CharacterEntries.AchievementEntry>();
            foreach (var kvp in achievementProgress)
            {
                list.Add(new CharacterEntries.AchievementEntry
                {
                    achievementID = kvp.Key,
                    isCompleted = kvp.Value.isCompleted,
                    isClaimed = kvp.Value.isClaimed,
                    progress = kvp.Value.currentCount,
                    completedTimestamp = kvp.Value.completedTime.Ticks
                });
            }
            Character.Instance.CharacterData.Achievements = list;
        }

        public List<RPGAchievement> GetCompletedAchievements()
        {
            var result = new List<RPGAchievement>();
            var all = GameDatabase.Instance.GetAchievements();
            foreach (var kvp in achievementProgress)
            {
                if (kvp.Value.isCompleted && all.ContainsKey(kvp.Key)) result.Add(all[kvp.Key]);
            }
            return result;
        }

        public int GetTotalPoints()
        {
            int total = 0;
            var all = GameDatabase.Instance.GetAchievements();
            foreach (var kvp in achievementProgress)
            {
                if (kvp.Value.isCompleted && all.ContainsKey(kvp.Key)) total += all[kvp.Key].points;
            }
            return total;
        }
    }
}
