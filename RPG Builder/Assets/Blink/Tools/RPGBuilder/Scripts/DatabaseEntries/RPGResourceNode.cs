using System;
using System.Collections.Generic;
using UnityEngine;

public class RPGResourceNode : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;
    
    
    public bool learnedByDefault;
    [SkillID] public int skillRequiredID = -1;

    [Serializable]
    public class RPGResourceNodeRankData
    {
        public bool ShowedInEditor;
        public int unlockCost;

        [LootTableID] public int lootTableID = -1;

        public int skillLevelRequired;

        public int Experience;

        public float distanceMax;

        public float gatherTime;
        public float respawnTime;
    }
    public List<RPGResourceNodeRankData> ranks = new List<RPGResourceNodeRankData>();

    public void CopyEntryData(RPGResourceNodeRankData original, RPGResourceNodeRankData copied)
    {
        original.unlockCost = copied.unlockCost;
        original.lootTableID = copied.lootTableID;
        original.skillLevelRequired = copied.skillLevelRequired;
        original.Experience = copied.Experience;
        original.distanceMax = copied.distanceMax;
        original.gatherTime = copied.gatherTime;
        original.respawnTime = copied.respawnTime;
    }
    

    // ==================== EXTENDED RESOURCE FEATURES ====================
    public bool hasQuality = false;
    [CraftingQualityID] public int qualityID = -1;
    public float qualityChance = 10f;
    public bool hasYieldScaling = true;
    public float yieldPerSkillLevel = 0.1f;
    public bool hasRareDrop = false;
    [ItemID] public int rareDropItemID = -1;
    public float rareDropChance = 5f;
    public bool hasRespawnScaling = false;
    public float respawnTimePerPlayer = 5f;
    public bool hasDepletion = false;
    public int maxGathersBeforeDepletion = 5;
    public bool hasSkillCheck = true;
    public int requiredSkillLevel = 1;
    public float failChance = 0f;
    public bool hasGatheringBonus = false;
    public float gatheringSpeedBonus = 0f;
    public bool hasMountBonus = false;
    public bool hasPetBonus = false;


    public void UpdateEntryData(RPGResourceNode newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        learnedByDefault = newEntryData.learnedByDefault;
        ranks = newEntryData.ranks;
        skillRequiredID = newEntryData.skillRequiredID;
    }
}