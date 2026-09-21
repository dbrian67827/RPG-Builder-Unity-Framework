using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance { get; private set; }

        public enum Language
        {
            English,
            Spanish,
            French,
            German,
            Italian,
            Portuguese,
            Russian,
            Chinese,
            Japanese,
            Korean,
            Polish,
            Turkish
        }

        [System.Serializable]
        public class LocalizedString
        {
            public string key;
            public Dictionary<Language, string> translations = new Dictionary<Language, string>();
        }

        private Dictionary<string, Dictionary<Language, string>> localizedTexts = new Dictionary<string, Dictionary<Language, string>>();
        private Language currentLanguage = Language.English;

        public Action<Language> OnLanguageChanged;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguagePreference();
        }

        private void LoadLanguagePreference()
        {
            string saved = PlayerPrefs.GetString("RPGBuilder_Language", "English");
            if (Enum.TryParse(saved, out Language lang))
                currentLanguage = lang;
        }

        public void SetLanguage(Language language)
        {
            currentLanguage = language;
            PlayerPrefs.SetString("RPGBuilder_Language", language.ToString());
            PlayerPrefs.Save();
            OnLanguageChanged?.Invoke(language);
            Debug.Log($"Language changed to: {language}");
        }

        public Language GetCurrentLanguage() => currentLanguage;

        public void AddLocalizedText(string key, Language language, string text)
        {
            if (!localizedTexts.ContainsKey(key))
                localizedTexts[key] = new Dictionary<Language, string>();
            localizedTexts[key][language] = text;
        }

        public string GetLocalizedText(string key, params object[] args)
        {
            if (!localizedTexts.ContainsKey(key))
            {
                // Fallback to key itself or try to get from database
                // Check if key is an entry name
                return TryGetFromDatabase(key, args);
            }

            var translations = localizedTexts[key];
            string text = translations.ContainsKey(currentLanguage) ? translations[currentLanguage] : 
                         translations.ContainsKey(Language.English) ? translations[Language.English] : key;

            if (args.Length > 0)
            {
                try { return string.Format(text, args); }
                catch { return text; }
            }
            return text;
        }

        private string TryGetFromDatabase(string key, object[] args)
        {
            // Try to find in items, abilities, etc.
            // This allows using entry display names as localization keys
            return key;
        }

        public string Localize(string key) => GetLocalizedText(key);

        // Helper for UI
        public static string L(string key, params object[] args)
        {
            if (Instance == null) return key;
            return Instance.GetLocalizedText(key, args);
        }

        // Load from CSV or JSON
        public void LoadFromCSV(TextAsset csvFile)
        {
            if (csvFile == null) return;
            var lines = csvFile.text.Split('\n');
            if (lines.Length == 0) return;

            var headers = lines[0].Split(',');
            var languageIndices = new Dictionary<Language, int>();
            for (int i = 1; i < headers.Length; i++)
            {
                string header = headers[i].Trim();
                if (Enum.TryParse(header, out Language lang))
                    languageIndices[lang] = i;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                if (values.Length == 0) continue;
                string key = values[0].Trim();
                if (string.IsNullOrEmpty(key)) continue;

                foreach (var kvp in languageIndices)
                {
                    int idx = kvp.Value;
                    if (idx < values.Length)
                    {
                        string text = values[idx].Trim().Trim('"');
                        AddLocalizedText(key, kvp.Key, text);
                    }
                }
            }
            Debug.Log($"Loaded {localizedTexts.Count} localized strings from CSV");
        }

        // Pluralization support
        public string GetPlural(string singularKey, string pluralKey, int count)
        {
            return count == 1 ? GetLocalizedText(singularKey) : GetLocalizedText(pluralKey);
        }

        // Rich text with icons
        public string GetWithIcon(string key, Sprite icon)
        {
            string text = GetLocalizedText(key);
            // Would add icon tag for TextMeshPro
            return $"<sprite name=\"{icon?.name}\"> {text}";
        }
    }

    public class DailyQuestManager : MonoBehaviour
    {
        public static DailyQuestManager Instance { get; private set; }

        private DateTime lastDailyReset;
        private DateTime lastWeeklyReset;
        private List<int> availableDailyQuests = new List<int>();
        private List<int> availableWeeklyQuests = new List<int>();

        public Action OnDailyReset;
        public Action OnWeeklyReset;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            CheckResets();
            InvokeRepeating(nameof(CheckResets), 60f, 60f);
        }

        private void CheckResets()
        {
            var now = DateTime.Now;

            // Daily reset at midnight
            if (now.Date > lastDailyReset.Date)
            {
                ResetDailyQuests();
                lastDailyReset = now;
            }

            // Weekly reset on Monday
            if (now.Date > lastWeeklyReset.Date && now.DayOfWeek == DayOfWeek.Monday)
            {
                ResetWeeklyQuests();
                lastWeeklyReset = now;
            }
        }

        private void ResetDailyQuests()
        {
            Debug.Log("Daily Quests Reset");
            var allQuests = GameDatabase.Instance.GetQuests();
            availableDailyQuests.Clear();

            foreach (var kvp in allQuests)
            {
                if (kvp.Value.isDaily || kvp.Value.questType == RPGQuest.QuestType.Daily)
                    availableDailyQuests.Add(kvp.Key);
            }

            // Reset player daily completions
            if (Character.Instance?.CharacterData?.DailyQuests != null)
            {
                foreach (var dq in Character.Instance.CharacterData.DailyQuests)
                    dq.completionsToday = 0;
            }

            OnDailyReset?.Invoke();
        }

        private void ResetWeeklyQuests()
        {
            Debug.Log("Weekly Quests Reset");
            var allQuests = GameDatabase.Instance.GetQuests();
            availableWeeklyQuests.Clear();

            foreach (var kvp in allQuests)
            {
                if (kvp.Value.isWeekly || kvp.Value.questType == RPGQuest.QuestType.Weekly)
                    availableWeeklyQuests.Add(kvp.Key);
            }

            OnWeeklyReset?.Invoke();
        }

        public List<int> GetAvailableDailyQuests() => new List<int>(availableDailyQuests);
        public List<int> GetAvailableWeeklyQuests() => new List<int>(availableWeeklyQuests);
        public bool IsDailyQuest(int questID) => availableDailyQuests.Contains(questID);
        public bool IsWeeklyQuest(int questID) => availableWeeklyQuests.Contains(questID);
    }

    public class CraftingQualityManager : MonoBehaviour
    {
        public static CraftingQualityManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public RPGCraftingQuality RollQuality(int recipeID, int crafterSkillLevel, int characterLevel)
        {
            var allQualities = GameDatabase.Instance.GetCraftingQualities();
            if (allQualities == null || allQualities.Count == 0) return null;

            var recipe = GameDatabase.Instance.GetRecipes()[recipeID];
            float baseChance = recipe.baseSuccessChance;

            // Skill bonus
            baseChance += crafterSkillLevel * recipe.bonusChancePerSkillLevel;

            // Sort qualities by tier
            var sorted = allQualities.Values.OrderBy(q => q.tier).ToList();

            // Roll
            float roll = UnityEngine.Random.Range(0f, 100f);
            RPGCraftingQuality selected = sorted[0]; // default lowest

            foreach (var quality in sorted)
            {
                if (characterLevel < quality.requiredCharacterLevel) continue;
                if (crafterSkillLevel < quality.requiredSkillLevel) continue;

                if (roll <= quality.chanceToCraft)
                {
                    selected = quality;
                    break;
                }
                roll -= quality.chanceToCraft;
            }

            return selected;
        }

        public bool RollSuccess(int recipeID, int skillLevel)
        {
            var recipe = GameDatabase.Instance.GetRecipes()[recipeID];
            float successChance = recipe.baseSuccessChance + (skillLevel * recipe.bonusChancePerSkillLevel);
            return UnityEngine.Random.Range(0f, 100f) <= successChance;
        }
    }
}
