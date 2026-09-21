using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGMount : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum MountType
    {
        Ground,
        Flying,
        Aquatic,
        MultiTerrain,
        Special
    }

    public MountType mountType = MountType.Ground;
    public GameObject mountPrefab;
    public RuntimeAnimatorController animatorController;
    public Avatar animatorAvatar;
    
    public float baseSpeed = 7f;
    public float sprintSpeed = 10f;
    public float turnSpeed = 5f;
    public float acceleration = 8f;
    
    public bool canFly = false;
    public bool canSwim = false;
    public bool canJump = true;
    public bool canAttackWhileMounted = false;
    public bool dismountOnCombat = true;
    public bool dismountOnDamage = false;
    public float dismountDamageThreshold = 0.3f;
    
    public float summonCastTime = 1.5f;
    public bool isInterruptible = true;
    public bool canUseInCombat = false;
    public bool canUseIndoors = false;
    public bool canUseInDungeons = true;
    
    public GameObject summonEffect;
    public GameObject dismountEffect;
    public AudioClip summonSound;
    public AudioClip dismountSound;
    public AudioClip loopSound;
    
    [Serializable]
    public class MountStatBonus
    {
        [StatID] public int statID = -1;
        public float amount;
        public bool isPercent;
        public bool onlyWhileMounted = true;
    }
    [RPGDataList] public List<MountStatBonus> statBonuses = new List<MountStatBonus>();
    
    [Serializable]
    public class MountAbility
    {
        [AbilityID] public int abilityID = -1;
        public bool autoLearn = true;
    }
    [RPGDataList] public List<MountAbility> mountAbilities = new List<MountAbility>();
    
    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;
    
    [RPGDataList] public List<GameActionsData.GameAction> GameActions = new List<GameActionsData.GameAction>();
    public bool UseGameActionsTemplate;
    public GameActionsTemplate GameActionsTemplate;
    
    public string description;
    public bool isAccountWide = false;
    public bool isTradeable = false;
    public int requiredLevel = 1;
    [CurrencyID] public int purchaseCurrencyID = -1;
    public int purchasePrice = 0;
    
    public float stamina = 100f;
    public float staminaDrainRate = 5f;
    public float staminaRegenRate = 10f;
    public bool useStamina = false;
    
    public int maxPassengers = 0;
    public bool isPassengerAllowedToAttack = false;
    
    public void UpdateEntryData(RPGMount newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        mountType = newEntryData.mountType;
        mountPrefab = newEntryData.mountPrefab;
        animatorController = newEntryData.animatorController;
        animatorAvatar = newEntryData.animatorAvatar;
        baseSpeed = newEntryData.baseSpeed;
        sprintSpeed = newEntryData.sprintSpeed;
        turnSpeed = newEntryData.turnSpeed;
        acceleration = newEntryData.acceleration;
        canFly = newEntryData.canFly;
        canSwim = newEntryData.canSwim;
        canJump = newEntryData.canJump;
        canAttackWhileMounted = newEntryData.canAttackWhileMounted;
        dismountOnCombat = newEntryData.dismountOnCombat;
        dismountOnDamage = newEntryData.dismountOnDamage;
        dismountDamageThreshold = newEntryData.dismountDamageThreshold;
        summonCastTime = newEntryData.summonCastTime;
        isInterruptible = newEntryData.isInterruptible;
        canUseInCombat = newEntryData.canUseInCombat;
        canUseIndoors = newEntryData.canUseIndoors;
        canUseInDungeons = newEntryData.canUseInDungeons;
        summonEffect = newEntryData.summonEffect;
        dismountEffect = newEntryData.dismountEffect;
        summonSound = newEntryData.summonSound;
        dismountSound = newEntryData.dismountSound;
        loopSound = newEntryData.loopSound;
        statBonuses = newEntryData.statBonuses;
        mountAbilities = newEntryData.mountAbilities;
        Requirements = newEntryData.Requirements;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
        GameActions = newEntryData.GameActions;
        UseGameActionsTemplate = newEntryData.UseGameActionsTemplate;
        GameActionsTemplate = newEntryData.GameActionsTemplate;
        description = newEntryData.description;
        isAccountWide = newEntryData.isAccountWide;
        isTradeable = newEntryData.isTradeable;
        requiredLevel = newEntryData.requiredLevel;
        purchaseCurrencyID = newEntryData.purchaseCurrencyID;
        purchasePrice = newEntryData.purchasePrice;
        stamina = newEntryData.stamina;
        staminaDrainRate = newEntryData.staminaDrainRate;
        staminaRegenRate = newEntryData.staminaRegenRate;
        useStamina = newEntryData.useStamina;
        maxPassengers = newEntryData.maxPassengers;
        isPassengerAllowedToAttack = newEntryData.isPassengerAllowedToAttack;
    }
}
