using System;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Combat;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class ThreatManager : MonoBehaviour
    {
        public static ThreatManager Instance { get; private set; }

        public class ThreatTable
        {
            public CombatEntity owner;
            public Dictionary<CombatEntity, float> threat = new Dictionary<CombatEntity, float>();
            public CombatEntity currentTarget;
            public float lastThreatDecay;
        }

        private Dictionary<CombatEntity, ThreatTable> threatTables = new Dictionary<CombatEntity, ThreatTable>();

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void AddThreat(CombatEntity target, CombatEntity attacker, float amount)
        {
            if (target == null || attacker == null) return;
            if (!threatTables.ContainsKey(target))
                threatTables[target] = new ThreatTable { owner = target };

            var table = threatTables[target];
            if (!table.threat.ContainsKey(attacker)) table.threat[attacker] = 0;
            table.threat[attacker] += amount;

            // Tank multiplier
            var combatSettings = GameDatabase.Instance.GetCombatSettings();
            if (combatSettings != null && combatSettings.EnableThreatSystem)
            {
                // Check if attacker is tank (would check class/spec)
                // For now, apply generic multiplier
            }

            UpdateTarget(target);
        }

        public void RemoveThreat(CombatEntity target, CombatEntity attacker)
        {
            if (!threatTables.ContainsKey(target)) return;
            threatTables[target].threat.Remove(attacker);
            UpdateTarget(target);
        }

        public void ClearThreat(CombatEntity target)
        {
            if (threatTables.ContainsKey(target))
                threatTables[target].threat.Clear();
        }

        public CombatEntity GetHighestThreatTarget(CombatEntity owner)
        {
            if (!threatTables.ContainsKey(owner)) return null;
            var table = threatTables[owner];
            if (table.threat.Count == 0) return null;
            return table.threat.OrderByDescending(kvp => kvp.Value).First().Key;
        }

        public float GetThreat(CombatEntity owner, CombatEntity attacker)
        {
            if (!threatTables.ContainsKey(owner)) return 0;
            var table = threatTables[owner];
            return table.threat.ContainsKey(attacker) ? table.threat[attacker] : 0;
        }

        public void Taunt(CombatEntity target, CombatEntity taunter)
        {
            if (!threatTables.ContainsKey(target)) return;
            var table = threatTables[target];
            float highest = table.threat.Count > 0 ? table.threat.Values.Max() : 0;
            var combatSettings = GameDatabase.Instance.GetCombatSettings();
            float tauntMultiplier = combatSettings != null ? combatSettings.TauntThreatMultiplier : 2f;

            table.threat[taunter] = highest * tauntMultiplier;
            table.currentTarget = taunter;
        }

        private void UpdateTarget(CombatEntity owner)
        {
            if (!threatTables.ContainsKey(owner)) return;
            var table = threatTables[owner];
            var highest = GetHighestThreatTarget(owner);
            if (highest != null && highest != table.currentTarget)
            {
                table.currentTarget = highest;
                // Notify AI to switch target
                var ai = owner.GetComponent<AI.AIEntity>();
                if (ai != null)
                {
                    // ai.SetTarget(highest.gameObject);
                }
            }
        }

        private void Update()
        {
            var combatSettings = GameDatabase.Instance.GetCombatSettings();
            if (combatSettings == null || !combatSettings.EnableThreatSystem) return;

            foreach (var table in threatTables.Values)
            {
                if (Time.time - table.lastThreatDecay > combatSettings.ThreatDecayDelay)
                {
                    var keys = table.threat.Keys.ToList();
                    foreach (var key in keys)
                    {
                        table.threat[key] -= combatSettings.ThreatDecayRate * Time.deltaTime;
                        if (table.threat[key] <= 0) table.threat.Remove(key);
                    }
                    table.lastThreatDecay = Time.time;
                }
            }
        }
    }

    public class DiminishingReturnsManager : MonoBehaviour
    {
        public static DiminishingReturnsManager Instance { get; private set; }

        public class DRTable
        {
            public Dictionary<string, int> stacks = new Dictionary<string, int>();
            public Dictionary<string, float> lastApplication = new Dictionary<string, float>();
        }

        private Dictionary<CombatEntity, DRTable> drTables = new Dictionary<CombatEntity, DRTable>();

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public float GetDRMultiplier(CombatEntity target, string category)
        {
            var settings = GameDatabase.Instance.GetCombatSettings();
            if (settings == null || !settings.EnableDiminishingReturnsCC) return 1f;

            if (!drTables.ContainsKey(target)) return 1f;
            var table = drTables[target];

            if (!table.stacks.ContainsKey(category)) return 1f;

            // Check if expired
            if (table.lastApplication.ContainsKey(category) && Time.time - table.lastApplication[category] > settings.CCDRDuration)
            {
                table.stacks[category] = 0;
                return 1f;
            }

            int stacks = table.stacks[category];
            return Mathf.Pow(settings.CCDRFactor, stacks);
        }

        public void ApplyDR(CombatEntity target, string category)
        {
            if (!drTables.ContainsKey(target)) drTables[target] = new DRTable();
            var table = drTables[target];

            if (!table.stacks.ContainsKey(category)) table.stacks[category] = 0;
            table.stacks[category]++;
            table.lastApplication[category] = Time.time;

            var settings = GameDatabase.Instance.GetCombatSettings();
            if (settings != null && table.stacks[category] > settings.CCDRMaxStacks)
            {
                // Immune
            }
        }

        public void ClearDR(CombatEntity target)
        {
            if (drTables.ContainsKey(target)) drTables.Remove(target);
        }

        private void Update()
        {
            var settings = GameDatabase.Instance.GetCombatSettings();
            if (settings == null) return;

            var toRemove = new List<CombatEntity>();
            foreach (var kvp in drTables)
            {
                var table = kvp.Value;
                bool allExpired = true;
                foreach (var cat in table.lastApplication.Keys.ToList())
                {
                    if (Time.time - table.lastApplication[cat] > settings.CCDRDuration)
                    {
                        table.stacks[cat] = 0;
                    }
                    else
                    {
                        allExpired = false;
                    }
                }
                if (allExpired && table.stacks.Values.All(s => s == 0))
                    toRemove.Add(kvp.Key);
            }
            foreach (var e in toRemove) drTables.Remove(e);
        }
    }

    public class ShieldManager : MonoBehaviour
    {
        public static ShieldManager Instance { get; private set; }

        public class Shield
        {
            public int effectID;
            public float amount;
            public float maxAmount;
            public float duration;
            public float remaining;
            public CombatEntity caster;
            public CombatEntity target;
            public bool isExpired => remaining <= 0 || amount <= 0;
        }

        private Dictionary<CombatEntity, List<Shield>> shields = new Dictionary<CombatEntity, List<Shield>>();

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void AddShield(CombatEntity target, float amount, float duration, int effectID, CombatEntity caster)
        {
            if (!shields.ContainsKey(target)) shields[target] = new List<Shield>();

            var settings = GameDatabase.Instance.GetCombatSettings();
            if (settings != null && !settings.ShieldsStack)
            {
                // Replace if stronger
                var existing = shields[target].FirstOrDefault(s => s.effectID == effectID);
                if (existing != null)
                {
                    if (amount > existing.amount)
                    {
                        existing.amount = amount;
                        existing.maxAmount = amount;
                        existing.duration = duration;
                        existing.remaining = duration;
                    }
                    return;
                }
            }

            shields[target].Add(new Shield
            {
                effectID = effectID,
                amount = amount,
                maxAmount = amount,
                duration = duration,
                remaining = duration,
                caster = caster,
                target = target
            });
        }

        public float AbsorbDamage(CombatEntity target, float damage)
        {
            if (!shields.ContainsKey(target) || shields[target].Count == 0) return damage;

            float remainingDamage = damage;
            var shieldList = shields[target].ToList();

            foreach (var shield in shieldList)
            {
                if (remainingDamage <= 0) break;
                if (shield.isExpired) continue;

                float absorbed = Mathf.Min(shield.amount, remainingDamage);
                shield.amount -= absorbed;
                remainingDamage -= absorbed;

                if (shield.amount <= 0)
                    shields[target].Remove(shield);
            }

            return remainingDamage;
        }

        public float GetTotalShield(CombatEntity target)
        {
            if (!shields.ContainsKey(target)) return 0;
            return shields[target].Sum(s => s.amount);
        }

        public void ClearShields(CombatEntity target)
        {
            if (shields.ContainsKey(target)) shields[target].Clear();
        }

        private void Update()
        {
            var toRemove = new List<CombatEntity>();
            foreach (var kvp in shields)
            {
                var list = kvp.Value;
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    list[i].remaining -= Time.deltaTime;
                    if (list[i].isExpired) list.RemoveAt(i);
                }
                if (list.Count == 0) toRemove.Add(kvp.Key);
            }
            foreach (var e in toRemove) shields.Remove(e);
        }
    }

    public class DamageNumbersManager : MonoBehaviour
    {
        public static DamageNumbersManager Instance { get; private set; }

        public enum DamageNumberType
        {
            Physical,
            Magical,
            Neutral,
            Heal,
            CriticalPhysical,
            CriticalMagical,
            CriticalHeal,
            Miss,
            Dodge,
            Parry,
            Block,
            Immune,
            Shield
        }

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void ShowDamageNumber(Vector3 position, float amount, DamageNumberType type, bool isPlayer = false)
        {
            // Would instantiate floating text prefab
            // For now, just log
            // Debug.Log($"{type} {amount} at {position}");

            var uiSettings = GameDatabase.Instance.GetUISettings();
            if (uiSettings == null) return;

            Color color = Color.white;
            switch (type)
            {
                case DamageNumberType.Physical:
                    color = uiSettings.PhysicalDamageColor;
                    break;
                case DamageNumberType.Magical:
                    color = uiSettings.MagicalDamageColor;
                    break;
                case DamageNumberType.Neutral:
                    color = uiSettings.NeutralDamageColor;
                    break;
                case DamageNumberType.Heal:
                    color = uiSettings.HealingColor;
                    break;
                case DamageNumberType.CriticalPhysical:
                    color = uiSettings.PhysicalCriticalDamageColor;
                    break;
                case DamageNumberType.CriticalMagical:
                    color = uiSettings.MagicalCriticalDamageColor;
                    break;
                case DamageNumberType.CriticalHeal:
                    color = uiSettings.HealingCriticalColor;
                    break;
            }

            // Instantiate floating text via ScreenTextDisplayManager or similar
            ScreenTextDisplayManager.Instance?.ShowScreenText(position, amount.ToString(), color);
        }
    }
}
