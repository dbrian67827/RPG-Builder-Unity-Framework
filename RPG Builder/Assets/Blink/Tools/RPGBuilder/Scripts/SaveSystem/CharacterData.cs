using System.Collections.Generic;
using BLINK.RPGBuilder.Combat;
using UnityEngine;

namespace BLINK.RPGBuilder.Characters
{
    [System.Serializable]
    public class CharacterData
    {
        [Header("GENERAL")] public bool IsCreated;
        public int RaceID = -1;
        public int ClassID = -1;
        public string CharacterName;
        public string Gender;

        [Header("LEVEL")] public int Level;
        public int CurrentExperience, ExperienceNeeded;
        public int levelTemplateID = -1;

        [Header("ABILITIES")]
        public List<CharacterEntries.AbilityEntry> Abilities = new List<CharacterEntries.AbilityEntry>();

        [Header("RECIPES")]
        public List<CharacterEntries.RecipeEntry> Recipes = new List<CharacterEntries.RecipeEntry>();

        [Header("RESOURCE NODES")]
        public List<CharacterEntries.ResourceNodeEntry> Resources = new List<CharacterEntries.ResourceNodeEntry>();

        [Header("BONUSES")] public List<CharacterEntries.BonusEntry> Bonuses = new List<CharacterEntries.BonusEntry>();

        [Header("TALENT TREES")]
        public List<CharacterEntries.TalentTreeEntry> TalentTrees = new List<CharacterEntries.TalentTreeEntry>();

        [Header("SKILLS")] public List<CharacterEntries.SkillEntry> Skills = new List<CharacterEntries.SkillEntry>();

        [Header("WEAPON TEMPLATES")]
        public List<CharacterEntries.WeaponTemplateEntry> WeaponTemplates =
            new List<CharacterEntries.WeaponTemplateEntry>();

        [Header("FACTIONS")]
        public List<CharacterEntries.FactionEntry> Factions = new List<CharacterEntries.FactionEntry>();

        [Header("INVENTORY")] public CharacterEntries.InventoryData Inventory = new CharacterEntries.InventoryData();

        [Header("CURRENCIES")]
        public List<CharacterEntries.CurrencyEntry> Currencies = new List<CharacterEntries.CurrencyEntry>();

        [Header("EQUIPPED ITEMS")] public List<CharacterEntries.ArmorEquippedEntry>
            ArmorPiecesEquipped = new List<CharacterEntries.ArmorEquippedEntry>();

        public List<CharacterEntries.WeaponEquippedEntry> WeaponsEquipped =
            new List<CharacterEntries.WeaponEquippedEntry>();

        [Header("RANDOMIZED ITEMS")] public int nextAvailableItemID = 0;
        public List<CharacterEntries.ItemEntry> ItemEntries = new List<CharacterEntries.ItemEntry>();
        public int nextAvailableRandomItemID = 0;
        public List<CharacterEntries.RandomizedItem> RandomizedItems = new List<CharacterEntries.RandomizedItem>();

        [Header("STAT ALLOCATION")] public int MainMenuStatAllocationPoints = 0;
        public int MainMenuStatAllocationMaxPoints = 0;
        public List<CharacterEntries.AllocatedStatData> AllocatedStats = new List<CharacterEntries.AllocatedStatData>();

        [Header("POINTS")]
        public List<CharacterEntries.TreePointEntry> Points = new List<CharacterEntries.TreePointEntry>();

        [Header("ACTION ABILITIES")]
        public List<CharacterEntries.ActionAbilityEntry> ActionAbilities =
            new List<CharacterEntries.ActionAbilityEntry>();

        [Header("QUESTS")] public List<CharacterEntries.QuestEntry> Quests = new List<CharacterEntries.QuestEntry>();

        [Header("DIALOGUES")]
        public List<CharacterEntries.DialogueEntry> Dialogues = new List<CharacterEntries.DialogueEntry>();

