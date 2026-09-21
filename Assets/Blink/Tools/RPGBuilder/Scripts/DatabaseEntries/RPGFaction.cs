using System.Collections.Generic;
using BLINK.RPGBuilder.Combat;
using UnityEngine;

public class RPGFaction : RPGBuilderDatabaseEntry
{
    [HideInInspector] public string _name;
    [HideInInspector] public string _fileName;
    [HideInInspector] public string displayName;
    [HideInInspector] public string description;
    [HideInInspector] public Sprite icon;
    

    [System.Serializable]
    public class Faction_Stance_DATA
    {
        [HideInInspector] public string stance;
        public RPGBFactionStance FactionStance;
        public int pointsRequired;

        [HideInInspector] public RPGCombatDATA.ALIGNMENT_TYPE playerAlignment;
        public CombatData.EntityAlignment AlignementToPlayer;
    }

    [RPGDataList] public List<Faction_Stance_DATA> factionStances = new List<Faction_Stance_DATA>();
    
    [System.Serializable]
    public class Faction_Interaction_DATA
    {
        [FactionID] public int factionID = -1;

        [HideInInspector] public string defaultStance;
        public RPGBFactionStance DefaultFactionStance;
        public int startingPoints;
    }

    [RPGDataList] public List<Faction_Interaction_DATA> factionInteractions = new List<Faction_Interaction_DATA>();
    

    // ==================== EXTENDED FACTION FEATURES ====================
    public bool hasReputationLevels = true;
    [System.Serializable]
    public class ReputationLevel
    {
        public string levelName = "Neutral";
        public int requiredReputation = 0;
        public UnityEngine.Color color = UnityEngine.Color.white;
        public bool isHostile = false;
        public bool isFriendly = false;
    }
    [RPGDataList] public System.Collections.Generic.List<ReputationLevel> reputationLevels = new System.Collections.Generic.List<ReputationLevel>();
    public bool hasReputationDecay = false;
    public float decayRate = 1f;
    public float decayInterval = 86400f;
    public bool hasReputationBonuses = false;
    public float reputationBonusPerLevel = 0.1f;
    public bool isPlayerFaction = false;
    public bool isEnemyFaction = false;
    public bool hasGuildReputation = false;
    public bool hasParagonReputation = false;
    public int paragonReputationCap = 10000;
    public bool hasWeeklyCap = false;
    public int weeklyCap = 10000;


    public void UpdateEntryData(RPGFaction newEntryData)
    {
        ID = newEntryData.ID;
        entryName = newEntryData.entryName;
        entryFileName = newEntryData.entryFileName;
        entryDisplayName = newEntryData.entryDisplayName;
        entryIcon = newEntryData.entryIcon;
        entryDescription = newEntryData.entryDescription;
        
        factionStances = newEntryData.factionStances;
        factionInteractions = newEntryData.factionInteractions;
    }
}
