using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class WorldEventManager : MonoBehaviour
    {
        public static WorldEventManager Instance { get; private set; }

        public class ActiveWorldEvent
        {
            public int eventID;
            public float remainingTime;
            public float totalDuration;
            public bool isActive;
            public DateTime startTime;
            public List<GameObject> spawnedNPCs = new List<GameObject>();
            public GameObject activeVFXInstance;
        }

        private Dictionary<int, ActiveWorldEvent> activeEvents = new Dictionary<int, ActiveWorldEvent>();
        private Dictionary<int, float> eventCooldowns = new Dictionary<int, float>();
        private Dictionary<int, int> dailyCompletions = new Dictionary<int, int>();
        private Dictionary<int, int> weeklyCompletions = new Dictionary<int, int>();

        public Action<RPGWorldEvent> OnEventStarted;
        public Action<RPGWorldEvent> OnEventEnded;
        public Action<RPGWorldEvent, float> OnEventTick;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            InvokeRepeating(nameof(CheckRandomEvents), 5f, 10f);
            InvokeRepeating(nameof(CheckScheduledEvents), 1f, 60f);
        }

        private void Update()
        {
            var toRemove = new List<int>();
            foreach (var kvp in activeEvents)
            {
                var active = kvp.Value;
                if (!active.isActive) continue;

                active.remainingTime -= Time.deltaTime;
                var worldEvent = GameDatabase.Instance.GetWorldEvents()[active.eventID];

                // Tick actions
                if (worldEvent.TickGameActions.Count > 0 || (worldEvent.UseGameActionsTemplate && worldEvent.GameActionsTemplate != null))
                {
                    // Execute every second? For now every tick
                    if (Time.frameCount % 60 == 0)
                    {
                        OnEventTick?.Invoke(worldEvent, active.remainingTime);
                    }
                }

                if (active.remainingTime <= 0)
                {
                    EndEvent(active.eventID);
                    toRemove.Add(active.eventID);
                }
            }
        }

        private void CheckRandomEvents()
        {
            var allEvents = GameDatabase.Instance.GetWorldEvents();
            if (allEvents == null) return;

            foreach (var kvp in allEvents)
            {
                var evt = kvp.Value;
                if (!evt.isRandom || evt.isManualOnly) continue;
                if (IsEventActive(evt.ID)) continue;
                if (eventCooldowns.ContainsKey(evt.ID) && eventCooldowns[evt.ID] > 0) continue;

                if (UnityEngine.Random.Range(0f, 100f) <= evt.randomChance)
                {
                    if (CanStartEvent(evt))
                        StartEvent(evt.ID);
                }
            }

            // Cooldowns
            var keys = eventCooldowns.Keys.ToList();
            foreach (var key in keys)
            {
                eventCooldowns[key] -= 10f;
                if (eventCooldowns[key] <= 0) eventCooldowns.Remove(key);
            }
        }

        private void CheckScheduledEvents()
        {
            var allEvents = GameDatabase.Instance.GetWorldEvents();
            if (allEvents == null) return;

            var timeData = TimeManager.Instance?.GetTimeData();
            int currentHour = timeData != null ? timeData.Hour : DateTime.Now.Hour;

            foreach (var kvp in allEvents)
            {
                var evt = kvp.Value;
                if (evt.isRandom || evt.isManualOnly) continue;
                if (IsEventActive(evt.ID)) continue;

                bool shouldStart = false;
                switch (evt.scheduleType)
                {
                    case RPGWorldEvent.EventScheduleType.TimeOfDay:
                        shouldStart = currentHour >= evt.startHour && currentHour <= evt.endHour;
                        break;
                    case RPGWorldEvent.EventScheduleType.Always:
                        shouldStart = true;
                        break;
                }

                if (shouldStart && CanStartEvent(evt))
                    StartEvent(evt.ID);
            }
        }

        public bool CanStartEvent(RPGWorldEvent evt)
        {
            if (evt.maxCompletionsPerDay > 0)
            {
                int completions = dailyCompletions.ContainsKey(evt.ID) ? dailyCompletions[evt.ID] : 0;
                if (completions >= evt.maxCompletionsPerDay) return false;
            }
            if (evt.maxCompletionsPerWeek > 0)
            {
                int completions = weeklyCompletions.ContainsKey(evt.ID) ? weeklyCompletions[evt.ID] : 0;
                if (completions >= evt.maxCompletionsPerWeek) return false;
            }

            return RequirementsManager.Instance.CheckRequirements(evt.Requirements, evt.RequirementsTemplate);
        }

        public void StartEvent(int eventID)
        {
            if (IsEventActive(eventID)) return;
            var allEvents = GameDatabase.Instance.GetWorldEvents();
            if (!allEvents.ContainsKey(eventID)) return;

            var evt = allEvents[eventID];
            var active = new ActiveWorldEvent
            {
                eventID = eventID,
                remainingTime = evt.duration,
                totalDuration = evt.duration,
                isActive = true,
                startTime = DateTime.Now
            };

            // Spawn NPCs
            foreach (var spawn in evt.npcSpawns)
            {
                for (int i = 0; i < spawn.count; i++)
                {
                    // Simplified spawn - would use NPCSpawner
                    if (GameDatabase.Instance.GetNPCs().ContainsKey(spawn.npcID))
                    {
                        var npc = GameDatabase.Instance.GetNPCs()[spawn.npcID];
                        if (npc.NPCVisual != null)
                        {
                            Vector3 pos = Vector3.zero;
                            if (evt.locations.Count > 0)
                            {
                                var loc = evt.locations[0];
                                pos = loc.position + UnityEngine.Random.insideUnitSphere * spawn.spawnRadius;
                                pos.y = loc.position.y;
                            }
                            var go = Instantiate(npc.NPCVisual, pos, Quaternion.identity);
                            active.spawnedNPCs.Add(go);
                        }
                    }
                }
            }

            if (evt.startVFX != null)
                active.activeVFXInstance = Instantiate(evt.startVFX);

            activeEvents[eventID] = active;

            // Game Actions
            if (evt.UseGameActionsTemplate && evt.GameActionsTemplate != null)
                GameActionsManager.Instance.ExecuteGameActions(evt.GameActionsTemplate.GameActions, null, null);
            else
                GameActionsManager.Instance.ExecuteGameActions(evt.StartGameActions, null, null);

            // Announce
            if (evt.announceToAll)
                AlertMessagesDisplay.Instance?.ShowMessage(evt.startMessage);

            if (evt.startSFX != null)
                AudioSource.PlayClipAtPoint(evt.startSFX, Vector3.zero);

            OnEventStarted?.Invoke(evt);
            Debug.Log($"World Event Started: {evt.entryDisplayName}");
        }

        public void EndEvent(int eventID)
        {
            if (!activeEvents.ContainsKey(eventID)) return;
            var active = activeEvents[eventID];
            var allEvents = GameDatabase.Instance.GetWorldEvents();
            if (!allEvents.ContainsKey(eventID)) return;

            var evt = allEvents[eventID];

            // Destroy spawned NPCs
            foreach (var go in active.spawnedNPCs)
                if (go != null) Destroy(go);

            if (active.activeVFXInstance != null) Destroy(active.activeVFXInstance);
            if (evt.endVFX != null)
                Instantiate(evt.endVFX, Vector3.zero, Quaternion.identity);

            // Game Actions
            if (evt.UseGameActionsTemplate && evt.GameActionsTemplate != null)
                GameActionsManager.Instance.ExecuteGameActions(evt.GameActionsTemplate.GameActions, null, null);
            else
                GameActionsManager.Instance.ExecuteGameActions(evt.EndGameActions, null, null);

            // Rewards
            foreach (var reward in evt.rewards)
            {
                if (reward.onlyOnCompletion) GrantReward(reward);
            }

            if (evt.announceToAll)
                AlertMessagesDisplay.Instance?.ShowMessage(evt.endMessage);

            if (evt.endSFX != null)
                AudioSource.PlayClipAtPoint(evt.endSFX, Vector3.zero);

            activeEvents.Remove(eventID);
            eventCooldowns[eventID] = evt.cooldown;

            // Track completions
            if (!dailyCompletions.ContainsKey(eventID)) dailyCompletions[eventID] = 0;
            dailyCompletions[eventID]++;
            if (!weeklyCompletions.ContainsKey(eventID)) weeklyCompletions[eventID] = 0;
            weeklyCompletions[eventID]++;

            OnEventEnded?.Invoke(evt);
            Debug.Log($"World Event Ended: {evt.entryDisplayName}");
        }

        private void GrantReward(RPGWorldEvent.EventReward reward)
        {
            if (UnityEngine.Random.Range(0f, 100f) > reward.chance) return;

            if (reward.itemID != -1)
                InventoryManager.Instance.AddItem(reward.itemID, reward.amount);
            if (reward.currencyID != -1)
                InventoryManager.Instance.AddCurrency(reward.currencyID, reward.amount);
        }

        public bool IsEventActive(int eventID) => activeEvents.ContainsKey(eventID) && activeEvents[eventID].isActive;
        public List<RPGWorldEvent> GetActiveEvents()
        {
            var result = new List<RPGWorldEvent>();
            var all = GameDatabase.Instance.GetWorldEvents();
            foreach (var kvp in activeEvents)
                if (all.ContainsKey(kvp.Key)) result.Add(all[kvp.Key]);
            return result;
        }

        public float GetRemainingTime(int eventID)
        {
            return activeEvents.ContainsKey(eventID) ? activeEvents[eventID].remainingTime : 0f;
        }

        public float GetProgress(int eventID)
        {
            if (!activeEvents.ContainsKey(eventID)) return 0f;
            var active = activeEvents[eventID];
            return 1f - (active.remainingTime / active.totalDuration);
        }
    }
}
