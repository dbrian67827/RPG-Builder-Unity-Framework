using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBuilderCharacterSettings : RPGBuilderDatabaseEntry
{
    public List<string> StatFunctionsList = new List<string>();
    public List<string> nodeSocketNames = new List<string>();

    public bool NoClasses = true;
    public bool CanTargetPlayerOnClick = true;
    
    [PointID] public int StatAllocationPointID = -1;
    public bool MustSpendAllStatPointsToCreateCharacter;
    public bool CanRefundStatPointInGame;

    [StatID] public int SprintStatDrainID = -1;
    public int SprintStatDrainAmount;
    public float SprintStatDrainInterval;
    

    [Header("APPEARANCE")]
    public bool enableBarberShop = true;
    public bool enableTransmog = true;
    public bool enableDyeSystem = false;
    public bool enableAppearanceSave = true;
    public int maxSavedAppearances = 5;

    [Header("MOVEMENT")]
    public bool enableDoubleJump = false;
    public bool enableDash = true;
    public float dashCooldown = 5f;
    public float dashDistance = 5f;
    public bool enableGliding = false;
    public bool enableSwimming = true;

    [Header("INTERACTION")]
    public bool enableAutoTarget = true;
    public float interactionRange = 3f;
    public bool enableClickToMove = false;
    public bool enableAutoAttack = true;


    public void UpdateEntryData(RPGBuilderCharacterSettings newEntryData)
    {
        NoClasses = newEntryData.NoClasses;
        StatAllocationPointID = newEntryData.StatAllocationPointID;
        MustSpendAllStatPointsToCreateCharacter = newEntryData.MustSpendAllStatPointsToCreateCharacter;
        CanRefundStatPointInGame = newEntryData.CanRefundStatPointInGame;
        CanTargetPlayerOnClick = newEntryData.CanTargetPlayerOnClick;
        SprintStatDrainID = newEntryData.SprintStatDrainID;
        SprintStatDrainInterval = newEntryData.SprintStatDrainInterval;
        SprintStatDrainAmount = newEntryData.SprintStatDrainAmount;
        enableBarberShop = newEntryData.enableBarberShop;
        enableTransmog = newEntryData.enableTransmog;
        enableDyeSystem = newEntryData.enableDyeSystem;
        enableAppearanceSave = newEntryData.enableAppearanceSave;
        maxSavedAppearances = newEntryData.maxSavedAppearances;
        enableDoubleJump = newEntryData.enableDoubleJump;
        enableDash = newEntryData.enableDash;
        dashCooldown = newEntryData.dashCooldown;
        dashDistance = newEntryData.dashDistance;
        enableGliding = newEntryData.enableGliding;
        enableSwimming = newEntryData.enableSwimming;
        enableAutoTarget = newEntryData.enableAutoTarget;
        interactionRange = newEntryData.interactionRange;
        enableClickToMove = newEntryData.enableClickToMove;
        enableAutoAttack = newEntryData.enableAutoAttack;
    }
}
