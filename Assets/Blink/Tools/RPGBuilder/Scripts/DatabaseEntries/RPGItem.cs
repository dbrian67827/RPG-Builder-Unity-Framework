using System;
using System.Collections.Generic;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;
using UnityEngine;

public class RPGItem : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    
    [HideInInspector] public string equipmentSlot;
    public RPGBArmorSlot ArmorSlot;
    [HideInInspector] public string itemType;
    public RPGBItemType ItemType;
    [HideInInspector] public string weaponType;
    public RPGBWeaponType WeaponType;
    [HideInInspector] public string armorType;
    public RPGBArmorType ArmorType;
    [HideInInspector] public string slotType;
    public RPGBWeaponHandSlot WeaponSlot;
    [HideInInspector] public string rarity;
    public RPGBItemRarity ItemRarity;
    
    public string itemModelName;
    public GameObject weaponModel;
    public Material modelMaterial;

    [Serializable]
    public class WeaponPositionData
    {
        [RaceID] public int raceID = -1;

        [Serializable]
        public class GenderPositionData
        {
            public Vector3 CombatPositionInSlot = Vector3.zero;
            public Vector3 CombatRotationInSlot = Vector3.zero;
            public Vector3 CombatScaleInSlot = Vector3.one;
            public Vector3 RestPositionInSlot = Vector3.zero;
            public Vector3 RestRotationInSlot = Vector3.zero;
            public Vector3 RestScaleInSlot = Vector3.one;
            
            public Vector3 CombatPositionInSlot2 = Vector3.zero;
            public Vector3 CombatRotationInSlot2 = Vector3.zero;
            public Vector3 CombatScaleInSlot2 = Vector3.one;
            public Vector3 RestPositionInSlot2 = Vector3.zero;
            public Vector3 RestRotationInSlot2 = Vector3.zero;
            public Vector3 RestScaleInSlot2 = Vector3.one;
        }
        [RPGDataList] public List<GenderPositionData> genderPositionDatas = new List<GenderPositionData>();
    }
    [RPGDataList] public List<WeaponPositionData> weaponPositionDatas = new List<WeaponPositionData>();
    public bool showWeaponPositionData;
    [RPGDataList] public List<WeaponTransform> WeaponTransforms = new List<WeaponTransform>();
    public bool UseWeaponTransformTemplate;
    public WeaponTransformTemplate WeaponTransformTemplate;
    
    public float AttackSpeed;
    public int minDamage;
    public int maxDamage;

    [AbilityID] public int autoAttackAbilityID = -1;

    [Serializable]
    public class ITEM_STATS
    {
        [StatID] public int statID = -1;
        public float amount;
        public bool isPercent;
    }

    [RPGDataList] public List<ITEM_STATS> stats = new List<ITEM_STATS>();

    [RPGDataList] public List<RPGItemDATA.RandomizedStatData> randomStats = new List<RPGItemDATA.RandomizedStatData>();
    public int randomStatsMax;
    
    public int sellPrice;
    [CurrencyID] public int convertToCurrency = -1;
    [CurrencyID] public int sellCurrencyID = -1;
    public int buyPrice;
    [CurrencyID] public int buyCurrencyID = -1;
    public int stackLimit = 1;
    public string description;

    public bool dropInWorld;
    public GameObject itemWorldModel;
    public float durationInWorld = 60f;
    public int worldInteractableLayer;

    [EnchantmentID] public int enchantmentID = -1;
    public bool isEnchantmentConsumed;
    
    [Serializable]
    public class SOCKETS_DATA
    {
        [HideInInspector] public string socketType;
        public RPGBGemSocketType GemSocketType;
    }
    [RPGDataList] public List<SOCKETS_DATA> sockets = new List<SOCKETS_DATA>();
    
    [Serializable]
    public class GEM_DATA
    {
        [HideInInspector] public string socketType;
        public RPGBGemSocketType GemSocketType;
        
        [Serializable]
        public class GEM_STATS
        {
            [StatID] public int statID = -1;
            public float amount;
            public bool isPercent;
        }
        [RPGDataList] public List<GEM_STATS> gemStats = new List<GEM_STATS>();
    }
    public GEM_DATA gemData = new GEM_DATA();
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;
    
    [RPGDataList] public List<RPGCombatDATA.ActionAbilityDATA> actionAbilities = new List<RPGCombatDATA.ActionAbilityDATA>();

    [RPGDataList] public List<GameActionsData.GameAction> GameActions = new List<GameActionsData.GameAction>();
    public bool UseGameActionsTemplate;
    public GameActionsTemplate GameActionsTemplate;

    public BodyCullingTemplate BodyCullingTemplate;

    // ==================== EXTENDED FULL RPG FEATURES ====================
    public enum ItemBindType
    {
        None,
        BindOnPickup,
        BindOnEquip,
        BindOnUse,
        QuestItem,
        AccountBound
    }
    public ItemBindType bindType = ItemBindType.None;
    public bool isUnique = false;
    public int uniqueMaxCount = 1;
    public bool isQuestItem = false;
    public bool isTradable = true;
    public bool isDestroyable = true;
    public bool isSellable = true;
    public bool isDroppable = true;
    public bool isStorableInBank = true;

    public enum ItemQuality
    {
        Poor,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artifact,
        Heirloom
    }
    // Legacy quality kept as ItemRarity, new tier system
    public int itemLevel = 1;
    public int requiredLevel = 1;
    public int requiredItemLevel = 0;

    public float weight = 0f;
    public bool hasDurability = false;
    public int maxDurability = 100;
    public int durabilityLossOnDeath = 10;
    public bool canBeRepaired = true;
    [CurrencyID] public int repairCurrencyID = -1;
    public float repairCostMultiplier = 0.1f;

    public bool hasCooldown = false;
    public float cooldownDuration = 0f;
    [HideInInspector] public string cooldownTag;
    public RPGBAbilityCooldownTag CooldownTag;
    public bool shareCooldown = false;

    public bool isConsumable = false;
    public int maxUseCount = 1;
    public bool consumeOnUse = true;
    public float useCastTime = 0f;
    public bool interruptOnMove = true;

    public bool hasTransmog = false;
    [TransmogID] public int transmogID = -1;
    public bool unlockTransmogOnPickup = true;

    [CraftingQualityID] public int craftingQualityID = -1;
    public bool hasRandomQuality = false;

    [RuneID] public int socketedRuneID = -1;
    public bool canBeSocketedWithRune = false;

    [GlyphID] public int appliedGlyphID = -1;

    public bool isTwoHanded = false;
    public bool canDualWield = false;

    public float criticalChanceBonus = 0f;
    public float criticalDamageBonus = 0f;

    public bool hasSetBonus = false;
    [GearSetID] public int gearSetID = -1;

    public bool hasAppearanceOverride = false;
    public GameObject appearanceOverrideModel;

    public bool hasUseSound = false;
    public AudioClip useSound;
    public GameObject useEffect;

    public bool hasLevelScaling = false;
    public float scalingFactor = 1f;
    public int scalingMaxLevel = 60;

    public bool isCraftedItem = false;
    [RecipeID] public int craftedFromRecipeID = -1;

    public bool hasDeconstruction = false;
    [Serializable]
    public class DeconstructionResult
    {
        [ItemID] public int itemID = -1;
        [CurrencyID] public int currencyID = -1;
        public int minAmount = 1;
        public int maxAmount = 1;
        public float chance = 100f;
    }
    [RPGDataList] public List<DeconstructionResult> deconstructionResults = new List<DeconstructionResult>();

    public bool hasLore = false;
    [LoreID] public int loreID = -1;

    public bool showInCodex = true;
    public bool isCollectionItem = false;
    public string collectionCategory = "";

    // Economy extended
    public bool hasDynamicPrice = false;
    public float priceVariance = 0.2f;
    public bool isAuctionable = true;
    public float auctionDepositRate = 0.05f;

    // Tooltip extended
    public bool showItemLevel = true;
    public bool showDurability = true;
    public bool showBindType = true;
    public bool showStatsComparison = true;

    public void UpdateEntryData(RPGItem newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        ItemType = newEntryData.ItemType;
        WeaponType = newEntryData.WeaponType;
        ArmorType = newEntryData.ArmorType;
        WeaponSlot = newEntryData.WeaponSlot;
        itemModelName = newEntryData.itemModelName;
        ArmorSlot = newEntryData.ArmorSlot;
        weaponModel = newEntryData.weaponModel;
        modelMaterial = newEntryData.modelMaterial;
        WeaponTransforms = newEntryData.WeaponTransforms;
        UseWeaponTransformTemplate = newEntryData.UseWeaponTransformTemplate;
        WeaponTransformTemplate = newEntryData.WeaponTransformTemplate;
        AttackSpeed = newEntryData.AttackSpeed;
        minDamage = newEntryData.minDamage;
        maxDamage = newEntryData.maxDamage;
        stats = newEntryData.stats;
        sellPrice = newEntryData.sellPrice;
        buyPrice = newEntryData.buyPrice;
        stackLimit = newEntryData.stackLimit;
        convertToCurrency = newEntryData.convertToCurrency;
        sellCurrencyID = newEntryData.sellCurrencyID;
        buyCurrencyID = newEntryData.buyCurrencyID;
        Requirements = newEntryData.Requirements;
        GameActions = newEntryData.GameActions;
        UseGameActionsTemplate = newEntryData.UseGameActionsTemplate;
        GameActionsTemplate = newEntryData.GameActionsTemplate;
        autoAttackAbilityID = newEntryData.autoAttackAbilityID;
        ItemRarity = newEntryData.ItemRarity;
        dropInWorld = newEntryData.dropInWorld;
        itemWorldModel = newEntryData.itemWorldModel;
        durationInWorld = newEntryData.durationInWorld;
        randomStats = newEntryData.randomStats;
        enchantmentID = newEntryData.enchantmentID;
        isEnchantmentConsumed = newEntryData.isEnchantmentConsumed;
        sockets = newEntryData.sockets;
        gemData = newEntryData.gemData;
        actionAbilities = newEntryData.actionAbilities;
        randomStatsMax = newEntryData.randomStatsMax;
        worldInteractableLayer = newEntryData.worldInteractableLayer;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
        BodyCullingTemplate = newEntryData.BodyCullingTemplate;

        // Extended
        bindType = newEntryData.bindType;
        isUnique = newEntryData.isUnique;
        uniqueMaxCount = newEntryData.uniqueMaxCount;
        isQuestItem = newEntryData.isQuestItem;
        isTradable = newEntryData.isTradable;
        isDestroyable = newEntryData.isDestroyable;
        isSellable = newEntryData.isSellable;
        isDroppable = newEntryData.isDroppable;
        isStorableInBank = newEntryData.isStorableInBank;
        itemLevel = newEntryData.itemLevel;
        requiredLevel = newEntryData.requiredLevel;
        requiredItemLevel = newEntryData.requiredItemLevel;
        weight = newEntryData.weight;
        hasDurability = newEntryData.hasDurability;
        maxDurability = newEntryData.maxDurability;
        durabilityLossOnDeath = newEntryData.durabilityLossOnDeath;
        canBeRepaired = newEntryData.canBeRepaired;
        repairCurrencyID = newEntryData.repairCurrencyID;
        repairCostMultiplier = newEntryData.repairCostMultiplier;
        hasCooldown = newEntryData.hasCooldown;
        cooldownDuration = newEntryData.cooldownDuration;
        CooldownTag = newEntryData.CooldownTag;
        shareCooldown = newEntryData.shareCooldown;
        isConsumable = newEntryData.isConsumable;
        maxUseCount = newEntryData.maxUseCount;
        consumeOnUse = newEntryData.consumeOnUse;
        useCastTime = newEntryData.useCastTime;
        interruptOnMove = newEntryData.interruptOnMove;
        hasTransmog = newEntryData.hasTransmog;
        transmogID = newEntryData.transmogID;
        unlockTransmogOnPickup = newEntryData.unlockTransmogOnPickup;
        craftingQualityID = newEntryData.craftingQualityID;
        hasRandomQuality = newEntryData.hasRandomQuality;
        socketedRuneID = newEntryData.socketedRuneID;
        canBeSocketedWithRune = newEntryData.canBeSocketedWithRune;
        appliedGlyphID = newEntryData.appliedGlyphID;
        isTwoHanded = newEntryData.isTwoHanded;
        canDualWield = newEntryData.canDualWield;
        criticalChanceBonus = newEntryData.criticalChanceBonus;
        criticalDamageBonus = newEntryData.criticalDamageBonus;
        hasSetBonus = newEntryData.hasSetBonus;
        gearSetID = newEntryData.gearSetID;
        hasAppearanceOverride = newEntryData.hasAppearanceOverride;
        appearanceOverrideModel = newEntryData.appearanceOverrideModel;
        hasUseSound = newEntryData.hasUseSound;
        useSound = newEntryData.useSound;
        useEffect = newEntryData.useEffect;
        hasLevelScaling = newEntryData.hasLevelScaling;
        scalingFactor = newEntryData.scalingFactor;
        scalingMaxLevel = newEntryData.scalingMaxLevel;
        isCraftedItem = newEntryData.isCraftedItem;
        craftedFromRecipeID = newEntryData.craftedFromRecipeID;
        hasDeconstruction = newEntryData.hasDeconstruction;
        deconstructionResults = newEntryData.deconstructionResults;
        hasLore = newEntryData.hasLore;
        loreID = newEntryData.loreID;
        showInCodex = newEntryData.showInCodex;
        isCollectionItem = newEntryData.isCollectionItem;
        collectionCategory = newEntryData.collectionCategory;
        hasDynamicPrice = newEntryData.hasDynamicPrice;
        priceVariance = newEntryData.priceVariance;
        isAuctionable = newEntryData.isAuctionable;
        auctionDepositRate = newEntryData.auctionDepositRate;
        showItemLevel = newEntryData.showItemLevel;
        showDurability = newEntryData.showDurability;
        showBindType = newEntryData.showBindType;
        showStatsComparison = newEntryData.showStatsComparison;
    }
}