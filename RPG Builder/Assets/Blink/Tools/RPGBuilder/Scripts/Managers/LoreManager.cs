using System;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Characters;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class LoreManager : MonoBehaviour
    {
        public static LoreManager Instance { get; private set; }

        private List<int> unlockedLore = new List<int>();
        public Action<RPGLore> OnLoreUnlocked;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadLore();
        }

        private void LoadLore()
        {
            if (Character.Instance?.CharacterData?.UnlockedLore != null)
                unlockedLore = new List<int>(Character.Instance.CharacterData.UnlockedLore);

            // Unlock defaults
            var all = GameDatabase.Instance.GetLore();
            if (all != null)
            {
                foreach (var kvp in all)
                {
                    if (kvp.Value.isUnlockedByDefault && !unlockedLore.Contains(kvp.Key))
                        UnlockLore(kvp.Key, false);
                }
            }
        }

        public void UnlockLore(int loreID, bool showNotification = true)
        {
            if (unlockedLore.Contains(loreID)) return;
            var all = GameDatabase.Instance.GetLore();
            if (!all.ContainsKey(loreID)) return;

            unlockedLore.Add(loreID);
            var lore = all[loreID];

            if (showNotification && lore.showNotificationOnUnlock)
                AlertMessagesDisplay.Instance?.ShowMessage(lore.unlockMessage);

            // Rewards
            foreach (var reward in lore.rewards)
            {
                switch (reward.rewardType)
                {
                    case RPGLore.LoreReward.RewardType.Item:
                        InventoryManager.Instance.AddItem(reward.itemID, reward.amount);
                        break;
                    case RPGLore.LoreReward.RewardType.Currency:
                        InventoryManager.Instance.AddCurrency(reward.currencyID, reward.amount);
                        break;
                    case RPGLore.LoreReward.RewardType.Experience:
                        LevelingManager.Instance.AddExperience(reward.amount);
                        break;
                    case RPGLore.LoreReward.RewardType.Title:
                        TitleManager.Instance?.UnlockTitle(reward.titleID);
                        break;
                }
            }

            GameActionsManager.Instance.ExecuteGameActions(lore.OnUnlockActions, null, null);
            OnLoreUnlocked?.Invoke(lore);
            SaveLore();

            Debug.Log($"Lore Unlocked: {lore.title}");
        }

        public bool IsLoreUnlocked(int loreID) => unlockedLore.Contains(loreID);
        public List<RPGLore> GetUnlockedLore()
        {
            var result = new List<RPGLore>();
            var all = GameDatabase.Instance.GetLore();
            foreach (int id in unlockedLore)
                if (all.ContainsKey(id)) result.Add(all[id]);
            return result;
        }

        public List<RPGLore> GetLoreByCategory(RPGLore.LoreCategory category)
        {
            return GetUnlockedLore().Where(l => l.category == category).ToList();
        }

        public float GetCollectionProgress(string collectionName)
        {
            var all = GameDatabase.Instance.GetLore();
            if (all == null) return 0f;
            var collection = all.Values.Where(l => l.isPartOfCollection && l.collectionName == collectionName).ToList();
            if (collection.Count == 0) return 0f;
            int unlockedInCollection = collection.Count(l => unlockedLore.Contains(l.ID));
            return (float)unlockedInCollection / collection.Count;
        }

        private void SaveLore()
        {
            if (Character.Instance?.CharacterData == null) return;
            Character.Instance.CharacterData.UnlockedLore = new List<int>(unlockedLore);
        }
    }

    public class BestiaryManager : MonoBehaviour
    {
        public static BestiaryManager Instance { get; private set; }
        private Dictionary<int, int> creatureKills = new Dictionary<int, int>();
        private List<int> unlockedBestiary = new List<int>();

        public Action<RPGBestiary> OnBestiaryUnlocked;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadBestiary();
        }

        private void LoadBestiary()
        {
            if (Character.Instance?.CharacterData?.BestiaryKills != null)
            {
                foreach (var entry in Character.Instance.CharacterData.BestiaryKills)
                    creatureKills[entry.npcID] = entry.killCount;
            }
            if (Character.Instance?.CharacterData?.UnlockedBestiary != null)
                unlockedBestiary = new List<int>(Character.Instance.CharacterData.UnlockedBestiary);
        }

        public void AddKill(int npcID)
        {
            if (!creatureKills.ContainsKey(npcID)) creatureKills[npcID] = 0;
            creatureKills[npcID]++;

            var all = GameDatabase.Instance.GetBestiary();
            var entry = all.Values.FirstOrDefault(b => b.npcID == npcID);
            if (entry != null && !unlockedBestiary.Contains(entry.ID))
            {
                if (creatureKills[npcID] >= entry.killsRequiredToUnlock)
                    UnlockBestiary(entry.ID);
            }

            SaveBestiary();
        }

        public void UnlockBestiary(int bestiaryID)
        {
            if (unlockedBestiary.Contains(bestiaryID)) return;
            unlockedBestiary.Add(bestiaryID);
            var all = GameDatabase.Instance.GetBestiary();
            if (all.ContainsKey(bestiaryID))
            {
                OnBestiaryUnlocked?.Invoke(all[bestiaryID]);
                Debug.Log($"Bestiary Unlocked: {all[bestiaryID].creatureName}");
            }
            SaveBestiary();
        }

        public bool IsBestiaryUnlocked(int bestiaryID) => unlockedBestiary.Contains(bestiaryID);
        public int GetKillCount(int npcID) => creatureKills.ContainsKey(npcID) ? creatureKills[npcID] : 0;

        private void SaveBestiary()
        {
            if (Character.Instance?.CharacterData == null) return;
            var kills = new List<CharacterEntries.BestiaryKillEntry>();
            foreach (var kvp in creatureKills)
                kills.Add(new CharacterEntries.BestiaryKillEntry { npcID = kvp.Key, killCount = kvp.Value });
            Character.Instance.CharacterData.BestiaryKills = kills;
            Character.Instance.CharacterData.UnlockedBestiary = new List<int>(unlockedBestiary);
        }
    }

    public class MailManager : MonoBehaviour
    {
        public static MailManager Instance { get; private set; }

        public class Mail
        {
            public int templateID;
            public string subject;
            public string body;
            public string sender;
            public bool isRead;
            public bool hasAttachments;
            public List<RPGMailTemplate.MailAttachment> attachments = new List<RPGMailTemplate.MailAttachment>();
            public DateTime expiry;
            public DateTime sentTime;
            public int mailIndex;
        }

        private List<Mail> inbox = new List<Mail>();
        private int nextMailIndex = 0;

        public Action<Mail> OnMailReceived;
        public Action<Mail> OnMailRead;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void SendMail(int templateID)
        {
            var all = GameDatabase.Instance.GetMailTemplates();
            if (!all.ContainsKey(templateID)) return;
            var template = all[templateID];

            var mail = new Mail
            {
                templateID = templateID,
                subject = template.subject,
                body = template.body,
                sender = template.senderName,
                isRead = false,
                hasAttachments = template.attachments.Count > 0,
                attachments = new List<RPGMailTemplate.MailAttachment>(template.attachments),
                expiry = DateTime.Now.AddSeconds(template.expiryTime),
                sentTime = DateTime.Now,
                mailIndex = nextMailIndex++
            };

            inbox.Add(mail);
            OnMailReceived?.Invoke(mail);
            Debug.Log($"Mail Received: {mail.subject} from {mail.sender}");
        }

        public void SendCustomMail(string subject, string body, string sender, List<RPGMailTemplate.MailAttachment> attachments = null)
        {
            var mail = new Mail
            {
                templateID = -1,
                subject = subject,
                body = body,
                sender = sender,
                isRead = false,
                hasAttachments = attachments != null && attachments.Count > 0,
                attachments = attachments ?? new List<RPGMailTemplate.MailAttachment>(),
                expiry = DateTime.Now.AddDays(30),
                sentTime = DateTime.Now,
                mailIndex = nextMailIndex++
            };
            inbox.Add(mail);
            OnMailReceived?.Invoke(mail);
        }

        public void ReadMail(int mailIndex)
        {
            var mail = inbox.FirstOrDefault(m => m.mailIndex == mailIndex);
            if (mail == null) return;
            mail.isRead = true;
            OnMailRead?.Invoke(mail);
        }

        public void ClaimAttachments(int mailIndex)
        {
            var mail = inbox.FirstOrDefault(m => m.mailIndex == mailIndex);
            if (mail == null || !mail.hasAttachments) return;

            foreach (var att in mail.attachments)
            {
                if (att.attachmentType == RPGMailTemplate.MailAttachment.AttachmentType.Item)
                    InventoryManager.Instance.AddItem(att.itemID, att.amount);
                else
                    InventoryManager.Instance.AddCurrency(att.currencyID, att.amount);
            }
            mail.attachments.Clear();
            mail.hasAttachments = false;
        }

        public void DeleteMail(int mailIndex)
        {
            inbox.RemoveAll(m => m.mailIndex == mailIndex);
        }

        public List<Mail> GetInbox() => new List<Mail>(inbox);
        public int GetUnreadCount() => inbox.Count(m => !m.isRead);
        public bool HasMail() => inbox.Count > 0;
    }
}
