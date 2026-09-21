using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

public class RPGTitle : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public Sprite icon;

    public enum TitleCategory
    {
        Achievement,
        Reputation,
        Quest,
        Dungeon,
        PvP,
        Crafting,
        Exploration,
        Event,
        Special
    }

    public TitleCategory category = TitleCategory.Achievement;
    public string titleText = "{playerName} the Brave";
    public string prefixText = "";
    public string suffixText = "the Brave";
    public bool isPrefix = false;
    public bool isSuffix = true;
    
    public Color titleColor = Color.white;
    public bool useColor = false;
    
    public int sortOrder = 0;
    public bool isHidden = false;
    public bool isAccountWide = false;

    [Serializable]
    public class TitleStatBonus
    {
        [StatID] public int statID = -1;
        public float amount;
        public bool isPercent;
    }
    [RPGDataList] public List<TitleStatBonus> statBonuses = new List<TitleStatBonus>();

    public List<RequirementsData.RequirementGroup> Requirements = new List<RequirementsData.RequirementGroup>();
    public bool UseRequirementsTemplate;
    public RequirementsTemplate RequirementsTemplate;

    [RPGDataList] public List<GameActionsData.GameAction> GameActions = new List<GameActionsData.GameAction>();
    public bool UseGameActionsTemplate;
    public GameActionsTemplate GameActionsTemplate;

    public string description;
    public int rarityWeight = 1;
    public bool showInChat = true;
    public bool showOnNameplate = true;

    public void UpdateEntryData(RPGTitle newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        category = newEntryData.category;
        titleText = newEntryData.titleText;
        prefixText = newEntryData.prefixText;
        suffixText = newEntryData.suffixText;
        isPrefix = newEntryData.isPrefix;
        isSuffix = newEntryData.isSuffix;
        titleColor = newEntryData.titleColor;
        useColor = newEntryData.useColor;
        sortOrder = newEntryData.sortOrder;
        isHidden = newEntryData.isHidden;
        isAccountWide = newEntryData.isAccountWide;
        statBonuses = newEntryData.statBonuses;
        Requirements = newEntryData.Requirements;
        UseRequirementsTemplate = newEntryData.UseRequirementsTemplate;
        RequirementsTemplate = newEntryData.RequirementsTemplate;
        GameActions = newEntryData.GameActions;
        UseGameActionsTemplate = newEntryData.UseGameActionsTemplate;
        GameActionsTemplate = newEntryData.GameActionsTemplate;
        description = newEntryData.description;
        rarityWeight = newEntryData.rarityWeight;
        showInChat = newEntryData.showInChat;
        showOnNameplate = newEntryData.showOnNameplate;
    }
}
