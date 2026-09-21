using System;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Characters;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class MountManager : MonoBehaviour
    {
        public static MountManager Instance { get; private set; }

        private List<int> unlockedMounts = new List<int>();
        private int activeMountID = -1;
        private bool isMounted = false;
        private GameObject currentMountGO;
        private float currentStamina;

        public Action<RPGMount> OnMountUnlocked;
        public Action<RPGMount> OnMountSummoned;
        public Action OnMountDismounted;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadMounts();
        }

        private void LoadMounts()
        {
            if (Character.Instance?.CharacterData != null)
            {
                unlockedMounts = new List<int>(Character.Instance.CharacterData.UnlockedMounts ?? new List<int>());
            }
        }

        public void UnlockMount(int mountID)
        {
            if (unlockedMounts.Contains(mountID)) return;
            if (!GameDatabase.Instance.GetMounts().ContainsKey(mountID)) return;

            unlockedMounts.Add(mountID);
            var mount = GameDatabase.Instance.GetMounts()[mountID];
            Debug.Log($"Mount Unlocked: {mount.entryDisplayName}");
            OnMountUnlocked?.Invoke(mount);
            SaveMounts();
        }

        public bool CanSummonMount(int mountID)
        {
            if (!unlockedMounts.Contains(mountID)) return false;
            if (isMounted) return false;
            if (Character.Instance.CombatEntity.isInCombat && !GameDatabase.Instance.GetMounts()[mountID].canUseInCombat) return false;

            // Requirements check
            var mount = GameDatabase.Instance.GetMounts()[mountID];
            return RequirementsManager.Instance.CheckRequirements(mount.Requirements, mount.RequirementsTemplate);
        }

        public void SummonMount(int mountID)
        {
            if (!CanSummonMount(mountID)) return;

            var mount = GameDatabase.Instance.GetMounts()[mountID];
            
            // Cast time simulation
            if (mount.summonCastTime > 0)
            {
                // Would trigger cast bar
                Character.Instance.StartCoroutine(SummonWithCast(mount));
            }
            else
            {
                DoSummon(mount);
            }
        }

        private System.Collections.IEnumerator SummonWithCast(RPGMount mount)
        {
            // Simplified cast
            float elapsed = 0f;
            while (elapsed < mount.summonCastTime)
            {
                if (Character.Instance.CombatEntity.isInCombat && mount.isInterruptible)
                {
                    yield break;
                }
                elapsed += Time.deltaTime;
                yield return null;
            }
            DoSummon(mount);
        }

        private void DoSummon(RPGMount mount)
        {
            if (mount.mountPrefab != null)
            {
                currentMountGO = Instantiate(mount.mountPrefab, Character.Instance.transform.position, Character.Instance.transform.rotation);
                currentMountGO.transform.SetParent(Character.Instance.transform);
            }

            activeMountID = mount.ID;
            isMounted = true;
            currentStamina = mount.stamina;

            if (mount.summonEffect != null)
                Instantiate(mount.summonEffect, Character.Instance.transform.position, Quaternion.identity);

            // Apply stat bonuses
            foreach (var bonus in mount.statBonuses)
            {
                if (bonus.onlyWhileMounted)
                {
                    // Apply via BonusManager or stat system
                    BonusManager.Instance?.ApplyMountBonus(bonus);
                }
            }

            // Modify character controller for mount speed
            var controller = Character.Instance.GetComponent<RPGBCharacterController>();
            if (controller != null)
            {
                // Would adjust speed
            }

            OnMountSummoned?.Invoke(mount);
            Debug.Log($"Mounted: {mount.entryDisplayName}");
        }

        public void Dismount()
        {
            if (!isMounted) return;

            var mount = GameDatabase.Instance.GetMounts()[activeMountID];

            if (currentMountGO != null) Destroy(currentMountGO);
            if (mount.dismountEffect != null)
                Instantiate(mount.dismountEffect, Character.Instance.transform.position, Quaternion.identity);

            // Remove stat bonuses
            foreach (var bonus in mount.statBonuses)
            {
                if (bonus.onlyWhileMounted)
                    BonusManager.Instance?.RemoveMountBonus(bonus);
            }

            isMounted = false;
            activeMountID = -1;
            OnMountDismounted?.Invoke();
        }

        public bool IsMounted() => isMounted;
        public bool IsMountUnlocked(int mountID) => unlockedMounts.Contains(mountID);
        public RPGMount GetActiveMount()
        {
            if (activeMountID == -1) return null;
            var mounts = GameDatabase.Instance.GetMounts();
            return mounts.ContainsKey(activeMountID) ? mounts[activeMountID] : null;
        }

        public List<RPGMount> GetUnlockedMounts()
        {
            var result = new List<RPGMount>();
            var all = GameDatabase.Instance.GetMounts();
            foreach (int id in unlockedMounts)
                if (all.ContainsKey(id)) result.Add(all[id]);
            return result;
        }

        public void HandleDamageTaken(float damagePercent)
        {
            if (!isMounted) return;
            var mount = GetActiveMount();
            if (mount == null) return;
            if (mount.dismountOnDamage && damagePercent >= mount.dismountDamageThreshold)
                Dismount();
        }

        public void HandleCombatEnter()
        {
            if (!isMounted) return;
            var mount = GetActiveMount();
            if (mount != null && mount.dismountOnCombat) Dismount();
        }

        private void SaveMounts()
        {
            if (Character.Instance?.CharacterData == null) return;
            Character.Instance.CharacterData.UnlockedMounts = new List<int>(unlockedMounts);
        }

        private void Update()
        {
            if (!isMounted) return;
            var mount = GetActiveMount();
            if (mount == null || !mount.useStamina) return;

            // Stamina drain while sprinting
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentStamina -= mount.staminaDrainRate * Time.deltaTime;
                if (currentStamina <= 0)
                {
                    currentStamina = 0;
                    Dismount();
                }
            }
            else
            {
                currentStamina = Mathf.Min(mount.stamina, currentStamina + mount.staminaRegenRate * Time.deltaTime);
            }
        }
    }
}
