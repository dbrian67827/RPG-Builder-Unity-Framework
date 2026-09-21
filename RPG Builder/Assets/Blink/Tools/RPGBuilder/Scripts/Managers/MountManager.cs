
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BLINK.RPGBuilder.Templates;
using BLINK.RPGBuilder.Characters;

namespace BLINK.RPGBuilder.Managers
{
    public class MountManager : MonoBehaviour
    {
        public static MountManager Instance;
        private HashSet<int> unlockedMounts = new HashSet<int>();
        private int activeMountID = -1;
        private GameObject activeMountObject;
        private bool isMounted = false;
        public System.Action<int> OnMountUnlocked;
        public System.Action<int> OnMountSummoned;
        public System.Action OnMountDismounted;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsMountUnlocked(int id) => unlockedMounts.Contains(id);
        public void UnlockMount(int id) { if (unlockedMounts.Contains(id)) return; unlockedMounts.Add(id); OnMountUnlocked?.Invoke(id); Debug.Log($"[Mount] Unlocked {id}"); }
        public bool CanSummonMount(int id) { if (!IsMountUnlocked(id)) return false; if (Character.Instance.Combat.GetIsInCombat()) return false; return true; }
        public void SummonMount(int id)
        {
            if (!CanSummonMount(id)) return;
            if (!GameDatabase.Instance.GetMounts().TryGetValue(id, out var mount)) return;
            Dismount();
            activeMountID = id;
            isMounted = true;
            if (mount.mountPrefab != null) activeMountObject = Instantiate(mount.mountPrefab, Character.Instance.transform.position, Character.Instance.transform.rotation);
            OnMountSummoned?.Invoke(id);
            Debug.Log($"[Mount] Summoned {mount.entryDisplayName}");
        }
        public void Dismount() { if (!isMounted) return; isMounted = false; if (activeMountObject != null) Destroy(activeMountObject); OnMountDismounted?.Invoke(); }
        public bool IsMounted() => isMounted;
        public List<int> GetUnlockedMounts() => unlockedMounts.ToList();
        public int GetActiveMount() => activeMountID;
    }
}
