
using System.Collections.Generic;
using UnityEngine;

namespace BLINK.RPGBuilder.Templates
{
    [CreateAssetMenu(fileName = "New Weather", menuName = "Blink/RPGBuilder/Weather/New Weather")]
    public class RPGWeather : RPGBuilderDatabaseEntry
    {
        public string weatherName = "Sunny";
        public Sprite icon;
        public GameObject weatherEffect;
        public AudioClip weatherSound;
        public Color skyColor = Color.blue;
        public Color fogColor = Color.gray;
        public float fogDensity = 0.01f;
        public float windStrength = 0f;
        public float rainIntensity = 0f;
        public float snowIntensity = 0f;
        public float duration = 600f;
        public float transitionTime = 10f;
        public List<int> statModifiers = new List<int>();
        public bool affectsGameplay = false;
        public float movementSpeedModifier = 1f;
        public float visibilityModifier = 1f;
        public bool isRare = false;
        public float chance = 10f;
        public string category = "Normal";
        public int sortOrder = 0;
    }
}
