# RPG Builder - Full Blown RPG Framework for Unity 6000.7.0b1

This branch (`arena/01a0c521-rpg-builder-unity-framework`) transforms RPG Builder into a complete AAA-grade RPG framework.

## Unity 6000.7.0b1 Upgrade

### Core Upgrade
- **Project Version**: Updated from `6000.0.50f1` to `6000.7.0b1`
- **Packages Updated**:
  - `com.unity.ai.navigation`: 2.0.7 → 2.0.8
  - `com.unity.inputsystem`: 1.14.0 → 1.14.2
  - `com.unity.render-pipelines.universal`: 17.0.4 → 17.1.0
  - `com.unity.timeline`: 1.8.7 → 1.8.8
  - `com.unity.visualscripting`: 1.9.6 → 1.9.7
  - Added `com.unity.localization` 1.5.5
  - Added `com.unity.netcode.gameobjects` 2.4.0
  - Added `com.unity.multiplayer.center` 1.0.0
  - Added `com.unity.cinemachine` 3.1.3

### Obsolete API Fixes
- Replaced `FindObjectOfType<T>()` → `FindFirstObjectByType<T>()`
- Replaced `FindObjectsOfType<T>()` → `FindObjectsByType<T>(FindObjectsSortMode.None)`
- Created `RPGBuilderUnity6Compatibility` utility for unified API access
- Added `RPGBuilderPerformanceOptimizer` for Unity 6000 SRP Batcher, LOD, Dynamic Resolution
- Added `RPGBuilderInputSystemUnity6` for Input System 1.14+ haptics and smoothing

### Performance Optimizations
- SRP Batcher enabled
- GPU Instancing support
- LOD Group optimizations
- Dynamic Resolution
- Optimized Input System settings

---

## New Database Entries (17 New Systems)

### 1. Titles System (`RPGTitle`)
- Prefix/Suffix titles
- Categories: Achievement, Reputation, Quest, Dungeon, PvP, Crafting, Exploration, Event, Special
- Color customization
- Stat bonuses per title
- Requirements & Game Actions
- Account-wide support
- Chat & Nameplate display

### 2. Achievements (`RPGAchievement`)
- 13 Categories: General, Quests, Exploration, Combat, Dungeons, Crafting, Gathering, Social, PvP, Collections, Events, FeatsOfStrength, Legacy
- 18 Objective Types: KillNPC, CompleteQuest, ReachLevel, GainItem, CraftItem, etc.
- 10 Reward Types: Item, Currency, Title, Mount, Pet, Ability, etc.
- Chains, hidden achievements, account-wide, progress bars
- Visual/Sound effects, guild/zone announcements
- Points system

### 3. Mounts (`RPGMount`)
- 5 Types: Ground, Flying, Aquatic, MultiTerrain, Special
- Speed, turn, acceleration, stamina
- Flying/Swimming/Jumping capabilities
- Combat dismount logic
- Summon cast time, interruptible
- Indoor/dungeon restrictions
- Stat bonuses while mounted
- Mount abilities
- Passenger support
- Account-wide, tradeable

### 4. Pets / Companions (`RPGPet`)
- 5 Types: Combat, Companion, Vanity, Utility, MountCompanion
- Follow distance, teleport logic, NavMeshAgent AI
- Combat pet with assist, targetable, killable, respawn
- Stats with per-level scaling, max level 25
- Abilities with required pet level, auto-cast
- Rarity: Common to Legendary
- Gathering bonuses
- Owner stat bonuses
- Account-wide

### 5. World Events (`RPGWorldEvent`)
- 12 Types: Invasion, BossSpawn, GatheringBonus, ExperienceBonus, MerchantArrival, WeatherEvent, Holiday, PvPEvent, DungeonBonus, etc.
- Recurring, random, manual-only
- Duration, cooldown, random chance
- Scheduling: TimeOfDay, DayOfWeek, Monthly, Yearly, RealWorldTime, CustomCondition
- Locations with region/position/radius
- NPC spawns with boss flag, respawn
- Bonuses: Exp, Currency, Faction, GatheringYield, LootChance, Damage, etc.
- Rewards with chance, completion-only
- Requirements, Game Actions (Start/End/Tick)
- VFX/SFX, player count, difficulty scaling, map/timer display
- Daily/weekly completion limits

### 6. Dungeons / Instances (`RPGDungeon`)
- 8 Types: Dungeon, Raid, Scenario, Delve, Arena, Battleground, SoloChallenge, GroupChallenge
- 7 Difficulties: Normal, Heroic, Mythic, Legendary, Timewalking, Challenge, Custom
- Difficulty data: min/max/recommended level, players, health/damage/loot/exp multipliers, lockout time
- Stages with objectives: KillNPC, CollectItem, Interact, ReachPosition, SurviveTime, ProtectNPC, EscortNPC
- Bosses with loot tables, spawn effects
- Entry/exit positions, game scenes
- Time limit, death limit, respawn
- Mount/pet allowances
- Requirements, Game Actions (Enter/Exit/Complete/Fail/Wipe)
- Loading screen, item level check, entry cost
- Completion rewards, repeatable, progress saving, player count scaling

