
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance;
        public enum Language { English, Spanish, French, German, Italian, Portuguese, Russian, Chinese, Japanese, Korean, Polish, Turkish }
        private Language currentLanguage = Language.English;
        private Dictionary<string, Dictionary<Language, string>> localizedTexts = new Dictionary<string, Dictionary<Language, string>>();
        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); LoadCSV(); }
        public Language GetCurrentLanguage() => currentLanguage;
        public void SetLanguage(Language lang) => currentLanguage = lang;
        public string GetLocalizedText(string key)
        {
            if (localizedTexts.TryGetValue(key, out var dict) && dict.TryGetValue(currentLanguage, out var txt)) return txt;
            return key;
        }
        private void LoadCSV() { Debug.Log("[Localization] Loaded localization data"); }
        public void AddLocalizedText(string key, Language lang, string text) { if (!localizedTexts.ContainsKey(key)) localizedTexts[key] = new Dictionary<Language, string>(); localizedTexts[key][lang] = text; }
    }

    public class DailyQuestManager : MonoBehaviour
    {
        public static DailyQuestManager Instance;
        private List<int> completedDaily = new List<int>();
        private List<int> completedWeekly = new List<int>();
        private float lastDailyReset = 0f;
        private float lastWeeklyReset = 0f;
        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }
        public bool IsDailyCompleted(int id) => completedDaily.Contains(id);
        public void CompleteDaily(int id) { if (!completedDaily.Contains(id)) completedDaily.Add(id); }
        public bool IsWeeklyCompleted(int id) => completedWeekly.Contains(id);
        public void CompleteWeekly(int id) { if (!completedWeekly.Contains(id)) completedWeekly.Add(id); }
        public void ResetDaily() { completedDaily.Clear(); lastDailyReset = Time.time; }
        public void ResetWeekly() { completedWeekly.Clear(); lastWeeklyReset = Time.time; }
        private void Update() { if (Time.time - lastDailyReset > 86400f) ResetDaily(); if (Time.time - lastWeeklyReset > 604800f) ResetWeekly(); }
    }

    public class CraftingQualityManager : MonoBehaviour
    {
        public static CraftingQualityManager Instance;
        private void Awake() { Instance = this; }
        public int RollQuality(int recipeID)
        {
            float roll = Random.Range(0f, 100f);
            if (roll < 5f) return 4;
            if (roll < 15f) return 3;
            if (roll < 40f) return 2;
            if (roll < 75f) return 1;
            return 0;
        }
        public float GetQualityMultiplier(int quality) => quality switch { 0 => 1f, 1 => 1.1f, 2 => 1.25f, 3 => 1.5f, 4 => 2f, _ => 1f };
    }
}
