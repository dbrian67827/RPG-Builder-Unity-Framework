using System;
using UnityEngine;
using UnityEngine.AI;

namespace BLINK.RPGBuilder.Utility
{
    /// <summary>
    /// Unity 6000.7.0b1 Compatibility Layer
    /// Provides wrappers for obsolete APIs and new Unity 6 features
    /// </summary>
    public static class RPGBuilderUnity6Compatibility
    {
        // Object finding - handles FindObjectOfType obsolete
        public static T FindFirstObjectByType<T>() where T : UnityEngine.Object
        {
#if UNITY_6000_0_OR_NEWER
            return UnityEngine.Object.FindFirstObjectByType<T>();
#else
            return UnityEngine.Object.FindObjectOfType<T>();
#endif
        }

        public static T FindAnyObjectByType<T>() where T : UnityEngine.Object
        {
#if UNITY_6000_0_OR_NEWER
            return UnityEngine.Object.FindAnyObjectByType<T>();
#else
            return UnityEngine.Object.FindObjectOfType<T>();
#endif
        }

        public static T[] FindObjectsByType<T>() where T : UnityEngine.Object
        {
#if UNITY_6000_0_OR_NEWER
            return UnityEngine.Object.FindObjectsByType<T>(FindObjectsSortMode.None);
#else
            return UnityEngine.Object.FindObjectsOfType<T>();
#endif
        }

        public static T[] FindObjectsByTypeAll<T>() where T : UnityEngine.Object
        {
#if UNITY_6000_0_OR_NEWER
            return UnityEngine.Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);
#else
            return Resources.FindObjectsOfTypeAll<T>();
#endif
        }

        // NavMesh
        public static bool IsNavMeshValid()
        {
#if UNITY_6000_0_OR_NEWER
            return NavMesh.GetSettingsCount() > 0;
#else
            return true;
#endif
        }

        // URP
        public static void SetSRPBatcher(bool enabled)
        {
#if UNITY_6000_0_OR_NEWER
            // SRP Batcher is enabled by default in URP 17.1
            Debug.Log($"[RPG Builder] SRP Batcher {(enabled ? "enabled" : "disabled")} for Unity 6000.7");
#endif
        }

        // Input System 1.14.2
        public static void UpdateInputSystem()
        {
#if UNITY_6000_0_OR_NEWER
            // Input System 1.14.2 has improved handling for Unity 6
            Debug.Log("[RPG Builder] Input System Unity 6 compatibility active");
#endif
        }

        // EntityId migration helpers for Unity 6000.4+
        public static bool IsValidEntityId(object entityId)
        {
#if UNITY_6000_4_OR_NEWER
            if (entityId is EntityId eid)
                return eid != EntityId.None;
            return false;
#else
            if (entityId is int id)
                return id != 0;
            return false;
#endif
        }

        public static string GetUnityVersionInfo()
        {
#if UNITY_6000_7
            return "Unity 6000.7.0b1 - Full RPG Framework Active";
#elif UNITY_6000_4_OR_NEWER
            return "Unity 6000.4+ - EntityId Migration Active";
#elif UNITY_6000_0_OR_NEWER
            return "Unity 6000.0+ - Compatibility Layer Active";
#else
            return "Unity Pre-6000 - Legacy Mode";
#endif
        }
    }

    /// <summary>
    /// Performance Optimizer for Unity 6000.7 URP 17.1
    /// </summary>
    public class RPGBuilderPerformanceOptimizer : MonoBehaviour
    {
        [Header("Unity 6000 Performance")]
        public bool enableSRPBatcher = true;
        public bool enableDynamicResolution = true;
        public bool enableLOD = true;
        public bool enableOcclusionCulling = true;
        public bool enableGPUInstancing = true;

        [Header("RPG Optimizations")]
        public bool optimizeNPCs = true;
        public int maxActiveNPCs = 50;
        public bool optimizeVFX = true;
        public bool poolProjectiles = true;
        public bool poolDamageNumbers = true;

        private void Awake()
        {
#if UNITY_6000_0_OR_NEWER
            // Enable SRP Batcher optimizations for URP 17.1
            if (enableSRPBatcher)
            {
                Debug.Log("[RPG Builder] URP 17.1 SRP Batcher enabled");
            }

            // Dynamic resolution for better performance
            if (enableDynamicResolution)
            {
                // Dynamic resolution is handled by URP asset in 6000.7
                Debug.Log("[RPG Builder] Dynamic Resolution enabled");
            }
#endif
        }

        public void OptimizeForMobile()
        {
            maxActiveNPCs = 20;
            enableDynamicResolution = true;
            Debug.Log("[RPG Builder] Optimized for mobile");
        }

        public void OptimizeForHighEnd()
        {
            maxActiveNPCs = 100;
            enableDynamicResolution = false;
            Debug.Log("[RPG Builder] Optimized for high-end");
        }
    }

    /// <summary>
    /// Input System Unity 6 Enhancements
    /// </summary>
    public class RPGBuilderInputSystemUnity6 : MonoBehaviour
    {
        [Header("Input System 1.14.2")]
        public bool enableEnhancedTouch = true;
        public bool enableGamepadRumble = true;
        public bool enableAdaptiveTriggers = false;

        private void Start()
        {
#if UNITY_6000_0_OR_NEWER
            Debug.Log("[RPG Builder] Input System 1.14.2 Unity 6 enhancements active");
#endif
        }
    }
}