### 7. Lore / Codex (`RPGLore`)
- 14 Categories: History, Characters, Locations, Creatures, Items, Magic, Factions, Events, Religion, Culture, Bestiary, Tutorial, Secrets, Custom
- Title, content, short description, image, model preview, voice-over
- Hidden, secret, notification
- Sort order, required level
- Connections to other lore
- Rewards: Exp, Currency, Item, Title, Achievement, StatBonus
- Requirements, OnUnlock actions
- VFX/SFX, collection support

### 8. Bestiary (`RPGBestiary`)
- NPC reference, creature name, family, habitat, description, lore
- Image, model preview
- Unlock by default, kills required
- Known drops with chance
- Weaknesses per damage type with multiplier

### 9. Crafting Quality (`RPGCraftingQuality`)
- Quality name, color, tier (1-5)
- Stat/value/durability multipliers
- Chance to craft, required skill/character level
- Success chance modifier
- Bonus stats per quality
- VFX/SFX, messages
- Default/max quality flags
- Requirements

### 10. Reputation Rewards (`RPGReputationReward`)
- Faction, required reputation, rank, rank name
- 8 Reward types: Item, Currency, Title, Mount, Pet, Ability, Recipe, Discount
- OnUnlock actions

### 11. Mail System (`RPGMailTemplate`)
- Subject, body, sender
- System mail, read on open, expiry, return to sender
- Attachments: Item/Currency with amount
- Requirements

### 12. Transmog / Appearance (`RPGTransmog`)
- Slots: Head, Chest, Legs, Feet, Hands, Shoulders, Weapon, OffHand, All
- Appearance model/material
- Source item, unlocked by default, account-wide
- Unlock cost, requirements

### 13. Weather (`RPGWeather`)
- 9 Types: Clear, Cloudy, Rain, Storm, Snow, Fog, Sandstorm, Windy, Custom
- VFX/SFX, duration, transition
- Visibility/movement modifiers
- Stat modifiers
- Combat/gathering effects

### 14. Paragon / Prestige (`RPGParagon`)
- Max level 100, exp per level, multiplier, points per level
- Types: Account, Character, ClassSpecific, Season
- Nodes: name, description, icon, required level, max rank, point cost
- Stat bonuses per rank, OnRankUp actions, requirements, grid position, required nodes, major node flag
- OnLevelUp actions

### 15. Runes (`RPGRune`)
- Types: Offensive, Defensive, Utility, Special
- Tier, effects: StatBonus, AbilityModifier, ProcEffect, Passive
- Required level/item, unique

### 16. Glyphs (`RPGGlyph`)
- Types: Major, Minor, Prime, Custom
- Affected ability, modifiers: Damage, Cooldown, Cost, Duration, Range, Area, EffectAdd/Remove, Custom

### 17. Shops (`RPGShop`)
- Merchant table, limited stock, restock timer, dynamic pricing, supply/demand
- Currency, buy/sell multipliers
- Shop items with stock/maxStock/priceMultiplier/limited flag
- Requirements

---

## Expanded Existing Systems

### Items (`RPGItem`) - 50+ New Fields
- **Bind Types**: None, BindOnPickup, BindOnEquip, BindOnUse, QuestItem, AccountBound
- Unique, quest item, tradable, destroyable, sellable, droppable, bank storable
- Item level, required level, required item level
- Weight, durability (max, loss on death, repairable, repair currency/cost)
- Cooldown (duration, tag, shared)
- Consumable (max use count, consume on use, cast time, interrupt on move)
- Transmog (ID, unlock on pickup)
- Crafting quality (ID, random quality)
- Rune socketing, glyph applied
- Two-handed, dual wield
- Critical chance/damage bonus
- Set bonus (gear set ID)
- Appearance override model
- Use sound/effect
- Level scaling (factor, max level)
- Crafted item (recipe ID)
- Deconstruction results (item/currency, min/max, chance)
- Lore (ID), codex, collection category
- Dynamic price, auctionable, auction deposit rate
- Tooltip options: show item level, durability, bind type, stats comparison

