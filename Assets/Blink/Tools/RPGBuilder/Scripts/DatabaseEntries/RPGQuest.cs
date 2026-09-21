using System;
using System.Collections.Generic;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;
using UnityEngine;

public class RPGQuest : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string displayName;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string description;
    
    
    public string ObjectiveText;
    public string ProgressText;

    public bool repeatable;
    public bool canBeTurnedInWithoutNPC;

    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;

    [Serializable]
    public class QuestItemsGivenDATA
    {
        [ItemID] public int itemID = -1;
        public int count;
    }

    [RPGDataList] public List<QuestItemsGivenDATA> itemsGiven = new List<QuestItemsGivenDATA>();

    public enum QuestObjectiveType
    {
        task
    }

    [Serializable]
    public class QuestObjectiveDATA
    {
        public QuestObjectiveType objectiveType;
        [TaskID] public int taskID = -1;
        public float timeLimit;
    }

    [RPGDataList] public List<QuestObjectiveDATA> objectives = new List<QuestObjectiveDATA>();

    public enum QuestRewardType
    {
        item,
        currency,
        treePoint,
        Experience,
        FactionPoint,
        weaponTemplateEXP
    }

    [Serializable]
    public class QuestRewardDATA
    {
        public QuestRewardType rewardType;
        [ItemID] public int itemID = -1;
        [CurrencyID] public int currencyID = -1;
        [PointID] public int treePointID = -1;
        [FactionID] public int factionID = -1;
        [WeaponTemplateID] public int weaponTemplateID = -1;
        public int count;
        public int Experience;
    }

    [RPGDataList] public List<QuestRewardDATA> rewardsGiven = new List<QuestRewardDATA>();
    [RPGDataList] public List<QuestRewardDATA> rewardsToPick = new List<QuestRewardDATA>();


    public enum QuestType
    {
        Main,
        Side,
        Daily,
        Weekly,
        Repeatable,
        Event,
        Dungeon,
        Raid,
        PvP,
        Crafting,
        Gathering,
        Escort,
        Timed,
        Chain,
        Hidden
    }
    public QuestType questType = QuestType.Side;
    public bool isDaily = false;
    public bool isWeekly = false;
    public bool isRepeatableDaily = false;
    public bool isRepeatableWeekly = false;
    public float timeLimit = 0f;
    public bool failOnDeath = false;
    public bool failOnLogout = false;
    public int maxCompletionsPerDay = 1;
    public int maxCompletionsPerWeek = 1;
    public bool isChainQuest = false;
    [QuestID] public int previousQuestID = -1;
    [QuestID] public int nextQuestID = -1;
    public bool isEscortQuest = false;
    [NPCID] public int escortNPCID = -1;
    public Vector3 escortDestination;
    public float escortRadius = 10f;
    public bool isTimed = false;
    public float questTimer = 0f;
    public bool shareable = true;
    public bool abandonable = true;
    public bool autoComplete = false;
    public bool autoAccept = false;
    public int requiredLevel = 1;
    public int recommendedLevel = 1;
    public bool isAccountWide = false;
    public bool showOnMap = true;
    public bool showTracker = true;
    public int sortOrder = 0;
    public string completedText = "Quest Completed!";
    public string failedText = "Quest Failed!";
    public GameObject questStartVFX;
    public GameObject questCompleteVFX;
    public AudioClip questStartSFX;
    public AudioClip questCompleteSFX;
    public bool hasBonusObjectives = false;
    [RPGDataList] public List<QuestObjectiveDATA> bonusObjectives = new List<QuestObjectiveDATA>();


    public void UpdateEntryData(RPGQuest newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        ObjectiveText = newEntryData.ObjectiveText;
        questType = newEntryData.questType;
        isDaily = newEntryData.isDaily;
        isWeekly = newEntryData.isWeekly;
        isRepeatableDaily = newEntryData.isRepeatableDaily;
        isRepeatableWeekly = newEntryData.isRepeatableWeekly;
        timeLimit = newEntryData.timeLimit;
        failOnDeath = newEntryData.failOnDeath;
        failOnLogout = newEntryData.failOnLogout;
        maxCompletionsPerDay = newEntryData.maxCompletionsPerDay;
        maxCompletionsPerWeek = newEntryData.maxCompletionsPerWeek;
        isChainQuest = newEntryData.isChainQuest;
        previousQuestID = newEntryData.previousQuestID;
        nextQuestID = newEntryData.nextQuestID;
        isEscortQuest = newEntryData.isEscortQuest;
        escortNPCID = newEntryData.escortNPCID;
        escortDestination = newEntryData.escortDestination;
        escortRadius = newEntryData.escortRadius;
        isTimed = newEntryData.isTimed;
        questTimer = newEntryData.questTimer;
        shareable = newEntryData.shareable;
        abandonable = newEntryData.abandonable;
        autoComplete = newEntryData.autoComplete;
        autoAccept = newEntryData.autoAccept;
        requiredLevel = newEntryData.requiredLevel;
        recommendedLevel = newEntryData.recommendedLevel;
        isAccountWide = newEntryData.isAccountWide;
        showOnMap = newEntryData.showOnMap;
        showTracker = newEntryData.showTracker;
        sortOrder = newEntryData.sortOrder;
        completedText = newEntryData.completedText;
        failedText = newEntryData.failedText;
        questStartVFX = newEntryData.questStartVFX;
        questCompleteVFX = newEntryData.questCompleteVFX;
        questStartSFX = newEntryData.questStartSFX;
        questCompleteSFX = newEntryData.questCompleteSFX;
        hasBonusObjectives = newEntryData.hasBonusObjectives;
        bonusObjectives = newEntryData.bonusObjectives;

        ProgressText = newEntryData.ProgressText;
        repeatable = newEntryData.repeatable;
        Requirements = newEntryData.Requirements;
        itemsGiven = newEntryData.itemsGiven;
        objectives = newEntryData.objectives;
        rewardsGiven = newEntryData.rewardsGiven;
        rewardsToPick = newEntryData.rewardsToPick;
        canBeTurnedInWithoutNPC = newEntryData.canBeTurnedInWithoutNPC;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
    }
}