
using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Title", menuName = "Blink/RPGBuilder/Titles/New Title")]
    public class RPGTitle : RPGBuilderDatabaseEntry
    {
        public string titleText = "New Title";
        public string titlePrefix = "";
        public string titleSuffix = "";
        public bool isPrefix = true;
        public Color titleColor = Color.white;
        public Sprite titleIcon;
        public enum TitleRarity { Common, Uncommon, Rare, Epic, Legendary, Mythic }
        public TitleRarity rarity = TitleRarity.Common;
        public bool isHidden = false;
        public bool isAccountWide = false;
        public string description = "";
        public int requiredAchievementID = -1;
        public int requiredQuestID = -1;
        public int requiredLevel = 1;
        public List<BonusData> bonuses = new List<BonusData>();
        public GameObject titleEffect;
        public AudioClip unlockSound;
        public int sortOrder = 0;
        public string category = "General";
        public bool showInChat = true;
        public bool showOnNameplate = true;
        public bool isTemporary = false;
        public float duration = 0f;
        public int requiredHonor = 0;
        public int requiredReputationID = -1;
        public int requiredReputationValue = 0;
        public bool requiresGuild = false;
        public int requiredGuildLevel = 1;
    }
}
