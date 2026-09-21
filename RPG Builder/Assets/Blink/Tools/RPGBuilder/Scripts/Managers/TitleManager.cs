
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BLINK.RPGBuilder.Templates;
using BLINK.RPGBuilder.Characters;

namespace BLINK.RPGBuilder.Managers
{
    public class TitleManager : MonoBehaviour
    {
        public static TitleManager Instance;
        private HashSet<int> unlockedTitles = new HashSet<int>();
        private int activeTitleID = -1;
        public System.Action<int> OnTitleUnlocked;
        public System.Action<int> OnTitleChanged;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsTitleUnlocked(int id) => unlockedTitles.Contains(id);
        public void UnlockTitle(int id)
        {
            if (unlockedTitles.Contains(id)) return;
            unlockedTitles.Add(id);
            OnTitleUnlocked?.Invoke(id);
            Debug.Log($"[Title] Unlocked {id}");
            if (GameDatabase.Instance.GetTitles().TryGetValue(id, out var title))
            {
                if (title.bonuses != null) foreach (var b in title.bonuses) BonusManager.Instance?.ApplyBonus(b);
            }
        }
        public void SetActiveTitle(int id) { activeTitleID = id; OnTitleChanged?.Invoke(id); }
        public int GetActiveTitle() => activeTitleID;
        public List<int> GetUnlockedTitles() => unlockedTitles.ToList();
        public string GetActiveTitleText()
        {
            if (activeTitleID == -1) return "";
            if (GameDatabase.Instance.GetTitles().TryGetValue(activeTitleID, out var t)) return t.titleText;
            return "";
        }
    }
}
