using System;
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Templates
{
    // ==================== EXTENDED TEMPLATES FOR FULL RPG ====================

    [CreateAssetMenu(fileName = "New Damage Type", menuName = "Blink/RPGBuilder/Extended/Damage Type")]
    public class ExtendedDamageTypeTemplate : ScriptableObject
    {
        public string damageTypeName = "Fire";
        public Color damageColor = Color.red;
        public Sprite damageIcon;
        public bool canCrit = true;
        public bool canBeBlocked = true;
        public bool canBeDodged = true;
        public bool ignoresArmor = false;
        public float armorPenetration = 0f;
        public float criticalMultiplier = 2f;
        public GameObject hitEffect;
        public AudioClip hitSound;
        public List<StatModifier> statModifiers = new List<StatModifier>();

        [Serializable]
        public class StatModifier
        {
            [StatID] public int statID = -1;
            public float modifier = 0f;
            public bool isPercent = true;
        }
    }

    [CreateAssetMenu(fileName = "New Loot Modifier", menuName = "Blink/RPGBuilder/Extended/Loot Modifier")]
    public class LootModifierTemplate : ScriptableObject
    {
        public enum ModifierType
        {
            Luck,
            MagicFind,
            Quantity,
            Quality,
            Rarity
        }
        public ModifierType modifierType = ModifierType.Luck;
        public float modifierValue = 1.1f;
        public float duration = 3600f;
        [StatID] public int requiredStatID = -1;
        public float requiredStatValue = 0f;
    }

    [CreateAssetMenu(fileName = "New Experience Modifier", menuName = "Blink/RPGBuilder/Extended/Experience Modifier")]
    public class ExperienceModifierTemplate : ScriptableObject
    {
        public float expMultiplier = 1.5f;
        public float duration = 3600f;
        public bool affectsAllExp = true;
        public bool affectsQuestExp = true;
        public bool affectsKillExp = true;
        public bool affectsCraftingExp = true;
        public bool affectsGatheringExp = true;
        public bool affectsDungeonExp = true;
        public bool stacks = false;
        public int maxStacks = 1;
    }

    [CreateAssetMenu(fileName = "New Reputation Modifier", menuName = "Blink/RPGBuilder/Extended/Reputation Modifier")]
    public class ReputationModifierTemplate : ScriptableObject
    {
        [FactionID] public int factionID = -1;
        public float repMultiplier = 1.5f;
        public float duration = 3600f;
        public bool affectsAllFactions = false;
        public bool stacks = false;
    }

    [CreateAssetMenu(fileName = "New Crafting Bonus", menuName = "Blink/RPGBuilder/Extended/Crafting Bonus")]
    public class CraftingBonusTemplate : ScriptableObject
    {
        public float successChanceBonus = 10f;
        public float qualityChanceBonus = 10f;
        public float speedBonus = 0.2f;
        public float duration = 3600f;
        [SkillID] public int requiredSkillID = -1;
        public int requiredSkillLevel = 1;
        public bool affectsAllRecipes = true;
        [RecipeID] public int specificRecipeID = -1;
    }

    [CreateAssetMenu(fileName = "New Gathering Bonus", menuName = "Blink/RPGBuilder/Extended/Gathering Bonus")]
    public class GatheringBonusTemplate : ScriptableObject
    {
        public float yieldBonus = 0.2f;
        public float speedBonus = 0.2f;
        public float rareChanceBonus = 5f;
        public float duration = 3600f;
        [ResourceID] public int resourceID = -1;
        public bool affectsAllResources = true;
        public bool requiresTool = false;
        [ItemID] public int requiredToolID = -1;
    }

    [CreateAssetMenu(fileName = "New Title Category", menuName = "Blink/RPGBuilder/Extended/Title Category")]
    public class TitleCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Achievement";
        public Color categoryColor = Color.white;
        public Sprite categoryIcon;
        public int sortOrder = 0;
        public bool isHidden = false;
    }

    [CreateAssetMenu(fileName = "New Achievement Category", menuName = "Blink/RPGBuilder/Extended/Achievement Category")]
    public class AchievementCategoryTemplate : ScriptableObject
    {
        public string categoryName = "General";
        public Color categoryColor = Color.white;
        public Sprite categoryIcon;
        public int sortOrder = 0;
        public bool showProgress = true;
        public bool isAccountWide = false;
    }

    [CreateAssetMenu(fileName = "New Mount Category", menuName = "Blink/RPGBuilder/Extended/Mount Category")]
    public class MountCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Ground";
        public bool canFly = false;
        public bool canSwim = false;
        public float speedMultiplier = 1f;
        public Sprite categoryIcon;
    }

    [CreateAssetMenu(fileName = "New Pet Category", menuName = "Blink/RPGBuilder/Extended/Pet Category")]
    public class PetCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Companion";
        public bool isCombatPet = false;
        public bool canGather = false;
        public Sprite categoryIcon;
    }

    [CreateAssetMenu(fileName = "New Dungeon Modifier", menuName = "Blink/RPGBuilder/Extended/Dungeon Modifier")]
    public class DungeonModifierTemplate : ScriptableObject
    {
        public string modifierName = "Hard Mode";
        public string description = "Increases difficulty";
        public float healthMultiplier = 1.5f;
        public float damageMultiplier = 1.5f;
        public float lootMultiplier = 1.5f;
        public float expMultiplier = 1.5f;
        public List<AbilityModifier> abilityModifiers = new List<AbilityModifier>();

        [Serializable]
        public class AbilityModifier
        {
            [AbilityID] public int abilityID = -1;
            public float cooldownMultiplier = 1f;
            public float damageMultiplier = 1f;
        }
    }

    [CreateAssetMenu(fileName = "New World Event Modifier", menuName = "Blink/RPGBuilder/Extended/World Event Modifier")]
    public class WorldEventModifierTemplate : ScriptableObject
    {
        public string modifierName = "Bonus Loot";
        public float lootChanceMultiplier = 2f;
        public float expMultiplier = 2f;
        public float currencyMultiplier = 2f;
        public float duration = 3600f;
    }

    [CreateAssetMenu(fileName = "New Currency Category", menuName = "Blink/RPGBuilder/Extended/Currency Category")]
    public class CurrencyCategoryTemplate : ScriptableObject
    {
        public string categoryName = "General";
        public bool isAccountWide = false;
        public bool hasCap = false;
        public int capAmount = 10000;
        public bool showInUI = true;
        public Sprite categoryIcon;
        public Color categoryColor = Color.white;
    }

    [CreateAssetMenu(fileName = "New Item Category", menuName = "Blink/RPGBuilder/Extended/Item Category")]
    public class ItemCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Weapons";
        public bool canBeEquipped = true;
        public bool canBeTraded = true;
        public bool canBeSold = true;
        public bool hasDurability = false;
        public Sprite categoryIcon;
        public Color categoryColor = Color.white;
    }

    [CreateAssetMenu(fileName = "New Stat Category Extended", menuName = "Blink/RPGBuilder/Extended/Stat Category Extended")]
    public class StatCategoryExtendedTemplate : ScriptableObject
    {
        public string categoryName = "Offensive";
        public Color categoryColor = Color.red;
        public Sprite categoryIcon;
        public int sortOrder = 0;
        public bool showInCharacterPanel = true;
        public bool isPrimary = false;
        public bool isSecondary = true;
    }

    [CreateAssetMenu(fileName = "New Buff Category", menuName = "Blink/RPGBuilder/Extended/Buff Category")]
    public class BuffCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Offensive Buff";
        public Color categoryColor = Color.green;
        public Sprite categoryIcon;
        public bool isPositive = true;
        public bool canBeDispelled = true;
        public bool isStackable = false;
        public int maxStacks = 1;
        public bool showInUI = true;
    }

    [CreateAssetMenu(fileName = "New Quest Category", menuName = "Blink/RPGBuilder/Extended/Quest Category")]
    public class QuestCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Main Story";
        public Color categoryColor = Color.yellow;
        public Sprite categoryIcon;
        public int sortOrder = 0;
        public bool isMainQuest = false;
        public bool isDaily = false;
        public bool isWeekly = false;
        public bool showInTracker = true;
        public bool showOnMap = true;
    }

    [CreateAssetMenu(fileName = "New NPC Category", menuName = "Blink/RPGBuilder/Extended/NPC Category")]
    public class NPCCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Humanoid";
        public Color categoryColor = Color.white;
        public Sprite categoryIcon;
        public bool isHostileByDefault = false;
        public bool canBeTamed = false;
        public bool isBoss = false;
        public bool isRare = false;
        public float rareSpawnChance = 5f;
    }

    [CreateAssetMenu(fileName = "New Weather Category", menuName = "Blink/RPGBuilder/Extended/Weather Category")]
    public class WeatherCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Rain";
        public Color skyColor = Color.gray;
        public float fogDensity = 0.02f;
        public float windStrength = 1f;
        public GameObject weatherEffect;
        public AudioClip weatherSound;
        public bool affectsGameplay = false;
    }

    [CreateAssetMenu(fileName = "New Housing Category", menuName = "Blink/RPGBuilder/Extended/Housing Category")]
    public class HousingCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Furniture";
        public int maxPlaceable = 100;
        public bool canBePlacedOutside = false;
        public bool canBePlacedInside = true;
        public bool requiresFloor = true;
        public Sprite categoryIcon;
    }

    [CreateAssetMenu(fileName = "New Fishing Category", menuName = "Blink/RPGBuilder/Extended/Fishing Category")]
    public class FishingCategoryTemplate : ScriptableObject
    {
        public string categoryName = "Freshwater Fish";
        public Sprite categoryIcon;
        public Color waterColor = Color.blue;
        public List<int> possibleFishIDs = new List<int>();
        public float rareFishChance = 5f;
        public int requiredFishingLevel = 1;
    }
}
