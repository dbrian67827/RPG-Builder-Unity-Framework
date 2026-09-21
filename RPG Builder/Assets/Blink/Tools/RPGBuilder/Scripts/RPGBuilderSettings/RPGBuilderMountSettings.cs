
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    [CreateAssetMenu(fileName = "Mount Settings", menuName = "Blink/RPGBuilder/Settings/Mount Settings")]
    public class RPGBuilderMountSettings : ScriptableObject
    {
        [Header("General")]
        public bool mountsEnabled = true;
        public float mountCastTime = 1.5f;
        public bool dismountOnCombat = true;
        public bool dismountOnDamage = false;
        public float dismountDamageThreshold = 0.1f;
        public bool canUseMountsInCombat = false;
        public bool canUseMountsIndoors = false;
        public bool canUseMountsInDungeons = false;

        [Header("Movement")]
        public float groundMountSpeed = 1.5f;
        public float flyingMountSpeed = 2f;
        public float swimMountSpeed = 1.2f;
        public float mountAcceleration = 5f;
        public float mountTurnSpeed = 180f;

        [Header("Flying")]
        public bool flyingEnabled = true;
        public float flyingStamina = 100f;
        public float flyingStaminaRegen = 10f;
        public bool requiresFlyingSkill = true;
        public int flyingSkillID = -1;

        [Header("Customization")]
        public bool mountCustomizationEnabled = true;
        public bool mountAccessoriesEnabled = true;
        public bool mountColorEnabled = true;

        [Header("UI")]
        public bool showMountBar = true;
        public bool showMountStamina = true;
        public GameObject mountUIPrefab;
    }

    [System.Serializable]
    public class PetSettings
    {
        public bool petsEnabled = true;
        public float petFollowDistance = 2f;
        public float petFollowSpeed = 3.5f;
        public bool petCollision = false;
        public bool petCanGather = false;
        public bool petCanAttack = false;
        public bool petLevelingEnabled = false;
        public int maxPetLevel = 25;
    }

    [System.Serializable]
    public class AchievementSettings
    {
        public bool achievementsEnabled = true;
        public bool showToast = true;
        public float toastDuration = 5f;
        public bool playSound = true;
        public AudioClip unlockSound;
        public bool broadcastToGuild = false;
        public bool broadcastToServer = false;
        public bool accountWide = false;
    }

    [System.Serializable]
    public class DungeonSettings
    {
        public bool dungeonsEnabled = true;
        public bool lockoutsEnabled = true;
        public float lockoutDuration = 86400f;
        public bool weeklyLockout = false;
        public bool dailyLockout = true;
        public bool mythicPlusEnabled = false;
        public int maxMythicPlusLevel = 30;
        public bool requireKey = false;
    }

    [System.Serializable]
    public class WorldEventSettings
    {
        public bool worldEventsEnabled = true;
        public bool autoStartEvents = false;
        public bool scheduledEvents = true;
        public bool broadcastStart = true;
        public bool broadcastEnd = true;
        public float eventCheckInterval = 60f;
        public bool scalingEnabled = true;
    }
}
