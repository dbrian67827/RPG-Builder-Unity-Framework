using System.Collections;
using System.Collections.Generic;
using BLINK.RPGBuilder.AI;
using BLINK.RPGBuilder.Templates;
using UnityEngine;

public class RPGBuilderCombatSettings : RPGBuilderDatabaseEntry
{
    public List<string> FactionStancesList = new List<string>();
    public List<string> AbilityCooldownTagList = new List<string>();
    public List<string> EffectTagList = new List<string>();
    
    public float CriticalHitBonus = 2;
    public float GlobalCooldownDuration = 0.75f;
    
    public float ResetCombatDuration = 15;

    public bool AutomaticCombatStates = true;
    
    [StatID] public int HealthStatID = -1;

    public AIBehaviorTemplate DefaultAIBehaviorTemplate;
    public GameObject DefaultAILogicTemplate;

    public LayerMask ProjectileRaycastLayers;
    public LayerMask ProjectileDestroyLayers;
    public LayerMask InterruptLeapLayers;

    public float NPCSpawnerDistanceCheckInterval = 5;

    // ==================== EXTENDED FULL RPG COMBAT SETTINGS ====================
    [Header("CRITICAL & COMBAT")]
    public float CriticalHitChanceCap = 100f;
    public float CriticalDamageCap = 500f;
    public bool UseDiminishingReturns = true;
    public float DiminishingReturnFactor = 0.5f;
    
    [Header("DODGE / PARRY / BLOCK")]
    public bool EnableDodge = true;
    public bool EnableParry = true;
    public bool EnableBlock = true;
    public float BaseDodgeChance = 5f;
    public float BaseParryChance = 5f;
    public float BaseBlockChance = 5f;
    public float BlockDamageReduction = 0.3f;
    public float DodgeChanceCap = 75f;
    public float ParryChanceCap = 75f;
    public float BlockChanceCap = 75f;

    [Header("RESISTANCES")]
    public bool EnableResistances = true;
    public float ResistanceCap = 75f;
    public float ResistancePenetrationCap = 100f;

    [Header("THREAT & AGGRO")]
    public bool EnableThreatSystem = true;
    public float ThreatDecayRate = 1f;
    public float ThreatDecayDelay = 5f;
    public float TauntThreatMultiplier = 2f;
    public float TankThreatMultiplier = 1.5f;

    [Header("CROWD CONTROL")]
    public bool EnableDiminishingReturnsCC = true;
    public float CCDRDuration = 15f;
    public float CCDRFactor = 0.5f;
    public int CCDRMaxStacks = 3;
    public float StunDurationCap = 8f;
    public float FearDurationCap = 8f;
    public float RootDurationCap = 8f;

    [Header("SHIELDS & ABSORBS")]
    public bool EnableShields = true;
    public float MaxShieldPercentOfHealth = 200f;
    public bool ShieldsStack = false;
    public bool ShieldAbsorbsAllDamage = true;

    [Header("DAMAGE OVER TIME")]
    public bool EnableDoTStacking = true;
    public int MaxDoTStacks = 10;
    public bool DoTCrits = true;
    public bool DoTCanBeDodged = false;

    [Header("HEALING")]
    public bool EnableHealingReductionInPvP = true;
    public float HealingReductionInPvP = 0.3f;
    public bool OverhealingCreatesShield = false;
    public float OverhealShieldPercent = 0.5f;

    [Header("PVP")]
    public bool EnablePvPModifiers = true;
    public float PvPDamageReduction = 0.3f;
    public float PvPHealingReduction = 0.3f;
    public float PvPDurationReduction = 0.3f;
    public bool EnablePvPFlagging = true;
    public float PvPFlagDuration = 300f;

    [Header("COMBO POINTS")]
    public bool EnableComboPoints = true;
    public int MaxComboPoints = 5;
    public float ComboPointDecayTime = 30f;
    public bool ComboPointsResetOnCombatEnd = false;

    [Header("PROJECTILES")]
    public bool EnableProjectileHoming = false;
    public float ProjectileHomingStrength = 5f;
    public bool EnableProjectilePenetration = false;
    public int MaxProjectilePenetrations = 3;
    public bool ProjectilesCanCrit = true;

