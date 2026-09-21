using System;
using System.Collections.Generic;
using BLINK.RPGBuilder.Combat;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;
using UnityEngine;

public class RPGAbility : RPGBuilderDatabaseEntry
{
    [Header("-----BASE DATA-----")] [HideInInspector]
    public string _name;

    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public bool learnedByDefault;

    public enum AbilityType
    {
        Normal,
        PlayerAutoAttack,
        PlayerActionAbility
    }

    public AbilityType abilityType;

    public enum TARGET_TYPES
    {
        SELF,
        CONE,
        AOE,
        LINEAR,
        PROJECTILE,
        SQUARE,
        GROUND,
        GROUND_LEAP,
        TARGET_PROJECTILE,
        TARGET_INSTANT
    }

    public enum ABILITY_TAGS
    {
        onHit,
        onKill,
        shapeshifting,
        stealth
    }


    public enum AbilityActivationType
    {
        Instant,
        Casted,
        Channeled,
        Charged
    }

    [Serializable]
    public class AbilityEffectsApplied
    {
        [EffectID] public int effectID = -1;
        public float chance = 100f;
        public int effectRank;
        public RPGCombatDATA.TARGET_TYPE target = RPGCombatDATA.TARGET_TYPE.Target;

        public bool isSpread;
        public float spreadDistanceMax;
        public int spreadUnitMax;

        public float delay;
        public string tooltipText;
    }

    public enum COST_TYPES
    {
        FLAT,
        PERCENT_OF_MAX,
        PERCENT_OF_CURRENT
    }

    [Serializable]
    public class AbilityTagsData
    {
        public ABILITY_TAGS tag;
        [EffectID] public int effectID = -1;
    }

    [Serializable]
    public class RPGAbilityRankData
    {
        public bool ShowedInEditor;
        public int unlockCost;

        public string TooltipText;

        public AbilityActivationType activationType;
        public float castTime;
        public bool castInRun;
        public bool castBarVisible = true;
        public bool faceCursorWhileCasting = true;
        public bool faceCursorWhenOnCastStart = true;
        public bool faceCursorWhenOnCastEnd = true;
        public bool canBeUsedStunned;
        public bool animationTriggered;
        public bool comboStarsAfterCastComplete;
        public bool cancelStealth;
        public bool canUseWhileMounted;

        [RPGDataList] public List<RequirementsData.RequirementGroup> Requirements =
            new List<RequirementsData.RequirementGroup>();

        public bool UseRequirementsTemplate;
        public RequirementsTemplate RequirementsTemplate;

        public TARGET_TYPES targetType;

        public bool isToggle, isToggleCostOnTrigger;
        public float toggledTriggerInterval;

        public int MaxUnitHit = 1;
        public float minRange;
        public float maxRange;

        public float standTimeDuration;
        public bool canRotateInStandTime;
        public float castSpeedSlowAmount;
        public float castSpeedSlowTime;
        public float castSpeedSlowRate;
        public float AIAttackTime = 1;
            
        public float coneDegree;
        public int ConeHitCount = 1;
        public float ConeHitInterval;
        public float AOERadius;
        public int AOEHitCount = 1;
        public float AOEHitInterval;
        public float linearWidth;
        public float linearHeight;
        public float linearLength;
        public float projectileSpeed;
        public float projectileDistance;
        public float projectileAngleSpread;
        public int projectileCount = 1;
        public float firstProjectileDelay;
        public float projectileDelay;
        public float projectileDuration = 5;
        public float projectileComeBackTime;
        public float projectileComeBackSpeed;
        public bool isProjectileComeBack;
        public float projectileNearbyUnitDistanceMax;
        public float projectileNearbyUnitMaxHit;
        public bool isProjectileNearbyUnit;
        public bool projectileDestroyedByEnvironment = true;
        public LayerMask projectileDestroyLayers;
        public bool projectileAffectedByGravity;
        public bool projectileShootOnClickPosition;
        public float projectileDistanceMaxForNPC = 50;
        public bool mustLookAtTarget = true;
        public float squareWidth;
        public float squareLength;
        public float squareHeight;
        public float groundRadius;
        public float groundRange;
        public float groundHitTime;
        public int groundHitCount = 1;
        public float groundHitInterval;
        
        public bool CanHitPlayer, CanHitAlly, CanHitNeutral = true, CanHitEnemy = true, CanHitSelf, CanHitPet, CanHitOwner;

