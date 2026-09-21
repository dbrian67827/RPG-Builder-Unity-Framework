using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGPet : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum PetType
    {
        Combat,
        Companion,
        Vanity,
        Utility,
        MountCompanion
    }

    public PetType petType = PetType.Companion;
    public GameObject petPrefab;
    public RuntimeAnimatorController animatorController;
    
    public float followDistance = 2f;
    public float followSpeed = 3.5f;
    public bool canTeleportToOwner = true;
    public float teleportDistance = 15f;
    
    public bool isCombatPet = false;
    public bool assistOwnerInCombat = true;
    public bool canBeTargeted = false;
    public bool canBeKilled = false;
    public float respawnTime = 10f;
    
    [Serializable]
    public class PetStat
    {
        [StatID] public int statID = -1;
        public float baseValue;
        public float perLevelValue;
        public float maxValue;
    }
    [RPGDataList] public List<PetStat> stats = new List<PetStat>();
    
    [Serializable]
    public class PetAbility
    {
        [AbilityID] public int abilityID = -1;
        public int requiredPetLevel = 1;
        public bool autoCast = false;
    }
    [RPGDataList] public List<PetAbility> abilities = new List<PetAbility>();
    
    public int maxLevel = 25;
    public int expPerLevel = 100;
    public bool scaleWithOwnerLevel = true;
    public float scaleFactor = 0.5f;
    
    public enum PetRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public PetRarity rarity = PetRarity.Common;
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;
    
    [RPGDataList] public List<GameActionsData.GameAction> GameActions = new List<GameActionsData.GameAction>();
    public bool UseGameActionsTemplate;
    public GameActionsTemplate GameActionsTemplate;
    
    public string description;
    public bool isAccountWide = false;
    public GameObject summonEffect;
    public GameObject dismissEffect;
    
    public bool canGather = false;
    [ResourceID] public int gatherBonusResourceID = -1;
    public float gatherSpeedBonus = 0f;
    
    [Serializable]
    public class PetBonus
    {
        [StatID] public int statID = -1;
        public float amount;
        public bool isPercent;
        public bool onlyWhileActive = true;
    }
    [RPGDataList] public List<PetBonus> ownerBonuses = new List<PetBonus>();
    
    public void UpdateEntryData(RPGPet newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        petType = newEntryData.petType;
        petPrefab = newEntryData.petPrefab;
        animatorController = newEntryData.animatorController;
        followDistance = newEntryData.followDistance;
        followSpeed = newEntryData.followSpeed;
        canTeleportToOwner = newEntryData.canTeleportToOwner;
        teleportDistance = newEntryData.teleportDistance;
        isCombatPet = newEntryData.isCombatPet;
        assistOwnerInCombat = newEntryData.assistOwnerInCombat;
        canBeTargeted = newEntryData.canBeTargeted;
        canBeKilled = newEntryData.canBeKilled;
        respawnTime = newEntryData.respawnTime;
        stats = newEntryData.stats;
        abilities = newEntryData.abilities;
        maxLevel = newEntryData.maxLevel;
        expPerLevel = newEntryData.expPerLevel;
        scaleWithOwnerLevel = newEntryData.scaleWithOwnerLevel;
        scaleFactor = newEntryData.scaleFactor;
        rarity = newEntryData.rarity;
        Requirements = newEntryData.Requirements;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
        GameActions = newEntryData.GameActions;
        UseGameActionsTemplate = newEntryData.UseGameActionsTemplate;
        GameActionsTemplate = newEntryData.GameActionsTemplate;
        description = newEntryData.description;
        isAccountWide = newEntryData.isAccountWide;
        summonEffect = newEntryData.summonEffect;
        dismissEffect = newEntryData.dismissEffect;
        canGather = newEntryData.canGather;
        gatherBonusResourceID = newEntryData.gatherBonusResourceID;
        gatherSpeedBonus = newEntryData.gatherSpeedBonus;
        ownerBonuses = newEntryData.ownerBonuses;
    }
}
