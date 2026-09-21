using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]

public class BaseCustomAttribute : Attribute {}
 
public class IDAttribute : BaseCustomAttribute {}
 
public class AbilityIDAttribute : IDAttribute {}
public class EffectIDAttribute : IDAttribute {}
public class NPCIDAttribute : IDAttribute {}
public class StatIDAttribute : IDAttribute {}
public class PointIDAttribute : IDAttribute {}
public class SpellbookIDAttribute : IDAttribute {}
public class FactionIDAttribute : IDAttribute {}
public class WeaponTemplateIDAttribute : IDAttribute {}
public class SpeciesIDAttribute : IDAttribute {}
public class ComboIDAttribute : IDAttribute {}
public class ItemIDAttribute : IDAttribute {}
public class SkillIDAttribute : IDAttribute {}
public class LevelsIDAttribute : IDAttribute {}
public class RaceIDAttribute : IDAttribute {}
public class ClassIDAttribute : IDAttribute {}
public class LootTableIDAttribute : IDAttribute {}
public class MerchantTableIDAttribute : IDAttribute {}
public class CurrencyIDAttribute : IDAttribute {}
public class RecipeIDAttribute : IDAttribute {}
public class CraftingStationIDAttribute : IDAttribute {}
public class TalentTreeIDAttribute : IDAttribute {}
public class BonusIDAttribute : IDAttribute {}
public class GearSetIDAttribute : IDAttribute {}
public class EnchantmentIDAttribute : IDAttribute {}
public class TaskIDAttribute : IDAttribute {}
public class QuestIDAttribute : IDAttribute {}
public class CoordinateIDAttribute : IDAttribute {}
public class ResourceIDAttribute : IDAttribute {}
public class GameSceneIDAttribute : IDAttribute {}
public class DialogueIDAttribute : IDAttribute {}
public class GameModifierIDAttribute : IDAttribute {}

// NEW RPG BUILDER EXTENDED IDs - Full Blown RPG
public class TitleIDAttribute : IDAttribute {}
public class AchievementIDAttribute : IDAttribute {}
public class MountIDAttribute : IDAttribute {}
public class PetIDAttribute : IDAttribute {}
public class WorldEventIDAttribute : IDAttribute {}
public class DungeonIDAttribute : IDAttribute {}
public class LoreIDAttribute : IDAttribute {}
public class BestiaryIDAttribute : IDAttribute {}
public class MailTemplateIDAttribute : IDAttribute {}
public class GuildIDAttribute : IDAttribute {}
public class ReputationRewardIDAttribute : IDAttribute {}
public class CraftingQualityIDAttribute : IDAttribute {}
public class ShopIDAttribute : IDAttribute {}
public class TransmogIDAttribute : IDAttribute {}
public class AppearanceIDAttribute : IDAttribute {}
public class WeatherIDAttribute : IDAttribute {}
public class MiniGameIDAttribute : IDAttribute {}
public class HousingIDAttribute : IDAttribute {}
public class DialogueBranchIDAttribute : IDAttribute {}
public class ReputationLevelIDAttribute : IDAttribute {}
public class FactionRankIDAttribute : IDAttribute {}
public class BuffCategoryIDAttribute : IDAttribute {}
public class DeBuffCategoryIDAttribute : IDAttribute {}
public class ItemSetIDAttribute : IDAttribute {} // alias for GearSet but new
public class SkillTreeIDAttribute : IDAttribute {}
public class ParagonIDAttribute : IDAttribute {}
public class RuneIDAttribute : IDAttribute {}
public class GlyphIDAttribute : IDAttribute {}
public class TalentIDAttribute : IDAttribute {}
 
public class RPGDataListAttribute : BaseCustomAttribute {}
public class RPGNonSerializedAttribute : BaseCustomAttribute {}
 