        [Header("KEYS")]
        public List<CharacterEntries.ActionKeyEntry> ActionKeys = new List<CharacterEntries.ActionKeyEntry>();

        [Header("ACTION BARS")]
        public List<CharacterEntries.ActionBarSlotEntry> ActionBarSlots =
            new List<CharacterEntries.ActionBarSlotEntry>();

        public List<CharacterEntries.ActionBarSlotEntry> ShapeshiftingActionBarSlots =
            new List<CharacterEntries.ActionBarSlotEntry>();

        public List<CharacterEntries.ActionBarSlotEntry> StealthedActionBarSlots =
            new List<CharacterEntries.ActionBarSlotEntry>();

        [Header("GAME MODIFIERS")]
        public List<CharacterEntries.GameModifierEntry> GameModifiers = new List<CharacterEntries.GameModifierEntry>();

        public int MenuGameModifierPoints;
        public int WorldGameModifierPoints;

        [Header("ACHIEVEMENTS")]
        public List<CharacterEntries.NPCKilledEntry> KilledNPCs = new List<CharacterEntries.NPCKilledEntry>();

        public List<CharacterEntries.SceneEnteredEntry> EnteredScenes = new List<CharacterEntries.SceneEnteredEntry>();

        public List<CharacterEntries.RegionEnteredEntry> EnteredRegions =
            new List<CharacterEntries.RegionEnteredEntry>();

        public List<CharacterEntries.AbilityLearnedEntry> LearnedAbilities =
            new List<CharacterEntries.AbilityLearnedEntry>();

        public List<CharacterEntries.BonusLearnedEntry> LearnedBonuses = new List<CharacterEntries.BonusLearnedEntry>();

        public List<CharacterEntries.RecipeLearnedEntry> LearnedRecipes =
            new List<CharacterEntries.RecipeLearnedEntry>();

        public List<CharacterEntries.ResourceNodeLearnedEntry> LearnedResourceNodes =
            new List<CharacterEntries.ResourceNodeLearnedEntry>();

        public List<CharacterEntries.ItemGainedEntry> GainedItems = new List<CharacterEntries.ItemGainedEntry>();

        [Header("WORLD PERSISTENCE")] public int GameSceneEntryIndex = -1;
        public List<CharacterEntries.GameSceneEntry> GameScenes = new List<CharacterEntries.GameSceneEntry>();
        public List<CharacterEntries.StateEntry> States = new List<CharacterEntries.StateEntry>();
        public List<CombatData.VitalityStatEntry> VitalityStats = new List<CombatData.VitalityStatEntry>();

        public CharacterEntries.TimeData Time = new CharacterEntries.TimeData();

        public Dictionary<string, string> CustomStringData = new Dictionary<string, string>();
        public List<string> CustomStringDataKeys = new List<string>();
        public List<string> CustomStringDataValues = new List<string>();

        public Dictionary<string, int> CustomIntData = new Dictionary<string, int>();
        public List<string> CustomIntDataKeys = new List<string>();
        public List<int> CustomIntDataValues = new List<int>();

        // ========== NEW FULL RPG SYSTEMS ==========
        [Header("TITLES")]
        public List<int> UnlockedTitles = new List<int>();
        public int ActiveTitleID = -1;

        [Header("ACHIEVEMENTS")]
        public List<CharacterEntries.AchievementEntry> Achievements = new List<CharacterEntries.AchievementEntry>();
        public int AchievementPoints = 0;

        [Header("MOUNTS")]
        public List<int> UnlockedMounts = new List<int>();
        public int ActiveMountID = -1;
        public int FavoriteMountID = -1;

        [Header("PETS")]
        public List<int> UnlockedPets = new List<int>();
        public int ActivePetID = -1;
        public int FavoritePetID = -1;
        public List<int> ActivePetAbilities = new List<int>();

