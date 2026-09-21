using System.Collections.Generic;
using UnityEngine;

public class RPGBuilderMountSettings : RPGBuilderDatabaseEntry
{
    public bool enableMounts = true;
    public bool enableFlyingMounts = true;
    public bool enableAquaticMounts = true;
    public bool enableMountedCombat = false;
    public float mountSummonCastTime = 1.5f;
    public bool dismountOnCombat = true;
    public bool dismountOnDamage = false;
    public float dismountDamageThreshold = 0.3f;
    public bool enableMountStamina = false;
    public float staminaDrainRate = 5f;
    public float staminaRegenRate = 10f;
    public bool enableMountLeveling = false;
    public int maxMountLevel = 25;
    public bool enablePassengerMounts = false;
    public int maxPassengers = 1;
    public GameObject defaultMountSummonEffect;
    public GameObject defaultMountDismountEffect;
    public List<string> restrictedZones = new List<string>();

    public void UpdateEntryData(RPGBuilderMountSettings newEntryData)
    {
        enableMounts = newEntryData.enableMounts;
        enableFlyingMounts = newEntryData.enableFlyingMounts;
        enableAquaticMounts = newEntryData.enableAquaticMounts;
        enableMountedCombat = newEntryData.enableMountedCombat;
        mountSummonCastTime = newEntryData.mountSummonCastTime;
        dismountOnCombat = newEntryData.dismountOnCombat;
        dismountOnDamage = newEntryData.dismountOnDamage;
        dismountDamageThreshold = newEntryData.dismountDamageThreshold;
        enableMountStamina = newEntryData.enableMountStamina;
        staminaDrainRate = newEntryData.staminaDrainRate;
        staminaRegenRate = newEntryData.staminaRegenRate;
        enableMountLeveling = newEntryData.enableMountLeveling;
        maxMountLevel = newEntryData.maxMountLevel;
        enablePassengerMounts = newEntryData.enablePassengerMounts;
        maxPassengers = newEntryData.maxPassengers;
        defaultMountSummonEffect = newEntryData.defaultMountSummonEffect;
        defaultMountDismountEffect = newEntryData.defaultMountDismountEffect;
        restrictedZones = newEntryData.restrictedZones;
    }
}

public class RPGBuilderPetSettings : RPGBuilderDatabaseEntry
{
    public bool enablePets = true;
    public bool enableCombatPets = true;
    public bool enableCompanionPets = true;
    public int maxActivePets = 1;
    public int maxOwnedPets = 50;
    public float petFollowDistance = 2f;
    public float petTeleportDistance = 15f;
    public bool petsCanBeKilled = false;
    public float petRespawnTime = 10f;
    public bool enablePetLeveling = true;
    public int maxPetLevel = 25;
    public bool enablePetAbilities = true;
    public bool enablePetGathering = false;
    public float petGatheringBonus = 0.1f;
    public GameObject defaultPetSummonEffect;
    public GameObject defaultPetDismissEffect;

    public void UpdateEntryData(RPGBuilderPetSettings newEntryData)
    {
        enablePets = newEntryData.enablePets;
        enableCombatPets = newEntryData.enableCombatPets;
        enableCompanionPets = newEntryData.enableCompanionPets;
        maxActivePets = newEntryData.maxActivePets;
        maxOwnedPets = newEntryData.maxOwnedPets;
        petFollowDistance = newEntryData.petFollowDistance;
        petTeleportDistance = newEntryData.petTeleportDistance;
        petsCanBeKilled = newEntryData.petsCanBeKilled;
        petRespawnTime = newEntryData.petRespawnTime;
        enablePetLeveling = newEntryData.enablePetLeveling;
        maxPetLevel = newEntryData.maxPetLevel;
        enablePetAbilities = newEntryData.enablePetAbilities;
        enablePetGathering = newEntryData.enablePetGathering;
        petGatheringBonus = newEntryData.petGatheringBonus;
        defaultPetSummonEffect = newEntryData.defaultPetSummonEffect;
        defaultPetDismissEffect = newEntryData.defaultPetDismissEffect;
    }
}

