using System.Collections;
using System.Collections.Generic;
using BLINK.RPGBuilder.Managers;
using UnityEngine;

public class RPGBuilderUISettings : RPGBuilderDatabaseEntry
{
    public List<string> UIStatsCategoriesList = new List<string>();
    
    public GameObject RPGBuilderEssentialsPrefab;
    public GameObject LoadingScreenManagerPrefab;

    public Color PhysicalDamageColor = Color.white,
        MagicalDamageColor = Color.white,
        NeutralDamageColor = Color.white,
        HealingColor = Color.white,
        RequirementMetColor = Color.white,
        RequirementNotMetColor = Color.white,
        PhysicalCriticalDamageColor = Color.white,
        MagicalCriticalDamageColor = Color.white,
        NeutralCriticalDamageColor = Color.white,
        HealingCriticalColor = Color.white,
        SelfDamageColor = Color.white,
        SelfHealColor = Color.white,
        SelfDamageCriticalColor = Color.white,
        SelfHealCriticalColor = Color.white,
        ThornDamageColor = Color.white,
        EXPColor = Color.white,
        LevelUpColor = Color.white,
        FactionColor = Color.white,
        ImmuneColor = Color.white;
    

    [Header("DAMAGE NUMBERS")]
    public bool enableDamageNumbers = true;
    public bool enableHealingNumbers = true;
    public bool enableCriticalNumbers = true;
    public float damageNumberDuration = 2f;
    public float damageNumberSpeed = 1f;
    public bool stackDamageNumbers = false;

    [Header("NAMEPLATES")]
    public bool enableNameplates = true;
    public bool enableHealthBars = true;
    public bool enableManaBars = false;
    public float nameplateDistance = 30f;
    public bool enableNameplateScaling = true;

    [Header("MINIMAP")]
    public bool enableMinimapRotation = true;
    public bool enableMinimapZoom = true;
    public float minimapZoomMin = 0.5f;
    public float minimapZoomMax = 2f;
    public bool showPOIOnMinimap = true;
    public bool showQuestsOnMinimap = true;
    public bool showPartyOnMinimap = true;

    [Header("TOOLTIPS")]
    public bool enableItemComparison = true;
    public bool enableAdvancedTooltips = false;
    public bool showItemLevelInTooltip = true;
    public bool showDurabilityInTooltip = true;
    public float tooltipDelay = 0.3f;

    [Header("HUD")]
    public bool enableHUDCustomization = true;
    public bool enableActionBarCustomization = true;
    public bool enableChatBubbles = true;
    public bool enableFloatingCombatText = true;

    [Header("ACCESSIBILITY")]
    public bool enableHighContrastUI = false;
    public bool enableLargeText = false;
    public bool enableScreenReaderSupport = false;
    public float uiScale = 1f;


    public void UpdateEntryData(RPGBuilderUISettings newEntryData)
    {
        UIStatsCategoriesList = newEntryData.UIStatsCategoriesList;
        RPGBuilderEssentialsPrefab = newEntryData.RPGBuilderEssentialsPrefab;
        LoadingScreenManagerPrefab = newEntryData.LoadingScreenManagerPrefab;

        PhysicalDamageColor = newEntryData.PhysicalDamageColor;
        MagicalDamageColor = newEntryData.MagicalDamageColor;
        NeutralDamageColor = newEntryData.NeutralDamageColor;
        HealingColor = newEntryData.HealingColor;
        RequirementMetColor = newEntryData.RequirementMetColor;
        RequirementNotMetColor = newEntryData.RequirementNotMetColor;
        PhysicalCriticalDamageColor = newEntryData.PhysicalCriticalDamageColor;
        MagicalCriticalDamageColor = newEntryData.MagicalCriticalDamageColor;
        NeutralCriticalDamageColor = newEntryData.NeutralCriticalDamageColor;
        HealingCriticalColor = newEntryData.HealingCriticalColor;
        SelfDamageColor = newEntryData.SelfDamageColor;
        SelfHealColor = newEntryData.SelfHealColor;
        SelfDamageCriticalColor = newEntryData.SelfDamageCriticalColor;
        SelfHealCriticalColor = newEntryData.SelfHealCriticalColor;
        ThornDamageColor = newEntryData.ThornDamageColor;
        EXPColor = newEntryData.EXPColor;
        LevelUpColor = newEntryData.LevelUpColor;
        FactionColor = newEntryData.FactionColor;
        ImmuneColor = newEntryData.ImmuneColor;
        enableDamageNumbers = newEntryData.enableDamageNumbers;
        enableHealingNumbers = newEntryData.enableHealingNumbers;
        enableCriticalNumbers = newEntryData.enableCriticalNumbers;
        damageNumberDuration = newEntryData.damageNumberDuration;
        damageNumberSpeed = newEntryData.damageNumberSpeed;
        stackDamageNumbers = newEntryData.stackDamageNumbers;
        enableNameplates = newEntryData.enableNameplates;
        enableHealthBars = newEntryData.enableHealthBars;
        enableManaBars = newEntryData.enableManaBars;
        nameplateDistance = newEntryData.nameplateDistance;
        enableNameplateScaling = newEntryData.enableNameplateScaling;
        enableMinimapRotation = newEntryData.enableMinimapRotation;
        enableMinimapZoom = newEntryData.enableMinimapZoom;
        minimapZoomMin = newEntryData.minimapZoomMin;
        minimapZoomMax = newEntryData.minimapZoomMax;
        showPOIOnMinimap = newEntryData.showPOIOnMinimap;
        showQuestsOnMinimap = newEntryData.showQuestsOnMinimap;
        showPartyOnMinimap = newEntryData.showPartyOnMinimap;
        enableItemComparison = newEntryData.enableItemComparison;
        enableAdvancedTooltips = newEntryData.enableAdvancedTooltips;
        showItemLevelInTooltip = newEntryData.showItemLevelInTooltip;
        showDurabilityInTooltip = newEntryData.showDurabilityInTooltip;
        tooltipDelay = newEntryData.tooltipDelay;
        enableHUDCustomization = newEntryData.enableHUDCustomization;
        enableActionBarCustomization = newEntryData.enableActionBarCustomization;
        enableChatBubbles = newEntryData.enableChatBubbles;
        enableFloatingCombatText = newEntryData.enableFloatingCombatText;
        enableHighContrastUI = newEntryData.enableHighContrastUI;
        enableLargeText = newEntryData.enableLargeText;
        enableScreenReaderSupport = newEntryData.enableScreenReaderSupport;
        uiScale = newEntryData.uiScale;
    }
}
