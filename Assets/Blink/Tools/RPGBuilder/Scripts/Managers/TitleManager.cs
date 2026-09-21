using System;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Characters;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class TitleManager : MonoBehaviour
    {
        public static TitleManager Instance { get; private set; }

        private List<int> unlockedTitles = new List<int>();
        private int activeTitleID = -1;

        public Action<RPGTitle> OnTitleUnlocked;
        public Action<RPGTitle> OnTitleEquipped;
        public Action OnTitleUnequipped;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadTitles();
        }

        private void LoadTitles()
        {
            if (Character.Instance != null && Character.Instance.CharacterData != null)
            {
                unlockedTitles = new List<int>(Character.Instance.CharacterData.UnlockedTitles ?? new List<int>());
                activeTitleID = Character.Instance.CharacterData.ActiveTitleID;
            }
        }

        public void UnlockTitle(int titleID)
        {
            if (unlockedTitles.Contains(titleID)) return;
            if (!GameDatabase.Instance.GetTitles().ContainsKey(titleID)) return;

            unlockedTitles.Add(titleID);
            var title = GameDatabase.Instance.GetTitles()[titleID];
            Debug.Log($"Title Unlocked: {title.titleText}");

            // Game Actions
            if (title.UseGameActionsTemplate && title.GameActionsTemplate != null)
                GameActionsManager.Instance.ExecuteGameActions(title.GameActionsTemplate.GameActions, null, null);
            else
                GameActionsManager.Instance.ExecuteGameActions(title.GameActions, null, null);

            OnTitleUnlocked?.Invoke(title);
            SaveTitles();
        }

        public void EquipTitle(int titleID)
        {
            if (!unlockedTitles.Contains(titleID)) return;
            activeTitleID = titleID;
            var title = GameDatabase.Instance.GetTitles()[titleID];
            OnTitleEquipped?.Invoke(title);
            SaveTitles();
        }

        public void UnequipTitle()
        {
            activeTitleID = -1;
            OnTitleUnequipped?.Invoke();
            SaveTitles();
        }

        public bool IsTitleUnlocked(int titleID) => unlockedTitles.Contains(titleID);
        public bool HasActiveTitle() => activeTitleID != -1;
        public RPGTitle GetActiveTitle()
        {
            if (activeTitleID == -1) return null;
            var titles = GameDatabase.Instance.GetTitles();
            return titles.ContainsKey(activeTitleID) ? titles[activeTitleID] : null;
        }

        public string GetFormattedTitle(string playerName)
        {
            var active = GetActiveTitle();
            if (active == null) return playerName;

            string formatted = active.titleText.Replace("{playerName}", playerName);
            if (active.isPrefix && !string.IsNullOrEmpty(active.prefixText))
                formatted = active.prefixText + " " + playerName;
            else if (active.isSuffix && !string.IsNullOrEmpty(active.suffixText))
                formatted = playerName + " " + active.suffixText;

            return formatted;
        }

        public List<RPGTitle> GetUnlockedTitles()
        {
            var result = new List<RPGTitle>();
            var all = GameDatabase.Instance.GetTitles();
            foreach (int id in unlockedTitles)
                if (all.ContainsKey(id)) result.Add(all[id]);
            return result;
        }

        public List<RPGTitle> GetTitlesByCategory(RPGTitle.TitleCategory category)
        {
            return GetUnlockedTitles().Where(t => t.category == category).ToList();
        }

        private void SaveTitles()
        {
            if (Character.Instance?.CharacterData == null) return;
            Character.Instance.CharacterData.UnlockedTitles = new List<int>(unlockedTitles);
            Character.Instance.CharacterData.ActiveTitleID = activeTitleID;
        }
    }
}
