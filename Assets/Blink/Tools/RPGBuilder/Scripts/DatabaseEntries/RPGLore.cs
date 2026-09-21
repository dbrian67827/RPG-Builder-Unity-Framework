using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGLore : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum LoreCategory
    {
        History,
        Characters,
        Locations,
        Creatures,
        Items,
        Magic,
        Factions,
        Events,
        Religion,
        Culture,
        Bestiary,
        Tutorial,
        Secrets,
        Custom
    }

    public LoreCategory category = LoreCategory.History;
    public string subCategory = "";
    
    public string title = "Lore Title";
    [TextArea(5,20)] public string content = "Lore content...";
    [TextArea(3,10)] public string shortDescription = "Short description";
    
    public Sprite loreImage;
    public GameObject loreModelPreview;
    public AudioClip voiceOver;
    
    public bool isUnlockedByDefault = false;
    public bool isSecret = false;
    public bool showNotificationOnUnlock = true;
    
    public int sortOrder = 0;
    public int requiredLevel = 0;
    
    [Serializable]
    public class LoreConnection
    {
        [LoreID] public int loreID = -1;
        public string connectionDescription = "Related to...";
    }
    [RPGDataList] public List<LoreConnection> connectedLore = new List<LoreConnection>();
    
    [Serializable]
    public class LoreReward
    {
        public enum RewardType
        {
            Experience,
            Currency,
            Item,
            Title,
            Achievement,
            StatBonus
        }
        public RewardType rewardType;
        [CurrencyID] public int currencyID = -1;
        [ItemID] public int itemID = -1;
        [TitleID] public int titleID = -1;
        [AchievementID] public int achievementID = -1;
        [StatID] public int statID = -1;
        public int amount = 1;
        public float statAmount = 0f;
    }
    [RPGDataList] public List<LoreReward> rewards = new List<LoreReward>();
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;
    
    [RPGDataList] public List<GameActionsData.GameAction> OnUnlockActions = new List<GameActionsData.GameAction>();
    public bool UseGameActionsTemplate;
    public GameActionsTemplate GameActionsTemplate;
    
    public string unlockMessage = "Lore Unlocked!";
    public GameObject unlockVFX;
    public AudioClip unlockSFX;
    
    public bool isPartOfCollection = false;
    public string collectionName = "";
    public int collectionIndex = 0;
    public int collectionTotal = 0;
    
    public void UpdateEntryData(RPGLore newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        category = newEntryData.category;
        subCategory = newEntryData.subCategory;
        title = newEntryData.title;
        content = newEntryData.content;
        shortDescription = newEntryData.shortDescription;
        loreImage = newEntryData.loreImage;
        loreModelPreview = newEntryData.loreModelPreview;
        voiceOver = newEntryData.voiceOver;
        isUnlockedByDefault = newEntryData.isUnlockedByDefault;
        isSecret = newEntryData.isSecret;
        showNotificationOnUnlock = newEntryData.showNotificationOnUnlock;
        sortOrder = newEntryData.sortOrder;
        requiredLevel = newEntryData.requiredLevel;
        connectedLore = newEntryData.connectedLore;
        rewards = newEntryData.rewards;
        Requirements = newEntryData.Requirements;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
        OnUnlockActions = newEntryData.OnUnlockActions;
        UseGameActionsTemplate = newEntryData.UseGameActionsTemplate;
        GameActionsTemplate = newEntryData.GameActionsTemplate;
        unlockMessage = newEntryData.unlockMessage;
        unlockVFX = newEntryData.unlockVFX;
        unlockSFX = newEntryData.unlockSFX;
        isPartOfCollection = newEntryData.isPartOfCollection;
        collectionName = newEntryData.collectionName;
        collectionIndex = newEntryData.collectionIndex;
        collectionTotal = newEntryData.collectionTotal;
    }
}