        public bool UsedOnALly;
        public bool LookAtTargetDuringCombatAction;
        public bool LookAtTargetOnCombatAction;

        public GameObject projectileEffect;
        [HideInInspector] public string projectileSocketName;
        [HideInInspector] public string projectileTargetSocketName;
        public RPGBNodeSocket projectileNodeSocket;
        public RPGBNodeSocket projectileTargetNodeSocket;
        public bool projectileUseNodeSocket;
        public bool projectileTargetUseNodeSocket;
        public bool projectileParentedToCaster;
        [HideInInspector] public AudioClip projectileSound;
        public SoundTemplate projectileSoundTemplate;
        public bool projectileSoundParentedToEffect;

        public bool useCustomCollision;
        public RPGNpc.NPCColliderType projectileColliderType;
        public Vector3 colliderCenter, colliderSize;
        public float colliderRadius, colliderHeight;

        public GameObject groundVisualEffect;
        public Vector3 effectPositionOffset;
        public float groundVisualEffectDuration = 5;

        public bool hitEffectUseSocket;
        public GameObject hitEffect;
        public float hitEffectDuration;
        [HideInInspector] public string hitEffectSocketName;
        public RPGBNodeSocket hitEffectNodeSocket;
        public Vector3 hitEffectPositionOffset;
        public bool hitAttachedToNode;

        public float channelTime;
        public float groundLeapDuration;
        public float groundLeapHeight;
        public float groundLeapSpeed;
        public LayerMask groundLeapBlockLayers;

        public bool isAttackSpeedCooldown;
        public bool isAttackSpeedGCD;
        public float cooldown;
        public bool isGCD;
        public bool startCDOnActivate = true;
        public bool CanUseDuringGCD;
        public bool isSharingCooldown;
        [HideInInspector] public string cooldownTag;
        public RPGBAbilityCooldownTag abilityCooldownTag;

        [RPGDataList] public List<AbilityEffectsApplied> effectsApplied = new List<AbilityEffectsApplied>();
        [RPGDataList] public List<AbilityEffectsApplied> casterEffectsApplied = new List<AbilityEffectsApplied>();
        
        
        public List<CombatData.AbilityAction> Actions = new List<CombatData.AbilityAction>();

        [RPGDataList] public List<VisualEffectEntry> VisualEffectEntries = new List<VisualEffectEntry>();
        [RPGDataList] public List<AnimationEntry> AnimationEntries = new List<AnimationEntry>();
        [RPGDataList] public List<SoundEntry> SoundEntries = new List<SoundEntry>();

        [RPGDataList]
        public List<RPGCombatDATA.CombatVisualEffect> visualEffects = new List<RPGCombatDATA.CombatVisualEffect>();

        [RPGDataList]
        public List<RPGCombatDATA.CombatVisualAnimation> visualAnimations =
            new List<RPGCombatDATA.CombatVisualAnimation>();

        [RPGDataList] public List<AbilityTagsData> tagsData = new List<AbilityTagsData>();

        [AbilityID] public int extraAbilityExecutedID;
        public RPGCombatDATA.CombatVisualActivationType extraAbilityExecutedActivationType;

        // ========== EXTENDED RPG FEATURES ==========
        public bool hasCharges = false;
        public int maxCharges = 2;
        public float chargeRecoveryTime = 10f;
        public bool chargesShareCooldown = false;

        public bool canBeDodged = true;
        public bool canBeParried = true;
        public bool canBeBlocked = true;
        public bool canBeReflected = false;
        public bool canBeInterrupted = true;
        public bool isInterruptArmor = false;
        public float interruptArmorAmount = 0f;

        public bool canCastWhileMoving = false;
        public float movementSpeedWhileCasting = 0.5f;

        public bool hasComboPointCost = false;
        public int comboPointsCost = 0;
        public bool generatesComboPoints = false;
        public int comboPointsGenerated = 1;

        public bool hasRuneCost = false;
        [RuneID] public int runeCostID = -1;
        public int runeCostAmount = 1;

        public bool hasPvPModifiers = false;
        public float pvpDamageMultiplier = 1f;
        public float pvpHealingMultiplier = 1f;
        public float pvpDurationMultiplier = 1f;

        public bool hasScaling = true;
        public float damageScalingPerLevel = 0.05f;
        public float healingScalingPerLevel = 0.05f;

