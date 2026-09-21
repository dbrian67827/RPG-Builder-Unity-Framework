using System;
using System.Collections.Generic;
using System.Linq;
using BLINK.RPGBuilder.Characters;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class PetManager : MonoBehaviour
    {
        public static PetManager Instance { get; private set; }

        private List<int> unlockedPets = new List<int>();
        private int activePetID = -1;
        private GameObject activePetGO;
        private int activePetLevel = 1;
        private int activePetExp = 0;

        public Action<RPGPet> OnPetUnlocked;
        public Action<RPGPet> OnPetSummoned;
        public Action OnPetDismissed;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadPets();
        }

        private void LoadPets()
        {
            if (Character.Instance?.CharacterData != null)
            {
                unlockedPets = new List<int>(Character.Instance.CharacterData.UnlockedPets ?? new List<int>());
                activePetID = Character.Instance.CharacterData.ActivePetID;
                if (activePetID != -1) SummonPet(activePetID);
            }
        }

        public void UnlockPet(int petID)
        {
            if (unlockedPets.Contains(petID)) return;
            if (!GameDatabase.Instance.GetPets().ContainsKey(petID)) return;

            unlockedPets.Add(petID);
            var pet = GameDatabase.Instance.GetPets()[petID];
            Debug.Log($"Pet Unlocked: {pet.entryDisplayName}");
            OnPetUnlocked?.Invoke(pet);
            SavePets();
        }

        public void SummonPet(int petID)
        {
            if (!unlockedPets.Contains(petID)) return;

            DismissPet();

            var pet = GameDatabase.Instance.GetPets()[petID];
            if (pet.petPrefab != null)
            {
                Vector3 spawnPos = Character.Instance.transform.position + Character.Instance.transform.forward * 2f;
                activePetGO = Instantiate(pet.petPrefab, spawnPos, Quaternion.identity);
                var petAI = activePetGO.AddComponent<PetAI>();
                petAI.Initialize(pet, Character.Instance.gameObject);
            }

            activePetID = petID;
            if (pet.summonEffect != null)
                Instantiate(pet.summonEffect, Character.Instance.transform.position, Quaternion.identity);

            OnPetSummoned?.Invoke(pet);
            SavePets();
        }

        public void DismissPet()
        {
            if (activePetGO != null) Destroy(activePetGO);
            if (activePetID != -1)
            {
                var pet = GameDatabase.Instance.GetPets()[activePetID];
                if (pet.dismissEffect != null)
                    Instantiate(pet.dismissEffect, Character.Instance.transform.position, Quaternion.identity);
            }
            activePetID = -1;
            OnPetDismissed?.Invoke();
            SavePets();
        }

        public bool IsPetUnlocked(int petID) => unlockedPets.Contains(petID);
        public bool HasActivePet() => activePetID != -1 && activePetGO != null;
        public RPGPet GetActivePet()
        {
            if (activePetID == -1) return null;
            var pets = GameDatabase.Instance.GetPets();
            return pets.ContainsKey(activePetID) ? pets[activePetID] : null;
        }

        public List<RPGPet> GetUnlockedPets()
        {
            var result = new List<RPGPet>();
            var all = GameDatabase.Instance.GetPets();
            foreach (int id in unlockedPets)
                if (all.ContainsKey(id)) result.Add(all[id]);
            return result;
        }

        public void AddPetExperience(int amount)
        {
            if (activePetID == -1) return;
            var pet = GetActivePet();
            if (pet == null) return;

            activePetExp += amount;
            while (activePetExp >= pet.expPerLevel && activePetLevel < pet.maxLevel)
            {
                activePetExp -= pet.expPerLevel;
                activePetLevel++;
                Debug.Log($"Pet Level Up: {pet.entryDisplayName} now level {activePetLevel}");
            }
        }

        private void SavePets()
        {
            if (Character.Instance?.CharacterData == null) return;
            Character.Instance.CharacterData.UnlockedPets = new List<int>(unlockedPets);
            Character.Instance.CharacterData.ActivePetID = activePetID;
        }
    }

    public class PetAI : MonoBehaviour
    {
        private RPGPet petData;
        private GameObject owner;
        private UnityEngine.AI.NavMeshAgent agent;

        public void Initialize(RPGPet pet, GameObject ownerObj)
        {
            petData = pet;
            owner = ownerObj;
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent == null) agent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            agent.speed = pet.followSpeed;
            agent.stoppingDistance = pet.followDistance;
        }

        private void Update()
        {
            if (owner == null || petData == null) return;

            float dist = Vector3.Distance(transform.position, owner.transform.position);
            if (dist > petData.teleportDistance && petData.canTeleportToOwner)
            {
                transform.position = owner.transform.position + owner.transform.forward * 2f;
            }
            else if (dist > petData.followDistance)
            {
                agent.SetDestination(owner.transform.position);
            }
        }
    }
}