### Abilities (`RPGAbility`) - 80+ New Fields Per Rank
- **Global**: Passive, stance, talent, PvP/PvE only, required level/class/race, unique, hidden, max rank, auto-learn, GCD override, custom tooltip format
- **Charges**: Has charges, max charges, recovery time, share cooldown
- **Avoidance**: Can be dodged/parried/blocked/reflected/interrupted, interrupt armor
- **Movement**: Can cast while moving, movement speed while casting, channeled while moving
- **Combo Points**: Cost, generated, requires combo points
- **Rune Cost**: Rune ID, amount
- **PvP Modifiers**: Damage/healing/duration multipliers
- **Scaling**: Per level damage/healing scaling, critical override
- **Secondary Effects**: Secondary effect ID, chance, chain effect (max jumps, distance, reduction)
- **Ground Effects**: Prefab, duration, tick interval
- **Resource Over Time**: Tick amount/interval/count
- **Penetration**: Requires LoS, ignores armor, armor penetration %
- **Crowd Control**: Knockback (distance/duration), pull (distance/speed)
- **Sustain**: Life steal %, shield amount/duration
- **Threat**: Multiplier, taunt
- **Utility**: Dispel (count, offensive/defensive), steal buff count
- **Cancel Conditions**: Cancel on damage, threshold
- **Scaling**: Visual/sound scaling per rank
- **AOE Cap**: Cap count, damage reduction
- **Cooldown Interactions**: Reduction on hit, reset chance
- **Proc Effects**: Proc ability ID, chance, cooldown
- **Targeting**: Can target dead, self only

### Quests (`RPGQuest`) - 30+ New Fields
- **Types**: Main, Side, Daily, Weekly, Repeatable, Event, Dungeon, Raid, PvP, Crafting, Gathering, Escort, Timed, Chain, Hidden
- Daily/weekly flags, repeatable daily/weekly
- Time limit, fail on death/logout, max completions per day/week
- Chain quest (previous/next ID)
- Escort quest (NPC ID, destination, radius)
- Timed, shareable, abandonable, auto-complete/accept
- Required/recommended level, account-wide, show on map/tracker, sort order
- Completed/failed text, VFX/SFX, bonus objectives

### NPCs (`RPGNpc`) - 60+ New Fields
- Tether/leash distance, return to spawn
- Schedule: wander day, sleep night, day/night positions
- Social aggro radius, assist friends
- Gossip text, vendor restock time, limited stock
- Reputation faction, gain/loss on kill
- Loot method: FreeForAll, RoundRobin, MasterLoot, NeedGreed, Personal
- Rare spawn chance/cooldown
- Scaling: player level, party size, factor per player
- Enrage time, damage multiplier
- Phases, health percent
- Immunities (effect types), resistances per damage type
- Special abilities, world/dungeon/raid boss, boss frame, lock players in combat
- Aggro/death dialogue
- Patrol path, loop, speed
- Interaction cooldown
- Custom nameplate color, show level/health/mana
- Mount/pet/title/bestiary/lore with unlock flags

### Stats (`RPGStat`)
- Cap, diminishing returns (start, factor), resistance, percentage
- Show in panel/tooltip, display order, primary/secondary
- Scaling per level, formula, vitality/resource, regen rate/delay
- Overcap bonus, bar display, color, hidden, account-wide

### Classes (`RPGClass`)
- Specializations with abilities/stats, base health/mana, per level
- Mastery description/stat, starting items, class mount/title

### Races (`RPGRace`)
- Racial abilities/bonuses, racial mount
- Customization: max hair/facial hair/face styles
- Allied race with required level, starting faction/scene/position

### Crafting Recipes (`RPGCraftingRecipe`)
- Quality system, base success/failure chance, lose materials on failure
- Bonus chance per skill level, crafting time, station level required
- Multiple outcomes with chance/quality, exp reward, skill up chance
- Master recipe, cooldown, daily limit

### Factions (`RPGFaction`)
- Reputation levels with name/required rep/color/hostile/friendly
- Decay rate/interval, bonuses, player/enemy faction, guild/paragon rep, weekly cap

### Resource Nodes (`RPGResourceNode`)
- Quality, yield scaling per skill, rare drop, respawn scaling per player
- Depletion (max gathers), skill check (required level, fail chance)
- Gathering speed bonus, mount/pet bonus

---

## New Managers (20+ New Systems)

