
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Crafting Quality", menuName = "Blink/RPGBuilder/CraftingQuality/New Crafting Quality")]
    public class RPGCraftingQuality : RPGBuilderDatabaseEntry
    {
        public string qualityName = "Normal";
        public Color qualityColor = Color.white;
        public Sprite qualityIcon;
        public float qualityMultiplier = 1f;
        public float statMultiplier = 1f;
        public float valueMultiplier = 1f;
        public float successChance = 100f;
        public int requiredSkillLevel = 1;
        public float chanceToGet = 50f;
        public int sortOrder = 0;
        public bool isBestQuality = false;
        public bool isLowestQuality = true;
        public GameObject craftEffect;
        public AudioClip craftSound;
    }
}
