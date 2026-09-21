# Full RPG Features - Unity 6000.7.0b1

## New Database Entries (17)
- Title, Achievement, Mount, Pet, WorldEvent, Dungeon, Lore, Bestiary, MailTemplate, CraftingQuality, ReputationReward, Transmog, Weather, Paragon, Rune, Glyph, Shop

## New Managers (30+)
- AchievementManager, TitleManager, MountManager, PetManager+PetAI, WorldEventManager, DungeonManager, LoreManager, BestiaryManager, MailManager, TransmogManager, BankManager, ParagonManager, WeatherManager, ReputationManager, PartyManager, GuildManager, HousingManager, FishingManager, AuctionHouseManager, TradingManager, ThreatManager, DiminishingReturnsManager, ShieldManager, DamageNumbersManager, LocalizationManager (12 langs), DailyQuestManager, CraftingQualityManager, InventoryManagerExtended, EquipmentSetManager, AutoLootManager, RPGBuilderFullRPGIntegration

## Unity 6000.7.0b1 Compatibility
- Fixed EndNameEditAction obsolete (xNode) - now uses AssetCreationEndAction with EntityId for 6000.4+
- Fixed InstanceIDToObject obsolete - now uses EntityIdToObject
- Fixed FindObjectOfType obsolete - now uses FindFirstObjectByType
- Fixed FindObjectsOfType obsolete - now uses FindObjectsByType
- Fixed GetInstanceID obsolete - now uses GetEntityId
- Updated packages: AI Nav 2.0.8, InputSystem 1.14.2, URP 17.1.0, Timeline 1.8.8, VS 1.9.7, Localization 1.5.5, Netcode 2.4.0, Multiplayer Center 1.0.0, Cinemachine 3.1.3
- Added performance optimizer for SRP Batcher, LOD, Dynamic Resolution
- Added Input System Unity 6 enhancements

## Expanded Existing Entries
- Item: +50 fields (bind, durability, transmog, quality, runes, glyphs, etc.)
- Ability: +80 fields/rank (charges, combo points, rune cost, PvP mods, etc.)
- Quest: +30 fields (daily/weekly/escort/timed/chain)
- Npc: +60 fields (tether, schedule, social aggro, scaling, enrage, etc.)
- Stat, Class, Race, Recipe, Faction, ResourceNode all expanded

## Settings
- Combat: +60 fields (dodge/parry/block, threat, DR, shields, PvP, etc.)
- General: +50 fields (QoL, social, PvP, world, economy, progression, accessibility, performance, multiplayer)
- Economy: +30 fields (bank, AH, durability, quality)
- New: Mount, Pet, Achievement, Dungeon, WorldEvent settings

## Save System
- CharacterData: +40 fields
- CharacterEntries: +15 types

## Requirements & GameActions
- +40 requirement types, +50 action types
- Handlers for Title, Achievement, Mount, Pet, WorldEvent, Dungeon, Lore, Bestiary, Transmog, Weather, Paragon, Reputation, Guild, Party, ItemLevel, etc.

## Usage Examples
```csharp
// Titles
TitleManager.Instance.UnlockTitle(1);
TitleManager.Instance.SetActiveTitle(1);

// Achievements
AchievementManager.Instance.AddProgress(1, 1);

// Mounts
MountManager.Instance.UnlockMount(1);
MountManager.Instance.SummonMount(1);

// World Events
WorldEventManager.Instance.StartEvent(1);

// Dungeons
DungeonManager.Instance.EnterDungeon(1);

// Weather
WeatherManager.Instance.ChangeWeather(1);

// Full status
Debug.Log(RPGBuilderFullRPGIntegration.Instance.GetFullRPGStatus());
```
