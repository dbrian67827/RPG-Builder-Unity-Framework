using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBuilderEconomySettings : RPGBuilderDatabaseEntry
{
    public List<string> itemRarityList = new List<string>();
    public List<Sprite> itemRarityImagesList = new List<Sprite>();
    public List<Color> itemRarityColorsList = new List<Color>();
    public List<string> itemTypeList = new List<string>();
    public List<string> weaponTypeList = new List<string>();
    public List<string> armorTypeList = new List<string>();
    public List<string> armorSlotsList = new List<string>();
    public List<string> weaponSlotsList = new List<string>();
    public List<string> slotTypeList = new List<string>();
    public List<string> socketTypeList = new List<string>();
    
    public List<RPGItemDATA.WeaponAnimatorOverride> weaponAnimatorOverrides;
    
    public int InventorySlots;


    [System.Serializable]
    public class StartingItemsDATA
    {
        public int itemID = -1;
        public int count = 1;
        public bool equipped;
    }

    
    [System.Serializable]
    public class RandomItemData
    {
        public List<RandomizedStat> randomStats = new List<RandomizedStat>();
        public int randomItemID = -1;
    }
    
    [System.Serializable]
    public class RandomizedStat
    {
        public int statID = -1;
        public float statValue;
    }
    
    [System.Serializable]
    public class RandomizedStatData
    {
        public int statID = -1;
        public float minValue, maxValue = 1f;
        public bool isPercent;
        public bool isInt;
        public float chance = 100f;
    }


    [Header("BANK")]
    public int BankSlots = 100;
    public int BankSlotsPerTab = 25;
    public int MaxBankTabs = 6;
    [CurrencyID] public int BankTabCurrencyID = -1;
    public int BankTabCost = 1000;
    public float BankTabCostMultiplier = 2f;

    [Header("AUCTION HOUSE")]
    public bool EnableAuctionHouse = false;
    public float AuctionHouseCutPercent = 5f;
    public float AuctionDepositPercent = 5f;
    public int MaxAuctionsPerPlayer = 20;
    public float AuctionDurationMin = 3600f;
    public float AuctionDurationMax = 172800f;

    [Header("DURABILITY")]
    public bool EnableDurability = true;
    public float DurabilityLossOnDeathPercent = 10f;
    public float RepairCostMultiplier = 0.1f;
    public bool CanRepairInField = false;

    [Header("CRAFTING QUALITY")]
    public bool EnableCraftingQuality = true;
    public float BaseQualityChance = 50f;
    public float QualitySkillBonus = 0.5f;

    [Header("SHOPS")]
    public bool EnableDynamicPricing = false;
    public bool EnableLimitedStock = true;
    public bool EnableRestocking = true;
    public float RestockInterval = 3600f;
    public float PriceVariance = 0.2f;

    [Header("LOOT")]
    public bool EnablePersonalLoot = false;
    public bool EnableMasterLoot = true;
    public bool EnableNeedGreed = true;
    public float LootRollTime = 60f;
    public bool EnableAOELoot = true;


    public GameObject LootBagPrefab;
    
    public void UpdateEntryData(RPGBuilderEconomySettings newEntryData)
    {
        itemTypeList = newEntryData.itemTypeList;
        weaponTypeList = newEntryData.weaponTypeList;
        armorTypeList = newEntryData.armorTypeList;
        itemRarityList = newEntryData.itemRarityList;
        armorSlotsList = newEntryData.armorSlotsList;
        weaponSlotsList = newEntryData.weaponSlotsList;
        slotTypeList = newEntryData.slotTypeList;
        InventorySlots = newEntryData.InventorySlots;
        itemRarityImagesList = newEntryData.itemRarityImagesList;
        itemRarityColorsList = newEntryData.itemRarityColorsList;
        socketTypeList = newEntryData.socketTypeList;
        weaponAnimatorOverrides = newEntryData.weaponAnimatorOverrides;
        LootBagPrefab = newEntryData.LootBagPrefab;
        BankSlots = newEntryData.BankSlots;
        BankSlotsPerTab = newEntryData.BankSlotsPerTab;
        MaxBankTabs = newEntryData.MaxBankTabs;
        BankTabCurrencyID = newEntryData.BankTabCurrencyID;
        BankTabCost = newEntryData.BankTabCost;
        BankTabCostMultiplier = newEntryData.BankTabCostMultiplier;
        EnableAuctionHouse = newEntryData.EnableAuctionHouse;
        AuctionHouseCutPercent = newEntryData.AuctionHouseCutPercent;
        AuctionDepositPercent = newEntryData.AuctionDepositPercent;
        MaxAuctionsPerPlayer = newEntryData.MaxAuctionsPerPlayer;
        AuctionDurationMin = newEntryData.AuctionDurationMin;
        AuctionDurationMax = newEntryData.AuctionDurationMax;
        EnableDurability = newEntryData.EnableDurability;
        DurabilityLossOnDeathPercent = newEntryData.DurabilityLossOnDeathPercent;
        RepairCostMultiplier = newEntryData.RepairCostMultiplier;
        CanRepairInField = newEntryData.CanRepairInField;
        EnableCraftingQuality = newEntryData.EnableCraftingQuality;
        BaseQualityChance = newEntryData.BaseQualityChance;
        QualitySkillBonus = newEntryData.QualitySkillBonus;
        EnableDynamicPricing = newEntryData.EnableDynamicPricing;
        EnableLimitedStock = newEntryData.EnableLimitedStock;
        EnableRestocking = newEntryData.EnableRestocking;
        RestockInterval = newEntryData.RestockInterval;
        PriceVariance = newEntryData.PriceVariance;
        EnablePersonalLoot = newEntryData.EnablePersonalLoot;
        EnableMasterLoot = newEntryData.EnableMasterLoot;
        EnableNeedGreed = newEntryData.EnableNeedGreed;
        LootRollTime = newEntryData.LootRollTime;
        EnableAOELoot = newEntryData.EnableAOELoot;
    }
}
