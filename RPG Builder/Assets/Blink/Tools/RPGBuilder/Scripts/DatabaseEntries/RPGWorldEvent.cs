
using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New World Event", menuName = "Blink/RPGBuilder/WorldEvents/New World Event")]
    public class RPGWorldEvent : RPGBuilderDatabaseEntry
    {
        public string eventName = "New World Event";
        public string description = "";
        public Sprite icon;
        public enum WorldEventType { Invasion, Boss, Gathering, PvP, Holiday, Seasonal, Dynamic, Escort, Defense, Race, Treasure, Riddle, Dungeon, Raid }
        public WorldEventType eventType = WorldEventType.Dynamic;
        public bool isActive = false;
        public bool isRepeatable = true;
        public float duration = 3600f;
        public float cooldown = 7200f;
        public float startDelay = 0f;
        public bool autoStart = false;
        public bool scheduled = false;
        public List<string> scheduleTimes = new List<string>();
        public int requiredLevel = 1;
        public int maxLevel = 100;
        public int minPlayers = 1;
        public int maxPlayers = 100;
        public bool isPublic = true;
        public bool isInstanced = false;
        public int gameSceneID = -1;
        public Vector3 eventLocation = Vector3.zero;
        public float eventRadius = 100f;
        public List<int> requiredQuests = new List<int>();
        public List<int> requiredAchievements = new List<int>();
        public List<int> spawnedNPCs = new List<int>();
        public List<int> rewardItemIDs = new List<int>();
        public List<int> rewardCurrencyIDs = new List<int>();
        public int expReward = 100;
        public int currencyRewardID = -1;
        public int currencyRewardAmount = 0;
        public List<BonusData> bonuses = new List<BonusData>();
        public GameObject startEffect;
        public GameObject endEffect;
        public AudioClip startSound;
        public AudioClip endSound;
        public bool hasPhases = false;
        public int totalPhases = 1;
        public List<string> phaseNames = new List<string>();
        public bool hasLeaderboard = false;
        public bool hasTimer = true;
        public float timeLimit = 1800f;
        public bool failOnTimeout = true;
        public bool broadcastStart = true;
        public bool broadcastEnd = true;
        public string startMessage = "A world event has started!";
        public string endMessage = "The world event has ended!";
        public bool isHoliday = false;
        public string holidayName = "";
        public bool hasWorldBuff = false;
        public int worldBuffID = -1;
        public float worldBuffDuration = 3600f;
        public bool scalesWithPlayers = true;
        public float scalingPerPlayer = 0.1f;
        public string category = "General";
        public int sortOrder = 0;
        public bool isAccountWideProgress = false;
        public bool requiresGuild = false;
        public int requiredGuildLevel = 1;
        public List<int> requiredReputationIDs = new List<int>();
        public List<int> requiredReputationValues = new List<int>();
        public bool isPvP = false;
        public bool isCrossFaction = false;
        public int requiredHonor = 0;
    }
}