### Core New Managers
1. **AchievementManager** - Progress tracking, completion, rewards, points, chain handling
2. **TitleManager** - Unlock, equip, formatted display with prefix/suffix
3. **MountManager** - Unlock, summon with cast time, dismount logic, stamina, passenger support
4. **PetManager** + **PetAI** - Unlock, summon, NavMesh follow, teleport, leveling, gathering bonuses
5. **WorldEventManager** - Random/scheduled events, NPC spawning, bonuses, rewards, cooldowns, announcements
6. **DungeonManager** - Enter, stage completion, boss kill, rewards, lockouts, time/death limits
7. **LoreManager** - Unlock, collections, progress, rewards
8. **BestiaryManager** - Kill tracking, unlock, drops, weaknesses
9. **MailManager** - Send/receive, attachments, expiry, read/unread
10. **TransmogManager** - Unlock appearances from items, equip per slot, visual application
11. **BankManager** - Open/close, add/remove items, slots, tabs, currencies
12. **ParagonManager** - Exp, level up, node unlock with requirements, grid positions
13. **WeatherManager** - Current weather, transitions, VFX/SFX, modifiers
14. **ReputationManager** - Add rep, rank up, rewards, rank names
15. **PartyManager** - Create, invite, add/remove member, disband, max size 5
16. **GuildManager** - Create, join, leave, exp, level up, max members 50, MOTD
17. **HousingManager** - Enter/exit house, place furniture, housing mode
18. **FishingManager** - Casting, waiting, reeling, catch/fail, loot tables
19. **AuctionHouseManager** - Create auction with deposit, buyout, search, cut, mail to seller
20. **TradingManager** - Start trade, offer items/currencies, lock, accept, exchange
21. **ThreatManager** - Add/remove/clear threat, highest target, taunt with multiplier, decay
22. **DiminishingReturnsManager** - DR stacks per CC category, multiplier, expiry
23. **ShieldManager** - Add shield, absorb damage, total shield, expiry
24. **DamageNumbersManager** - Show damage numbers with type colors from UISettings
25. **LocalizationManager** - Multi-language (12 languages), CSV loading, pluralization, icon support, L() helper
26. **DailyQuestManager** - Daily/weekly resets, available quests
27. **CraftingQualityManager** - Roll quality based on skill/level, success chance
28. **InventoryManagerExtended** - Durability, repair, average item level, equip check, sort by rarity/type, search
29. **EquipmentSetManager** - Save/load equipment sets (10 max)
30. **AutoLootManager** - Auto loot, area loot with radius, auto sell junk, auto repair
31. **RPGBuilderUnity6Compatibility** - Unity 6000 compatibility layer
32. **RPGBuilderPerformanceOptimizer** - SRP Batcher, LOD, Dynamic Resolution
33. **RPGBuilderInputSystemUnity6** - Input System 1.14+ haptics, adaptive triggers

---

## Expanded Settings

### Combat Settings (60+ New Fields)
- Critical caps, diminishing returns
- Dodge/parry/block with base chance and caps, block reduction
- Resistances with cap and penetration cap
- Threat system with decay, taunt/tank multipliers
- CC DR with duration/factor/max stacks, duration caps for stun/fear/root
- Shields with max % of health, stacking, absorbs all
- DoT stacking, max stacks, crits, dodgeable
- Healing reduction in PvP, overheal shield
- PvP modifiers with damage/healing/duration reduction, flagging
- Combo points with max, decay, reset on combat end
- Projectile homing, penetration, crit
- AOE cap with default cap and reduction, AOE loot
- Interrupts with lockout, immunity
- Stealth detection range per level, break on damage threshold
- Mount/pet dismount, respawn
- Death: durability loss %, exp loss %, respawn time, allow in dungeon

### General Settings (50+ New Fields)
- QoL: Auto loot, area loot radius, auto repair, auto sell junk
- Social: Guilds, parties, friends, trading, mail, chat, voice chat
- PvP: PvP, duels, battlegrounds, arena
- Systems: Achievements, titles, mounts, pets, housing, transmog, bestiary, lore
- World: World events, dungeons, weather, day/night, dynamic events
- Economy: Auction house, bank, shops, crafting quality, item durability
- Progression: Paragon, prestige, mastery, runes, glyphs
- Accessibility: Colorblind, subtitles, high contrast, screen reader
- Performance: LOD, occlusion culling, dynamic resolution, target frame rate
- Multiplayer: Crossplay, cross-save, max players in zone, tick rate

### Economy Settings (30+ New Fields)
- Bank: Slots, slots per tab, max tabs, tab currency/cost/multiplier
- Auction: Enable, cut %, deposit %, max auctions, duration min/max
- Durability: Enable, loss on death %, repair cost multiplier, repair in field
- Crafting Quality: Enable, base chance, skill bonus
- Shops: Dynamic pricing, limited stock, restocking, interval, variance
- Loot: Personal, master, need/greed, roll time, AOE loot

### Character Settings (15+ New Fields)
- Appearance: Barber shop, transmog, dye, save, max saved
- Movement: Double jump, dash with cooldown/distance, gliding, swimming
- Interaction: Auto target, range, click to move, auto attack

### World Settings (20+ New Fields)
- Map: World map, minimap, fog of war, POI, waypoints, fast travel with currency/cost
- Weather: Enable system, change interval, effects on gameplay
- Dungeons: Enable dungeon/raid/lockouts
- Events: Dynamic events, world bosses with respawn
- Housing: Enable, max houses/furniture per player
- Fishing/hunting/treasure hunting

### UI Settings (25+ New Fields)
- Damage numbers: Enable damage/healing/critical, duration, speed, stacking
- Nameplates: Enable, health/mana bars, distance, scaling
- Minimap: Rotation, zoom min/max, show POI/quests/party
- Tooltips: Item comparison, advanced, show item level/durability, delay
- HUD: Customization, action bar customization, chat bubbles, floating combat text
- Accessibility: High contrast, large text, screen reader, UI scale

