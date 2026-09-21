using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class HousingManager : MonoBehaviour
    {
        public static HousingManager Instance { get; private set; }

        public class House
        {
            public int houseID;
            public string houseName;
            public Vector3 position;
            public List<PlacedFurniture> furniture = new List<PlacedFurniture>();
            public int maxFurniture = 100;
        }

        public class PlacedFurniture
        {
            public int itemID;
            public Vector3 position;
            public Vector3 rotation;
            public Vector3 scale = Vector3.one;
        }

        private House currentHouse;
        private bool isInHousingMode = false;

        public Action<House> OnHouseEntered;
        public Action OnHouseExited;
        public Action<PlacedFurniture> OnFurniturePlaced;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void EnterHouse(int houseID)
        {
            // Load house
            currentHouse = new House { houseID = houseID, houseName = $"House {houseID}" };
            OnHouseEntered?.Invoke(currentHouse);
            Debug.Log($"Entered House: {houseID}");
        }

        public void ExitHouse()
        {
            currentHouse = null;
            OnHouseExited?.Invoke();
        }

        public void PlaceFurniture(int itemID, Vector3 pos, Vector3 rot)
        {
            if (currentHouse == null) return;
            if (currentHouse.furniture.Count >= currentHouse.maxFurniture) return;

            var furn = new PlacedFurniture { itemID = itemID, position = pos, rotation = rot };
            currentHouse.furniture.Add(furn);
            OnFurniturePlaced?.Invoke(furn);

            // Instantiate visual
            var item = GameDatabase.Instance.GetItems()[itemID];
            if (item.itemWorldModel != null)
                Instantiate(item.itemWorldModel, pos, Quaternion.Euler(rot));
        }

        public void ToggleHousingMode()
        {
            isInHousingMode = !isInHousingMode;
            Debug.Log($"Housing Mode: {isInHousingMode}");
        }

        public bool IsInHousingMode() => isInHousingMode;
        public House GetCurrentHouse() => currentHouse;
    }

    public class FishingManager : MonoBehaviour
    {
        public static FishingManager Instance { get; private set; }

        public enum FishingState { Idle, Casting, Waiting, Reeling, Caught, Failed }

        private FishingState currentState = FishingState.Idle;
        private float fishingTimer;
        private int currentFishingSpotID = -1;

        public Action OnFishingStarted;
        public Action<RPGItem> OnFishCaught;
        public Action OnFishingFailed;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void StartFishing(int spotID)
        {
            if (currentState != FishingState.Idle) return;
            currentState = FishingState.Casting;
            currentFishingSpotID = spotID;
            fishingTimer = 2f; // cast time
            OnFishingStarted?.Invoke();
            Debug.Log("Started Fishing...");
        }

        public void UpdateFishing()
        {
            switch (currentState)
            {
                case FishingState.Casting:
                    fishingTimer -= Time.deltaTime;
                    if (fishingTimer <= 0)
                    {
                        currentState = FishingState.Waiting;
                        fishingTimer = UnityEngine.Random.Range(3f, 10f);
                    }
                    break;
                case FishingState.Waiting:
                    fishingTimer -= Time.deltaTime;
                    if (fishingTimer <= 0)
                    {
                        // Fish bite
                        currentState = FishingState.Reeling;
                        fishingTimer = 2f;
                        Debug.Log("Fish Bite! Reel now!");
                    }
                    break;
                case FishingState.Reeling:
                    fishingTimer -= Time.deltaTime;
                    if (fishingTimer <= 0)
                    {
                        // Failed if not reeled in time
                        FailFishing();
                    }
                    break;
            }
        }

        public void ReelIn()
        {
            if (currentState != FishingState.Reeling) return;

            // Roll loot
            var allTables = GameDatabase.Instance.GetLootTables();
            // Simplified - would roll fishing loot table
            var allItems = GameDatabase.Instance.GetItems();
            if (allItems.Count > 0)
            {
                var randomItem = allItems.Values.ToList()[UnityEngine.Random.Range(0, allItems.Count)];
                InventoryManager.Instance.AddItem(randomItem.ID, 1);
                OnFishCaught?.Invoke(randomItem);
                Debug.Log($"Caught: {randomItem.entryDisplayName}");
            }

            currentState = FishingState.Idle;
        }

        public void FailFishing()
        {
            currentState = FishingState.Idle;
            OnFishingFailed?.Invoke();
            Debug.Log("Fishing Failed");
        }

        private void Update()
        {
            if (currentState != FishingState.Idle) UpdateFishing();
        }

        public FishingState GetState() => currentState;
    }

    public class AuctionHouseManager : MonoBehaviour
    {
        public static AuctionHouseManager Instance { get; private set; }

        public class Auction
        {
            public int auctionID;
            public int itemID;
            public int itemDataID;
            public int count;
            public int sellerID;
            public string sellerName;
            public int buyoutPrice;
            public int startingPrice;
            public int currentBid;
            public string currentBidder;
            public DateTime expiry;
            public int currencyID;
        }

        private List<Auction> activeAuctions = new List<Auction>();
        private List<Auction> myAuctions = new List<Auction>();
        private List<Auction> myBids = new List<Auction>();

        public Action<Auction> OnAuctionCreated;
        public Action<Auction> OnAuctionSold;
        public Action<Auction> OnAuctionExpired;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public bool CreateAuction(int itemID, int itemDataID, int count, int buyoutPrice, int currencyID, float durationHours)
        {
            var economySettings = GameDatabase.Instance.GetEconomySettings();
            if (economySettings != null && activeAuctions.Count >= economySettings.MaxAuctionsPerPlayer) return false;

            int deposit = Mathf.RoundToInt(buyoutPrice * (economySettings?.AuctionDepositPercent / 100f ?? 0.05f));
            if (!InventoryManager.Instance.HasCurrency(currencyID, deposit)) return false;

            InventoryManager.Instance.RemoveCurrency(currencyID, deposit);
            InventoryManager.Instance.RemoveItem(itemID, count);

            var auction = new Auction
            {
                auctionID = UnityEngine.Random.Range(1000, 999999),
                itemID = itemID,
                itemDataID = itemDataID,
                count = count,
                sellerName = Character.Instance.CharacterName,
                buyoutPrice = buyoutPrice,
                startingPrice = Mathf.RoundToInt(buyoutPrice * 0.7f),
                currentBid = 0,
                currencyID = currencyID,
                expiry = DateTime.Now.AddHours(durationHours)
            };

            activeAuctions.Add(auction);
            myAuctions.Add(auction);
            OnAuctionCreated?.Invoke(auction);
            Debug.Log($"Auction Created: {itemID} x{count} for {buyoutPrice}");
            return true;
        }

        public bool BuyoutAuction(int auctionID)
        {
            var auction = activeAuctions.FirstOrDefault(a => a.auctionID == auctionID);
            if (auction == null) return false;
            if (!InventoryManager.Instance.HasCurrency(auction.currencyID, auction.buyoutPrice)) return false;

            InventoryManager.Instance.RemoveCurrency(auction.currencyID, auction.buyoutPrice);
            InventoryManager.Instance.AddItem(auction.itemID, auction.count);

            // Pay seller minus cut
            float cut = GameDatabase.Instance.GetEconomySettings()?.AuctionHouseCutPercent / 100f ?? 0.05f;
            int sellerReceive = Mathf.RoundToInt(auction.buyoutPrice * (1f - cut));

            // Would send mail to seller
            MailManager.Instance?.SendCustomMail("Auction Sold", $"Your auction for {auction.itemID} sold for {auction.buyoutPrice}", "Auction House",
                new List<RPGMailTemplate.MailAttachment> { new RPGMailTemplate.MailAttachment { attachmentType = RPGMailTemplate.MailAttachment.AttachmentType.Currency, currencyID = auction.currencyID, amount = sellerReceive } });

            activeAuctions.Remove(auction);
            OnAuctionSold?.Invoke(auction);
            return true;
        }

        public List<Auction> SearchAuctions(string searchTerm, int maxPrice = -1)
        {
            var result = activeAuctions.Where(a =>
            {
                var item = GameDatabase.Instance.GetItems()[a.itemID];
                bool matchesSearch = string.IsNullOrEmpty(searchTerm) || item.entryDisplayName.ToLower().Contains(searchTerm.ToLower());
                bool matchesPrice = maxPrice == -1 || a.buyoutPrice <= maxPrice;
                return matchesSearch && matchesPrice;
            }).ToList();
            return result;
        }

        public List<Auction> GetActiveAuctions() => new List<Auction>(activeAuctions);
        public List<Auction> GetMyAuctions() => new List<Auction>(myAuctions);
    }

    public class TradingManager : MonoBehaviour
    {
        public static TradingManager Instance { get; private set; }

        public class Trade
        {
            public string otherPlayerName;
            public List<CharacterEntries.ItemEntry> offeredItems = new List<CharacterEntries.ItemEntry>();
            public List<CharacterEntries.CurrencyEntry> offeredCurrencies = new List<CharacterEntries.CurrencyEntry>();
            public List<CharacterEntries.ItemEntry> otherOfferedItems = new List<CharacterEntries.ItemEntry>();
            public List<CharacterEntries.CurrencyEntry> otherOfferedCurrencies = new List<CharacterEntries.CurrencyEntry>();
            public bool isLocked = false;
            public bool otherIsLocked = false;
            public bool isAccepted = false;
            public bool otherIsAccepted = false;
        }

        private Trade currentTrade;
        private bool isTrading = false;

        public Action<Trade> OnTradeStarted;
        public Action<Trade> OnTradeUpdated;
        public Action OnTradeCompleted;
        public Action OnTradeCancelled;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void StartTrade(string otherPlayerName)
        {
            currentTrade = new Trade { otherPlayerName = otherPlayerName };
            isTrading = true;
            OnTradeStarted?.Invoke(currentTrade);
            Debug.Log($"Trade started with {otherPlayerName}");
        }

        public void AddItemToTrade(int itemID, int count)
        {
            if (currentTrade == null) return;
            currentTrade.offeredItems.Add(new CharacterEntries.ItemEntry { itemID = itemID, count = count });
            OnTradeUpdated?.Invoke(currentTrade);
        }

        public void LockTrade()
        {
            if (currentTrade == null) return;
            currentTrade.isLocked = true;
            OnTradeUpdated?.Invoke(currentTrade);
            TryCompleteTrade();
        }

        public void AcceptTrade()
        {
            if (currentTrade == null || !currentTrade.isLocked || !currentTrade.otherIsLocked) return;
            currentTrade.isAccepted = true;
            OnTradeUpdated?.Invoke(currentTrade);
            TryCompleteTrade();
        }

        private void TryCompleteTrade()
        {
            if (currentTrade == null) return;
            if (currentTrade.isAccepted && currentTrade.otherIsAccepted)
            {
                // Exchange items
                foreach (var item in currentTrade.otherOfferedItems)
                    InventoryManager.Instance.AddItem(item.itemID, item.count);
                foreach (var cur in currentTrade.otherOfferedCurrencies)
                    InventoryManager.Instance.AddCurrency(cur.currencyID, cur.amount);

                foreach (var item in currentTrade.offeredItems)
                    InventoryManager.Instance.RemoveItem(item.itemID, item.count);
                foreach (var cur in currentTrade.offeredCurrencies)
                    InventoryManager.Instance.RemoveCurrency(cur.currencyID, cur.amount);

                isTrading = false;
                OnTradeCompleted?.Invoke();
                currentTrade = null;
                Debug.Log("Trade Completed");
            }
        }

        public void CancelTrade()
        {
            isTrading = false;
            currentTrade = null;
            OnTradeCancelled?.Invoke();
            Debug.Log("Trade Cancelled");
        }

        public bool IsTrading() => isTrading;
        public Trade GetCurrentTrade() => currentTrade;
    }
}