    [Header("AOE")]
    public bool EnableAOECap = true;
    public int DefaultAOECap = 5;
    public float AOECapDamageReduction = 0.5f;
    public bool EnableAOELoot = true;

    [Header("INTERRUPTS")]
    public bool EnableInterrupts = true;
    public float InterruptLockoutDuration = 4f;
    public bool InterruptGrantsImmunity = false;
    public float InterruptImmunityDuration = 2f;

    [Header("STEALTH")]
    public bool EnableStealthDetection = true;
    public float BaseStealthDetectionRange = 5f;
    public float StealthDetectionPerLevel = 0.5f;
    public bool StealthBreaksOnDamage = true;
    public float StealthBreakDamageThreshold = 0f;

    [Header("MOUNTS & PETS")]
    public bool DismountOnCombat = true;
    public bool DismountOnDamage = false;
    public float DismountDamageThreshold = 0.3f;
    public bool PetAssistOwner = true;
    public float PetRespawnTime = 10f;

    [Header("DEATH & RESPAWN")]
    public bool EnableDurabilityLossOnDeath = true;
    public float DurabilityLossPercentOnDeath = 10f;
    public bool EnableExperienceLossOnDeath = false;
    public float ExperienceLossPercentOnDeath = 5f;
    public float RespawnTime = 10f;
    public bool AllowRespawnInDungeon = true;
    