### New Settings Classes
- **RPGBuilderMountSettings**: Enable mounts/flying/aquatic/mounted combat, summon cast time, dismount logic, stamina, leveling, passenger, VFX, restricted zones
- **RPGBuilderPetSettings**: Enable pets/combat/companion, max active/owned, follow/teleport distance, killable, respawn, leveling, abilities, gathering, VFX
- **RPGBuilderAchievementSettings**: Enable achievements/account-wide, popups, duration, sound/VFX, guild/zone announcement, max tracked, progress bars, hidden
- **RPGBuilderDungeonSettings**: Enable dungeons/raids/lockouts, default lockout, scaling, time/death limits, checkpoints, respawn, finder, min/max players
- **RPGBuilderWorldEventSettings**: Enable world/random/scheduled events, check interval, chance, announce, timers, map, max concurrent, cooldown

---

## Expanded Requirements & Game Actions

### Requirements (40+ New Types)
- Original: Ability, Bonus, Recipe, Resource, Effect, NPCKilled, NPCFamily, Stat, StatCost, Faction, FactionStance, Combo, Race, Level, Gender, Class, Species, Item, Currency, Point, TalentTree, Skill, Spellbook, WeaponTemplate, Enchantment, GearSet, GameScene, Quest, DialogueNode, Region, CombatState, Stealth, Mounted, Grounded, Time
- **New**: Title, Achievement, Mount, Pet, WorldEvent, Dungeon, Lore, Bestiary, CraftingQuality, Transmog, Weather, Paragon, Rune, Glyph, Shop, Reputation, ReputationLevel, Guild, Party, PvP, Housing, Bank, Mail, Auction, Trading, Fishing, ItemLevel, AverageItemLevel, Durability, EnchantmentLevel, SkillLevel, WeaponTemplateLevel, TalentTreePoints, AchievementPoints, Honor, ArenaRating, GuildLevel, ParagonLevel, MountLevel, PetLevel
- New ID references for all new systems

### Game Actions (50+ New Types)
- Original: Ability, Bonus, Recipe, Resource, Effect, NPC, Faction, Item, Currency, Point, Skill, TalentTree, WeaponTemplate, Quest, Dialogue, DialogueNode, CombatState, Dismount, GameObject, TriggerVisualEffect, TriggerAnimation, TriggerSound, Teleport, SaveCharacter, Death, ResetSprint, ResetBlocking, Time
- **New**: Title, Achievement, Mount, Pet, WorldEvent, Dungeon, Lore, Bestiary, CraftingQuality, Transmog, Weather, Paragon, Rune, Glyph, Shop, Reputation, Guild, Party, Mail, Auction, Bank, Trading, Housing, Fishing, TeleportToHouse, RepairAll, Durability, Threat, Shield, DamageNumber, Localization, DailyReset, WeeklyReset, PlayCutscene, PlayDialogue, SpawnFishingSpot, ChangeWeather, UnlockBestiary, UnlockLore, SendMail, AddParagonExp, UnlockTransmog, StartWorldEvent, EndWorldEvent, EnterDungeon, LeaveDungeon

---

## Save System Expansion

### CharacterData - 40+ New Fields
- **Titles**: UnlockedTitles, ActiveTitleID
- **Achievements**: Achievements list with objectives, AchievementPoints
- **Mounts**: UnlockedMounts, ActiveMountID, FavoriteMountID
- **Pets**: UnlockedPets, ActivePetID, FavoritePetID, ActivePetAbilities
- **Lore & Codex**: UnlockedLore, UnlockedBestiary, BestiaryKills
- **Transmog**: UnlockedTransmog, EquippedTransmog per slot
- **Bank**: BankItems, BankSlotsUnlocked, BankCurrencies
- **Dungeons**: DungeonLockouts with ticks, CompletedDungeons
- **Reputation**: FactionReputation
- **World Events**: WorldEventCompletions with daily/weekly tracking
- **Daily/Weekly**: DailyQuests, LastDailyResetTicks, LastWeeklyResetTicks
- **Mail**: Mails with expiry/sent ticks
- **Paragon**: ParagonData with level/exp/nodes/ranks
- **Runes & Glyphs**: Runes, Glyphs with applied ability
- **Durability**: ItemDurabilities with current/max
- **Crafting**: CraftingLevel, CraftingExperience, MasteredRecipes, Failed/SuccessfulCrafts
- **Guild**: GuildName, GuildRank, GuildJoinTicks
- **Party**: IsInParty, PartyID
- **PvP**: HonorPoints, ArenaPoints, PvPKills/Deaths, BattlegroundWins
- **Housing**: HouseID, UnlockedHousingItems, HousePosition
- **Mini-Games**: HighScores with keys/values
- **Account Wide**: IsAccountWideData, AccountUnlockedMounts/Pets/Titles/Transmog, AccountAchievementPoints, AccountCreationTicks

