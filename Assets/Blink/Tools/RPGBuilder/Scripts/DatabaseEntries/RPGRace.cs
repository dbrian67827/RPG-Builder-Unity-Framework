using System;
using System.Collections.Generic;
using BLINK.RPGBuilder.Characters;
using BLINK.RPGBuilder.Combat;
using BLINK.RPGBuilder.Templates;
using UnityEngine;

public class RPGRace : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public string description;
    
    public int startingSceneID = -1;
    public int startingPositionID = -1;
    
    [FactionID] public int factionID = -1;
    [SpeciesID] public int speciesID = -1;

    public GameObject malePrefab;
    public Sprite maleIcon;
    public GameObject femalePrefab;
    public Sprite femaleIcon;
    public bool dynamicAnimator;
    public RuntimeAnimatorController restAnimatorController, combatAnimatorController;

    public int levelTemplateID = -1;
    
    [Serializable]
    public class RaceGenderData
    {
        public RPGBGender Gender;
        public GameObject Prefab;
        public Sprite Icon;
        public bool DynamicAnimator;
        public RuntimeAnimatorController RestAnimatorController, CombatAnimatorController;
    }
    public List<RaceGenderData> Genders = new List<RaceGenderData>();

    [Serializable]
    public class RACE_CLASSES_DATA
    {
        [ClassID] public int classID = -1;
    }

    [RPGDataList] public List<RACE_CLASSES_DATA> availableClasses = new List<RACE_CLASSES_DATA>();

    
    [Serializable]
    public class WEAPON_TEMPLATES_DATA
    {
        [WeaponTemplateID] public int weaponTemplateID = -1;
    }

    [RPGDataList] public List<WEAPON_TEMPLATES_DATA> weaponTemplates = new List<WEAPON_TEMPLATES_DATA>();
    
    [Serializable]
    public class RACE_STATS_DATA
    {
        [StatID] public int statID = -1;
        public float amount;
        public bool isPercent;
    }

    [RPGDataList] public List<RACE_STATS_DATA> stats = new List<RACE_STATS_DATA>();
    [RPGDataList] public List<CombatData.CustomStatValues> CustomStats = new List<CombatData.CustomStatValues>();
    public bool UseStatListTemplate;
    public StatListTemplate StatListTemplate;
    

    [RPGDataList] public List<RPGItemDATA.StartingItemsDATA> startItems = new List<RPGItemDATA.StartingItemsDATA>();
    [RPGDataList] public List<RPGCombatDATA.ActionAbilityDATA> actionAbilities = new List<RPGCombatDATA.ActionAbilityDATA>();

    public int allocationStatPoints;
    public List<CharacterEntries.AllocatedStatEntry> allocatedStatsEntries = new List<CharacterEntries.AllocatedStatEntry>();

    // ==================== EXTENDED RACE FEATURES ====================
    public bool hasRacialAbilities = false;
    [RPGDataList] public System.Collections.Generic.List<int> racialAbilityIDs = new System.Collections.Generic.List<int>();
    public bool hasRacialPassives = false;
    [RPGDataList] public System.Collections.Generic.List<int> racialBonusIDs = new System.Collections.Generic.List<int>();
    public bool hasRacialMount = false;
    [MountID] public int racialMountID = -1;
    public bool hasCustomization = true;
    public int maxHairStyles = 10;
    public int maxFacialHairStyles = 10;
    public int maxFaceStyles = 10;
    public bool hasAlliedRace = false;
    [RaceID] public int alliedRaceID = -1;
    public int requiredLevelForAlliedRace = 40;
    [FactionID] public int startingFactionID = -1;
    public bool hasStartingArea = false;
    [GameSceneID] public int startingGameSceneID = -1;
    public UnityEngine.Vector3 startingPosition;


    public void UpdateEntryData(RPGRace newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;

        Genders = newEntryData.Genders;
        availableClasses = newEntryData.availableClasses;
        weaponTemplates = newEntryData.weaponTemplates;
        CustomStats = newEntryData.CustomStats;
        UseStatListTemplate = newEntryData.UseStatListTemplate;
        StatListTemplate = newEntryData.StatListTemplate;
        startingSceneID = newEntryData.startingSceneID;
        startingPositionID = newEntryData.startingPositionID;
        startItems = newEntryData.startItems;
        actionAbilities = newEntryData.actionAbilities;
        factionID = newEntryData.factionID;
        allocatedStatsEntries = newEntryData.allocatedStatsEntries;
        allocationStatPoints = newEntryData.allocationStatPoints;
        speciesID = newEntryData.speciesID;
        levelTemplateID = newEntryData.levelTemplateID;
    }
}