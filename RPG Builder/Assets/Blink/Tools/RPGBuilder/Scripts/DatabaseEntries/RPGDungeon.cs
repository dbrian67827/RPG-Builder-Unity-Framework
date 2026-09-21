using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGDungeon : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum DungeonType
    {
        Dungeon,
        Raid,
        Scenario,
        Delve,
        Arena,
        Battleground,
        SoloChallenge,
        GroupChallenge
    }

    public DungeonType dungeonType = DungeonType.Dungeon;
    
    public enum DungeonDifficulty
    {
        Normal,
        Heroic,
        Mythic,
        Legendary,
        Timewalking,
        Challenge,
        Custom
    }

    [Serializable]
    public class DungeonDifficultyData
    {
        public DungeonDifficulty difficulty = DungeonDifficulty.Normal;
        public string difficultyName = "Normal";
        public int minLevel = 1;
        public int maxLevel = 10;
        public int recommendedLevel = 5;
        public int minPlayers = 1;
        public int maxPlayers = 5;
        public float healthMultiplier = 1f;
        public float damageMultiplier = 1f;
        public float lootMultiplier = 1f;
        public float expMultiplier = 1f;
        [LootTableID] public int lootTableID = -1;
        public float lockoutTime = 86400f; // 24h
        public bool hasLockout = true;
        public bool resetOnWipe = false;
    }
    [RPGDataList] public List<DungeonDifficultyData> difficulties = new List<DungeonDifficultyData>();

    [Serializable]
    public class DungeonStage
    {
        public string stageName = "Stage 1";
        public string description = "";
        [GameSceneID] public int gameSceneID = -1;
        public Vector3 entryPosition;
        public Vector3 exitPosition;
        
        [Serializable]
        public class StageObjective
        {
            public enum ObjectiveType
            {
                KillNPC,
                KillAllNPCs,
                CollectItem,
                InteractWithObject,
                ReachPosition,
                SurviveTime,
                ProtectNPC,
                EscortNPC,
                Custom
            }
            public ObjectiveType objectiveType;
            [NPCID] public int npcID = -1;
            [ItemID] public int itemID = -1;
            public int requiredAmount = 1;
            public Vector3 position;
            public float radius = 5f;
            public float timeLimit = 0f;
            public string customKey = "";
        }
        [RPGDataList] public List<StageObjective> objectives = new List<StageObjective>();
        
        [RPGDataList] public List<GameActionsData.GameAction> OnStageStartActions = new List<GameActionsData.GameAction>();
        [RPGDataList] public List<GameActionsData.GameAction> OnStageCompleteActions = new List<GameActionsData.GameAction>();
    }
    [RPGDataList] public List<DungeonStage> stages = new List<DungeonStage>();

    [Serializable]
    public class DungeonBoss
    {
        [NPCID] public int npcID = -1;
        public string bossNameOverride = "";
        public int stageIndex = 0;
        public bool isFinalBoss = false;
        [LootTableID] public int lootTableID = -1;
        public GameObject spawnEffect;
        public Vector3 spawnPosition;
    }
    [RPGDataList] public List<DungeonBoss> bosses = new List<DungeonBoss>();

    [GameSceneID] public int entryGameSceneID = -1;
    [GameSceneID] public int dungeonGameSceneID = -1;
    public Vector3 entryWorldPosition;
    public Vector3 dungeonEntryPosition;
    
    public float timeLimit = 0f; // 0 = no limit
    public int deathLimit = 0; // 0 = unlimited
    public bool allowRespawn = true;
    public float respawnTime = 10f;
    public bool allowMount = false;
    public bool allowPet = true;
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;
    
    [RPGDataList] public List<GameActionsData.GameAction> OnEnterActions = new List<GameActionsData.GameAction>();
    [RPGDataList] public List<GameActionsData.GameAction> OnExitActions = new List<GameActionsData.GameAction>();
    [RPGDataList] public List<GameActionsData.GameAction> OnCompleteActions = new List<GameActionsData.GameAction>();
    [RPGDataList] public List<GameActionsData.GameAction> OnFailActions = new List<GameActionsData.GameAction>();
    [RPGDataList] public List<GameActionsData.GameAction> OnWipeActions = new List<GameActionsData.GameAction>();
    public bool UseGameActionsTemplate;
    public GameActionsTemplate GameActionsTemplate;
    
    public string description;
    public string loreDescription;
    
    public GameObject loadingScreenPrefab;
    public Sprite loadingScreenImage;
    public string loadingScreenText;
    
    public int requiredItemLevel = 0;
    public bool checkItemLevel = false;
    
    [CurrencyID] public int entryCurrencyID = -1;
    public int entryCost = 0;
    public bool consumeEntryCost = true;
    
    [Serializable]
    public class DungeonReward
    {
        public enum RewardType
        {
            Item,
            Currency,
            Experience,
            Title,
            Achievement,
            Mount,
            Pet
        }
        public RewardType rewardType;
        [ItemID] public int itemID = -1;
        [CurrencyID] public int currencyID = -1;
        [TitleID] public int titleID = -1;
        [AchievementID] public int achievementID = -1;
        [MountID] public int mountID = -1;
        [PetID] public int petID = -1;
        public int amount = 1;
        public float chance = 100f;
        public bool isGuaranteed = false;
    }
    [RPGDataList] public List<DungeonReward> completionRewards = new List<DungeonReward>();
    
    public bool isRepeatable = true;
    public bool saveProgress = true;
    public bool scaleWithPlayerCount = false;
    
    public void UpdateEntryData(RPGDungeon newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        dungeonType = newEntryData.dungeonType;
        difficulties = newEntryData.difficulties;
        stages = newEntryData.stages;
        bosses = newEntryData.bosses;
        entryGameSceneID = newEntryData.entryGameSceneID;
        dungeonGameSceneID = newEntryData.dungeonGameSceneID;
        entryWorldPosition = newEntryData.entryWorldPosition;
        dungeonEntryPosition = newEntryData.dungeonEntryPosition;
        timeLimit = newEntryData.timeLimit;
        deathLimit = newEntryData.deathLimit;
        allowRespawn = newEntryData.allowRespawn;
        respawnTime = newEntryData.respawnTime;
        allowMount = newEntryData.allowMount;
        allowPet = newEntryData.allowPet;
        Requirements = newEntryData.Requirements;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
        OnEnterActions = newEntryData.OnEnterActions;
        OnExitActions = newEntryData.OnExitActions;
        OnCompleteActions = newEntryData.OnCompleteActions;
        OnFailActions = newEntryData.OnFailActions;
        OnWipeActions = newEntryData.OnWipeActions;
        UseGameActionsTemplate = newEntryData.UseGameActionsTemplate;
        GameActionsTemplate = newEntryData.GameActionsTemplate;
        description = newEntryData.description;
        loreDescription = newEntryData.loreDescription;
        loadingScreenPrefab = newEntryData.loadingScreenPrefab;
        loadingScreenImage = newEntryData.loadingScreenImage;
        loadingScreenText = newEntryData.loadingScreenText;
        requiredItemLevel = newEntryData.requiredItemLevel;
        checkItemLevel = newEntryData.checkItemLevel;
        entryCurrencyID = newEntryData.entryCurrencyID;
        entryCost = newEntryData.entryCost;
        consumeEntryCost = newEntryData.consumeEntryCost;
        completionRewards = newEntryData.completionRewards;
        isRepeatable = newEntryData.isRepeatable;
        saveProgress = newEntryData.saveProgress;
        scaleWithPlayerCount = newEntryData.scaleWithPlayerCount;
    }
}