public class RPGBuilderAchievementSettings : RPGBuilderDatabaseEntry
{
    public bool enableAchievements = true;
    public bool enableAccountWideAchievements = true;
    public bool showAchievementPopups = true;
    public float popupDuration = 5f;
    public bool playSoundOnComplete = true;
    public AudioClip achievementSound;
    public GameObject achievementVFX;
    public bool announceToGuild = false;
    public bool announceToZone = false;
    public int maxTrackedAchievements = 5;
    public bool enableProgressBars = true;
    public bool enableHiddenAchievements = true;

    public void UpdateEntryData(RPGBuilderAchievementSettings newEntryData)
    {
        enableAchievements = newEntryData.enableAchievements;
        enableAccountWideAchievements = newEntryData.enableAccountWideAchievements;
        showAchievementPopups = newEntryData.showAchievementPopups;
        popupDuration = newEntryData.popupDuration;
        playSoundOnComplete = newEntryData.playSoundOnComplete;
        achievementSound = newEntryData.achievementSound;
        achievementVFX = newEntryData.achievementVFX;
        announceToGuild = newEntryData.announceToGuild;
        announceToZone = newEntryData.announceToZone;
        maxTrackedAchievements = newEntryData.maxTrackedAchievements;
        enableProgressBars = newEntryData.enableProgressBars;
        enableHiddenAchievements = newEntryData.enableHiddenAchievements;
    }
}

public class RPGBuilderDungeonSettings : RPGBuilderDatabaseEntry
{
    public bool enableDungeons = true;
    public bool enableRaids = true;
    public bool enableLockouts = true;
    public float defaultLockoutTime = 86400f;
    public bool enableScaling = false;
    public bool enableTimeLimits = false;
    public bool enableDeathLimits = false;
    public int defaultDeathLimit = 0;
    public bool enableCheckpoints = true;
    public bool allowRespawnInDungeon = true;
    public float respawnTime = 10f;
    public bool enableDungeonFinder = false;
    public int minPlayersForDungeon = 1;
    public int maxPlayersForDungeon = 5;
    public int minPlayersForRaid = 10;
    public int maxPlayersForRaid = 25;

    public void UpdateEntryData(RPGBuilderDungeonSettings newEntryData)
    {
        enableDungeons = newEntryData.enableDungeons;
        enableRaids = newEntryData.enableRaids;
        enableLockouts = newEntryData.enableLockouts;
        defaultLockoutTime = newEntryData.defaultLockoutTime;
        enableScaling = newEntryData.enableScaling;
        enableTimeLimits = newEntryData.enableTimeLimits;
        enableDeathLimits = newEntryData.enableDeathLimits;
        defaultDeathLimit = newEntryData.defaultDeathLimit;
        enableCheckpoints = newEntryData.enableCheckpoints;
        allowRespawnInDungeon = newEntryData.allowRespawnInDungeon;
        respawnTime = newEntryData.respawnTime;
        enableDungeonFinder = newEntryData.enableDungeonFinder;
        minPlayersForDungeon = newEntryData.minPlayersForDungeon;
        maxPlayersForDungeon = newEntryData.maxPlayersForDungeon;
        minPlayersForRaid = newEntryData.minPlayersForRaid;
        maxPlayersForRaid = newEntryData.maxPlayersForRaid;
    }
}

public class RPGBuilderWorldEventSettings : RPGBuilderDatabaseEntry
{
    public bool enableWorldEvents = true;
    public bool enableRandomEvents = true;
    public float randomEventCheckInterval = 60f;
    public float randomEventChance = 5f;
    public bool enableScheduledEvents = true;
    public bool announceEvents = true;
    public bool showEventTimers = true;
    public bool showEventOnMap = true;
    public int maxConcurrentEvents = 3;
    public float eventCooldown = 3600f;

    public void UpdateEntryData(RPGBuilderWorldEventSettings newEntryData)
    {
        enableWorldEvents = newEntryData.enableWorldEvents;
        enableRandomEvents = newEntryData.enableRandomEvents;
        randomEventCheckInterval = newEntryData.randomEventCheckInterval;
        randomEventChance = newEntryData.randomEventChance;
        enableScheduledEvents = newEntryData.enableScheduledEvents;
        announceEvents = newEntryData.announceEvents;
        showEventTimers = newEntryData.showEventTimers;
        showEventOnMap = newEntryData.showEventOnMap;
        maxConcurrentEvents = newEntryData.maxConcurrentEvents;
        eventCooldown = newEntryData.eventCooldown;
    }
}
