using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGCraftingQuality : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public string qualityName = "Normal";
    public Color qualityColor = Color.white;
    public int tier = 1; // 1 = lowest, 5 = highest
    public float statMultiplier = 1.0f;
    public float valueMultiplier = 1.0f;
    public float chanceToCraft = 50f;
    public float requiredSkillLevel = 0f;
    public int requiredCharacterLevel = 1;
    
    public float durabilityMultiplier = 1f;
    public float successChanceModifier = 0f;
    
    [Serializable]
    public class QualityStatBonus
    {
        [StatID] public int statID = -1;
        public float bonusMin = 0f;
        public float bonusMax = 0f;
        public bool isPercent = false;
    }
    [RPGDataList] public List<QualityStatBonus> bonusStats = new List<QualityStatBonus>();
    
    public GameObject craftVFX;
    public AudioClip craftSFX;
    public string craftMessage = "Crafted with {quality} quality!";
    
    public bool isDefaultQuality = false;
    public bool isMaxQuality = false;
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;
    
    public void UpdateEntryData(RPGCraftingQuality newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        qualityName = newEntryData.qualityName;
        qualityColor = newEntryData.qualityColor;
        tier = newEntryData.tier;
        statMultiplier = newEntryData.statMultiplier;
        valueMultiplier = newEntryData.valueMultiplier;
        chanceToCraft = newEntryData.chanceToCraft;
        requiredSkillLevel = newEntryData.requiredSkillLevel;
        requiredCharacterLevel = newEntryData.requiredCharacterLevel;
        durabilityMultiplier = newEntryData.durabilityMultiplier;
        successChanceModifier = newEntryData.successChanceModifier;
        bonusStats = newEntryData.bonusStats;
        craftVFX = newEntryData.craftVFX;
        craftSFX = newEntryData.craftSFX;
        craftMessage = newEntryData.craftMessage;
        isDefaultQuality = newEntryData.isDefaultQuality;
        isMaxQuality = newEntryData.isMaxQuality;
        Requirements = newEntryData.Requirements;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
    }
}

public class RPGReputationReward : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    [FactionID] public int factionID = -1;
    public int requiredReputation = 1000;
    public int reputationRank = 1;
    public string rankName = "Friendly";
    
    [Serializable]
    public class RepReward
    {
        public enum RewardType { Item, Currency, Title, Mount, Pet, Ability, Recipe, Discount }
        public RewardType rewardType;
        [ItemID] public int itemID = -1;
        [CurrencyID] public int currencyID = -1;
        [TitleID] public int titleID = -1;
        [MountID] public int mountID = -1;
        [PetID] public int petID = -1;
        [AbilityID] public int abilityID = -1;
        [RecipeID] public int recipeID = -1;
        public int amount = 1;
        public float discountPercent = 0f;
    }
    [RPGDataList] public List<RepReward> rewards = new List<RepReward>();
    
    [RPGDataList] public List<GameActionsData.GameAction> OnUnlockActions = new List<GameActionsData.GameAction>();
    
    public string description;
    
    public void UpdateEntryData(RPGReputationReward newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        factionID = newEntryData.factionID;
        requiredReputation = newEntryData.requiredReputation;
        reputationRank = newEntryData.reputationRank;
        rankName = newEntryData.rankName;
        rewards = newEntryData.rewards;
        OnUnlockActions = newEntryData.OnUnlockActions;
        description = newEntryData.description;
    }
}

public class RPGMailTemplate : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public string subject = "Mail Subject";
    [TextArea(3,10)] public string body = "Mail body...";
    public string senderName = "System";
    
    public bool isSystemMail = true;
    public bool isReadOnOpen = false;
    public float expiryTime = 2592000f; // 30 days
    public bool returnToSenderOnExpiry = false;
    
    [Serializable]
    public class MailAttachment
    {
        public enum AttachmentType { Item, Currency }
        public AttachmentType attachmentType;
        [ItemID] public int itemID = -1;
        [CurrencyID] public int currencyID = -1;
        public int amount = 1;
    }
    [RPGDataList] public List<MailAttachment> attachments = new List<MailAttachment>();
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    
    public void UpdateEntryData(RPGMailTemplate newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        subject = newEntryData.subject;
        body = newEntryData.body;
        senderName = newEntryData.senderName;
        isSystemMail = newEntryData.isSystemMail;
        isReadOnOpen = newEntryData.isReadOnOpen;
        expiryTime = newEntryData.expiryTime;
        returnToSenderOnExpiry = newEntryData.returnToSenderOnExpiry;
        attachments = newEntryData.attachments;
        Requirements = newEntryData.Requirements;
    }
}

public class RPGTransmog : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum TransmogSlot
    {
        Head,
        Chest,
        Legs,
        Feet,
        Hands,
        Shoulders,
        Weapon,
        OffHand,
        All
    }
    
    public TransmogSlot slot = TransmogSlot.All;
    public GameObject appearanceModel;
    public Material appearanceMaterial;
    
    [ItemID] public int sourceItemID = -1;
    public bool unlockedByDefault = false;
    public bool isAccountWide = true;
    
    [CurrencyID] public int unlockCurrencyID = -1;
    public int unlockCost = 0;
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    
    public void UpdateEntryData(RPGTransmog newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        slot = newEntryData.slot;
        appearanceModel = newEntryData.appearanceModel;
        appearanceMaterial = newEntryData.appearanceMaterial;
        sourceItemID = newEntryData.sourceItemID;
        unlockedByDefault = newEntryData.unlockedByDefault;
        isAccountWide = newEntryData.isAccountWide;
        unlockCurrencyID = newEntryData.unlockCurrencyID;
        unlockCost = newEntryData.unlockCost;
        Requirements = newEntryData.Requirements;
    }
}

public class RPGWeather : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum WeatherType
    {
        Clear,
        Cloudy,
        Rain,
        Storm,
        Snow,
        Fog,
        Sandstorm,
        Windy,
        Custom
    }
    
    public WeatherType weatherType = WeatherType.Clear;
    public GameObject weatherVFX;
    public AudioClip weatherSFX;
    public float duration = 600f;
    public float transitionTime = 5f;
    
    public float visibilityModifier = 1f;
    public float movementSpeedModifier = 1f;
    
    [Serializable]
    public class WeatherStatModifier
    {
        [StatID] public int statID = -1;
        public float modifier = 0f;
        public bool isPercent = true;
    }
    [RPGDataList] public List<WeatherStatModifier> statModifiers = new List<WeatherStatModifier>();
    
    public bool affectCombat = false;
    public bool affectGathering = false;
    public float gatheringYieldModifier = 1f;
    
    public void UpdateEntryData(RPGWeather newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        weatherType = newEntryData.weatherType;
        weatherVFX = newEntryData.weatherVFX;
        weatherSFX = newEntryData.weatherSFX;
        duration = newEntryData.duration;
        transitionTime = newEntryData.transitionTime;
        visibilityModifier = newEntryData.visibilityModifier;
        movementSpeedModifier = newEntryData.movementSpeedModifier;
        statModifiers = newEntryData.statModifiers;
        affectCombat = newEntryData.affectCombat;
        affectGathering = newEntryData.affectGathering;
        gatheringYieldModifier = newEntryData.gatheringYieldModifier;
    }
}
