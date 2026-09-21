# Changelog - Full Blown RPG Upgrade for Unity 6000.7.0b1

## [2.0.0] - 2026-09-21 - Full RPG Framework

### Unity 6000.7.0b1 Upgrade
- Updated `ProjectVersion.txt` from 6000.0.50f1 to 6000.7.0b1
- Updated `manifest.json` packages:
  - ai.navigation 2.0.7 → 2.0.8
  - inputsystem 1.14.0 → 1.14.2
  - universal RP 17.0.4 → 17.1.0
  - timeline 1.8.7 → 1.8.8
  - visualscripting 1.9.6 → 1.9.7
  - Added localization 1.5.5
  - Added netcode.gameobjects 2.4.0
  - Added multiplayer.center 1.0.0
  - Added cinemachine 3.1.3
- Fixed obsolete APIs: FindObjectOfType → FindFirstObjectByType
- Created Unity 6 compatibility layer
- Added performance optimizer for SRP Batcher, LOD, Dynamic Resolution
- Added Input System Unity 6 enhancements

### New Database Entries (17)
- RPGTitle
- RPGAchievement
- RPGMount
- RPGPet
- RPGWorldEvent
- RPGDungeon
- RPGLore
- RPGBestiary
- RPGMailTemplate
- RPGCraftingQuality
- RPGReputationReward
- RPGTransmog
- RPGWeather
- RPGParagon
- RPGRune
- RPGGlyph
- RPGShop

### Expanded Existing Entries
- RPGItem: +50 fields (bind types, durability, transmog, quality, runes, glyphs, deconstruction, lore, auction, etc.)
- RPGAbility: +80 fields per rank (charges, dodge/parry/block, combo points, rune cost, PvP modifiers, scaling, chain, ground effect, threat, etc.) + global fields
- RPGQuest: +30 fields (types: daily/weekly/escort/timed/chain, fail conditions, etc.)
- RPGNpc: +60 fields (tether, schedule, social aggro, gossip, reputation, loot method, rare spawn, scaling, enrage, immunities, resistances, boss flags, patrol, nameplate, mount/pet/title/bestiary/lore)
- RPGStat: +20 fields (cap, DR, resistance, display, scaling, regen, overcap, bar, etc.)
- RPGClass: Specializations, base health/mana, mastery, starting items, mount/title
- RPGRace: Racial abilities, customization, allied race, starting faction/scene
- RPGCraftingRecipe: Quality system, success/failure, crafting time, outcomes, exp, skill up, cooldown, daily limit
- RPGFaction: Reputation levels, decay, bonuses, guild/paragon rep, weekly cap
- RPGResourceNode: Quality, yield scaling, rare drop, respawn scaling, depletion, skill check, bonuses

### New Managers (30+)
- AchievementManager
- TitleManager
- MountManager
- PetManager + PetAI
- WorldEventManager
- DungeonManager
- LoreManager
- BestiaryManager
- MailManager
- TransmogManager
- BankManager
- ParagonManager
- WeatherManager
- ReputationManager
- PartyManager
- GuildManager
- HousingManager
- FishingManager
- AuctionHouseManager
- TradingManager
- ThreatManager
- DiminishingReturnsManager
- ShieldManager
- DamageNumbersManager
- LocalizationManager
- DailyQuestManager
- CraftingQualityManager
- InventoryManagerExtended
- EquipmentSetManager
- AutoLootManager
- RPGBuilderUnity6Compatibility
- RPGBuilderPerformanceOptimizer
- RPGBuilderInputSystemUnity6

### Expanded Settings
- CombatSettings: +60 fields (dodge/parry/block, resistances, threat, CC DR, shields, DoT, healing, PvP, combo points, projectiles, AOE, interrupts, stealth, mounts/pets, death)
- GeneralSettings: +50 fields (QoL, social, PvP, systems, world, economy, progression, accessibility, performance, multiplayer)
- EconomySettings: +30 fields (bank, auction, durability, quality, shops, loot)
- CharacterSettings: +15 fields (appearance, movement, interaction)
- WorldSettings: +20 fields (map, weather, dungeons, events, housing, fishing)
- UISettings: +25 fields (damage numbers, nameplates, minimap, tooltips, HUD, accessibility)
- New Settings: MountSettings, PetSettings, AchievementSettings, DungeonSettings, WorldEventSettings

### Save System
- CharacterData: +40 fields (titles, achievements, mounts, pets, lore, bestiary, transmog, bank, dungeons, reputation, events, daily/weekly, mail, paragon, runes, glyphs, durability, crafting, guild, party, PvP, housing, mini-games, account-wide)
- CharacterEntries: +15 entry types

### Requirements & Game Actions
- Requirements: +40 new types + 16 new ID references
- GameActions: +50 new types + 15 new ID references
- Added handling in RequirementsManager and GameActionsManager for new types

### Editor
- 17 new editor modules for new database entries
- Database folders created for all new types
- Generic DrawView with full inspector support

### Utilities
- InventoryManager: Added overloads for AddItem, HasCurrency, RemoveItem, InitEquippedItems, IsItemEquipped
- BonusManager: Added Apply/Remove for mount/pet/title bonuses
- Compatibility layer for Unity 6

### Documentation
- FULL_RPG_FEATURES.md - Comprehensive feature list
- UNITY_6000_UPGRADE_GUIDE.md - Upgrade guide
- CHANGELOG_FULL_RPG.md - This file

### Statistics
- New Files: ~50
- Modified Files: ~20
- Lines Added: ~15,000+
- New Systems: 17 database entries, 30+ managers
- New Fields: 300+ across existing entries

### Breaking Changes
- None - backward compatible with existing database
- New folders empty - need to create ScriptableObjects
- New managers need to be added to RPGBuilderEssentials prefab
- Editor modules need ScriptableObject assets in EditorData/EditorEntryModules

### Migration
- Existing projects can upgrade by copying new files
- No database migration needed - new fields have defaults
- Unity 6000.7.0b1 required for new packages, but code has #if UNITY_6000_0_OR_NEWER guards for backward compatibility where possible

### Future Work
- UI panels for new systems (Achievements, Titles, Mounts, Pets, Lore, Bestiary, Mail, Transmog, Bank, Auction, Guild, Party, Housing, Fishing)
- Network integration for multiplayer systems
- Visual scripting nodes for new actions
- More editor polish for new modules
- Example scenes for new systems
- Automated tests