    public void UpdateEntryData(RPGBuilderCombatSettings newEntryData)
    {
        CriticalHitBonus = newEntryData.CriticalHitBonus;
        HealthStatID = newEntryData.HealthStatID;
        ResetCombatDuration = newEntryData.ResetCombatDuration;
        AutomaticCombatStates = newEntryData.AutomaticCombatStates;
        FactionStancesList = newEntryData.FactionStancesList;
        GlobalCooldownDuration = newEntryData.GlobalCooldownDuration;
        AbilityCooldownTagList = newEntryData.AbilityCooldownTagList;
        EffectTagList = newEntryData.EffectTagList;
        DefaultAIBehaviorTemplate = newEntryData.DefaultAIBehaviorTemplate;
        DefaultAILogicTemplate = newEntryData.DefaultAILogicTemplate;
        ProjectileRaycastLayers = newEntryData.ProjectileRaycastLayers;
        ProjectileDestroyLayers = newEntryData.ProjectileDestroyLayers;
        InterruptLeapLayers = newEntryData.InterruptLeapLayers;
        NPCSpawnerDistanceCheckInterval = newEntryData.NPCSpawnerDistanceCheckInterval;

        // Extended
        CriticalHitChanceCap = newEntryData.CriticalHitChanceCap;
        CriticalDamageCap = newEntryData.CriticalDamageCap;
        UseDiminishingReturns = newEntryData.UseDiminishingReturns;
        DiminishingReturnFactor = newEntryData.DiminishingReturnFactor;
        EnableDodge = newEntryData.EnableDodge;
        EnableParry = newEntryData.EnableParry;
        EnableBlock = newEntryData.EnableBlock;
        BaseDodgeChance = newEntryData.BaseDodgeChance;
        BaseParryChance = newEntryData.BaseParryChance;
        BaseBlockChance = newEntryData.BaseBlockChance;
        BlockDamageReduction = newEntryData.BlockDamageReduction;
        DodgeChanceCap = newEntryData.DodgeChanceCap;
        ParryChanceCap = newEntryData.ParryChanceCap;
        BlockChanceCap = newEntryData.BlockChanceCap;
        EnableResistances = newEntryData.EnableResistances;
        ResistanceCap = newEntryData.ResistanceCap;
        ResistancePenetrationCap = newEntryData.ResistancePenetrationCap;
        EnableThreatSystem = newEntryData.EnableThreatSystem;
        ThreatDecayRate = newEntryData.ThreatDecayRate;
        ThreatDecayDelay = newEntryData.ThreatDecayDelay;
        TauntThreatMultiplier = newEntryData.TauntThreatMultiplier;
        TankThreatMultiplier = newEntryData.TankThreatMultiplier;
        EnableDiminishingReturnsCC = newEntryData.EnableDiminishingReturnsCC;
        CCDRDuration = newEntryData.CCDRDuration;
        CCDRFactor = newEntryData.CCDRFactor;
        CCDRMaxStacks = newEntryData.CCDRMaxStacks;
        StunDurationCap = newEntryData.StunDurationCap;
        FearDurationCap = newEntryData.FearDurationCap;
        RootDurationCap = newEntryData.RootDurationCap;
        EnableShields = newEntryData.EnableShields;
        MaxShieldPercentOfHealth = newEntryData.MaxShieldPercentOfHealth;
        ShieldsStack = newEntryData.ShieldsStack;
        ShieldAbsorbsAllDamage = newEntryData.ShieldAbsorbsAllDamage;
        EnableDoTStacking = newEntryData.EnableDoTStacking;
        MaxDoTStacks = newEntryData.MaxDoTStacks;
        DoTCrits = newEntryData.DoTCrits;
        DoTCanBeDodged = newEntryData.DoTCanBeDodged;
        EnableHealingReductionInPvP = newEntryData.EnableHealingReductionInPvP;
        HealingReductionInPvP = newEntryData.HealingReductionInPvP;
        OverhealingCreatesShield = newEntryData.OverhealingCreatesShield;
        OverhealShieldPercent = newEntryData.OverhealShieldPercent;
        EnablePvPModifiers = newEntryData.EnablePvPModifiers;
        PvPDamageReduction = newEntryData.PvPDamageReduction;
        PvPHealingReduction = newEntryData.PvPHealingReduction;
        PvPDurationReduction = newEntryData.PvPDurationReduction;
        EnablePvPFlagging = newEntryData.EnablePvPFlagging;
        PvPFlagDuration = newEntryData.PvPFlagDuration;
        EnableComboPoints = newEntryData.EnableComboPoints;
        MaxComboPoints = newEntryData.MaxComboPoints;
        ComboPointDecayTime = newEntryData.ComboPointDecayTime;
        ComboPointsResetOnCombatEnd = newEntryData.ComboPointsResetOnCombatEnd;
        EnableProjectileHoming = newEntryData.EnableProjectileHoming;
        ProjectileHomingStrength = newEntryData.ProjectileHomingStrength;
        EnableProjectilePenetration = newEntryData.EnableProjectilePenetration;
        MaxProjectilePenetrations = newEntryData.MaxProjectilePenetrations;
        ProjectilesCanCrit = newEntryData.ProjectilesCanCrit;
        EnableAOECap = newEntryData.EnableAOECap;
        DefaultAOECap = newEntryData.DefaultAOECap;
        AOECapDamageReduction = newEntryData.AOECapDamageReduction;
        EnableAOELoot = newEntryData.EnableAOELoot;
        EnableInterrupts = newEntryData.EnableInterrupts;
        InterruptLockoutDuration = newEntryData.InterruptLockoutDuration;
        InterruptGrantsImmunity = newEntryData.InterruptGrantsImmunity;
        InterruptImmunityDuration = newEntryData.InterruptImmunityDuration;
        EnableStealthDetection = newEntryData.EnableStealthDetection;
        BaseStealthDetectionRange = newEntryData.BaseStealthDetectionRange;
        StealthDetectionPerLevel = newEntryData.StealthDetectionPerLevel;
        StealthBreaksOnDamage = newEntryData.StealthBreaksOnDamage;
        StealthBreakDamageThreshold = newEntryData.StealthBreakDamageThreshold;
        DismountOnCombat = newEntryData.DismountOnCombat;
        DismountOnDamage = newEntryData.DismountOnDamage;
        DismountDamageThreshold = newEntryData.DismountDamageThreshold;
        PetAssistOwner = newEntryData.PetAssistOwner;
        PetRespawnTime = newEntryData.PetRespawnTime;
        EnableDurabilityLossOnDeath = newEntryData.EnableDurabilityLossOnDeath;
        DurabilityLossPercentOnDeath = newEntryData.DurabilityLossPercentOnDeath;
        EnableExperienceLossOnDeath = newEntryData.EnableExperienceLossOnDeath;
        ExperienceLossPercentOnDeath = newEntryData.ExperienceLossPercentOnDeath;
        RespawnTime = newEntryData.RespawnTime;
        AllowRespawnInDungeon = newEntryData.AllowRespawnInDungeon;
    }
}
