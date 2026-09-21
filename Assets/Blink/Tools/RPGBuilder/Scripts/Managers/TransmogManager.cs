using System;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Characters;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class TransmogManager : MonoBehaviour
    {
        public static TransmogManager Instance { get; private set; }

        private List<int> unlockedAppearances = new List<int>();
        private Dictionary<string, int> equippedTransmog = new Dictionary<string, int>(); // slot -> transmogID

        public Action<RPGTransmog> OnAppearanceUnlocked;
        public Action<string, RPGTransmog> OnTransmogEquipped;
        public Action<string> OnTransmogRemoved;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadTransmog();
        }

        private void LoadTransmog()
        {
            if (Character.Instance?.CharacterData != null)
            {
                unlockedAppearances = new List<int>(Character.Instance.CharacterData.UnlockedTransmog ?? new List<int>());
                if (Character.Instance.CharacterData.EquippedTransmog != null)
                {
                    foreach (var entry in Character.Instance.CharacterData.EquippedTransmog)
                        equippedTransmog[entry.slotName] = entry.transmogID;
                }
            }
        }

        public void UnlockAppearance(int transmogID)
        {
            if (unlockedAppearances.Contains(transmogID)) return;
            var all = GameDatabase.Instance.GetTransmog();
            if (!all.ContainsKey(transmogID)) return;

            unlockedAppearances.Add(transmogID);
            OnAppearanceUnlocked?.Invoke(all[transmogID]);
            Debug.Log($"Transmog Unlocked: {all[transmogID].entryDisplayName}");
            SaveTransmog();
        }

        public void UnlockAppearanceFromItem(int itemID)
        {
            var all = GameDatabase.Instance.GetTransmog();
            var transmog = all.Values.FirstOrDefault(t => t.sourceItemID == itemID);
            if (transmog != null) UnlockAppearance(transmog.ID);
        }

        public void EquipTransmog(string slotName, int transmogID)
        {
            if (!unlockedAppearances.Contains(transmogID)) return;
            equippedTransmog[slotName] = transmogID;
            var all = GameDatabase.Instance.GetTransmog();
            if (all.ContainsKey(transmogID))
                OnTransmogEquipped?.Invoke(slotName, all[transmogID]);
            SaveTransmog();
            ApplyTransmogVisuals();
        }

        public void RemoveTransmog(string slotName)
        {
            if (equippedTransmog.ContainsKey(slotName))
            {
                equippedTransmog.Remove(slotName);
                OnTransmogRemoved?.Invoke(slotName);
                SaveTransmog();
                ApplyTransmogVisuals();
            }
        }

        private void ApplyTransmogVisuals()
        {
            // Would update character appearance
            var appearance = Character.Instance.GetComponent<PlayerAppearance>();
            if (appearance == null) return;

            foreach (var kvp in equippedTransmog)
            {
                var all = GameDatabase.Instance.GetTransmog();
                if (!all.ContainsKey(kvp.Value)) continue;
                var transmog = all[kvp.Value];
                // Apply model/material override
            }
        }

        public bool IsAppearanceUnlocked(int transmogID) => unlockedAppearances.Contains(transmogID);
        public List<RPGTransmog> GetUnlockedAppearances()
        {
            var result = new List<RPGTransmog>();
            var all = GameDatabase.Instance.GetTransmog();
            foreach (int id in unlockedAppearances)
                if (all.ContainsKey(id)) result.Add(all[id]);
            return result;
        }

        public RPGTransmog GetEquippedTransmog(string slotName)
        {
            if (!equippedTransmog.ContainsKey(slotName)) return null;
            var all = GameDatabase.Instance.GetTransmog();
            return all.ContainsKey(equippedTransmog[slotName]) ? all[equippedTransmog[slotName]] : null;
        }

        private void SaveTransmog()
        {
            if (Character.Instance?.CharacterData == null) return;
            Character.Instance.CharacterData.UnlockedTransmog = new List<int>(unlockedAppearances);
            var list = new List<CharacterEntries.TransmogEntry>();
            foreach (var kvp in equippedTransmog)
                list.Add(new CharacterEntries.TransmogEntry { slotName = kvp.Key, transmogID = kvp.Value });
            Character.Instance.CharacterData.EquippedTransmog = list;
        }
    }

    public class BankManager : MonoBehaviour
    {
        public static BankManager Instance { get; private set; }

        private List<CharacterEntries.ItemEntry> bankItems = new List<CharacterEntries.ItemEntry>();
        private int bankSlots = 100;
        private bool isBankOpen = false;

        public Action OnBankOpened;
        public Action OnBankClosed;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadBank();
        }

        private void LoadBank()
        {
            if (Character.Instance?.CharacterData?.BankItems != null)
                bankItems = new List<CharacterEntries.ItemEntry>(Character.Instance.CharacterData.BankItems);
            bankSlots = GameDatabase.Instance.GetEconomySettings()?.BankSlots ?? 100;
        }

        public void OpenBank()
        {
            isBankOpen = true;
            OnBankOpened?.Invoke();
        }

        public void CloseBank()
        {
            isBankOpen = false;
            OnBankClosed?.Invoke();
            SaveBank();
        }

        public bool AddItemToBank(int itemID, int count)
        {
            // Simplified
            var existing = bankItems.FirstOrDefault(b => b.itemID == itemID);
            if (existing != null)
            {
                existing.count += count;
            }
            else
            {
                if (bankItems.Count >= bankSlots) return false;
                bankItems.Add(new CharacterEntries.ItemEntry { itemID = itemID, count = count });
            }
            SaveBank();
            return true;
        }

        public bool RemoveItemFromBank(int itemID, int count)
        {
            var existing = bankItems.FirstOrDefault(b => b.itemID == itemID);
            if (existing == null || existing.count < count) return false;
            existing.count -= count;
            if (existing.count <= 0) bankItems.Remove(existing);
            SaveBank();
            return true;
        }

        public List<CharacterEntries.ItemEntry> GetBankItems() => new List<CharacterEntries.ItemEntry>(bankItems);
        public int GetBankSlots() => bankSlots;
        public int GetUsedSlots() => bankItems.Count;
        public bool IsBankOpen() => isBankOpen;

        private void SaveBank()
        {
            if (Character.Instance?.CharacterData == null) return;
            Character.Instance.CharacterData.BankItems = new List<CharacterEntries.ItemEntry>(bankItems);
        }
    }

    public class ParagonManager : MonoBehaviour
    {
        public static ParagonManager Instance { get; private set; }

        private Dictionary<int, int> paragonLevels = new Dictionary<int, int>();
        private Dictionary<int, int> paragonExp = new Dictionary<int, int>();
        private Dictionary<int, List<int>> unlockedNodes = new Dictionary<int, List<int>>();
        private Dictionary<int, Dictionary<int, int>> nodeRanks = new Dictionary<int, Dictionary<int, int>>();

        public Action<int, int> OnParagonLevelUp; // paragonID, newLevel
        public Action<int, int> OnParagonNodeUnlocked; // paragonID, nodeIndex

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadParagon();
        }

        private void LoadParagon()
        {
            if (Character.Instance?.CharacterData?.ParagonData != null)
            {
                foreach (var data in Character.Instance.CharacterData.ParagonData)
                {
                    paragonLevels[data.paragonID] = data.level;
                    paragonExp[data.paragonID] = data.exp;
                }
            }
        }

        public void AddParagonExp(int paragonID, int amount)
        {
            var all = GameDatabase.Instance.GetParagons();
            if (!all.ContainsKey(paragonID)) return;
            var paragon = all[paragonID];

            if (!paragonLevels.ContainsKey(paragonID)) paragonLevels[paragonID] = 1;
            if (!paragonExp.ContainsKey(paragonID)) paragonExp[paragonID] = 0;

            paragonExp[paragonID] += amount;

            int required = GetRequiredExp(paragon, paragonLevels[paragonID]);
            while (paragonExp[paragonID] >= required && paragonLevels[paragonID] < paragon.maxLevel)
            {
                paragonExp[paragonID] -= required;
                paragonLevels[paragonID]++;
                OnParagonLevelUp?.Invoke(paragonID, paragonLevels[paragonID]);

                GameActionsManager.Instance.ExecuteGameActions(paragon.OnLevelUpActions, null, null);
                Debug.Log($"Paragon Level Up: {paragon.entryDisplayName} now {paragonLevels[paragonID]}");

                required = GetRequiredExp(paragon, paragonLevels[paragonID]);
            }

            SaveParagon();
        }

        private int GetRequiredExp(RPGParagon paragon, int currentLevel)
        {
            return Mathf.RoundToInt(paragon.expPerLevel * Mathf.Pow(paragon.expMultiplierPerLevel, currentLevel - 1));
        }

        public bool CanUnlockNode(int paragonID, int nodeIndex)
        {
            var all = GameDatabase.Instance.GetParagons();
            if (!all.ContainsKey(paragonID)) return false;
            var paragon = all[paragonID];
            if (nodeIndex >= paragon.nodes.Count) return false;

            var node = paragon.nodes[nodeIndex];
            if (paragonLevels.ContainsKey(paragonID) && paragonLevels[paragonID] < node.requiredParagonLevel) return false;

            // Check required nodes
            foreach (int reqID in node.requiredNodeIDs)
            {
                if (!unlockedNodes.ContainsKey(paragonID) || !unlockedNodes[paragonID].Contains(reqID))
                    return false;
            }

            // Check requirements
            if (!RequirementsManager.Instance.CheckRequirements(node.Requirements)) return false;

            return true;
        }

        public void UnlockNode(int paragonID, int nodeIndex)
        {
            if (!CanUnlockNode(paragonID, nodeIndex)) return;

            if (!unlockedNodes.ContainsKey(paragonID)) unlockedNodes[paragonID] = new List<int>();
            if (!unlockedNodes[paragonID].Contains(nodeIndex))
            {
                unlockedNodes[paragonID].Add(nodeIndex);
                if (!nodeRanks.ContainsKey(paragonID)) nodeRanks[paragonID] = new Dictionary<int, int>();
                nodeRanks[paragonID][nodeIndex] = 1;

                var all = GameDatabase.Instance.GetParagons();
                var node = all[paragonID].nodes[nodeIndex];
                GameActionsManager.Instance.ExecuteGameActions(node.OnRankUpActions, null, null);

                OnParagonNodeUnlocked?.Invoke(paragonID, nodeIndex);
                SaveParagon();
            }
        }

        public int GetParagonLevel(int paragonID) => paragonLevels.ContainsKey(paragonID) ? paragonLevels[paragonID] : 1;
        public int GetParagonExp(int paragonID) => paragonExp.ContainsKey(paragonID) ? paragonExp[paragonID] : 0;

        private void SaveParagon()
        {
            if (Character.Instance?.CharacterData == null) return;
            var list = new List<CharacterEntries.ParagonEntry>();
            foreach (var kvp in paragonLevels)
            {
                list.Add(new CharacterEntries.ParagonEntry
                {
                    paragonID = kvp.Key,
                    level = kvp.Value,
                    exp = paragonExp.ContainsKey(kvp.Key) ? paragonExp[kvp.Key] : 0
                });
            }
            Character.Instance.CharacterData.ParagonData = list;
        }
    }
}