        [Header("LORE & CODEX")]
        public List<int> UnlockedLore = new List<int>();
        public List<int> UnlockedBestiary = new List<int>();
        public List<CharacterEntries.BestiaryKillEntry> BestiaryKills = new List<CharacterEntries.BestiaryKillEntry>();

        [Header("TRANSMOG")]
        public List<int> UnlockedTransmog = new List<int>();
        public List<CharacterEntries.TransmogEntry> EquippedTransmog = new List<CharacterEntries.TransmogEntry>();

        [Header("BANK")]
        public List<CharacterEntries.ItemEntry> BankItems = new List<CharacterEntries.ItemEntry>();
        public int BankSlotsUnlocked = 50;
        public List<CharacterEntries.CurrencyEntry> BankCurrencies = new List<CharacterEntries.CurrencyEntry>();

        [Header("DUNGEONS & LOCKOUTS")]
        public List<CharacterEntries.DungeonLockoutEntry> DungeonLockouts = new List<CharacterEntries.DungeonLockoutEntry>();
        public List<int> CompletedDungeons = new List<int>();

        [Header("REPUTATION")]
        public List<CharacterEntries.FactionReputationEntry> FactionReputation = new List<CharacterEntries.FactionReputationEntry>();

        [Header("WORLD EVENTS")]
        public List<CharacterEntries.WorldEventCompletionEntry> WorldEventCompletions = new List<CharacterEntries.WorldEventCompletionEntry>();

        [Header("DAILY / WEEKLY")]
        public List<CharacterEntries.DailyQuestEntry> DailyQuests = new List<CharacterEntries.DailyQuestEntry>();
        public long LastDailyResetTicks;
        public long LastWeeklyResetTicks;

        [Header("MAIL")]
        public List<CharacterEntries.MailEntry> Mails = new List<CharacterEntries.MailEntry>();

        [Header("PARAGON")]
        public List<CharacterEntries.ParagonEntry> ParagonData = new List<CharacterEntries.ParagonEntry>();

        [Header("RUNES & GLYPHS")]
        public List<CharacterEntries.RuneEntry> Runes = new List<CharacterEntries.RuneEntry>();
        public List<CharacterEntries.GlyphEntry> Glyphs = new List<CharacterEntries.GlyphEntry>();

        [Header("ITEM DURABILITY")]
        public List<CharacterEntries.ItemDurabilityEntry> ItemDurabilities = new List<CharacterEntries.ItemDurabilityEntry>();

        [Header("CRAFTING")]
        public int CraftingLevel = 1;
        public int CraftingExperience = 0;
        public List<int> MasteredRecipes = new List<int>();
        public int FailedCrafts = 0;
        public int SuccessfulCrafts = 0;

        [Header("GUILD")]
        public string GuildName = "";
        public int GuildRank = 0;
        public long GuildJoinTicks;

        [Header("PARTY")]
        public bool IsInParty = false;
        public string PartyID = "";

        [Header("PVP")]
        public int HonorPoints = 0;
        public int ArenaPoints = 0;
        public int PvPKills = 0;
        public int PvPDeaths = 0;
        public int BattlegroundWins = 0;

        [Header("HOUSING")]
        public int HouseID = -1;
        public List<int> UnlockedHousingItems = new List<int>();
        public Vector3 HousePosition;

        [Header("MINI-GAMES")]
        public Dictionary<string, int> MiniGameHighScores = new Dictionary<string, int>();
        public List<string> MiniGameHighScoreKeys = new List<string>();
        public List<int> MiniGameHighScoreValues = new List<int>();

        [Header("ACCOUNT WIDE")]
        public bool IsAccountWideData = false;
        public List<int> AccountUnlockedMounts = new List<int>();
        public List<int> AccountUnlockedPets = new List<int>();
        public List<int> AccountUnlockedTitles = new List<int>();
        public List<int> AccountUnlockedTransmog = new List<int>();
        public int AccountAchievementPoints = 0;
        public long AccountCreationTicks;
    }
}
