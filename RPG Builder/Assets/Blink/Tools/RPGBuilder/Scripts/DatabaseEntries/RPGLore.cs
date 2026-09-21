
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Lore", menuName = "Blink/RPGBuilder/Lore/New Lore")]
    public class RPGLore : RPGBuilderDatabaseEntry
    {
        public string loreTitle = "New Lore";
        public string loreText = "";
        public Sprite icon;
        public enum LoreCategory { History, Characters, Locations, Creatures, Items, Events, Factions, Magic, Religion, Technology, General }
        public LoreCategory category = LoreCategory.General;
        public string subcategory = "";
        public bool isHidden = false;
        public bool isSecret = false;
        public int requiredLevel = 1;
        public int requiredQuestID = -1;
        public int requiredAchievementID = -1;
        public int requiredItemID = -1;
        public int requiredNPCID = -1;
        public int requiredLoreID = -1;
        public List<int> requiredLoreIDs = new List<int>();
        public int parentLoreID = -1;
        public List<int> childLoreIDs = new List<int>();
        public int sortOrder = 0;
        public bool isAccountWide = false;
        public bool showNotification = true;
        public GameObject unlockEffect;
        public AudioClip unlockSound;
        public int expReward = 0;
        public List<int> rewardItemIDs = new List<int>();
        public int achievementID = -1;
        public bool hasImage = false;
        public Sprite loreImage;
        public bool hasAudio = false;
        public AudioClip loreAudio;
        public bool hasVideo = false;
        public string videoPath = "";
        public bool isCollectible = true;
        public int totalPages = 1;
        public int currentPage = 1;
        public List<string> pages = new List<string>();
        public bool hasChoices = false;
        public List<string> choices = new List<string>();
        public List<int> choiceLoreIDs = new List<int>();
        public bool isBook = false;
        public string bookTitle = "";
        public string author = "";
        public bool isLetter = false;
        public string sender = "";
        public string recipient = "";
        public bool isJournal = false;
        public string journalOwner = "";
    }
}
