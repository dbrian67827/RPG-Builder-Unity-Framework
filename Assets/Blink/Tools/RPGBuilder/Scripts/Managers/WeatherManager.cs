using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLINK.RPGBuilder.Managers
{
    public class WeatherManager : MonoBehaviour
    {
        public static WeatherManager Instance { get; private set; }

        private RPGWeather currentWeather;
        private float weatherTimer;
        private float transitionTimer;
        private RPGWeather transitioningTo;
        private GameObject currentWeatherVFX;
        private AudioSource weatherAudioSource;

        public Action<RPGWeather> OnWeatherChanged;
        public Action<RPGWeather> OnWeatherStarted;
        public Action<RPGWeather> OnWeatherEnded;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            weatherAudioSource = gameObject.AddComponent<AudioSource>();
            weatherAudioSource.loop = true;
        }

        private void Start()
        {
            // Start with clear weather
            var all = GameDatabase.Instance.GetWeather();
            if (all != null && all.Count > 0)
            {
                var clear = all.Values.FirstOrDefault(w => w.weatherType == RPGWeather.WeatherType.Clear);
                if (clear != null) SetWeather(clear.ID);
            }

            InvokeRepeating(nameof(CheckRandomWeather), 30f, 60f);
        }

        private void Update()
        {
            if (currentWeather != null)
            {
                weatherTimer -= Time.deltaTime;
                if (weatherTimer <= 0)
                {
                    EndCurrentWeather();
                }
            }

            if (transitioningTo != null)
            {
                transitionTimer -= Time.deltaTime;
                if (transitionTimer <= 0)
                {
                    CompleteTransition();
                }
            }
        }

        private void CheckRandomWeather()
        {
            var all = GameDatabase.Instance.GetWeather();
            if (all == null || all.Count == 0) return;

            if (UnityEngine.Random.Range(0f, 100f) < 15f) // 15% chance to change
            {
                var randomWeather = all.Values.ToList()[UnityEngine.Random.Range(0, all.Count)];
                if (randomWeather.ID != currentWeather?.ID)
                    TransitionToWeather(randomWeather.ID);
            }
        }

        public void SetWeather(int weatherID)
        {
            var all = GameDatabase.Instance.GetWeather();
            if (!all.ContainsKey(weatherID)) return;

            if (currentWeatherVFX != null) Destroy(currentWeatherVFX);

            currentWeather = all[weatherID];
            weatherTimer = currentWeather.duration;

            if (currentWeather.weatherVFX != null)
                currentWeatherVFX = Instantiate(currentWeather.weatherVFX);

            if (currentWeather.weatherSFX != null)
            {
                weatherAudioSource.clip = currentWeather.weatherSFX;
                weatherAudioSource.Play();
            }

            OnWeatherStarted?.Invoke(currentWeather);
            OnWeatherChanged?.Invoke(currentWeather);
            Debug.Log($"Weather Changed To: {currentWeather.entryDisplayName}");
        }

        public void TransitionToWeather(int weatherID)
        {
            var all = GameDatabase.Instance.GetWeather();
            if (!all.ContainsKey(weatherID)) return;

            transitioningTo = all[weatherID];
            transitionTimer = transitioningTo.transitionTime;
        }

        private void CompleteTransition()
        {
            if (transitioningTo == null) return;
            SetWeather(transitioningTo.ID);
            transitioningTo = null;
        }

        private void EndCurrentWeather()
        {
            if (currentWeather == null) return;
            OnWeatherEnded?.Invoke(currentWeather);

            if (currentWeatherVFX != null) Destroy(currentWeatherVFX);
            weatherAudioSource.Stop();

            // Default to clear
            var all = GameDatabase.Instance.GetWeather();
            var clear = all.Values.FirstOrDefault(w => w.weatherType == RPGWeather.WeatherType.Clear);
            if (clear != null) SetWeather(clear.ID);
            else currentWeather = null;
        }

        public RPGWeather GetCurrentWeather() => currentWeather;
        public bool IsWeatherActive(int weatherID) => currentWeather != null && currentWeather.ID == weatherID;
        public float GetWeatherRemainingTime() => weatherTimer;
        public float GetMovementSpeedModifier() => currentWeather?.movementSpeedModifier ?? 1f;
        public float GetVisibilityModifier() => currentWeather?.visibilityModifier ?? 1f;
    }

    public class ReputationManager : MonoBehaviour
    {
        public static ReputationManager Instance { get; private set; }

        private Dictionary<int, int> factionReputation = new Dictionary<int, int>();
        public Action<int, int, int> OnReputationChanged; // factionID, oldRep, newRep
        public Action<RPGReputationReward> OnReputationRankUp;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            LoadReputation();
        }

        private void LoadReputation()
        {
            if (Character.Instance?.CharacterData?.FactionReputation != null)
            {
                foreach (var rep in Character.Instance.CharacterData.FactionReputation)
                    factionReputation[rep.factionID] = rep.reputation;
            }
        }

        public void AddReputation(int factionID, int amount)
        {
            if (!factionReputation.ContainsKey(factionID)) factionReputation[factionID] = 0;
            int oldRep = factionReputation[factionID];
            factionReputation[factionID] += amount;

            var allRewards = GameDatabase.Instance.GetReputationRewards();
            if (allRewards != null)
            {
                var rewards = allRewards.Values.Where(r => r.factionID == factionID && r.requiredReputation > oldRep && r.requiredReputation <= factionReputation[factionID]).ToList();
                foreach (var reward in rewards)
                {
                    GrantReputationReward(reward);
                    OnReputationRankUp?.Invoke(reward);
                }
            }

            OnReputationChanged?.Invoke(factionID, oldRep, factionReputation[factionID]);
            SaveReputation();
            Debug.Log($"Reputation for faction {factionID}: {factionReputation[factionID]} (+{amount})");
        }

        private void GrantReputationReward(RPGReputationReward reward)
        {
            foreach (var r in reward.rewards)
            {
                switch (r.rewardType)
                {
                    case RPGReputationReward.RepReward.RewardType.Item:
                        InventoryManager.Instance.AddItem(r.itemID, r.amount);
                        break;
                    case RPGReputationReward.RepReward.RewardType.Currency:
                        InventoryManager.Instance.AddCurrency(r.currencyID, r.amount);
                        break;
                    case RPGReputationReward.RepReward.RewardType.Title:
                        TitleManager.Instance?.UnlockTitle(r.titleID);
                        break;
                    case RPGReputationReward.RepReward.RewardType.Mount:
                        MountManager.Instance?.UnlockMount(r.mountID);
                        break;
                }
            }
            GameActionsManager.Instance.ExecuteGameActions(reward.OnUnlockActions, null, null);
        }

        public int GetReputation(int factionID) => factionReputation.ContainsKey(factionID) ? factionReputation[factionID] : 0;
        public int GetReputationRank(int factionID)
        {
            var allRewards = GameDatabase.Instance.GetReputationRewards();
            if (allRewards == null) return 0;
            var rewards = allRewards.Values.Where(r => r.factionID == factionID).OrderBy(r => r.requiredReputation).ToList();
            int rank = 0;
            int rep = GetReputation(factionID);
            foreach (var r in rewards)
                if (rep >= r.requiredReputation) rank = r.reputationRank;
            return rank;
        }

        public string GetReputationRankName(int factionID)
        {
            var allRewards = GameDatabase.Instance.GetReputationRewards();
            if (allRewards == null) return "Neutral";
            var rewards = allRewards.Values.Where(r => r.factionID == factionID).OrderBy(r => r.requiredReputation).ToList();
            string rankName = "Neutral";
            int rep = GetReputation(factionID);
            foreach (var r in rewards)
                if (rep >= r.requiredReputation) rankName = r.rankName;
            return rankName;
        }

        private void SaveReputation()
        {
            if (Character.Instance?.CharacterData == null) return;
            var list = new List<CharacterEntries.FactionReputationEntry>();
            foreach (var kvp in factionReputation)
                list.Add(new CharacterEntries.FactionReputationEntry { factionID = kvp.Key, reputation = kvp.Value });
            Character.Instance.CharacterData.FactionReputation = list;
        }
    }

    public class PartyManager : MonoBehaviour
    {
        public static PartyManager Instance { get; private set; }

        public class PartyMember
        {
            public string playerName;
            public int level;
            public int classID;
            public float healthPercent;
            public bool isLeader;
            public bool isOnline;
            public Vector3 position;
            public int gameSceneID;
        }

        private List<PartyMember> partyMembers = new List<PartyMember>();
        private bool isInParty = false;
        private bool isLeader = false;
        private int maxPartySize = 5;

        public Action<PartyMember> OnMemberJoined;
        public Action<PartyMember> OnMemberLeft;
        public Action OnPartyDisbanded;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void CreateParty()
        {
            partyMembers.Clear();
            var self = new PartyMember
            {
                playerName = Character.Instance.CharacterName,
                level = Character.Instance.Level,
                classID = Character.Instance.ClassID,
                healthPercent = 1f,
                isLeader = true,
                isOnline = true
            };
            partyMembers.Add(self);
            isInParty = true;
            isLeader = true;
            Debug.Log("Party Created");
        }

        public void InvitePlayer(string playerName)
        {
            if (!isInParty) CreateParty();
            if (partyMembers.Count >= maxPartySize) return;
            // Would send network invite
            Debug.Log($"Invited {playerName} to party");
        }

        public void AddMember(PartyMember member)
        {
            if (partyMembers.Count >= maxPartySize) return;
            partyMembers.Add(member);
            OnMemberJoined?.Invoke(member);
        }

        public void RemoveMember(string playerName)
        {
            var member = partyMembers.FirstOrDefault(m => m.playerName == playerName);
            if (member != null)
            {
                partyMembers.Remove(member);
                OnMemberLeft?.Invoke(member);
                if (partyMembers.Count <= 1) DisbandParty();
            }
        }

        public void DisbandParty()
        {
            partyMembers.Clear();
            isInParty = false;
            isLeader = false;
            OnPartyDisbanded?.Invoke();
            Debug.Log("Party Disbanded");
        }

        public bool IsInParty() => isInParty;
        public bool IsLeader() => isLeader;
        public List<PartyMember> GetPartyMembers() => new List<PartyMember>(partyMembers);
        public int GetPartySize() => partyMembers.Count;
    }

    public class GuildManager : MonoBehaviour
    {
        public static GuildManager Instance { get; private set; }

        public class Guild
        {
            public string guildName;
            public int level;
            public int exp;
            public List<string> members = new List<string>();
            public string leaderName;
            public string motd;
            public int maxMembers = 50;
        }

        private Guild currentGuild;
        private bool isInGuild = false;

        public Action<Guild> OnGuildJoined;
        public Action OnGuildLeft;
        public Action<int> OnGuildLevelUp;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void CreateGuild(string guildName)
        {
            currentGuild = new Guild
            {
                guildName = guildName,
                level = 1,
                exp = 0,
                leaderName = Character.Instance.CharacterName,
                motd = $"Welcome to {guildName}!",
                maxMembers = 50
            };
            currentGuild.members.Add(Character.Instance.CharacterName);
            isInGuild = true;
            OnGuildJoined?.Invoke(currentGuild);
            Debug.Log($"Guild Created: {guildName}");
        }

        public void JoinGuild(Guild guild)
        {
            currentGuild = guild;
            isInGuild = true;
            OnGuildJoined?.Invoke(guild);
        }

        public void LeaveGuild()
        {
            currentGuild = null;
            isInGuild = false;
            OnGuildLeft?.Invoke();
        }

        public void AddGuildExp(int amount)
        {
            if (currentGuild == null) return;
            currentGuild.exp += amount;
            int required = currentGuild.level * 1000;
            if (currentGuild.exp >= required)
            {
                currentGuild.exp -= required;
                currentGuild.level++;
                OnGuildLevelUp?.Invoke(currentGuild.level);
            }
        }

        public bool IsInGuild() => isInGuild;
        public Guild GetGuild() => currentGuild;
    }
}
