
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Transmog", menuName = "Blink/RPGBuilder/Transmog/New Transmog")]
    public class RPGTransmog : RPGBuilderDatabaseEntry
    {
        public string transmogName = "New Transmog";
        public Sprite icon;
        public GameObject transmogModel;
        public int sourceItemID = -1;
        public int requiredItemID = -1;
        public bool isAccountWide = false;
        public int requiredLevel = 1;
        public int requiredAchievementID = -1;
        public int sortOrder = 0;
        public string category = "Armor";
        public bool isHidden = false;
        public int currencyCostID = -1;
        public int currencyCostAmount = 0;
    }
}