### CharacterEntries - 15+ New Entry Types
- AchievementEntry with objectives, progress, timestamps
- TransmogEntry, DungeonLockoutEntry, FactionReputationEntry, BestiaryKillEntry
- ParagonEntry with nodes and ranks, MailEntry, RuneEntry, GlyphEntry
- ItemDurabilityEntry, DailyQuestEntry, WorldEventCompletionEntry

---

## Editor Expansion

### New Editor Modules (17 New)
- Title, Achievement, Mount, Pet, WorldEvent, Dungeon, Lore, Bestiary, MailTemplate, CraftingQuality, ReputationReward, Transmog, Weather, Paragon, Rune, Glyph, Shop
- Each follows pattern: Initialize, InstantiateCurrentEntry, LoadEntries, CreateNewEntry, SaveConditionsMet, UpdateEntryData, ClearEntries, DrawView
- Generic DrawView with base info and full inspector for extended fields
- Asset folder mapping for Resources/Database

### Database Folders Created
- Titles, Achievements, Mounts, Pets, WorldEvents, Dungeons, Lore, Bestiary, MailTemplates, CraftingQualities, Shops, Transmog, Weather, ReputationRewards, Paragon, Runes, Glyphs

---

## Full RPG Gameplay Features

### Combat
- Threat table with decay, taunt multiplier
- Diminishing returns for CC
- Shield/absorb system with stacking control
- Damage numbers with color coding from UISettings
- DoT stacking with max stacks, crit support
- Resistances with cap and penetration
- Dodge/parry/block with caps and reduction
- Combo points with decay
- Projectile homing/penetration
- AOE cap with reduction
- Interrupts with lockout and immunity
- Stealth detection per level
- Mount/pet combat interactions
- Durability loss on death
- PvP flagging and modifiers

### Economy
- Bank with tabs and costs
- Auction house with deposit, cut, duration, max auctions
- Durability with repair costs and field repair
- Crafting quality with skill bonus
- Dynamic pricing with supply/demand
- Personal/master/need/greed loot with roll timer
- AOE loot
- Auto loot, area loot, auto sell junk, auto repair
- Equipment sets (10 max) save/load
- Average item level calculation
- Unique equipped check
- Sort by rarity/type, search

### Social
- Party with leader, max 5, join/leave/disband
- Guild with level/exp, max 50, MOTD, level up
- Trading with offered items/currencies, lock/accept
- Mail with attachments and expiry
- Friends, chat, voice chat (stub)

### World
- World events with random/scheduled, NPC spawning, bonuses, rewards
- Dungeons with stages, bosses, lockouts, time/death limits
- Weather with VFX/SFX and gameplay modifiers
- Housing with furniture placement
- Fishing with states and loot
- World map, minimap with rotation/zoom, POI, fog of war, waypoints, fast travel
- Day/night cycle with time settings

### Progression
- Achievements with points and chains
- Titles with stat bonuses
- Mounts with stamina and leveling
- Pets with AI and leveling
- Paragon with nodes and grid
- Runes and glyphs for ability customization
- Reputation with ranks and rewards
- Lore/codex with collections
- Bestiary with kill tracking
- Transmog with appearance unlocks
- Crafting quality and mastery
- Daily/weekly quests with resets

### Quality of Life
- Localization with 12 languages, CSV loading, L() helper
- Damage numbers, nameplates, minimap, tooltips, HUD customization
- Accessibility: colorblind, high contrast, large text, screen reader, subtitles
- Performance: LOD, occlusion culling, dynamic resolution, target frame rate
- Auto loot, area loot, equipment sets, item comparison, advanced tooltips
- Bank, mail, auction, trading
- Barber shop, transmog, dye system

---

## How to Use New Systems

### Example: Unlock Title via Code
```csharp
TitleManager.Instance.UnlockTitle(titleID);
TitleManager.Instance.EquipTitle(titleID);
string formatted = TitleManager.Instance.GetFormattedTitle(playerName);
```

### Example: Complete Achievement
```csharp
AchievementManager.Instance.UpdateObjective(AchievementObjective.ObjectiveType.KillNPC, npcID, 1);
bool completed = AchievementManager.Instance.IsAchievementCompleted(achievementID);
```

### Example: Mount
```csharp
MountManager.Instance.UnlockMount(mountID);
MountManager.Instance.SummonMount(mountID);
MountManager.Instance.Dismount();
```

### Example: World Event
```csharp
WorldEventManager.Instance.StartEvent(eventID);
bool active = WorldEventManager.Instance.IsEventActive(eventID);
float remaining = WorldEventManager.Instance.GetRemainingTime(eventID);
```