        public bool hasCriticalOverride = false;
        public float criticalChanceOverride = 0f;
        public float criticalDamageOverride = 0f;

        public bool hasSecondaryEffect = false;
        [EffectID] public int secondaryEffectID = -1;
        public float secondaryEffectChance = 25f;

        public bool hasChainEffect = false;
        public int chainMaxJumps = 3;
        public float chainJumpDistance = 10f;
        public float chainDamageReductionPerJump = 0.2f;

        public bool hasGroundEffect = false;
        public GameObject groundEffectPrefab;
        public float groundEffectDuration = 5f;
        public float groundEffectTickInterval = 1f;

        public bool hasResourceOverTime = false;
        public float resourceTickAmount = 5f;
        public float resourceTickInterval = 1f;
        public int resourceTickCount = 5;

        public bool requiresLineOfSight = true;
        public bool ignoresArmor = false;
        public float armorPenetrationPercent = 0f;

        public bool hasKnockback = false;
        public float knockbackDistance = 5f;
        public float knockbackDuration = 0.5f;

        public bool hasPull = false;
        public float pullDistance = 5f;
        public float pullSpeed = 10f;

        public bool hasLifeSteal = false;
        public float lifeStealPercent = 0.1f;

        public bool hasShield = false;
        public float shieldAmount = 100f;
        public float shieldDuration = 10f;

        public bool hasThreatModifier = false;
        public float threatMultiplier = 1f;
        public bool isTaunt = false;

        public bool hasDispel = false;
        public int dispelCount = 1;
        public bool dispelOffensive = true;
        public bool dispelDefensive = false;

        public bool hasStealBuff = false;
        public int stealBuffCount = 1;

        public bool isChanneledWhileMoving = false;
        public bool cancelOnDamage = false;
        public float damageThresholdToCancel = 0.3f;

        public bool hasVisualScaling = false;
        public float visualScalePerRank = 0.1f;

        public bool hasSoundScaling = false;
        public float soundVolumePerRank = 0f;

        public bool isAOECap = false;
        public int aoeCapCount = 5;
        public float aoeCapDamageReduction = 0.5f;

        public bool hasCooldownReductionOnHit = false;
        public float cooldownReductionPerHit = 0.5f;

        public bool hasCooldownResetChance = false;
        public float cooldownResetChance = 5f;

        public bool hasProcEffect = false;
        [AbilityID] public int procAbilityID = -1;
        public float procChance = 10f;
        public float procCooldown = 5f;

        // New targeting
        public bool canTargetDead = false;
        public bool canTargetSelfOnly = false;
        public bool requiresComboPoints = false;
        public int requiredComboPoints = 0;

    }

    [RPGDataList] public List<RPGAbilityRankData> ranks = new List<RPGAbilityRankData>();

    // Global ability extended data
    public bool isPassive = false;
    public bool isStance = false;
    public bool isTalent = false;
    public bool isPvPOnly = false;
    public bool isPvEOnly = false;
    public int requiredLevel = 1;
    [ClassID] public int requiredClassID = -1;
    [RaceID] public int requiredRaceID = -1;
    public bool isUnique = false;
    public bool isHidden = false;
    public int maxRank = 5;
    public bool hasAutoLearn = false;
    public bool hasGCDOverride = false;
    public float gcdOverrideDuration = 0f;
    public bool hasCustomTooltip = false;
    public string customTooltipFormat = "";
    public bool showCooldownInTooltip = true;
    public bool showRangeInTooltip = true;
    public bool showCostInTooltip = true;

    public void UpdateEntryData(RPGAbility newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;

        ranks = newEntryData.ranks;
        learnedByDefault = newEntryData.learnedByDefault;
        abilityType = newEntryData.abilityType;

        // Extended
        isPassive = newEntryData.isPassive;
        isStance = newEntryData.isStance;
        isTalent = newEntryData.isTalent;
        isPvPOnly = newEntryData.isPvPOnly;
        isPvEOnly = newEntryData.isPvEOnly;
        requiredLevel = newEntryData.requiredLevel;
        requiredClassID = newEntryData.requiredClassID;
        requiredRaceID = newEntryData.requiredRaceID;
        isUnique = newEntryData.isUnique;
        isHidden = newEntryData.isHidden;
        maxRank = newEntryData.maxRank;
        hasAutoLearn = newEntryData.hasAutoLearn;
        hasGCDOverride = newEntryData.hasGCDOverride;
        gcdOverrideDuration = newEntryData.gcdOverrideDuration;
        hasCustomTooltip = newEntryData.hasCustomTooltip;
        customTooltipFormat = newEntryData.customTooltipFormat;
        showCooldownInTooltip = newEntryData.showCooldownInTooltip;
        showRangeInTooltip = newEntryData.showRangeInTooltip;
        showCostInTooltip = newEntryData.showCostInTooltip;
    }

