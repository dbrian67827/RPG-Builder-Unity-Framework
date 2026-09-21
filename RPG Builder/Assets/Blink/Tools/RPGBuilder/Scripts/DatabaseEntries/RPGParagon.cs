using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGParagon : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public int maxLevel = 100;
    public int expPerLevel = 1000;
    public float expMultiplierPerLevel = 1.1f;
    public int pointsPerLevel = 1;
    
    public enum ParagonType
    {
        Account,
        Character,
        ClassSpecific,
        Season
    }
    public ParagonType paragonType = ParagonType.Character;
    
    [Serializable]
    public class ParagonNode
    {
        public string nodeName = "Paragon Node";
        public string description = "";
        public Sprite icon;
        public int requiredParagonLevel = 1;
        public int maxRank = 5;
        public int pointCostPerRank = 1;
        
        [Serializable]
        public class NodeStatBonus
        {
            [StatID] public int statID = -1;
            public float amountPerRank = 1f;
            public bool isPercent = false;
        }
        [RPGDataList] public List<NodeStatBonus> statBonuses = new List<NodeStatBonus>();
        
        [RPGDataList] public List<GameActionsData.GameAction> OnRankUpActions = new List<GameActionsData.GameAction>();
        
        public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
        
        public Vector2 gridPosition;
        public List<int> requiredNodeIDs = new List<int>();
        public bool isMajorNode = false;
    }
    [RPGDataList] public List<ParagonNode> nodes = new List<ParagonNode>();
    
    [RPGDataList] public List<GameActionsData.GameAction> OnLevelUpActions = new List<GameActionsData.GameAction>();
    
    public string description;
    
    public void UpdateEntryData(RPGParagon newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        maxLevel = newEntryData.maxLevel;
        expPerLevel = newEntryData.expPerLevel;
        expMultiplierPerLevel = newEntryData.expMultiplierPerLevel;
        pointsPerLevel = newEntryData.pointsPerLevel;
        paragonType = newEntryData.paragonType;
        nodes = newEntryData.nodes;
        OnLevelUpActions = newEntryData.OnLevelUpActions;
        description = newEntryData.description;
    }
}

public class RPGRune : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum RuneType
    {
        Offensive,
        Defensive,
        Utility,
        Special
    }
    public RuneType runeType = RuneType.Offensive;
    public int tier = 1;
    
    [Serializable]
    public class RuneEffect
    {
        public enum EffectType
        {
            StatBonus,
            AbilityModifier,
            ProcEffect,
            Passive
        }
        public EffectType effectType;
        [StatID] public int statID = -1;
        [AbilityID] public int abilityID = -1;
        [EffectID] public int effectID = -1;
        public float amount = 0f;
        public float chance = 100f;
        public bool isPercent = false;
    }
    [RPGDataList] public List<RuneEffect> effects = new List<RuneEffect>();
    
    public int requiredLevel = 1;
    [ItemID] public int requiredItemID = -1;
    public bool isUnique = false;
    
    public void UpdateEntryData(RPGRune newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        runeType = newEntryData.runeType;
        tier = newEntryData.tier;
        effects = newEntryData.effects;
        requiredLevel = newEntryData.requiredLevel;
        requiredItemID = newEntryData.requiredItemID;
        isUnique = newEntryData.isUnique;
    }
}

public class RPGGlyph : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum GlyphType
    {
        Major,
        Minor,
        Prime,
        Custom
    }
    public GlyphType glyphType = GlyphType.Major;
    
    [AbilityID] public int affectedAbilityID = -1;
    
    [Serializable]
    public class GlyphModifier
    {
        public enum ModifierType
        {
            Damage,
            Cooldown,
            Cost,
            Duration,
            Range,
            Area,
            EffectAdd,
            EffectRemove,
            Custom
        }
        public ModifierType modifierType;
        public float amount = 0f;
        public bool isPercent = true;
        [EffectID] public int effectID = -1;
        public string customDescription = "";
    }
    [RPGDataList] public List<GlyphModifier> modifiers = new List<GlyphModifier>();
    
    public string description;
    
    public void UpdateEntryData(RPGGlyph newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        glyphType = newEntryData.glyphType;
        affectedAbilityID = newEntryData.affectedAbilityID;
        modifiers = newEntryData.modifiers;
        description = newEntryData.description;
    }
}

public class RPGBestiary : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    [NPCID] public int npcID = -1;
    public string creatureName = "";
    public string family = "";
    public string habitat = "";
    public string description = "";
    public string lore = "";
    
    public Sprite bestiaryImage;
    public GameObject modelPreview;
    
    public bool isUnlockedByDefault = false;
    public int killsRequiredToUnlock = 1;
    
    [Serializable]
    public class BestiaryDropInfo
    {
        [ItemID] public int itemID = -1;
        public float dropChance = 10f;
    }
    [RPGDataList] public List<BestiaryDropInfo> knownDrops = new List<BestiaryDropInfo>();
    
    [Serializable]
    public class BestiaryWeakness
    {
        [HideInInspector] public string damageType;
        public RPGBDamageType DamageType;
        public float multiplier = 1.5f;
    }
    [RPGDataList] public List<BestiaryWeakness> weaknesses = new List<BestiaryWeakness>();
    
    public void UpdateEntryData(RPGBestiary newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        npcID = newEntryData.npcID;
        creatureName = newEntryData.creatureName;
        family = newEntryData.family;
        habitat = newEntryData.habitat;
        description = newEntryData.description;
        lore = newEntryData.lore;
        bestiaryImage = newEntryData.bestiaryImage;
        modelPreview = newEntryData.modelPreview;
        isUnlockedByDefault = newEntryData.isUnlockedByDefault;
        killsRequiredToUnlock = newEntryData.killsRequiredToUnlock;
        knownDrops = newEntryData.knownDrops;
        weaknesses = newEntryData.weaknesses;
    }
}

public class RPGShop : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    [MerchantTableID] public int merchantTableID = -1;
    public bool isLimitedStock = false;
    public bool restockOnTimer = true;
    public float restockTime = 3600f;
    public bool dynamicPricing = false;
    public float supplyDemandFactor = 0.1f;
    
    [CurrencyID] public int currencyID = -1;
    public float buyPriceMultiplier = 1f;
    public float sellPriceMultiplier = 1f;
    
    [Serializable]
    public class ShopItem
    {
        [ItemID] public int itemID = -1;
        public int stock = -1; // -1 unlimited
        public int maxStock = -1;
        public float priceMultiplier = 1f;
        public bool isLimited = false;
    }
    [RPGDataList] public List<ShopItem> shopItems = new List<ShopItem>();
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    
    public void UpdateEntryData(RPGShop newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        merchantTableID = newEntryData.merchantTableID;
        isLimitedStock = newEntryData.isLimitedStock;
        restockOnTimer = newEntryData.restockOnTimer;
        restockTime = newEntryData.restockTime;
        dynamicPricing = newEntryData.dynamicPricing;
        supplyDemandFactor = newEntryData.supplyDemandFactor;
        currencyID = newEntryData.currencyID;
        buyPriceMultiplier = newEntryData.buyPriceMultiplier;
        sellPriceMultiplier = newEntryData.sellPriceMultiplier;
        shopItems = newEntryData.shopItems;
        Requirements = newEntryData.Requirements;
    }
}