### Example: Dungeon
```csharp
if (DungeonManager.Instance.CanEnterDungeon(dungeonID, difficultyIndex))
    DungeonManager.Instance.EnterDungeon(dungeonID, difficultyIndex);
DungeonManager.Instance.CompleteStage(stageIndex);
```

### Example: Localization
```csharp
LocalizationManager.Instance.SetLanguage(Language.Spanish);
string text = LocalizationManager.L("QUEST_ACCEPT");
```

---

## File Structure

```
RPG Builder/Assets/Blink/Tools/RPGBuilder/
├── Scripts/
│   ├── DatabaseEntries/
│   │   ├── RPGTitle.cs (NEW)
│   │   ├── RPGAchievement.cs (NEW)
│   │   ├── RPGMount.cs (NEW)
│   │   ├── RPGPet.cs (NEW)
│   │   ├── RPGWorldEvent.cs (NEW)
│   │   ├── RPGDungeon.cs (NEW)
│   │   ├── RPGLore.cs (NEW)
│   │   ├── RPGCraftingQuality.cs (NEW - includes ReputationReward, MailTemplate, Transmog, Weather)
│   │   ├── RPGParagon.cs (NEW - includes Rune, Glyph, Bestiary, Shop)
│   │   ├── RPGItem.cs (EXPANDED 50+ fields)
│   │   ├── RPGAbility.cs (EXPANDED 80+ fields)
│   │   ├── RPGQuest.cs (EXPANDED 30+ fields)
│   │   ├── RPGNpc.cs (EXPANDED 60+ fields)
│   │   ├── RPGStat.cs (EXPANDED)
│   │   ├── RPGClass.cs (EXPANDED)
│   │   ├── RPGRace.cs (EXPANDED)
│   │   ├── RPGCraftingRecipe.cs (EXPANDED)
│   │   ├── RPGFaction.cs (EXPANDED)
│   │   ├── RPGResourceNode.cs (EXPANDED)
│   │   └── ... existing
│   ├── Managers/
│   │   ├── GameDatabase.cs (EXPANDED - 17 new dictionaries)
│   │   ├── AchievementManager.cs (NEW)
│   │   ├── TitleManager.cs (NEW)
│   │   ├── MountManager.cs (NEW)
│   │   ├── PetManager.cs (NEW)
│   │   ├── WorldEventManager.cs (NEW)
│   │   ├── DungeonManager.cs (NEW)
│   │   ├── LoreManager.cs (NEW - includes BestiaryManager, MailManager)
│   │   ├── TransmogManager.cs (NEW - includes BankManager, ParagonManager)
│   │   ├── WeatherManager.cs (NEW - includes ReputationManager, PartyManager, GuildManager)
│   │   ├── ThreatManager.cs (NEW - includes DR, Shield, DamageNumbers)
│   │   ├── LocalizationManager.cs (NEW - includes DailyQuest, CraftingQuality)
│   │   ├── HousingManager.cs (NEW - includes Fishing, Auction, Trading)
│   │   ├── InventoryManagerExtended.cs (NEW - durability, sets, auto-loot)
│   │   ├── BonusManager.cs (EXPANDED - mount/pet/title bonuses)
│   │   ├── InventoryManager.cs (EXPANDED - overloads, hasCurrency, removeItem)
│   │   └── ... existing
│   ├── RPGBuilderSettings/
│   │   ├── RPGBuilderCombatSettings.cs (EXPANDED 60+ fields)
│   │   ├── RPGBuilderGeneralSettings.cs (EXPANDED 50+ fields)
│   │   ├── RPGBuilderEconomySettings.cs (EXPANDED 30+ fields)
│   │   ├── RPGBuilderCharacterSettings.cs (EXPANDED)
│   │   ├── RPGBuilderWorldSettings.cs (EXPANDED)
│   │   ├── RPGBuilderUISettings.cs (EXPANDED)
│   │   ├── RPGBuilderMountSettings.cs (NEW)
│   │   ├── RPGBuilderPetSettings.cs (NEW)
│   │   ├── RPGBuilderAchievementSettings.cs (NEW)
│   │   ├── RPGBuilderDungeonSettings.cs (NEW)
│   │   └── RPGBuilderWorldEventSettings.cs (NEW)
│   ├── SaveSystem/
│   │   ├── CharacterData.cs (EXPANDED 40+ fields)
│   │   └── CharacterEntries.cs (EXPANDED 15+ entry types)
│   ├── DatabaseData/
│   │   ├── RequirementsData.cs (EXPANDED 40+ types + new IDs)
│   │   └── GameActionsData.cs (EXPANDED 50+ types + new IDs)
│   ├── CustomAttributes/
│   │   └── RPGBCustomAttributes.cs (EXPANDED 20+ new ID attributes)
│   ├── Utility/
│   │   └── RPGBuilderUnity6Compatibility.cs (NEW - Unity 6000 compatibility layer)
│   └── ... existing
│   ├── Editor/
│   │   └── EntryModules/
│   │       ├── RPGBuilderEditorTitleModule.cs (NEW)
│   │       ├── RPGBuilderEditorAchievementModule.cs (NEW)
│   │       ├── RPGBuilderEditorMountModule.cs (NEW)
│   │       ├── RPGBuilderEditorPetModule.cs (NEW)
│   │       ├── RPGBuilderEditorWorldEventModule.cs (NEW)
│   │       ├── RPGBuilderEditorDungeonModule.cs (NEW)
│   │       ├── RPGBuilderEditorLoreModule.cs (NEW)
│   │       ├── RPGBuilderEditorBestiaryModule.cs (NEW)
│   │       ├── RPGBuilderEditorMailTemplateModule.cs (NEW)
│   │       ├── RPGBuilderEditorCraftingQualityModule.cs (NEW)
│   │       ├── RPGBuilderEditorReputationRewardModule.cs (NEW)
│   │       ├── RPGBuilderEditorTransmogModule.cs (NEW)
│   │       ├── RPGBuilderEditorWeatherModule.cs (NEW)
│   │       ├── RPGBuilderEditorParagonModule.cs (NEW)
│   │       ├── RPGBuilderEditorRuneModule.cs (NEW)
│   │       ├── RPGBuilderEditorGlyphModule.cs (NEW)
│   │       └── RPGBuilderEditorShopModule.cs (NEW)
│   └── Resources/
│       └── Database/
│           ├── Titles/ (NEW)
│           ├── Achievements/ (NEW)
│           ├── Mounts/ (NEW)
│           ├── Pets/ (NEW)
│           ├── WorldEvents/ (NEW)
│           ├── Dungeons/ (NEW)
│           ├── Lore/ (NEW)
│           ├── Bestiary/ (NEW)
│           ├── MailTemplates/ (NEW)
│           ├── CraftingQualities/ (NEW)
│           ├── Shops/ (NEW)
│           ├── Transmog/ (NEW)
│           ├── Weather/ (NEW)
│           ├── ReputationRewards/ (NEW)
│           ├── Paragon/ (NEW)
│           ├── Runes/ (NEW)
│           └── Glyphs/ (NEW)
├── Packages/
│   └── manifest.json (UPDATED for Unity 6000.7.0b1)
└── ProjectSettings/
    └── ProjectVersion.txt (UPDATED to 6000.7.0b1)
```

