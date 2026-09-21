
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Managers
{
    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager Instance;
        private int currentDungeonID = -1;
        private bool inDungeon = false;
        private float dungeonTimer = 0f;
        private Dictionary<int, float> lockouts = new Dictionary<int, float>();
        public System.Action<int> OnDungeonEntered;
        public System.Action<int> OnDungeonExited;
        public System.Action<int> OnDungeonCompleted;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsInDungeon() => inDungeon;
        public bool IsOnLockout(int id) => lockouts.ContainsKey(id) && lockouts[id] > Time.time;
        public void EnterDungeon(int id)
        {
            if (!GameDatabase.Instance.GetDungeons().TryGetValue(id, out var dung)) return;
            if (IsOnLockout(id)) { Debug.Log($"[Dungeon] On lockout for {dung.entryDisplayName}"); return; }
            currentDungeonID = id;
            inDungeon = true;
            dungeonTimer = 0f;
            OnDungeonEntered?.Invoke(id);
            Debug.Log($"[Dungeon] Entered {dung.entryDisplayName}");
        }
        public void ExitDungeon() { if (!inDungeon) return; inDungeon = false; OnDungeonExited?.Invoke(currentDungeonID); currentDungeonID = -1; }
        public void CompleteDungeon()
        {
            if (!inDungeon) return;
            if (!GameDatabase.Instance.GetDungeons().TryGetValue(currentDungeonID, out var dung)) return;
            if (dung.hasLockout) lockouts[currentDungeonID] = Time.time + dung.lockoutDuration;
            OnDungeonCompleted?.Invoke(currentDungeonID);
            ExitDungeon();
        }
        private void Update() { if (inDungeon) dungeonTimer += Time.deltaTime; }
    }
}
