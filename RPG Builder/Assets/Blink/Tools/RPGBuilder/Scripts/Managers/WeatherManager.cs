
using System.Collections.Generic;
using UnityEngine;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Managers
{
    public class WeatherManager : MonoBehaviour
    {
        public static WeatherManager Instance;
        private int currentWeatherID = -1;
        private float weatherTimer = 0f;
        private float transitionTimer = 0f;
        private GameObject activeWeatherEffect;
        public System.Action<int> OnWeatherChanged;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public int GetCurrentWeatherID() => currentWeatherID;
        public RPGWeather GetCurrentWeather() => currentWeatherID != -1 && GameDatabase.Instance.GetWeather().TryGetValue(currentWeatherID, out var w) ? w : null;

        public void ChangeWeather(int id)
        {
            if (currentWeatherID == id) return;
            if (activeWeatherEffect != null) Destroy(activeWeatherEffect);
            currentWeatherID = id;
            if (GameDatabase.Instance.GetWeather().TryGetValue(id, out var weather))
            {
                weatherTimer = weather.duration;
                if (weather.weatherEffect != null) activeWeatherEffect = Instantiate(weather.weatherEffect);
                OnWeatherChanged?.Invoke(id);
                Debug.Log($"[Weather] Changed to {weather.entryDisplayName}");
            }
        }
        private void Update()
        {
            if (currentWeatherID == -1) return;
            weatherTimer -= Time.deltaTime;
            if (weatherTimer <= 0) { currentWeatherID = -1; if (activeWeatherEffect != null) Destroy(activeWeatherEffect); }
        }
    }

    public class ReputationManager : MonoBehaviour
    {
        public static ReputationManager Instance;
        private Dictionary<int, int> reputations = new Dictionary<int, int>();
        public System.Action<int, int> OnReputationChanged;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public int GetReputation(int factionID) => reputations.TryGetValue(factionID, out var r) ? r : 0;
        public void AddReputation(int factionID, int amount) { int cur = GetReputation(factionID); reputations[factionID] = cur + amount; OnReputationChanged?.Invoke(factionID, reputations[factionID]); }
        public void SetReputation(int factionID, int amount) { reputations[factionID] = amount; OnReputationChanged?.Invoke(factionID, amount); }
        public string GetReputationLevel(int factionID)
        {
            int rep = GetReputation(factionID);
            if (rep < 0) return "Hated";
            if (rep < 1000) return "Neutral";
            if (rep < 3000) return "Friendly";
            if (rep < 9000) return "Honored";
            if (rep < 21000) return "Revered";
            return "Exalted";
        }
    }

    public class PartyManager : MonoBehaviour
    {
        public static PartyManager Instance;
        private List<string> partyMembers = new List<string>();
        private bool inParty = false;
        public System.Action OnPartyJoined;
        public System.Action OnPartyLeft;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsInParty() => inParty;
        public void JoinParty(List<string> members) { partyMembers = members; inParty = true; OnPartyJoined?.Invoke(); }
        public void LeaveParty() { partyMembers.Clear(); inParty = false; OnPartyLeft?.Invoke(); }
        public List<string> GetMembers() => partyMembers;
        public int GetMemberCount() => partyMembers.Count;
    }

    public class GuildManager : MonoBehaviour
    {
        public static GuildManager Instance;
        private bool inGuild = false;
        private string guildName = "";
        private int guildLevel = 1;
        private int guildExp = 0;
        public System.Action OnGuildJoined;
        public System.Action OnGuildLeft;

        private void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }

        public bool IsInGuild() => inGuild;
        public string GetGuildName() => guildName;
        public int GetGuildLevel() => guildLevel;
        public void JoinGuild(string name) { guildName = name; inGuild = true; OnGuildJoined?.Invoke(); }
        public void LeaveGuild() { guildName = ""; inGuild = false; OnGuildLeft?.Invoke(); }
        public void AddExp(int amount) { guildExp += amount; }
    }
}
