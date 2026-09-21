
using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Dungeon", menuName = "Blink/RPGBuilder/Dungeons/New Dungeon")]
    public class RPGDungeon : RPGBuilderDatabaseEntry
    {
        public string dungeonName = "New Dungeon";
        public string description = "";
        public Sprite icon;
        public Sprite loadingScreen;
        public enum DungeonType { Dungeon, Raid, Scenario, Delve, MythicPlus, Heroic, LFR, Challenge, Story, WorldBoss }
        public DungeonType dungeonType = DungeonType.Dungeon;
        public enum DungeonDifficulty { Normal, Heroic, Mythic, LFR, Timewalking, Challenge, MythicPlus }
        public DungeonDifficulty difficulty = DungeonDifficulty.Normal;
        public int requiredLevel = 1;
        public int maxLevel = 100;
        public int requiredItemLevel = 0;
        public int minPlayers = 1;
        public int maxPlayers = 5;
        public int recommendedPlayers = 5;
        public bool isHeroic = false;
        public bool isMythic = false;
        public bool isRaid = false;
        public bool isLFR = false;
        public bool isMythicPlus = false;
        public int mythicPlusLevel = 0;
        public float mythicPlusScaling = 0.1f;
        public List<int> affixIDs = new List<int>();
        public int gameSceneID = -1;
        public Vector3 entranceLocation = Vector3.zero;
        public int entranceSceneID = -1;
        public List<int> requiredQuests = new List<int>();
        public List<int> requiredAchievements = new List<int>();
        public List<int> requiredItemIDs = new List<int>();
        public bool requiresKey = false;
        public int keyItemID = -1;
        public bool hasLockout = true;
        public float lockoutDuration = 86400f;
        public bool lockoutIsWeekly = false;
        public bool lockoutIsDaily = true;
        public bool hasCheckpoints = true;
        public List<string> checkpointNames = new List<string>();
        public List<Vector3> checkpointLocations = new List<Vector3>();
        public List<int> bossNPCs = new List<int>();
        public List<int> trashNPCs = new List<int>();
        public List<int> rewardItemIDs = new List<int>();
        public int expReward = 100;
        public int currencyRewardID = -1;
        public int currencyRewardAmount = 0;
        public List<BonusData> bonuses = new List<BonusData>();
        public GameObject entranceEffect;
        public AudioClip dungeonMusic;
        public bool hasTimer = false;
        public float timeLimit = 3600f;
        public bool failOnTimeout = false;
        public bool hasDeathLimit = false;
        public int deathLimit = 10;
        public bool hasLeaderboard = false;
        public bool isCrossFaction = false;
        public bool requiresGuild = false;
        public int requiredGuildLevel = 1;
        public int requiredHonor = 0;
        public List<int> requiredReputationIDs = new List<int>();
        public List<int> requiredReputationValues = new List<int>();
        public bool hasWorldBuff = false;
        public int worldBuffID = -1;
        public float worldBuffDuration = 3600f;
        public bool scalesWithPlayers = false;
        public float scalingPerPlayer = 0.1f;
        public string category = "Dungeon";
        public int sortOrder = 0;
        public bool isAccountWideLockout = false;
        public bool isMythicPlusTimed = false;
        public float mythicPlusTimeLimit = 1800f;
        public bool hasBonusObjectives = false;
        public List<string> bonusObjectiveNames = new List<string>();
        public bool isPvP = false;
        public bool isStoryMode = false;
        public int storyQuestID = -1;
        public bool hasCutscenes = false;
        public List<string> cutsceneNames = new List<string>();
    }
}