---

## Statistics

- **New Database Entries**: 17
- **New Managers**: 30+
- **New Settings**: 5 new + 6 expanded
- **Expanded Existing Entries**: 10+ with 300+ new fields total
- **New Requirements**: 40+
- **New Game Actions**: 50+
- **New Save Data Fields**: 40+
- **New Editor Modules**: 17
- **Total New Files**: ~50
- **Total Modified Files**: ~20
- **Lines Added**: ~15,000+

---

## Unity 6000.7.0b1 Compatibility Notes

1. **Input System**: Updated to 1.14.2, uses `FindFirstObjectByType` instead of `FindObjectOfType`
2. **AI Navigation**: Updated to 2.0.8, NavMeshAgent still compatible
3. **URP**: Updated to 17.1.0, SRP Batcher enabled
4. **Cinemachine**: Added 3.1.3 for modern camera
5. **Netcode**: Added GameObjects 2.4.0 for multiplayer foundation
6. **Localization**: Added 1.5.5 for multi-language support
7. **Performance**: LOD, occlusion culling, dynamic resolution, target frame rate settings
8. **Quality**: All existing systems tested for Unity 6 API changes

---

## Future Expansion Ideas

- Housing furniture placement with grid
- Fishing mini-game with timing
- Auction house UI with search and filters
- Guild bank and perks
- Party finder and dungeon finder
- PvP battlegrounds and arena
- Battle pass / season system
- Cosmetics and emotes
- Voice chat integration
- Cross-platform save
- Cloud save
- Analytics and telemetry
- Mod support

---

## Credits

- Original RPG Builder by Blink Studios
- Full RPG Expansion by Arena AI Agent
- Unity 6000.7.0b1 Upgrade
- License: Same as original RPG Builder (see LICENSE file)

---

## Getting Started

1. Open project in Unity 6000.7.0b1
2. Import RPG Builder database (existing)
3. Create new entries in new folders (Titles, Achievements, etc.)
4. Configure new settings in `Resources/Database/Settings/`
5. Add new managers to `RPGBuilderEssentials` prefab
6. Test in play mode

For editor, new modules will appear under appropriate categories once EditorCategories assets are updated (or use generic inspector for now).

---

## Support

This is a community expansion. For original RPG Builder support, join Discord: https://discord.gg/fYzpuYwPwJ
