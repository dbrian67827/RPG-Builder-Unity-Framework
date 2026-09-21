using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGAchievement : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum AchievementCategory
    {
        General,
        Quests,
        Exploration,
        Combat,
        Dungeons,
        Crafting,
        Gathering,
        Social,
        PvP,
        Collections,
        Events,
        FeatsOfStrength,
        Legacy
    }

    public AchievementCategory category = AchievementCategory.General;
    public int points = 10;
    public bool isHidden = false;
    public bool isAccountWide = false;
    public bool showProgressBar = true;
    public int sortOrder = 0;

    [Serializable]
    public class AchievementObjective
    {
        public enum ObjectiveType
        {
            KillNPC,
            KillNPCFamily,
            CompleteQuest,
            ReachLevel,
            GainItem,
            CraftItem,
            GatherResource,
            EnterRegion,
            EnterGameScene,
            ReachFactionRank,
            LearnAbility,
            LearnRecipe,
            EarnCurrency,
            CompleteDungeon,
            WinPvP,
            ExplorePOI,
            CompleteTask,
            Custom
        }
        public ObjectiveType objectiveType;
        [NPCID] public int npcID = -1;
        public RPGBNPCFamily npcFamily;
        [QuestID] public int questID = -1;
        [ItemID] public int itemID = -1;
        [RecipeID] public int recipeID = -1;
        [ResourceID] public int resourceID = -1;
        [FactionID] public int factionID = -1;
        [AbilityID] public int abilityID = -1;
        [CurrencyID] public int currencyID = -1;
        [DungeonID] public int dungeonID = -1;
        [GameSceneID] public int gameSceneID = -1;
        public RegionTemplate region;
        public int requiredAmount = 1;
        public string customKey = "";
        public string descriptionOverride = "";
    }

    [RPGDataList] public List<AchievementObjective> objectives = new List<AchievementObjective>();

    [Serializable]
    public class AchievementReward
    {
        public enum RewardType
        {
            Item,
            Currency,
            Title,
            Mount,
            Pet,
            Ability,
            Bonus,
            GameModifier,
            Experience,
            Lore
        }
        public RewardType rewardType;
        [ItemID] public int itemID = -1;
        [CurrencyID] public int currencyID = -1;
        [TitleID] public int titleID = -1;
        [MountID] public int mountID = -1;
        [PetID] public int petID = -1;
        [AbilityID] public int abilityID = -1;
        [BonusID] public int bonusID = -1;
        [GameModifierID] public int gameModifierID = -1;
        [LoreID] public int loreID = -1;
        public int amount = 1;
    }

    [RPGDataList] public List<AchievementReward> rewards = new List<AchievementReward>();

    [TitleID] public int titleRewardID = -1;
    public bool givesTitle = false;

    [Serializable]
    public class AchievementChain
    {
        [AchievementID] public int previousAchievementID = -1;
        [AchievementID] public int nextAchievementID = -1;
        public bool isPartOfChain = false;
    }
    public AchievementChain chain = new AchievementChain();

    public string description;
    public string completedDescription;
    public string incompleteDescription;

    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;

    [RPGDataList] public List<GameActionsData.GameAction> GameActions = new List<GameActionsData.GameAction>();
    public bool UseGameActionsTemplate;
    public GameActionsTemplate GameActionsTemplate;

    public GameObject visualEffectOnComplete;
    public AudioClip soundOnComplete;
    public bool announceToGuild = false;
    public bool announceToZone = false;

    public void UpdateEntryData(RPGAchievement newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;

        category = newEntryData.category;
        points = newEntryData.points;
        isHidden = newEntryData.isHidden;
        isAccountWide = newEntryData.isAccountWide;
        showProgressBar = newEntryData.showProgressBar;
        sortOrder = newEntryData.sortOrder;
        objectives = newEntryData.objectives;
        rewards = newEntryData.rewards;
        titleRewardID = newEntryData.titleRewardID;
        givesTitle = newEntryData.givesTitle;
        chain = newEntryData.chain;
        description = newEntryData.description;
        completedDescription = newEntryData.completedDescription;
        incompleteDescription = newEntryData.incompleteDescription;
        Requirements = newEntryData.Requirements;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
        GameActions = newEntryData.GameActions;
        UseGameActionsTemplate = newEntryData.UseGameActionsTemplate;
        GameActionsTemplate = newEntryData.GameActionsTemplate;
        visualEffectOnComplete = newEntryData.visualEffectOnComplete;
        soundOnComplete = newEntryData.soundOnComplete;
        announceToGuild = newEntryData.announceToGuild;
        announceToZone = newEntryData.announceToZone;
    }
}