    public void CopyEntryData(RPGAbilityRankData original, RPGAbilityRankData copied)
    {
        original.TooltipText = copied.TooltipText;
        original.unlockCost = copied.unlockCost;
        original.activationType = copied.activationType;
        original.castTime = copied.castTime;
        original.castInRun = copied.castInRun;
        original.castBarVisible = copied.castBarVisible;
        original.faceCursorWhileCasting = copied.faceCursorWhileCasting;
        original.faceCursorWhenOnCastEnd = copied.faceCursorWhenOnCastEnd;
        original.faceCursorWhenOnCastStart = copied.faceCursorWhenOnCastStart;
        original.canBeUsedStunned = copied.canBeUsedStunned;
        original.animationTriggered = copied.animationTriggered;
        original.Requirements = copied.Requirements;
        original.UseRequirementsTemplate = copied.UseRequirementsTemplate;
        original.RequirementsTemplate = copied.RequirementsTemplate;
        original.comboStarsAfterCastComplete = copied.comboStarsAfterCastComplete;
        original.cancelStealth = copied.cancelStealth;
        original.AIAttackTime = copied.AIAttackTime;
        original.canUseWhileMounted = copied.canUseWhileMounted;

        original.targetType = copied.targetType;

        original.isToggle = copied.isToggle;
        original.isToggleCostOnTrigger = copied.isToggleCostOnTrigger;
        original.toggledTriggerInterval = copied.toggledTriggerInterval;


        original.MaxUnitHit = copied.MaxUnitHit;
        original.minRange = copied.minRange;
        original.maxRange = copied.maxRange;

        original.standTimeDuration = copied.standTimeDuration;
        original.canRotateInStandTime = copied.canRotateInStandTime;
        original.castSpeedSlowAmount = copied.castSpeedSlowAmount;
        original.castSpeedSlowTime = copied.castSpeedSlowTime;
        original.castSpeedSlowRate = copied.castSpeedSlowRate;

        original.coneDegree = copied.coneDegree;
        original.ConeHitCount = copied.ConeHitCount;
        original.ConeHitInterval = copied.ConeHitInterval;
        original.AOERadius = copied.AOERadius;
        original.AOEHitCount = copied.AOEHitCount;
        original.AOEHitInterval = copied.AOEHitInterval;
        original.linearWidth = copied.linearWidth;
        original.linearHeight = copied.linearHeight;
        original.linearLength = copied.linearLength;
        original.projectileSpeed = copied.projectileSpeed;
        original.projectileDistance = copied.projectileDistance;
        original.projectileAngleSpread = copied.projectileAngleSpread;
        original.projectileCount = copied.projectileCount;
        original.projectileDelay = copied.projectileDelay;
        original.firstProjectileDelay = copied.firstProjectileDelay;
        original.projectileComeBackTime = copied.projectileComeBackTime;
        original.projectileComeBackSpeed = copied.projectileComeBackSpeed;
        original.isProjectileComeBack = copied.isProjectileComeBack;
        original.projectileNearbyUnitDistanceMax = copied.projectileNearbyUnitDistanceMax;
        original.projectileNearbyUnitMaxHit = copied.projectileNearbyUnitMaxHit;
        original.isProjectileNearbyUnit = copied.isProjectileNearbyUnit;
        original.projectileDestroyedByEnvironment = copied.projectileDestroyedByEnvironment;
        original.projectileAffectedByGravity = copied.projectileAffectedByGravity;
        original.projectileShootOnClickPosition = copied.projectileShootOnClickPosition;
        original.projectileSoundTemplate = copied.projectileSoundTemplate;
        original.projectileDistanceMaxForNPC = copied.projectileDistanceMaxForNPC;
        original.mustLookAtTarget = copied.mustLookAtTarget;
        original.squareWidth = copied.squareWidth;
        original.squareLength = copied.squareLength;
        original.squareHeight = copied.squareHeight;
        original.groundRadius = copied.groundRadius;
        original.groundRange = copied.groundRange;
        original.groundHitTime = copied.groundHitTime;
        original.groundHitCount = copied.groundHitCount;
        original.groundHitInterval = copied.groundHitInterval;

        original.projectileEffect = copied.projectileEffect;
        original.projectileNodeSocket = copied.projectileNodeSocket;
        original.projectileTargetNodeSocket = copied.projectileTargetNodeSocket;
        original.hitEffectNodeSocket = copied.hitEffectNodeSocket;
        original.projectileUseNodeSocket = copied.projectileUseNodeSocket;
        original.projectileTargetUseNodeSocket = copied.projectileTargetUseNodeSocket;
        original.hitEffect = copied.hitEffect;
        original.hitEffectDuration = copied.hitEffectDuration;
        original.projectileParentedToCaster = copied.projectileParentedToCaster;
        original.projectileSound = copied.projectileSound;
        original.projectileSoundParentedToEffect = copied.projectileSoundParentedToEffect;
        original.useCustomCollision = copied.useCustomCollision;
        original.projectileColliderType = copied.projectileColliderType;
        original.projectileDestroyLayers = copied.projectileDestroyLayers;
        original.colliderCenter = copied.colliderCenter;
        original.colliderRadius = copied.colliderRadius;
        original.colliderSize = copied.colliderSize;
        original.colliderHeight = copied.colliderHeight;
        original.groundVisualEffect = copied.groundVisualEffect;
        original.effectPositionOffset = copied.effectPositionOffset;
        
        original.CanHitPlayer = copied.CanHitPlayer;
        original.CanHitAlly = copied.CanHitAlly;
        original.CanHitNeutral = copied.CanHitNeutral;
        original.CanHitEnemy = copied.CanHitEnemy;
        original.CanHitSelf = copied.CanHitSelf;
        original.CanHitPet = copied.CanHitPet;
        original.CanHitOwner = copied.CanHitOwner;
        
        original.UsedOnALly = copied.UsedOnALly;
        original.LookAtTargetDuringCombatAction = copied.LookAtTargetDuringCombatAction;
        original.LookAtTargetOnCombatAction = copied.LookAtTargetOnCombatAction;

        original.channelTime = copied.channelTime;
        original.groundLeapDuration = copied.groundLeapDuration;
        original.groundLeapHeight = copied.groundLeapHeight;
        original.groundLeapSpeed = copied.groundLeapSpeed;
        original.groundLeapBlockLayers = copied.groundLeapBlockLayers;

        original.isAttackSpeedCooldown = copied.isAttackSpeedCooldown;
        original.isAttackSpeedGCD = copied.isAttackSpeedGCD;
        original.cooldown = copied.cooldown;
        original.isGCD = copied.isGCD;
        original.startCDOnActivate = copied.startCDOnActivate;
        original.CanUseDuringGCD = copied.CanUseDuringGCD;
        original.isSharingCooldown = copied.isSharingCooldown;
        original.abilityCooldownTag = copied.abilityCooldownTag;

        original.effectsApplied = new List<AbilityEffectsApplied>();
        for (var index = 0; index < copied.effectsApplied.Count; index++)
        {
            AbilityEffectsApplied newRef = new AbilityEffectsApplied
            {
                chance = copied.effectsApplied[index].chance,
                target = copied.effectsApplied[index].target,
                effectID = copied.effectsApplied[index].effectID,
                effectRank = copied.effectsApplied[index].effectRank,
                isSpread = copied.effectsApplied[index].isSpread,
                spreadDistanceMax = copied.effectsApplied[index].spreadDistanceMax,
                spreadUnitMax = copied.effectsApplied[index].spreadUnitMax,
                tooltipText = copied.effectsApplied[index].tooltipText
            };
            original.effectsApplied.Add(newRef);
        }
        
        original.casterEffectsApplied = new List<AbilityEffectsApplied>();
        for (var index = 0; index < copied.casterEffectsApplied.Count; index++)
        {
            AbilityEffectsApplied newRef = new AbilityEffectsApplied();
            newRef.chance = copied.casterEffectsApplied[index].chance;
            newRef.target = copied.casterEffectsApplied[index].target;
            newRef.effectID = copied.casterEffectsApplied[index].effectID;
            newRef.effectRank = copied.casterEffectsApplied[index].effectRank;
            newRef.isSpread = copied.casterEffectsApplied[index].isSpread;
            newRef.spreadDistanceMax = copied.casterEffectsApplied[index].spreadDistanceMax;
            newRef.spreadUnitMax = copied.casterEffectsApplied[index].spreadUnitMax;
            original.casterEffectsApplied.Add(newRef);
        }

        original.Actions = new List<CombatData.AbilityAction>();
        for (var index = 0; index < copied.Actions.Count; index++)
        {
            CombatData.AbilityAction newRef = new CombatData.AbilityAction
            {
                RequirementsTarget = copied.Actions[index].RequirementsTarget,
                RequirementsTemplate = copied.Actions[index].RequirementsTemplate,
                ActionsTarget = copied.Actions[index].ActionsTarget,
                GameActionsTemplate = copied.Actions[index].GameActionsTemplate,
            };
            original.Actions.Add(newRef);
        }

        original.VisualEffectEntries = new List<VisualEffectEntry>();
        for (var index = 0; index < copied.VisualEffectEntries.Count; index++)
        {
            VisualEffectEntry newVisualEffectEntry = new VisualEffectEntry
            {
                ActivationType = copied.VisualEffectEntries[index].ActivationType,
                Template = copied.VisualEffectEntries[index].Template,
                NodeSocket = copied.VisualEffectEntries[index].NodeSocket,
                ParentedToCaster = copied.VisualEffectEntries[index].ParentedToCaster,
                UseNodeSocket = copied.VisualEffectEntries[index].UseNodeSocket,
                Duration = copied.VisualEffectEntries[index].Duration,
                Delay = copied.VisualEffectEntries[index].Delay,
                Scale = copied.VisualEffectEntries[index].Scale,
                PositionOffset = copied.VisualEffectEntries[index].PositionOffset,
            };

            original.VisualEffectEntries.Add(newVisualEffectEntry);
        }

        original.AnimationEntries = new List<AnimationEntry>();
        for (var index = 0; index < copied.AnimationEntries.Count; index++)
        {
            AnimationEntry newAnimationEntry = new AnimationEntry
            {
                ActivationType = copied.AnimationEntries[index].ActivationType,
                Template = copied.AnimationEntries[index].Template,
                Delay = copied.AnimationEntries[index].Delay,
                ShowWeapons = copied.AnimationEntries[index].ShowWeapons,
                ShowWeaponsDuration = copied.AnimationEntries[index].ShowWeaponsDuration,
                ModifySpeed = copied.AnimationEntries[index].ModifySpeed,
                ModifierSpeed = copied.AnimationEntries[index].ModifierSpeed,
                SpeedParameterName = copied.AnimationEntries[index].SpeedParameterName,
            };
            original.AnimationEntries.Add(newAnimationEntry);
        }

        original.SoundEntries = new List<SoundEntry>();
        for (var index = 0; index < copied.SoundEntries.Count; index++)
        {
            SoundEntry newAnimationEntry = new SoundEntry
            {
                ActivationType = copied.SoundEntries[index].ActivationType,
                Template = copied.SoundEntries[index].Template,
                Delay = copied.SoundEntries[index].Delay,
            };
            original.SoundEntries.Add(newAnimationEntry);
        }

        original.tagsData = new List<AbilityTagsData>();
        for (var index = 0; index < copied.tagsData.Count; index++)
        {
            AbilityTagsData newRef = new AbilityTagsData
            {
                tag = copied.tagsData[index].tag, effectID = copied.tagsData[index].effectID
            };
            original.tagsData.Add(newRef);
        }

        original.Actions = new List<CombatData.AbilityAction>();
        for (var index = 0; index < copied.Actions.Count; index++)
        {
            CombatData.AbilityAction newRef = new CombatData.AbilityAction
            {
                RequirementsTarget = copied.Actions[index].RequirementsTarget,
                RequirementsTemplate = copied.Actions[index].RequirementsTemplate,
                ActionsTarget = copied.Actions[index].ActionsTarget,
                GameActionsTemplate = copied.Actions[index].GameActionsTemplate,
            };
            original.Actions.Add(newRef);
        }

        original.extraAbilityExecutedID = copied.extraAbilityExecutedID;
        original.extraAbilityExecutedActivationType = copied.extraAbilityExecutedActivationType;

        original.Requirements = new List<RequirementsData.RequirementGroup>();
        for (var index = 0; index < copied.Requirements.Count; index++)
        {
            RequirementsData.RequirementGroup newGroup = new RequirementsData.RequirementGroup
            {
                checkCount = copied.Requirements[index].checkCount,
                requiredCount = copied.Requirements[index].requiredCount,
            };

            foreach (var newData in copied.Requirements[index].Requirements)
            {
                RequirementsData.Requirement requirement = new RequirementsData.Requirement
                {
                    type = newData.type,
                    condition = newData.condition,
                    AbilityID = newData.AbilityID,
                    BonusID = newData.BonusID,
                    RecipeID = newData.RecipeID,
                    ResourceID = newData.ResourceID,
                    EffectID = newData.EffectID,
                    NPCID = newData.NPCID,
                    StatID = newData.StatID,
                    FactionID = newData.FactionID,
                    ComboID = newData.ComboID,
                    RaceID = newData.RaceID,
                    LevelsID = newData.LevelsID,
                    ClassID = newData.ClassID,
                    SpeciesID = newData.SpeciesID,
                    ItemID = newData.ItemID,
                    CurrencyID = newData.CurrencyID,
                    PointID = newData.PointID,
                    TalentTreeID = newData.TalentTreeID,
                    SkillID = newData.SkillID,
                    SpellbookID = newData.SpellbookID,
                    WeaponTemplateID = newData.WeaponTemplateID,
                    EnchantmentID = newData.EnchantmentID,
                    GearSetID = newData.GearSetID,
                    GameSceneID = newData.GameSceneID,
                    QuestID = newData.QuestID,
                    DialogueID = newData.DialogueID,

                    Knowledge = newData.Knowledge,
                    State = newData.State,
                    Comparison = newData.Comparison,
                    Value = newData.Value,
                    Ownership = newData.Ownership,
                    ItemCondition = newData.ItemCondition,
                    Progression = newData.Progression,
                    Entity = newData.Entity,
                    PointType = newData.PointType,
                    DialogueNodeState = newData.DialogueNodeState,
                    EffectCondition = newData.EffectCondition,
                    AmountType = newData.AmountType,

                    Amount1 = newData.Amount1,
                    Amount2 = newData.Amount2,
                    Float1 = newData.Float1,
                    Consume = newData.Consume,
                    BoolBalue1 = newData.BoolBalue1,
                    BoolBalue2 = newData.BoolBalue2,
                    BoolBalue3 = newData.BoolBalue3,
                    IsPercent = newData.IsPercent,

                    EffectTag = newData.EffectTag,
                    EffectType = newData.EffectType,
                    FactionStance = newData.FactionStance,
                    ItemType = newData.ItemType,
                    WeaponType = newData.WeaponType,
                    WeaponSlot = newData.WeaponSlot,
                    ArmorType = newData.ArmorType,
                    ArmorSlot = newData.ArmorSlot,
                    Gender = newData.Gender,
                    QuestState = newData.QuestState,
                    DialogueNode = newData.DialogueNode,
                };
                newGroup.Requirements.Add(requirement);
            }

            original.Requirements.Add(newGroup);
        }

        // Extended copy
        original.hasCharges = copied.hasCharges;
        original.maxCharges = copied.maxCharges;
        original.chargeRecoveryTime = copied.chargeRecoveryTime;
        original.chargesShareCooldown = copied.chargesShareCooldown;
        original.canBeDodged = copied.canBeDodged;
        original.canBeParried = copied.canBeParried;
        original.canBeBlocked = copied.canBeBlocked;
        original.canBeReflected = copied.canBeReflected;
        original.canBeInterrupted = copied.canBeInterrupted;
        original.isInterruptArmor = copied.isInterruptArmor;
        original.interruptArmorAmount = copied.interruptArmorAmount;
        original.canCastWhileMoving = copied.canCastWhileMoving;
        original.movementSpeedWhileCasting = copied.movementSpeedWhileCasting;
        original.hasComboPointCost = copied.hasComboPointCost;
        original.comboPointsCost = copied.comboPointsCost;
        original.generatesComboPoints = copied.generatesComboPoints;
        original.comboPointsGenerated = copied.comboPointsGenerated;
        original.hasRuneCost = copied.hasRuneCost;
        original.runeCostID = copied.runeCostID;
        original.runeCostAmount = copied.runeCostAmount;
        original.hasPvPModifiers = copied.hasPvPModifiers;
        original.pvpDamageMultiplier = copied.pvpDamageMultiplier;
        original.pvpHealingMultiplier = copied.pvpHealingMultiplier;
        original.pvpDurationMultiplier = copied.pvpDurationMultiplier;
        original.hasScaling = copied.hasScaling;
        original.damageScalingPerLevel = copied.damageScalingPerLevel;
        original.healingScalingPerLevel = copied.healingScalingPerLevel;
        original.hasCriticalOverride = copied.hasCriticalOverride;
        original.criticalChanceOverride = copied.criticalChanceOverride;
        original.criticalDamageOverride = copied.criticalDamageOverride;
        original.hasSecondaryEffect = copied.hasSecondaryEffect;
        original.secondaryEffectID = copied.secondaryEffectID;
        original.secondaryEffectChance = copied.secondaryEffectChance;
        original.hasChainEffect = copied.hasChainEffect;
        original.chainMaxJumps = copied.chainMaxJumps;
        original.chainJumpDistance = copied.chainJumpDistance;
        original.chainDamageReductionPerJump = copied.chainDamageReductionPerJump;
        original.hasGroundEffect = copied.hasGroundEffect;
        original.groundEffectPrefab = copied.groundEffectPrefab;
        original.groundEffectDuration = copied.groundEffectDuration;
        original.groundEffectTickInterval = copied.groundEffectTickInterval;
        original.hasResourceOverTime = copied.hasResourceOverTime;
        original.resourceTickAmount = copied.resourceTickAmount;
        original.resourceTickInterval = copied.resourceTickInterval;
        original.resourceTickCount = copied.resourceTickCount;
        original.requiresLineOfSight = copied.requiresLineOfSight;
        original.ignoresArmor = copied.ignoresArmor;
        original.armorPenetrationPercent = copied.armorPenetrationPercent;
        original.hasKnockback = copied.hasKnockback;
        original.knockbackDistance = copied.knockbackDistance;
        original.knockbackDuration = copied.knockbackDuration;
        original.hasPull = copied.hasPull;
        original.pullDistance = copied.pullDistance;
        original.pullSpeed = copied.pullSpeed;
        original.hasLifeSteal = copied.hasLifeSteal;
        original.lifeStealPercent = copied.lifeStealPercent;
        original.hasShield = copied.hasShield;
        original.shieldAmount = copied.shieldAmount;
        original.shieldDuration = copied.shieldDuration;
        original.hasThreatModifier = copied.hasThreatModifier;
        original.threatMultiplier = copied.threatMultiplier;
        original.isTaunt = copied.isTaunt;
        original.hasDispel = copied.hasDispel;
        original.dispelCount = copied.dispelCount;
        original.dispelOffensive = copied.dispelOffensive;
        original.dispelDefensive = copied.dispelDefensive;
        original.hasStealBuff = copied.hasStealBuff;
        original.stealBuffCount = copied.stealBuffCount;
        original.isChanneledWhileMoving = copied.isChanneledWhileMoving;
        original.cancelOnDamage = copied.cancelOnDamage;
        original.damageThresholdToCancel = copied.damageThresholdToCancel;
        original.hasVisualScaling = copied.hasVisualScaling;
        original.visualScalePerRank = copied.visualScalePerRank;
        original.hasSoundScaling = copied.hasSoundScaling;
        original.soundVolumePerRank = copied.soundVolumePerRank;
        original.isAOECap = copied.isAOECap;
        original.aoeCapCount = copied.aoeCapCount;
        original.aoeCapDamageReduction = copied.aoeCapDamageReduction;
        original.hasCooldownReductionOnHit = copied.hasCooldownReductionOnHit;
        original.cooldownReductionPerHit = copied.cooldownReductionPerHit;
        original.hasCooldownResetChance = copied.hasCooldownResetChance;
        original.cooldownResetChance = copied.cooldownResetChance;
        original.hasProcEffect = copied.hasProcEffect;
        original.procAbilityID = copied.procAbilityID;
        original.procChance = copied.procChance;
        original.procCooldown = copied.procCooldown;
        original.canTargetDead = copied.canTargetDead;
        original.canTargetSelfOnly = copied.canTargetSelfOnly;
        original.requiresComboPoints = copied.requiresComboPoints;
        original.requiredComboPoints = copied.requiredComboPoints;
    }
}