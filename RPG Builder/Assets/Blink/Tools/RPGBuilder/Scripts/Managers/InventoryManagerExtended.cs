
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Managers
{
    public class InventoryManagerExtended : MonoBehaviour
    {
        public static InventoryManagerExtended Instance;
        private void Awake() { Instance = this; }

        public static int GetAverageItemLevel()
        {
            int total = 0; int count = 0;
            if (BLINK.RPGBuilder.Characters.Character.Instance == null) return 0;
            var inv = InventoryManager.Instance;
            if (inv == null) return 0;
            foreach (var item in inv.GetEquippedItems())
            {
                if (item == null) continue;
                total += item.itemLevel;
                count++;
            }
            return count == 0 ? 0 : total / count;
        }

        public static bool HasDurability(int itemID) => true;
        public static float GetDurability(int itemID) => 100f;
        public static void DamageDurability(int itemID, float amount) { }
        public static void RepairItem(int itemID) { }
        public static void RepairAll() { Debug.Log("[InventoryExtended] Repaired all items"); }
    }

    public class EquipmentSetManager : MonoBehaviour
    {
        public static EquipmentSetManager Instance;
        private Dictionary<int, List<int>> equipmentSets = new Dictionary<int, List<int>>();
        private void Awake() { Instance = this; }
        public void SaveSet(int setID, List<int> itemIDs) => equipmentSets[setID] = new List<int>(itemIDs);
        public List<int> GetSet(int setID) => equipmentSets.TryGetValue(setID, out var set) ? set : new List<int>();
        public void EquipSet(int setID) { Debug.Log($"[EquipmentSet] Equipped set {setID}"); }
    }

    public class AutoLootManager : MonoBehaviour
    {
        public static AutoLootManager Instance;
        public bool autoLootEnabled = true;
        public bool autoLootCurrency = true;
        public bool autoLootQuestItems = true;
        public int autoLootRarityThreshold = 0;
        private void Awake() { Instance = this; }
        public bool ShouldAutoLoot(RPGItem item) { if (!autoLootEnabled) return false; if (item == null) return false; return true; }
    }
}
