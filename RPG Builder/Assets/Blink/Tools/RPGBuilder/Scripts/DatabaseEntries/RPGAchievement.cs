
using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Achievement", menuName = "Blink/RPGBuilder/Achievements/New Achievement")]
    public class RPGAchievement : RPGBuilderDatabaseEntry
    {
        public string achievementName = "New Achievement";
        public string description = "";
        public Sprite icon;
        public enum AchievementType { General, Quest, Kill, Collect, Explore, Craft, Social, PvP, Dungeon, WorldEvent, Hidden, FeatOfStrength, Progression, Reputation, Skill, Pet, Mount, Title, Lore, Bestiary }
        public AchievementType achievementType = AchievementType.General;
        public enum AchievementRarity { Common, Uncommon, Rare, Epic, Legendary }
        public AchievementRarity rarity = AchievementRarity.Common;
        public int points = 10;
        public bool isHidden = false;
        public bool isAccountWide = false;
        public bool isSecret = false;
        public int parentAchievementID = -1;
        public List<int> childAchievementIDs = new List<int>();
        public int requiredCount = 1;
        public int currentProgress = 0;
        public enum RequirementType { None, KillNPC, CompleteQuest, CollectItem, ReachLevel, ReachReputation, LearnRecipe, CastAbility, DiscoverLocation, CraftItem, GatherResource, WinPvP, CompleteDungeon, ParticipateWorldEvent, TamePet, UnlockMount, UnlockTitle, ReadLore, KillBestiary, EarnCurrency, EarnHonor, JoinGuild, ReachGuildLevel, CompleteDaily, Fishing, Housing }
        [Serializable]
        public class AchievementRequirement
        {
            public RequirementType type = RequirementType.None;
            public int id = -1;
            public int count = 1;
            public string description = "";
        }
        public List<AchievementRequirement> requirements = new List<AchievementRequirement>();
        public List<int> rewardItemIDs = new List<int>();
        public List<int> rewardCurrencyIDs = new List<int>();
        public List<int> rewardTitleIDs = new List<int>();
        public List<int> rewardMountIDs = new List<int>();
        public List<int> rewardPetIDs = new List<int>();
        public bool givesTitle = false;
        public int titleRewardID = -1;
        public bool givesMount = false;
        public int mountRewardID = -1;
        public bool givesPet = false;
        public int petRewardID = -1;
        public int expReward = 0;
        public bool showToast = true;
        public GameObject rewardEffect;
        public AudioClip unlockSound;
        public string category = "General";
        public int sortOrder = 0;
        public bool isTimed = false;
        public float timeLimit = 0f;
        public bool isRepeatable = false;
        public bool resetDaily = false;
        public bool resetWeekly = false;
        public int requiredLevel = 1;
        public bool hasProgressBar = true;
        public string progressText = "";
        public bool broadcastToGuild = false;
        public bool broadcastToServer = false;
        public List<int> requiredAchievementIDs = new List<int>();
    }
}
