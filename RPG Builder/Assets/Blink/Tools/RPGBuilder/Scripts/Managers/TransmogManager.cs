
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class TransmogManager : MonoBehaviour
    {
        public static TransmogManager Instance;
        private HashSet<int> unlockedTransmogs = new HashSet<int>();
        private Dictionary<int, int> equippedTransmogs = new Dictionary<int, int>();
        public System.Action<int> OnTransmogUnlocked;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsUnlocked(int id) => unlockedTransmogs.Contains(id);
        public void Unlock(int id) { if (unlockedTransmogs.Contains(id)) return; unlockedTransmogs.Add(id); OnTransmogUnlocked?.Invoke(id); }
        public void ApplyTransmog(int slot, int transmogID) { if (!IsUnlocked(transmogID)) return; equippedTransmogs[slot] = transmogID; }
        public void RemoveTransmog(int slot) => equippedTransmogs.Remove(slot);
        public List<int> GetUnlocked() => unlockedTransmogs.ToList();
    }

    public class BankManager : MonoBehaviour
    {
        public static BankManager Instance;
        private List<int> bankItems = new List<int>();
        private int bankSlots = 50;
        private int currency = 0;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool DepositItem(int itemID) { if (bankItems.Count >= bankSlots) return false; bankItems.Add(itemID); return true; }
        public bool WithdrawItem(int itemID) => bankItems.Remove(itemID);
        public List<int> GetBankItems() => bankItems;
        public int GetUsedSlots() => bankItems.Count;
        public int GetBankSlots() => bankSlots;
        public void ExpandSlots(int amount) => bankSlots += amount;
    }

    public class ParagonManager : MonoBehaviour
    {
        public static ParagonManager Instance;
        private int paragonLevel = 0;
        private float paragonExp = 0f;
        private Dictionary<int, int> paragonPoints = new Dictionary<int, int>();

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public int GetLevel() => paragonLevel;
        public float GetExp() => paragonExp;
        public void AddExp(float amount)
        {
            paragonExp += amount;
            float needed = 1000f * Mathf.Pow(1.1f, paragonLevel);
            while (paragonExp >= needed) { paragonExp -= needed; paragonLevel++; needed = 1000f * Mathf.Pow(1.1f, paragonLevel); }
        }
        public void AddPoint(int treeID, int amount = 1) { paragonPoints[treeID] = GetPoints(treeID) + amount; }
        public int GetPoints(int treeID) => paragonPoints.TryGetValue(treeID, out var p) ? p : 0;
    }
}
