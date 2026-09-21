
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Managers
{
    public class PetManager : MonoBehaviour
    {
        public static PetManager Instance;
        private HashSet<int> unlockedPets = new HashSet<int>();
        private int activePetID = -1;
        private GameObject activePetObject;
        public System.Action<int> OnPetUnlocked;
        public System.Action<int> OnPetSummoned;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsPetUnlocked(int id) => unlockedPets.Contains(id);
        public void UnlockPet(int id) { if (unlockedPets.Contains(id)) return; unlockedPets.Add(id); OnPetUnlocked?.Invoke(id); }
        public void SummonPet(int id)
        {
            if (!IsPetUnlocked(id)) return;
            if (!GameDatabase.Instance.GetPets().TryGetValue(id, out var pet)) return;
            DespawnPet();
            activePetID = id;
            if (pet.petPrefab != null) activePetObject = Instantiate(pet.petPrefab, BLINK.RPGBuilder.Characters.Character.Instance.transform.position + Vector3.forward*2, Quaternion.identity);
            OnPetSummoned?.Invoke(id);
        }
        public void DespawnPet() { if (activePetObject != null) Destroy(activePetObject); activePetID = -1; }
        public List<int> GetUnlockedPets() => unlockedPets.ToList();
        public bool HasActivePet() => activePetID != -1;
    }

    public class PetAI : MonoBehaviour
    {
        public Transform owner;
        public float followDistance = 2f;
        public float followSpeed = 3.5f;
        private void Update() { if (owner == null) return; float dist = Vector3.Distance(transform.position, owner.position); if (dist > followDistance) transform.position = Vector3.MoveTowards(transform.position, owner.position, followSpeed*Time.deltaTime); }
    }
}
