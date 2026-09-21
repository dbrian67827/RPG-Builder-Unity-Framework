using System;
using System.Collections.Generic;
using BLINK.RPGBuilder.AI;
using BLINK.RPGBuilder.Combat;
using BLINK.RPGBuilder.Templates;
using UnityEngine;
using UnityEngine.AI;

public class RPGNpc : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;
    
    public enum NPC_TYPE
    {
        MOB,
        RARE,
        BOSS,
        MERCHANT,
        BANK,
        QUEST_GIVER,
        DIALOGUE
    }

    public NPC_TYPE npcType;

    public GameObject AILogicTemplate;

    public List<AIData.AIPhase> Phases = new List<AIData.AIPhase>();
    public bool ResetPhaseAfterCombat = true;

    public bool InstantlyHealAfterCombat = true;
    
    public bool isDummyTarget;

    [FactionID] public int factionID = -1;
    
    [SpeciesID] public int speciesID = -1;
    
    [MerchantTableID] public int merchantTableID = -1;
    [RPGDataList] public List<AIData.NPCMerchantTable> MerchantTables = new List<AIData.NPCMerchantTable>();
    
    [DialogueID] public int dialogueID = -1;

    [Serializable]
    public class NPC_QUEST_DATA
    {
        public int questID = -1;
    }

    [RPGDataList] public List<NPC_QUEST_DATA> questGiven = new List<NPC_QUEST_DATA>();
    [RPGDataList] public List<NPC_QUEST_DATA> questCompleted = new List<NPC_QUEST_DATA>();

    public float MinRespawn = 60;
    public float MaxRespawn = 120;
    
    public float corpseDespawnTime = 15;

    public int MinEXP = 1;
    public int MaxEXP = 2;
    public float LowerLevelEXPModifier;
    public float HigherLevelEXPModifier;
    public int EXPBonusPerLevel;
    public GameObject lootBagPrefab;
    public float LootBagDuration = 60;

    public List<RPGCombatDATA.Faction_Reward_DATA> factionRewards = new List<RPGCombatDATA.Faction_Reward_DATA>();

    public int MinLevel = 1;
    public int MaxLevel = 2;
    public bool isScalingWithPlayer;


    [Serializable]
    public class NPC_STATS_DATA
    {
        [StatID] public int statID = -1;
        public float minValue;
        public float maxValue;
        public float baseValue;
        public float bonusPerLevel;
        
        [RPGDataList] public List<RPGStat.VitalityActions> vitalityActions = new List<RPGStat.VitalityActions>();
    }
    [RPGDataList] public List<NPC_STATS_DATA> stats = new List<NPC_STATS_DATA>();
    
    [RPGDataList] public List<CombatData.CustomStatValues> CustomStats = new List<CombatData.CustomStatValues>();
    public bool UseStatListTemplate;
    public StatListTemplate StatListTemplate;

    [Serializable]
    public class LOOT_TABLES
    {
        [LootTableID] public int lootTableID = -1;
        public float dropRate = 100f;
    }

    [RPGDataList] public List<LOOT_TABLES> lootTables = new List<LOOT_TABLES>();

    public bool isCombatEnabled = true;
    public bool isMovementEnabled = true;
    public bool isCollisionEnabled;
    public bool isTargetable = true;
    public bool isNameplateEnabled = true;
    public bool isPlayerInteractable = true;
    public bool isMerchant;
    public bool isQuestGiver;
    public bool isDialogue;

    public string MerchantText = "Show me your goods.";
    public string QuestText = "Can I do something for you?";
    public string DialogueText = "Tell me your story.";
    
    // PREFAB DATA
    public GameObject NPCVisual;
    public Vector3 modelPosition, modelScale = Vector3.one;

    public float nameplateYOffset = 1.5f;
    public string RendererName;
    
    public RuntimeAnimatorController animatorController;
    public Avatar animatorAvatar;

    public bool animatorUseRootMotion;
    public AnimatorUpdateMode animatorUpdateMode = AnimatorUpdateMode.Normal;
    public AnimatorCullingMode AnimatorCullingMode = AnimatorCullingMode.AlwaysAnimate;

    public float navmeshAgentRadius = 0.5f, navmeshAgentHeight = 2f, navmeshAgentAngularSpeed = 400;
    public ObstacleAvoidanceType navmeshObstacleAvoidance;
    
    public enum NPCColliderType
    {
        Capsule,
        Sphere,
        Box
    }

    public NPCColliderType colliderType;

    public Vector3 colliderCenter, colliderSize;
    public float colliderRadius = 1, colliderHeight = 2;
    
    [Serializable]
    public class NPC_AGGRO_LINK
    {
        public AIData.AggroLinkType type;
        [NPCID] public int npcID = -1;
        public RPGBNPCFamily npcFamily;
        public float maxDistance;
    }

    [RPGDataList] public List<NPC_AGGRO_LINK> aggroLinks = new List<NPC_AGGRO_LINK>();

    public bool SetLayer;
    public bool SetTag;
    public int GameObjectLayer;
    public string GameObjectTag = "Untagged";

    public RPGBNPCFamily npcFamily;
    

    // ==================== EXTENDED FULL RPG NPC FEATURES ====================
    public float tetherDistance = 100f;
    public bool useTether = true;
    public bool returnToSpawnOnTether = true;
    public float leashDistance = 50f;
    public bool useLeash = false;
    
    public bool hasSchedule = false;
    public bool wanderDuringDay = true;
    public bool sleepAtNight = false;
    public Vector3 dayPosition;
    public Vector3 nightPosition;
    
    public bool isSocial = true;
    public float socialAggroRadius = 10f;
    public bool assistFriends = true;
    
    public bool hasGossip = false;
    public string gossipText = "Hello there!";
    public bool hasVendorRestock = true;
    public float vendorRestockTime = 3600f;
    public bool vendorHasLimitedStock = true;
    
    public bool hasReputation = false;
    [FactionID] public int reputationFactionID = -1;
    public int reputationGainOnKill = 0;
    public int reputationLossOnKill = 0;
    
    public bool hasLootMethod = false;
    public enum LootMethod { FreeForAll, RoundRobin, MasterLoot, NeedGreed, Personal }
    public LootMethod lootMethod = LootMethod.FreeForAll;
    
    public bool hasRareSpawn = false;
    public float rareSpawnChance = 5f;
    public float rareSpawnCooldown = 3600f;
    
    public bool hasScaling = false;
    public bool scaleWithPlayerLevel = false;
    public bool scaleWithPartySize = false;
    public float scalingFactorPerPlayer = 0.2f;
    
    public bool hasEnrage = false;
    public float enrageTime = 300f;
    public float enrageDamageMultiplier = 2f;
    
    public bool hasPhases = false;
    public int phaseCount = 1;
    public float phaseHealthPercent = 50f;
    
    public bool hasImmunities = false;
    [RPGDataList] public List<RPGEffect.EFFECT_TYPE> immunities = new List<RPGEffect.EFFECT_TYPE>();
    
    public bool hasResistances = false;
    [System.Serializable]
    public class NPCResistance
    {
        [HideInInspector] public string damageType;
        public RPGBDamageType DamageType;
        public float resistancePercent = 0f;
    }
    [RPGDataList] public List<NPCResistance> resistances = new List<NPCResistance>();
    
    public bool hasSpecialAbilities = false;
    [RPGDataList] public List<AIData.AIPhase> specialAbilities = new List<AIData.AIPhase>();
    
    public bool isWorldBoss = false;
    public bool isDungeonBoss = false;
    public bool isRaidBoss = false;
    public bool showBossFrame = false;
    public bool lockPlayersInCombat = false;
    
    public bool hasDialogueOnAggro = false;
    public string aggroDialogue = "You dare challenge me?!";
    public bool hasDialogueOnDeath = false;
    public string deathDialogue = "Nooo!";
    
    public bool hasPatrolPath = false;
    public PatrolPath patrolPath;
    public bool patrolPathLoop = true;
    public float patrolPathSpeed = 2f;
    
    public bool hasInteractionCooldown = false;
    public float interactionCooldown = 5f;
    
    public bool hasCustomNameplate = false;
    public Color nameplateColor = Color.white;
    public bool showLevelInNameplate = true;
    public bool showHealthBar = true;
    public bool showManaBar = false;
    
    public bool hasMount = false;
    [MountID] public int mountID = -1;
    public bool mountOnSpawn = false;
    
    public bool hasPet = false;
    [PetID] public int petID = -1;
    public bool summonPetOnCombat = false;
    
    public bool hasTitle = false;
    [TitleID] public int titleID = -1;
    
    public bool hasBestiary = false;
    [BestiaryID] public int bestiaryID = -1;
    public bool unlockBestiaryOnKill = true;
    
    public bool hasLore = false;
    [LoreID] public int loreID = -1;
    public bool unlockLoreOnKill = false;
    

    public void UpdateEntryData(RPGNpc newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        CustomStats = newEntryData.CustomStats;
        UseStatListTemplate = newEntryData.UseStatListTemplate;
        StatListTemplate = newEntryData.StatListTemplate;
        isDummyTarget = newEntryData.isDummyTarget;
        lootTables = newEntryData.lootTables;
        aggroLinks = newEntryData.aggroLinks;
        MinRespawn = newEntryData.MinRespawn;
        MaxRespawn = newEntryData.MaxRespawn;
        npcType = newEntryData.npcType;
        MinEXP = newEntryData.MinEXP;
        MaxEXP = newEntryData.MaxEXP;
        LowerLevelEXPModifier = newEntryData.LowerLevelEXPModifier;
        HigherLevelEXPModifier = newEntryData.HigherLevelEXPModifier;
        EXPBonusPerLevel = newEntryData.EXPBonusPerLevel;
        lootBagPrefab = newEntryData.lootBagPrefab;
        MinLevel = newEntryData.MinLevel;
        MaxLevel = newEntryData.MaxLevel;
        isScalingWithPlayer = newEntryData.isScalingWithPlayer;
        MerchantTables = newEntryData.MerchantTables;
        questGiven = newEntryData.questGiven;
        questCompleted = newEntryData.questCompleted;
        isCombatEnabled = newEntryData.isCombatEnabled;
        isMovementEnabled = newEntryData.isMovementEnabled;
        isCollisionEnabled = newEntryData.isCollisionEnabled;
        factionID = newEntryData.factionID;
        factionRewards = newEntryData.factionRewards;
        dialogueID = newEntryData.dialogueID;
        speciesID = newEntryData.speciesID;
        NPCVisual = newEntryData.NPCVisual;
        nameplateYOffset = newEntryData.nameplateYOffset;
        RendererName = newEntryData.RendererName;
        animatorController = newEntryData.animatorController;
        animatorAvatar = newEntryData.animatorAvatar;
        animatorUseRootMotion = newEntryData.animatorUseRootMotion;
        animatorUpdateMode = newEntryData.animatorUpdateMode;
        AnimatorCullingMode = newEntryData.AnimatorCullingMode;
        navmeshAgentRadius = newEntryData.navmeshAgentRadius;
        navmeshAgentHeight = newEntryData.navmeshAgentHeight;
        navmeshAgentAngularSpeed = newEntryData.navmeshAgentAngularSpeed;
        navmeshObstacleAvoidance = newEntryData.navmeshObstacleAvoidance;
        colliderType = newEntryData.colliderType;
        colliderCenter = newEntryData.colliderCenter;
        colliderSize = newEntryData.colliderSize;
        colliderRadius = newEntryData.colliderRadius;
        colliderHeight = newEntryData.colliderHeight;
        modelPosition = newEntryData.modelPosition;
        modelScale = newEntryData.modelScale;
        corpseDespawnTime = newEntryData.corpseDespawnTime;
        isTargetable = newEntryData.isTargetable;
        isNameplateEnabled = newEntryData.isNameplateEnabled;
        isPlayerInteractable = newEntryData.isPlayerInteractable;
        isMerchant = newEntryData.isMerchant;
        isQuestGiver = newEntryData.isQuestGiver;
        isDialogue = newEntryData.isDialogue;
        MerchantText = newEntryData.MerchantText;
        QuestText = newEntryData.QuestText;
        DialogueText = newEntryData.DialogueText;
        AILogicTemplate = newEntryData.AILogicTemplate;
        Phases = newEntryData.Phases;
        GameObjectLayer = newEntryData.GameObjectLayer;
        GameObjectTag = newEntryData.GameObjectTag;
        SetLayer = newEntryData.SetLayer;
        SetTag = newEntryData.SetTag;
        npcFamily = newEntryData.npcFamily;
        ResetPhaseAfterCombat = newEntryData.ResetPhaseAfterCombat;
        InstantlyHealAfterCombat = newEntryData.InstantlyHealAfterCombat;
        LootBagDuration = newEntryData.LootBagDuration;
        tetherDistance = newEntryData.tetherDistance;
        useTether = newEntryData.useTether;
        returnToSpawnOnTether = newEntryData.returnToSpawnOnTether;
        leashDistance = newEntryData.leashDistance;
        useLeash = newEntryData.useLeash;
        hasSchedule = newEntryData.hasSchedule;
        wanderDuringDay = newEntryData.wanderDuringDay;
        sleepAtNight = newEntryData.sleepAtNight;
        dayPosition = newEntryData.dayPosition;
        nightPosition = newEntryData.nightPosition;
        isSocial = newEntryData.isSocial;
        socialAggroRadius = newEntryData.socialAggroRadius;
        assistFriends = newEntryData.assistFriends;
        hasGossip = newEntryData.hasGossip;
        gossipText = newEntryData.gossipText;
        hasVendorRestock = newEntryData.hasVendorRestock;
        vendorRestockTime = newEntryData.vendorRestockTime;
        vendorHasLimitedStock = newEntryData.vendorHasLimitedStock;
        hasReputation = newEntryData.hasReputation;
        reputationFactionID = newEntryData.reputationFactionID;
        reputationGainOnKill = newEntryData.reputationGainOnKill;
        reputationLossOnKill = newEntryData.reputationLossOnKill;
        hasLootMethod = newEntryData.hasLootMethod;
        lootMethod = newEntryData.lootMethod;
        hasRareSpawn = newEntryData.hasRareSpawn;
        rareSpawnChance = newEntryData.rareSpawnChance;
        rareSpawnCooldown = newEntryData.rareSpawnCooldown;
        hasScaling = newEntryData.hasScaling;
        scaleWithPlayerLevel = newEntryData.scaleWithPlayerLevel;
        scaleWithPartySize = newEntryData.scaleWithPartySize;
        scalingFactorPerPlayer = newEntryData.scalingFactorPerPlayer;
        hasEnrage = newEntryData.hasEnrage;
        enrageTime = newEntryData.enrageTime;
        enrageDamageMultiplier = newEntryData.enrageDamageMultiplier;
        hasPhases = newEntryData.hasPhases;
        phaseCount = newEntryData.phaseCount;
        phaseHealthPercent = newEntryData.phaseHealthPercent;
        hasImmunities = newEntryData.hasImmunities;
        immunities = newEntryData.immunities;
        hasResistances = newEntryData.hasResistances;
        resistances = newEntryData.resistances;
        hasSpecialAbilities = newEntryData.hasSpecialAbilities;
        specialAbilities = newEntryData.specialAbilities;
        isWorldBoss = newEntryData.isWorldBoss;
        isDungeonBoss = newEntryData.isDungeonBoss;
        isRaidBoss = newEntryData.isRaidBoss;
        showBossFrame = newEntryData.showBossFrame;
        lockPlayersInCombat = newEntryData.lockPlayersInCombat;
        hasDialogueOnAggro = newEntryData.hasDialogueOnAggro;
        aggroDialogue = newEntryData.aggroDialogue;
        hasDialogueOnDeath = newEntryData.hasDialogueOnDeath;
        deathDialogue = newEntryData.deathDialogue;
        hasPatrolPath = newEntryData.hasPatrolPath;
        patrolPath = newEntryData.patrolPath;
        patrolPathLoop = newEntryData.patrolPathLoop;
        patrolPathSpeed = newEntryData.patrolPathSpeed;
        hasInteractionCooldown = newEntryData.hasInteractionCooldown;
        interactionCooldown = newEntryData.interactionCooldown;
        hasCustomNameplate = newEntryData.hasCustomNameplate;
        nameplateColor = newEntryData.nameplateColor;
        showLevelInNameplate = newEntryData.showLevelInNameplate;
        showHealthBar = newEntryData.showHealthBar;
        showManaBar = newEntryData.showManaBar;
        hasMount = newEntryData.hasMount;
        mountID = newEntryData.mountID;
        mountOnSpawn = newEntryData.mountOnSpawn;
        hasPet = newEntryData.hasPet;
        petID = newEntryData.petID;
        summonPetOnCombat = newEntryData.summonPetOnCombat;
        hasTitle = newEntryData.hasTitle;
        titleID = newEntryData.titleID;
        hasBestiary = newEntryData.hasBestiary;
        bestiaryID = newEntryData.bestiaryID;
        unlockBestiaryOnKill = newEntryData.unlockBestiaryOnKill;
        hasLore = newEntryData.hasLore;
        loreID = newEntryData.loreID;
        unlockLoreOnKill = newEntryData.unlockLoreOnKill;
    }
}