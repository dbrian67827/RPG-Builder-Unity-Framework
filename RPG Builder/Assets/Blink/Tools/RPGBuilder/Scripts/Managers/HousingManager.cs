
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class HousingManager : MonoBehaviour
    {
        public static HousingManager Instance;
        private bool hasHouse = false;
        private int houseID = -1;
        private List<int> placedFurniture = new List<int>();
        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }
        public bool HasHouse() => hasHouse;
        public void BuyHouse(int id) { houseID = id; hasHouse = true; }
        public void PlaceFurniture(int id) => placedFurniture.Add(id);
        public void RemoveFurniture(int id) => placedFurniture.Remove(id);
        public List<int> GetPlacedFurniture() => placedFurniture;
    }

    public class FishingManager : MonoBehaviour
    {
        public static FishingManager Instance;
        private bool isFishing = false;
        private float fishingTimer = 0f;
        private void Awake() { Instance = this; }
        public bool IsFishing() => isFishing;
        public void StartFishing() { isFishing = true; fishingTimer = Random.Range(5f, 15f); }
        public void StopFishing() => isFishing = false;
        private void Update() { if (!isFishing) return; fishingTimer -= Time.deltaTime; if (fishingTimer <= 0) { isFishing = false; Debug.Log("[Fishing] Got a bite!"); } }
    }

    public class AuctionHouseManager : MonoBehaviour
    {
        public static AuctionHouseManager Instance;
        private List<AuctionEntry> auctions = new List<AuctionEntry>();
        public class AuctionEntry { public int id; public int itemID; public int quantity; public int price; public string seller; public float expiry; }
        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }
        public void CreateAuction(int itemID, int qty, int price, string seller) => auctions.Add(new AuctionEntry { id = auctions.Count, itemID = itemID, quantity = qty, price = price, seller = seller, expiry = Time.time + 86400f });
        public List<AuctionEntry> GetAuctions() => auctions;
        public bool BuyAuction(int id) { var a = auctions.Find(x => x.id == id); if (a == null) return false; auctions.Remove(a); return true; }
    }

    public class TradingManager : MonoBehaviour
    {
        public static TradingManager Instance;
        private bool inTrade = false;
        private List<int> offeredItems = new List<int>();
        private void Awake() { Instance = this; }
        public bool IsInTrade() => inTrade;
        public void StartTrade() { inTrade = true; offeredItems.Clear(); }
        public void AddItemToTrade(int itemID) => offeredItems.Add(itemID);
        public void AcceptTrade() { inTrade = false; offeredItems.Clear(); }
        public void CancelTrade() { inTrade = false; offeredItems.Clear(); }
    }
}
