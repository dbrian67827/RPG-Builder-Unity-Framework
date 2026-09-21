
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Managers
{
    public class AchievementManager : MonoBehaviour
    {
        public static AchievementManager Instance;
        private HashSet<int> completedAchievements = new HashSet<int>();
        private Dictionary<int, int> progress = new Dictionary<int, int>();
        public System.Action<RPGAchievement> OnAchievementCompleted;
        public System.Action<RPGAchievement, int> OnAchievementProgress;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsCompleted(int id) => completedAchievements.Contains(id);
        public int GetProgress(int id) => progress.TryGetValue(id, out var p) ? p : 0;
        public void AddProgress(int id, int amount = 1)
        {
            if (completedAchievements.Contains(id)) return;
            if (!GameDatabase.Instance.GetAchievements().TryGetValue(id, out var ach)) return;
            int cur = GetProgress(id) + amount;
            progress[id] = cur;
            OnAchievementProgress?.Invoke(ach, cur);
            if (cur >= ach.requiredCount) CompleteAchievement(id);
        }
        public void CompleteAchievement(int id)
        {
            if (completedAchievements.Contains(id)) return;
            if (!GameDatabase.Instance.GetAchievements().TryGetValue(id, out var ach)) return;
            completedAchievements.Add(id);
            OnAchievementCompleted?.Invoke(ach);
            Debug.Log($"[Achievement] Completed {ach.entryDisplayName}");
            if (ach.givesTitle && ach.titleRewardID != -1) TitleManager.Instance?.UnlockTitle(ach.titleRewardID);
            if (ach.givesMount && ach.mountRewardID != -1) MountManager.Instance?.UnlockMount(ach.mountRewardID);
            if (ach.givesPet && ach.petRewardID != -1) PetManager.Instance?.UnlockPet(ach.petRewardID);
        }
        public List<int> GetCompletedAchievements() => completedAchievements.ToList();
        public int GetTotalPoints()
        {
            int pts = 0;
            foreach (var id in completedAchievements) if (GameDatabase.Instance.GetAchievements().TryGetValue(id, out var a)) pts += a.points;
            return pts;
        }
    }
}
