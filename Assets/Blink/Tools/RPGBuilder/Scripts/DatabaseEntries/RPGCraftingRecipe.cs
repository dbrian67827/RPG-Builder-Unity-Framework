using System;
using System.Collections.Generic;
using UnityEngine;

public class RPGCraftingRecipe : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;
    
    
    public bool learnedByDefault;
    [SkillID] public int craftingSkillID = -1;
    [CraftingStationID] public int craftingStationID = -1;

    [Serializable]
    public class CraftedItemsDATA
    {
        public float chance = 100f;
        public int count = 1;
        [ItemID] public int craftedItemID = -1;
    }
    
    
    [Serializable]
    public class ComponentsRequired
    {
        public int count = 1;
        [ItemID] public int componentItemID = -1;
    }
    
    
    [Serializable]
    public class RPGCraftingRecipeRankData
    {
        public bool ShowedInEditor;
        public int unlockCost;
        public int Experience;
        public float craftTime;

        [RPGDataList] public List<CraftedItemsDATA> allCraftedItems = new List<CraftedItemsDATA>();
        [RPGDataList] public List<ComponentsRequired> allComponents = new List<ComponentsRequired>();
    }
    [RPGDataList] public List<RPGCraftingRecipeRankData> ranks = new List<RPGCraftingRecipeRankData>();

    public void CopyEntryData(RPGCraftingRecipeRankData original, RPGCraftingRecipeRankData copied)
    {
        original.Experience = copied.Experience;
        original.craftTime = copied.craftTime;
        original.unlockCost = copied.unlockCost;

        original.allCraftedItems = new List<CraftedItemsDATA>();
        for (var index = 0; index < copied.allCraftedItems.Count; index++)
        {
            CraftedItemsDATA newRef = new CraftedItemsDATA();
            newRef.chance = copied.allCraftedItems[index].chance;
            newRef.count = copied.allCraftedItems[index].count;
            newRef.craftedItemID = copied.allCraftedItems[index].craftedItemID;
            original.allCraftedItems.Add(newRef);
        }

        original.allComponents = new List<ComponentsRequired>();
        for (var index = 0; index < copied.allComponents.Count; index++)
        {
            ComponentsRequired newRef = new ComponentsRequired();
            newRef.count = copied.allComponents[index].count;
            newRef.componentItemID = copied.allComponents[index].componentItemID;
            original.allComponents.Add(newRef);
        }
    }


    // ==================== EXTENDED CRAFTING FEATURES ====================
    public bool hasQualitySystem = true;
    public float baseSuccessChance = 100f;
    public float failureChance = 0f;
    public bool loseMaterialsOnFailure = false;
    public float bonusChancePerSkillLevel = 0.5f;
    public float craftingTime = 2f;
    public bool requiresCraftingStationLevel = false;
    public int requiredStationLevel = 1;
    public bool hasMultipleOutcomes = false;
    [System.Serializable]
    public class CraftingOutcome
    {
        [ItemID] public int itemID = -1;
        public int minAmount = 1;
        public int maxAmount = 1;
        public float chance = 100f;
        [CraftingQualityID] public int qualityID = -1;
    }
    [RPGDataList] public System.Collections.Generic.List<CraftingOutcome> possibleOutcomes = new System.Collections.Generic.List<CraftingOutcome>();
    public bool hasExperienceReward = true;
    public int craftingExperience = 10;
    public bool hasSkillUpChance = true;
    public float skillUpChance = 50f;
    public bool isMasterRecipe = false;
    public bool hasCooldown = false;
    public float cooldownDuration = 0f;
    public bool hasDailyLimit = false;
    public int dailyLimit = 5;


    public void UpdateEntryData(RPGCraftingRecipe newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        ranks = newEntryData.ranks;
        learnedByDefault = newEntryData.learnedByDefault;
        craftingSkillID = newEntryData.craftingSkillID;
        craftingStationID = newEntryData.craftingStationID;
    }
}