using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBuilderGeneralSettings : RPGBuilderDatabaseEntry
{
    public bool automaticSave;
    public float automaticSaveDelay;

    public bool clickToLoadScene;
    public float DelayAfterSceneLoad;
    public float LoadingScreenEndDelay;
    public bool enableDevPanel = true;

    public Sprite mainMenuLoadingImage;
    public string mainMenuSceneName, mainMenuLoadingName, mainMenuLoadingDescription;

    public bool useOldController;
    
    public List<string> ActionKeyCategoryList = new List<string>();
    public List<RPGGeneralDATA.ActionKey> actionKeys = new List<RPGGeneralDATA.ActionKey>();

    public Texture2D defaultCursor, merchantCursor, questGiverCursor, interactiveObjectCursor, craftingStationCursor, enemyCursor;
    
    public enum ControllerTypes
    {
        ThirdPerson,
        ThirdPersonShooter,
        TopDownClickToMove,
        TopDownWASD,
        FirstPerson
    }
    

    // ==================== EXTENDED FULL RPG SETTINGS ====================
    [Header("QUALITY OF LIFE")]
    public bool enableAutoLoot = true;
    public bool enableAreaLoot = true;
    public float areaLootRadius = 10f;
    public bool enableAutoRepair = false;
    public bool enableAutoSellJunk = false;
    
    [Header("SOCIAL")]
    public bool enableGuilds = true;
    public bool enableParties = true;
    public bool enableFriends = true;
    public bool enableTrading = true;
    public bool enableMail = true;
    public bool enableChat = true;
    public bool enableVoiceChat = false;
    
    [Header("PVP")]
    public bool enablePvP = true;
    public bool enableDuels = true;
    public bool enableBattlegrounds = false;
    public bool enableArena = false;
    
    [Header("ADVANCED")]
    public bool enableAchievements = true;
    public bool enableTitles = true;
    public bool enableMounts = true;
    public bool enablePets = true;
    public bool enableHousing = false;
    public bool enableTransmog = true;
    public bool enableBestiary = true;
    public bool enableLore = true;
    
    [Header("WORLD")]
    public bool enableWorldEvents = true;
    public bool enableDungeons = true;
    public bool enableWeather = true;
    public bool enableDayNightCycle = true;
    public bool enableDynamicEvents = true;
    
    [Header("ECONOMY")]
    public bool enableAuctionHouse = false;
    public bool enableBank = true;
    public bool enableShops = true;
    public bool enableCraftingQuality = true;
    public bool enableItemDurability = true;
    
    [Header("PROGRESSION")]
    public bool enableParagon = true;
    public bool enablePrestige = false;
    public bool enableMastery = true;
    public bool enableRunes = true;
    public bool enableGlyphs = true;
    
    [Header("ACCESSIBILITY")]
    public bool enableColorblindMode = false;
    public bool enableSubtitles = true;
    public bool enableHighContrast = false;
    public bool enableScreenReader = false;
    
    [Header("PERFORMANCE")]
    public bool enableLOD = true;
    public bool enableOcclusionCulling = true;
    public bool enableDynamicResolution = true;
    public int targetFrameRate = 60;
    
    [Header("MULTIPLAYER")]
    public bool enableCrossplay = false;
    public bool enableCrossSave = true;
    public int maxPlayersInZone = 100;
    public float networkTickRate = 20f;
    

    public void UpdateEntryData(RPGBuilderGeneralSettings newEntryData)
    {
        automaticSave = newEntryData.automaticSave;
        automaticSaveDelay = newEntryData.automaticSaveDelay;
        clickToLoadScene = newEntryData.clickToLoadScene;
        mainMenuSceneName = newEntryData.mainMenuSceneName;
        mainMenuLoadingImage = newEntryData.mainMenuLoadingImage;
        mainMenuLoadingName = newEntryData.mainMenuLoadingName;
        mainMenuLoadingDescription = newEntryData.mainMenuLoadingDescription;
        enableDevPanel = newEntryData.enableDevPanel;
        useOldController = newEntryData.useOldController;
        actionKeys = newEntryData.actionKeys;
        ActionKeyCategoryList = newEntryData.ActionKeyCategoryList;
        DelayAfterSceneLoad = newEntryData.DelayAfterSceneLoad;
        LoadingScreenEndDelay = newEntryData.LoadingScreenEndDelay;
        defaultCursor = newEntryData.defaultCursor;
        merchantCursor = newEntryData.merchantCursor;
        questGiverCursor = newEntryData.questGiverCursor;
        interactiveObjectCursor = newEntryData.interactiveObjectCursor;
        craftingStationCursor = newEntryData.craftingStationCursor;
        enemyCursor = newEntryData.enemyCursor;
        enableAutoLoot = newEntryData.enableAutoLoot;
        enableAreaLoot = newEntryData.enableAreaLoot;
        areaLootRadius = newEntryData.areaLootRadius;
        enableAutoRepair = newEntryData.enableAutoRepair;
        enableAutoSellJunk = newEntryData.enableAutoSellJunk;
        enableGuilds = newEntryData.enableGuilds;
        enableParties = newEntryData.enableParties;
        enableFriends = newEntryData.enableFriends;
        enableTrading = newEntryData.enableTrading;
        enableMail = newEntryData.enableMail;
        enableChat = newEntryData.enableChat;
        enableVoiceChat = newEntryData.enableVoiceChat;
        enablePvP = newEntryData.enablePvP;
        enableDuels = newEntryData.enableDuels;
        enableBattlegrounds = newEntryData.enableBattlegrounds;
        enableArena = newEntryData.enableArena;
        enableAchievements = newEntryData.enableAchievements;
        enableTitles = newEntryData.enableTitles;
        enableMounts = newEntryData.enableMounts;
        enablePets = newEntryData.enablePets;
        enableHousing = newEntryData.enableHousing;
        enableTransmog = newEntryData.enableTransmog;
        enableBestiary = newEntryData.enableBestiary;
        enableLore = newEntryData.enableLore;
        enableWorldEvents = newEntryData.enableWorldEvents;
        enableDungeons = newEntryData.enableDungeons;
        enableWeather = newEntryData.enableWeather;
        enableDayNightCycle = newEntryData.enableDayNightCycle;
        enableDynamicEvents = newEntryData.enableDynamicEvents;
        enableAuctionHouse = newEntryData.enableAuctionHouse;
        enableBank = newEntryData.enableBank;
        enableShops = newEntryData.enableShops;
        enableCraftingQuality = newEntryData.enableCraftingQuality;
        enableItemDurability = newEntryData.enableItemDurability;
        enableParagon = newEntryData.enableParagon;
        enablePrestige = newEntryData.enablePrestige;
        enableMastery = newEntryData.enableMastery;
        enableRunes = newEntryData.enableRunes;
        enableGlyphs = newEntryData.enableGlyphs;
        enableColorblindMode = newEntryData.enableColorblindMode;
        enableSubtitles = newEntryData.enableSubtitles;
        enableHighContrast = newEntryData.enableHighContrast;
        enableScreenReader = newEntryData.enableScreenReader;
        enableLOD = newEntryData.enableLOD;
        enableOcclusionCulling = newEntryData.enableOcclusionCulling;
        enableDynamicResolution = newEntryData.enableDynamicResolution;
        targetFrameRate = newEntryData.targetFrameRate;
        enableCrossplay = newEntryData.enableCrossplay;
        enableCrossSave = newEntryData.enableCrossSave;
        maxPlayersInZone = newEntryData.maxPlayersInZone;
        networkTickRate = newEntryData.networkTickRate;
    }
}
