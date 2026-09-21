
using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Mount", menuName = "Blink/RPGBuilder/Mounts/New Mount")]
    public class RPGMount : RPGBuilderDatabaseEntry
    {
        public string mountName = "New Mount";
        public string description = "";
        public Sprite icon;
        public GameObject mountPrefab;
        public GameObject mountModel;
        public enum MountType { Ground, Flying, Aquatic, Multi, Hover, Dragonriding }
        public MountType mountType = MountType.Ground;
        public float speedMultiplier = 1.5f;
        public float flyingSpeedMultiplier = 2f;
        public float swimSpeedMultiplier = 1.2f;
        public bool canFly = false;
        public bool canSwim = false;
        public bool canDoubleJump = false;
        public bool canGlide = false;
        public float jumpHeight = 2f;
        public bool isAccountWide = false;
        public bool isHidden = false;
        public int requiredLevel = 1;
        public int requiredAchievementID = -1;
        public int requiredQuestID = -1;
        public int requiredReputationID = -1;
        public int requiredReputationValue = 0;
        public int requiredItemID = -1;
        public bool requiresRidingSkill = true;
        public int ridingSkillID = -1;
        public int ridingSkillLevel = 1;
        public List<BonusData> bonuses = new List<BonusData>();
        public GameObject summonEffect;
        public GameObject dismountEffect;
        public AudioClip summonSound;
        public AudioClip mountLoopSound;
        public bool hasCustomization = false;
        public List<Color> availableColors = new List<Color>();
        public List<GameObject> availableAccessories = new List<GameObject>();
        public bool isCombatMount = false;
        public bool canAttackWhileMounted = false;
        public List<int> mountAbilities = new List<int>();
        public float castTime = 1.5f;
        public bool canBeUsedInCombat = false;
        public bool canBeUsedIndoors = false;
        public bool canBeUsedInDungeons = false;
        public bool isWaterWalking = false;
        public bool isFlyingMount = false;
        public bool hasStamina = false;
        public float stamina = 100f;
        public float staminaRegen = 10f;
        public string category = "Ground";
        public int sortOrder = 0;
        public bool isTemporary = false;
        public float duration = 0f;
        public bool requiresGuild = false;
        public int requiredGuildLevel = 1;
        public int requiredHonor = 0;
        public bool isRare = false;
        public float dropChance = 1f;
    }
}
