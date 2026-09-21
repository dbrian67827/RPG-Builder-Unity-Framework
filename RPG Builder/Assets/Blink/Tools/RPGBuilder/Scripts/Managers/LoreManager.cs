
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Managers
{
    public class LoreManager : MonoBehaviour
    {
        public static LoreManager Instance;
        private HashSet<int> unlockedLore = new HashSet<int>();
        public System.Action<int> OnLoreUnlocked;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsLoreUnlocked(int id) => unlockedLore.Contains(id);
        public void UnlockLore(int id) { if (unlockedLore.Contains(id)) return; unlockedLore.Add(id); OnLoreUnlocked?.Invoke(id); Debug.Log($"[Lore] Unlocked {id}"); }
        public List<int> GetUnlockedLore() => unlockedLore.ToList();
        public int GetTotalLore() => GameDatabase.Instance.GetLore().Count;
        public float GetCompletionPercent() => GetTotalLore() == 0 ? 0 : (float)unlockedLore.Count / GetTotalLore() * 100f;
    }

    public class BestiaryManager : MonoBehaviour
    {
        public static BestiaryManager Instance;
        private Dictionary<int, int> killCounts = new Dictionary<int, int>();
        private HashSet<int> discovered = new HashSet<int>();
        public System.Action<int> OnBestiaryDiscovered;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsDiscovered(int id) => discovered.Contains(id);
        public void Discover(int id) { if (discovered.Contains(id)) return; discovered.Add(id); OnBestiaryDiscovered?.Invoke(id); }
        public void AddKill(int id, int count = 1) { Discover(id); killCounts[id] = GetKillCount(id) + count; }
        public int GetKillCount(int id) => killCounts.TryGetValue(id, out var c) ? c : 0;
        public List<int> GetDiscovered() => discovered.ToList();
    }

    public class MailManager : MonoBehaviour
    {
        public static MailManager Instance;
        private List<MailEntry> inbox = new List<MailEntry>();
        public System.Action<MailEntry> OnMailReceived;

        public class MailEntry { public int id; public string sender; public string subject; public string body; public List<int> itemIDs; public int currencyID; public int currencyAmount; public bool isRead; public float expiry; }

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public void SendMail(string sender, string subject, string body, List<int> items = null, int curID = -1, int curAmt = 0)
        {
            var mail = new MailEntry { id = inbox.Count, sender = sender, subject = subject, body = body, itemIDs = items ?? new List<int>(), currencyID = curID, currencyAmount = curAmt, isRead = false, expiry = Time.time + 2592000f };
            inbox.Add(mail);
            OnMailReceived?.Invoke(mail);
        }
        public List<MailEntry> GetInbox() => inbox;
        public int GetUnreadCount() => inbox.Count(m => !m.isRead);
    }
}
