
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class ThreatManager : MonoBehaviour
    {
        public static ThreatManager Instance;
        private Dictionary<int, Dictionary<string, float>> threatTables = new Dictionary<int, Dictionary<string, float>>();

        private void Awake() { Instance = this; }

        public void AddThreat(int npcID, string playerName, float amount)
        {
            if (!threatTables.ContainsKey(npcID)) threatTables[npcID] = new Dictionary<string, float>();
            if (!threatTables[npcID].ContainsKey(playerName)) threatTables[npcID][playerName] = 0;
            threatTables[npcID][playerName] += amount;
        }
        public void RemoveThreat(int npcID, string playerName) { if (threatTables.ContainsKey(npcID)) threatTables[npcID].Remove(playerName); }
        public string GetTopThreat(int npcID) { if (!threatTables.ContainsKey(npcID) || threatTables[npcID].Count == 0) return ""; return threatTables[npcID].OrderByDescending(kvp => kvp.Value).First().Key; }
        public float GetThreat(int npcID, string playerName) => threatTables.ContainsKey(npcID) && threatTables[npcID].ContainsKey(playerName) ? threatTables[npcID][playerName] : 0f;
        public void ClearThreat(int npcID) => threatTables.Remove(npcID);
        public void ModifyThreat(int npcID, string playerName, float multiplier) { if (threatTables.ContainsKey(npcID) && threatTables[npcID].ContainsKey(playerName)) threatTables[npcID][playerName] *= multiplier; }
    }

    public class DiminishingReturnsManager : MonoBehaviour
    {
        public static DiminishingReturnsManager Instance;
        private Dictionary<string, Dictionary<string, float>> drTables = new Dictionary<string, Dictionary<string, float>>();
        private Dictionary<string, Dictionary<string, float>> drTimers = new Dictionary<string, Dictionary<string, float>>();

        private void Awake() { Instance = this; }

        public float GetDRMultiplier(string target, string ccType)
        {
            if (!drTables.ContainsKey(target) || !drTables[target].ContainsKey(ccType)) return 1f;
            return drTables[target][ccType];
        }
        public void ApplyDR(string target, string ccType)
        {
            if (!drTables.ContainsKey(target)) { drTables[target] = new Dictionary<string, float>(); drTimers[target] = new Dictionary<string, float>(); }
            if (!drTables[target].ContainsKey(ccType)) drTables[target][ccType] = 1f;
            drTables[target][ccType] *= 0.5f;
            drTimers[target][ccType] = 15f;
            if (drTables[target][ccType] < 0.25f) drTables[target][ccType] = 0f;
        }
        private void Update()
        {
            foreach (var target in drTimers.Keys.ToList()) foreach (var cc in drTimers[target].Keys.ToList()) { drTimers[target][cc] -= Time.deltaTime; if (drTimers[target][cc] <= 0) { drTables[target].Remove(cc); drTimers[target].Remove(cc); } }
        }
    }

    public class ShieldManager : MonoBehaviour
    {
        public static ShieldManager Instance;
        private Dictionary<string, List<Shield>> shields = new Dictionary<string, List<Shield>>();

        public class Shield { public int id; public float amount; public float maxAmount; public float duration; public float remaining; public int casterID; }

        private void Awake() { Instance = this; }

        public void AddShield(string target, int id, float amount, float duration, int casterID = -1)
        {
            if (!shields.ContainsKey(target)) shields[target] = new List<Shield>();
            shields[target].Add(new Shield { id = id, amount = amount, maxAmount = amount, duration = duration, remaining = duration, casterID = casterID });
        }
        public float AbsorbDamage(string target, float damage)
        {
            if (!shields.ContainsKey(target) || shields[target].Count == 0) return damage;
            float remainingDamage = damage;
            for (int i = shields[target].Count - 1; i >= 0; i--) { var s = shields[target][i]; if (s.amount >= remainingDamage) { s.amount -= remainingDamage; remainingDamage = 0; break; } else { remainingDamage -= s.amount; shields[target].RemoveAt(i); } }
            return remainingDamage;
        }
        private void Update() { foreach (var target in shields.Keys.ToList()) for (int i = shields[target].Count - 1; i >= 0; i--) { shields[target][i].remaining -= Time.deltaTime; if (shields[target][i].remaining <= 0) shields[target].RemoveAt(i); } }
    }

    public class DamageNumbersManager : MonoBehaviour
    {
        public static DamageNumbersManager Instance;
        public GameObject damageNumberPrefab;
        public Transform worldCanvas;

        private void Awake() { Instance = this; }

        public void ShowDamageNumber(Vector3 position, float amount, bool isCrit = false, bool isHeal = false)
        {
            Debug.Log($"[Damage] {amount} {(isCrit ? "CRIT" : "")} {(isHeal ? "HEAL" : "")} at {position}");
        }
    }
}
