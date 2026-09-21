
using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Pet", menuName = "Blink/RPGBuilder/Pets/New Pet")]
    public class RPGPet : RPGBuilderDatabaseEntry
    {
        public string petName = "New Pet";
        public string description = "";
        public Sprite icon;
        public GameObject petPrefab;
        public GameObject petModel;
        public enum PetType { Companion, Combat, Utility, Vanity, BattlePet, Gatherer }
        public PetType petType = PetType.Companion;
        public bool isCombatPet = false;
        public float followDistance = 2f;
        public float followSpeed = 3.5f;
        public bool canFly = false;
        public bool canSwim = false;
        public bool isAccountWide = false;
        public bool isHidden = false;
        public int requiredLevel = 1;
        public int requiredAchievementID = -1;
        public int requiredQuestID = -1;
        public int requiredItemID = -1;
        public List<BonusData> bonuses = new List<BonusData>();
        public GameObject summonEffect;
        public AudioClip summonSound;
        public bool hasCustomization = false;
        public List<string> availableNames = new List<string>();
        public bool canBeRenamed = true;
        public bool canGather = false;
        public List<int> gatherableResources = new List<int>();
        public float gatherSpeed = 1f;
        public bool canAttack = false;
        public int petDamage = 10;
        public float attackSpeed = 1f;
        public List<int> petAbilities = new List<int>();
        public bool hasInventory = false;
        public int inventorySize = 10;
        public bool isTradable = false;
        public bool isTemporary = false;
        public float duration = 0f;
        public string category = "Companion";
        public int sortOrder = 0;
        public bool requiresGuild = false;
        public int requiredGuildLevel = 1;
        public bool isRare = false;
        public float dropChance = 1f;
        public bool hasLeveling = false;
        public int maxLevel = 25;
        public float expPerLevel = 100f;
        public List<int> petTalents = new List<int>();
        public bool canBattle = false;
        public int battlePetFamily = 0;
        public List<int> battleAbilities = new List<int>();
    }
}
