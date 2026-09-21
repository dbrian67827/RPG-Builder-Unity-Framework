using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGWorldEvent : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum WorldEventType
    {
        Invasion,
        BossSpawn,
        GatheringBonus,
        ExperienceBonus,
        MerchantArrival,
        WeatherEvent,
        Holiday,
        PvPEvent,
        DungeonBonus,
        CraftingBonus,
        DoubleLoot,
        Custom
    }

    public WorldEventType eventType = WorldEventType.Invasion;
    public bool isRecurring = true;
    public bool isRandom = false;
    public bool isManualOnly = false;
    
    public float duration = 3600f; // seconds
    public float cooldown = 7200f;
    public float randomChance = 10f;
    public float randomCheckInterval = 300f;
    
    public enum EventScheduleType
    {
        Always,
        TimeOfDay,
        DayOfWeek,
        Monthly,
        Yearly,
        RealWorldTime,
        CustomCondition
    }
    public EventScheduleType scheduleType = EventScheduleType.Always;
    
    public int startHour = 0;
    public int endHour = 23;
    public int startDay = 1;
    public int endDay = 7;
    
    [Serializable]
    public class EventLocation
    {
        [GameSceneID] public int gameSceneID = -1;
        public RegionTemplate region;
        public Vector3 position;
        public float radius = 100f;
        public bool useRegion = true;
    }
    [RPGDataList] public List<EventLocation> locations = new List<EventLocation>();
    
    [Serializable]
    public class EventNPCSpawn
    {
        [NPCID] public int npcID = -1;
        public int count = 1;
        public float spawnRadius = 10f;
        public bool isBoss = false;
        public float respawnTime = 60f;
    }
    [RPGDataList] public List<EventNPCSpawn> npcSpawns = new List<EventNPCSpawn>();
    
    [Serializable]
    public class EventBonus
    {
        public enum BonusType
        {
            Experience,
            Currency,
            Faction,
            GatheringYield,
            CraftingSpeed,
            LootChance,
            Damage,
            Defense,
            MovementSpeed,
            Stat
        }
        public BonusType bonusType;
        [CurrencyID] public int currencyID = -1;
        [FactionID] public int factionID = -1;
        [StatID] public int statID = -1;
        public float bonusAmount = 1.5f; // multiplier
        public bool isPercent = true;
    }
    [RPGDataList] public List<EventBonus> bonuses = new List<EventBonus>();
    
    [Serializable]
    public class EventReward
    {
        [ItemID] public int itemID = -1;
        [CurrencyID] public int currencyID = -1;
        public int amount = 1;
        public float chance = 100f;
        public bool onlyOnCompletion = true;
    }
    [RPGDataList] public List<EventReward> rewards = new List<EventReward>();
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;
    
    [RPGDataList] public List<GameActionsData.GameAction> StartGameActions = new List<GameActionsData.GameAction>();
    [RPGDataList] public List<GameActionsData.GameAction> EndGameActions = new List<GameActionsData.GameAction>();
    [RPGDataList] public List<GameActionsData.GameAction> TickGameActions = new List<GameActionsData.GameAction>();
    public bool UseGameActionsTemplate;
    public GameActionsTemplate GameActionsTemplate;
    
    public string description;
    public string startMessage = "A world event has begun!";
    public string endMessage = "The world event has ended.";
    public string activeDescription = "Event is active!";
    
    public GameObject startVFX;
    public GameObject endVFX;
    public GameObject activeVFX;
    public AudioClip startSFX;
    public AudioClip endSFX;
    
    public int minPlayersRequired = 1;
    public int maxPlayers = 0; // 0 = unlimited
    public bool scaleDifficultyWithPlayers = true;
    public bool announceToAll = true;
    public bool showOnMap = true;
    public bool showTimer = true;
    
    public int maxCompletionsPerDay = 0;
    public int maxCompletionsPerWeek = 0;
    
    public void UpdateEntryData(RPGWorldEvent newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        eventType = newEntryData.eventType;
        isRecurring = newEntryData.isRecurring;
        isRandom = newEntryData.isRandom;
        isManualOnly = newEntryData.isManualOnly;
        duration = newEntryData.duration;
        cooldown = newEntryData.cooldown;
        randomChance = newEntryData.randomChance;
        randomCheckInterval = newEntryData.randomCheckInterval;
        scheduleType = newEntryData.scheduleType;
        startHour = newEntryData.startHour;
        endHour = newEntryData.endHour;
        startDay = newEntryData.startDay;
        endDay = newEntryData.endDay;
        locations = newEntryData.locations;
        npcSpawns = newEntryData.npcSpawns;
        bonuses = newEntryData.bonuses;
        rewards = newEntryData.rewards;
        Requirements = newEntryData.Requirements;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
        StartGameActions = newEntryData.StartGameActions;
        EndGameActions = newEntryData.EndGameActions;
        TickGameActions = newEntryData.TickGameActions;
        UseGameActionsTemplate = newEntryData.UseGameActionsTemplate;
        GameActionsTemplate = newEntryData.GameActionsTemplate;
        description = newEntryData.description;
        startMessage = newEntryData.startMessage;
        endMessage = newEntryData.endMessage;
        activeDescription = newEntryData.activeDescription;
        startVFX = newEntryData.startVFX;
        endVFX = newEntryData.endVFX;
        activeVFX = newEntryData.activeVFX;
        startSFX = newEntryData.startSFX;
        endSFX = newEntryData.endSFX;
        minPlayersRequired = newEntryData.minPlayersRequired;
        maxPlayers = newEntryData.maxPlayers;
        scaleDifficultyWithPlayers = newEntryData.scaleDifficultyWithPlayers;
        announceToAll = newEntryData.announceToAll;
        showOnMap = newEntryData.showOnMap;
        showTimer = newEntryData.showTimer;
        maxCompletionsPerDay = newEntryData.maxCompletionsPerDay;
        maxCompletionsPerWeek = newEntryData.maxCompletionsPerWeek;
    }
}
