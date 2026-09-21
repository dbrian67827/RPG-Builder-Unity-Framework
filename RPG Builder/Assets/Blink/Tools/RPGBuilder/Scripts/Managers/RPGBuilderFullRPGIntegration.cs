
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Characters;

namespace BLINK.RPGBuilder.Managers
{
    public class RPGBuilderFullRPGIntegration : MonoBehaviour
    {
        public static RPGBuilderFullRPGIntegration Instance { get; private set; }
        public bool enableAllSystems = true;
        public bool autoInitialize = true;

        public AchievementManager achievementManager;
        public TitleManager titleManager;
        public MountManager mountManager;
        public PetManager petManager;
        public WorldEventManager worldEventManager;
        public DungeonManager dungeonManager;
        public LoreManager loreManager;
        public BestiaryManager bestiaryManager;
        public MailManager mailManager;
        public TransmogManager transmogManager;
        public BankManager bankManager;
        public ParagonManager paragonManager;
        public WeatherManager weatherManager;
        public ReputationManager reputationManager;
        public PartyManager partyManager;
        public GuildManager guildManager;
        public HousingManager housingManager;
        public FishingManager fishingManager;
        public AuctionHouseManager auctionHouseManager;
        public TradingManager tradingManager;
        public ThreatManager threatManager;
        public DiminishingReturnsManager drManager;
        public ShieldManager shieldManager;
        public DamageNumbersManager damageNumbersManager;
        public LocalizationManager localizationManager;
        public DailyQuestManager dailyQuestManager;
        public CraftingQualityManager craftingQualityManager;
        public InventoryManagerExtended inventoryExtended;
        public EquipmentSetManager equipmentSetManager;
        public AutoLootManager autoLootManager;
        public Utility.RPGBuilderPerformanceOptimizer performanceOptimizer;
        public Utility.RPGBuilderInputSystemUnity6 inputSystemUnity6;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (autoInitialize) InitializeAllSystems();
        }

        public void InitializeAllSystems()
        {
            EnsureManager(ref achievementManager, "AchievementManager");
            EnsureManager(ref titleManager, "TitleManager");
            EnsureManager(ref mountManager, "MountManager");
            EnsureManager(ref petManager, "PetManager");
            EnsureManager(ref worldEventManager, "WorldEventManager");
            EnsureManager(ref dungeonManager, "DungeonManager");
            EnsureManager(ref loreManager, "LoreManager");
            EnsureManager(ref bestiaryManager, "BestiaryManager");
            EnsureManager(ref mailManager, "MailManager");
            EnsureManager(ref transmogManager, "TransmogManager");
            EnsureManager(ref bankManager, "BankManager");
            EnsureManager(ref paragonManager, "ParagonManager");
            EnsureManager(ref weatherManager, "WeatherManager");
            EnsureManager(ref reputationManager, "ReputationManager");
            EnsureManager(ref partyManager, "PartyManager");
            EnsureManager(ref guildManager, "GuildManager");
            EnsureManager(ref housingManager, "HousingManager");
            EnsureManager(ref fishingManager, "FishingManager");
            EnsureManager(ref auctionHouseManager, "AuctionHouseManager");
            EnsureManager(ref tradingManager, "TradingManager");
            EnsureManager(ref threatManager, "ThreatManager");
            EnsureManager(ref drManager, "DiminishingReturnsManager");
            EnsureManager(ref shieldManager, "ShieldManager");
            EnsureManager(ref damageNumbersManager, "DamageNumbersManager");
            EnsureManager(ref localizationManager, "LocalizationManager");
            EnsureManager(ref dailyQuestManager, "DailyQuestManager");
            EnsureManager(ref craftingQualityManager, "CraftingQualityManager");
            EnsureManager(ref equipmentSetManager, "EquipmentSetManager");
            EnsureManager(ref autoLootManager, "AutoLootManager");
            EnsureManager(ref performanceOptimizer, "PerformanceOptimizer");
            EnsureManager(ref inputSystemUnity6, "InputSystemUnity6");
            Debug.Log("[RPG Builder] Full RPG Integration - All systems initialized for Unity 6000.7.0b1");
        }

        private void EnsureManager<T>(ref T manager, string name) where T : MonoBehaviour
        {
            if (manager != null) return;
            var existing = FindFirstObjectByType<T>();
            if (existing != null) { manager = existing; return; }
            var go = new GameObject(name);
            go.transform.SetParent(transform);
            manager = go.AddComponent<T>();
        }

        public int GetAverageItemLevel() => InventoryManagerExtended.GetAverageItemLevel();

        public string GetFullRPGStatus()
        {
            return $@"=== RPG Builder Full Blown RPG Status (Unity 6000.7.0b1) ===
Character: {Character.Instance?.CharacterName} Level {Character.Instance?.Level}
Item Level: {GetAverageItemLevel()}
Achievements: {AchievementManager.Instance?.GetCompletedAchievements().Count ?? 0}/{GameDatabase.Instance.GetAchievements().Count}
Titles: {TitleManager.Instance?.GetUnlockedTitles().Count ?? 0}/{GameDatabase.Instance.GetTitles().Count}
Mounts: {MountManager.Instance?.GetUnlockedMounts().Count ?? 0}/{GameDatabase.Instance.GetMounts().Count}
Pets: {PetManager.Instance?.GetUnlockedPets().Count ?? 0}/{GameDatabase.Instance.GetPets().Count}
Lore: {LoreManager.Instance?.GetUnlockedLore().Count ?? 0}/{GameDatabase.Instance.GetLore().Count}
Active World Events: {WorldEventManager.Instance?.GetActiveEvents().Count ?? 0}
In Dungeon: {DungeonManager.Instance?.IsInDungeon() ?? false}
In Party: {PartyManager.Instance?.IsInParty() ?? false}
In Guild: {GuildManager.Instance?.IsInGuild() ?? false}
Current Weather: {WeatherManager.Instance?.GetCurrentWeather()?.entryDisplayName ?? "None"}
";
        }
    }
}
