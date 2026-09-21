
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Paragon", menuName = "Blink/RPGBuilder/Paragon/New Paragon")]
    public class RPGParagon : RPGBuilderDatabaseEntry
    {
        public string paragonName = "Paragon";
        public string description = "";
        public Sprite icon;
        public int maxLevel = 100;
        public float expPerLevel = 1000f;
        public float expMultiplier = 1.1f;
        public List<int> bonusIDs = new List<int>();
        public int requiredLevel = 60;
        public bool isAccountWide = false;
        public int sortOrder = 0;
        public bool hasCap = false;
        public int cap = 100;
        public bool isSeasonal = false;
        public string season = "";
    }
}
