
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Managers
{
    public class WorldEventManager : MonoBehaviour
    {
        public static WorldEventManager Instance;
        private List<int> activeEvents = new List<int>();
        private Dictionary<int, float> eventTimers = new Dictionary<int, float>();
        public System.Action<int> OnEventStarted;
        public System.Action<int> OnEventEnded;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsEventActive(int id) => activeEvents.Contains(id);
        public void StartEvent(int id)
        {
            if (IsEventActive(id)) return;
            if (!GameDatabase.Instance.GetWorldEvents().TryGetValue(id, out var ev)) return;
            activeEvents.Add(id);
            eventTimers[id] = ev.duration;
            OnEventStarted?.Invoke(id);
            Debug.Log($"[WorldEvent] Started {ev.entryDisplayName}");
        }
        public void EndEvent(int id)
        {
            if (!IsEventActive(id)) return;
            activeEvents.Remove(id);
            eventTimers.Remove(id);
            OnEventEnded?.Invoke(id);
        }
        private void Update()
        {
            var toEnd = new List<int>();
            foreach (var kvp in eventTimers) { eventTimers[kvp.Key] -= Time.deltaTime; if (eventTimers[kvp.Key] <= 0) toEnd.Add(kvp.Key); }
            foreach (var id in toEnd) EndEvent(id);
        }
        public List<int> GetActiveEvents() => activeEvents.ToList();
    }
}
