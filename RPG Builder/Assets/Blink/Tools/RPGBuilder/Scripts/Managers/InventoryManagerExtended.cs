using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Characters;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public static class InventoryManagerExtended
    {
        public static bool HasDurability(int itemDataID)
        {
            if (Character.Instance?.CharacterData?.ItemDurabilities == null) return false;
            return Character.Instance.CharacterData.ItemDurabilities.Any(d => d.itemDataID == itemDataID);
        }

        public static int GetDurability(int itemDataID)
        {
            var entry = Character.Instance?.CharacterData?.ItemDurabilities?.FirstOrDefault(d => d.itemDataID == itemDataID);
            return entry?.currentDurability ?? 100;
        }

        public static void SetDurability(int itemDataID, int durability)
        {
            if (Character.Instance?.CharacterData?.ItemDurabilities == null)
                Character.Instance.CharacterData.ItemDurabilities = new List<CharacterEntries.ItemDurabilityEntry>();

            var entry = Character.Instance.CharacterData.ItemDurabilities.FirstOrDefault(d => d.itemDataID == itemDataID);
            if (entry == null)
            {
                entry = new CharacterEntries.ItemDurabilityEntry { itemDataID = itemDataID, currentDurability = durability, maxDurability = 100 };
                Character.Instance.CharacterData.ItemDurabilities.Add(entry);
            }
            else
            {
                entry.currentDurability = Mathf.Clamp(durability, 0, entry.maxDurability);
            }
        }

        public static void DamageItem(int itemDataID, int amount)
        {
            int current = GetDurability(itemDataID);
            SetDurability(itemDataID, current - amount);
            if (GetDurability(itemDataID) <= 0)
            {
                // Item broken
                Debug.Log($"Item {itemDataID} broken!");
            }
        }

        public static bool RepairItem(int itemDataID)
        {
            var itemEntry = Character.Instance.CharacterData.ItemEntries.FirstOrDefault(i => i.id == itemDataID);
            if (itemEntry == null) return false;

            var item = GameDatabase.Instance.GetItems()[itemEntry.itemID];
            if (!item.canBeRepaired) return false;

            int cost = Mathf.RoundToInt(item.buyPrice * item.repairCostMultiplier);
            if (item.repairCurrencyID != -1)
            {
                if (!InventoryManager.Instance.HasCurrency(item.repairCurrencyID, cost)) return false;
                InventoryManager.Instance.RemoveCurrency(item.repairCurrencyID, cost);
            }

            SetDurability(itemDataID, 100);
            return true;
        }

        public static void RepairAllItems()
        {
            foreach (var item in Character.Instance.CharacterData.ItemEntries)
            {
                if (item.id != -1) RepairItem(item.id);
            }
        }

        public static bool IsItemBroken(int itemDataID) => GetDurability(itemDataID) <= 0;

        public static int GetAverageItemLevel()
        {
            int total = 0;
            int count = 0;
            foreach (var equipped in Character.Instance.CharacterData.ArmorPiecesEquipped)
            {
                if (equipped.itemID == -1) continue;
                var item = GameDatabase.Instance.GetItems()[equipped.itemID];
                total += item.itemLevel;
                count++;
            }
            foreach (var equipped in Character.Instance.CharacterData.WeaponsEquipped)
            {
                if (equipped.itemID == -1) continue;
                var item = GameDatabase.Instance.GetItems()[equipped.itemID];
                total += item.itemLevel;
                count++;
            }
            return count > 0 ? total / count : Character.Instance.Level;
        }

        public static bool CanEquipItem(int itemID)
        {
            var item = GameDatabase.Instance.GetItems()[itemID];
            if (Character.Instance.Level < item.requiredLevel) return false;
            if (GetAverageItemLevel() < item.requiredItemLevel) return false;
            if (item.isUnique && HasUniqueEquipped(itemID)) return false;
            return RequirementsManager.Instance.CheckRequirements(item.Requirements, item.RequirementsTemplate);
        }

        private static bool HasUniqueEquipped(int itemID)
        {
            return Character.Instance.CharacterData.ArmorPiecesEquipped.Any(a => a.itemID == itemID) ||
                   Character.Instance.CharacterData.WeaponsEquipped.Any(w => w.itemID == itemID);
        }

        public static void SortInventoryByRarity()
        {
            var inventory = Character.Instance.CharacterData.Inventory.baseSlots;
            inventory.Sort((a, b) =>
            {
                if (a.itemID == -1 || b.itemID == -1) return 0;
                var itemA = GameDatabase.Instance.GetItems()[a.itemID];
                var itemB = GameDatabase.Instance.GetItems()[b.itemID];
                // Sort by rarity tier - would need to compare rarity enum
                return itemB.ItemRarity.entryName.CompareTo(itemA.ItemRarity.entryName);
            });
        }

        public static void SortInventoryByType()
        {
            var inventory = Character.Instance.CharacterData.Inventory.baseSlots;
            inventory.Sort((a, b) =>
            {
                if (a.itemID == -1 || b.itemID == -1) return 0;
                var itemA = GameDatabase.Instance.GetItems()[a.itemID];
                var itemB = GameDatabase.Instance.GetItems()[b.itemID];
                return itemA.ItemType.entryName.CompareTo(itemB.ItemType.entryName);
            });
        }

        public static List<int> SearchItems(string searchTerm)
        {
            var result = new List<int>();
            var allItems = GameDatabase.Instance.GetItems();
            foreach (var kvp in allItems)
            {
                if (kvp.Value.entryDisplayName.ToLower().Contains(searchTerm.ToLower()) ||
                    kvp.Value.entryDescription.ToLower().Contains(searchTerm.ToLower()))
                {
                    result.Add(kvp.Key);
                }
            }
            return result;
        }
    }

    public class EquipmentSetManager : MonoBehaviour
    {
        public static EquipmentSetManager Instance { get; private set; }

        [System.Serializable]
        public class EquipmentSet
        {
            public string setName;
            public List<CharacterEntries.ArmorEquippedEntry> armor = new List<CharacterEntries.ArmorEquippedEntry>();
            public List<CharacterEntries.WeaponEquippedEntry> weapons = new List<CharacterEntries.WeaponEquippedEntry>();
        }

        private List<EquipmentSet> savedSets = new List<EquipmentSet>();
        private const int MaxSets = 10;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void SaveCurrentSet(string setName)
        {
            if (savedSets.Count >= MaxSets) return;
            var set = new EquipmentSet
            {
                setName = setName,
                armor = new List<CharacterEntries.ArmorEquippedEntry>(Character.Instance.CharacterData.ArmorPiecesEquipped),
                weapons = new List<CharacterEntries.WeaponEquippedEntry>(Character.Instance.CharacterData.WeaponsEquipped)
            };
            savedSets.Add(set);
            Debug.Log($"Equipment Set Saved: {setName}");
        }

        public void LoadSet(int index)
        {
            if (index < 0 || index >= savedSets.Count) return;
            var set = savedSets[index];
            Character.Instance.CharacterData.ArmorPiecesEquipped = new List<CharacterEntries.ArmorEquippedEntry>(set.armor);
            Character.Instance.CharacterData.WeaponsEquipped = new List<CharacterEntries.WeaponEquippedEntry>(set.weapons);
            InventoryManager.Instance.InitEquippedItems();
            Debug.Log($"Equipment Set Loaded: {set.setName}");
        }

        public List<EquipmentSet> GetSavedSets() => new List<EquipmentSet>(savedSets);
    }

    public class AutoLootManager : MonoBehaviour
    {
        public static AutoLootManager Instance { get; private set; }

        public bool autoLootEnabled = true;
        public bool areaLootEnabled = true;
        public float areaLootRadius = 10f;
        public bool autoSellJunk = false;
        public bool autoRepair = false;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void TryAutoLoot(LootBag bag)
        {
            if (!autoLootEnabled) return;
            // Loot all
            bag.LootAll();
        }

        public void TryAreaLoot(Vector3 position)
        {
            if (!areaLootEnabled) return;
            var bags = FindObjectsByType<LootBag>(FindObjectsSortMode.None);
            foreach (var bag in bags)
            {
                if (Vector3.Distance(position, bag.transform.position) <= areaLootRadius)
                    TryAutoLoot(bag);
            }
        }

        public void TryAutoSellJunk()
        {
            if (!autoSellJunk) return;
            // Sell poor quality items
            var inventory = Character.Instance.CharacterData.Inventory.baseSlots;
            foreach (var slot in inventory.ToList())
            {
                if (slot.itemID == -1) continue;
                var item = GameDatabase.Instance.GetItems()[slot.itemID];
                // Check if junk (poor rarity)
                if (item.ItemRarity != null && item.ItemRarity.entryName.ToLower().Contains("poor"))
                {
                    InventoryManager.Instance.RemoveItem(item.ID, slot.itemStack);
                    InventoryManager.Instance.AddCurrency(item.sellCurrencyID, item.sellPrice * slot.itemStack);
                }
            }
        }
    }
}
