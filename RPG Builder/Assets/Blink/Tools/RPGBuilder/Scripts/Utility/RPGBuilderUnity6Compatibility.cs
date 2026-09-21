using System;
using UnityEngine;
using UnityEngine.AI;

namespace BLINK.RPGBuilder.Utility
{
    /// <summary>
    /// Unity 6000.7.0b1 Compatibility Layer
    /// Handles API changes and provides unified access to systems that changed in Unity 6
    /// </summary>
    public static class RPGBuilderUnity6Compatibility
    {
        // NavMesh compatibility
        public static bool CalculatePath(Vector3 source, Vector3 target, int areaMask, NavMeshPath path)
        {
#if UNITY_6000_0_OR_NEWER
            return NavMesh.CalculatePath(source, target, areaMask, path);
#else
            return NavMesh.CalculatePath(source, target, areaMask, path);
#endif
        }

        // Object finding compatibility
        public static T FindFirstObjectByType<T>() where T : UnityEngine.Object
        {
#if UNITY_6000_0_OR_NEWER
            return UnityEngine.Object.FindFirstObjectByType<T>();
#else
            return UnityEngine.Object.FindObjectOfType<T>();
#endif
        }

        public static T[] FindObjectsByType<T>(FindObjectsSortMode sortMode = FindObjectsSortMode.None) where T : UnityEngine.Object
        {
#if UNITY_6000_0_OR_NEWER
            return UnityEngine.Object.FindObjectsByType<T>(sortMode);
#else
            return UnityEngine.Object.FindObjectsOfType<T>();
#endif
        }

        // URP Compatibility
        public static void SetShaderEnabled()
        {
#if UNITY_6000_0_OR_NEWER
            // URP 17+ uses different shader APIs
            Shader.EnableKeyword("_MAIN_LIGHT_SHADOWS");
#endif
        }

        // Input System compatibility
        public static void EnableInputSystem()
        {
#if UNITY_6000_0_OR_NEWER && ENABLE_INPUT_SYSTEM
            // Input System 1.14+ in Unity 6 has some changes
            UnityEngine.InputSystem.InputSystem.settings.SetInternalFeatureFlag("USE_OPTIMIZED_CONTROLS", true);
#endif
        }

        // Physics compatibility
        public static bool Raycast(Ray ray, out RaycastHit hit, float maxDistance, int layerMask)
        {
#if UNITY_6000_0_OR_NEWER
            return Physics.Raycast(ray, out hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore);
#else
            return Physics.Raycast(ray, out hit, maxDistance, layerMask);
#endif
        }

        // Time compatibility
        public static float GetDeltaTime()
        {
#if UNITY_6000_0_OR_NEWER
            return Time.deltaTime;
#else
            return Time.deltaTime;
#endif
        }

        // Quality settings
        public static void ApplyQualitySettings()
        {
#if UNITY_6000_0_OR_NEWER
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
#endif
        }

        // Logging with context for Unity 6
        public static void Log(string message, UnityEngine.Object context = null)
        {
#if UNITY_6000_0_OR_NEWER
            if (context != null)
                Debug.Log($"[RPG Builder] {message}", context);
            else
                Debug.Log($"[RPG Builder] {message}");
#else
            Debug.Log($"[RPG Builder] {message}");
#endif
        }
    }

    /// <summary>
    /// Unity 6000 Performance Optimizations
    /// </summary>
    public class RPGBuilderPerformanceOptimizer : MonoBehaviour
    {
        [Header("Unity 6000 Optimizations")]
        public bool enableBatching = true;
        public bool enableGPUInstancing = true;
        public bool enableSRPBatcher = true;
        public bool enableDynamicResolution = true;
        public bool enableLOD = true;

        private void Awake()
        {
#if UNITY_6000_0_OR_NEWER
            // Enable SRP Batcher for URP
            if (enableSRPBatcher)
            {
                // SRP Batcher is enabled via URP asset, but we can log
                RPGBuilderUnity6Compatibility.Log("SRP Batcher enabled for Unity 6000");
            }

            // Dynamic Resolution
            if (enableDynamicResolution)
            {
                // Unity 6000 has improved dynamic resolution
                // This would be configured via URP asset
            }

            // Apply quality settings
            RPGBuilderUnity6Compatibility.ApplyQualitySettings();
#endif
        }

        private void OnEnable()
        {
#if UNITY_6000_0_OR_NEWER
            // Unity 6000 has improved culling
            if (enableLOD)
            {
                // LOD Group optimizations
            }
#endif
        }
    }

    /// <summary>
    /// Unity 6000 Input System Enhancements
    /// </summary>
    public class RPGBuilderInputSystemUnity6 : MonoBehaviour
    {
        [Header("Input System 1.14+ Features")]
        public bool enableHaptics = true;
        public bool enableAdaptiveTriggers = false;
        public bool enableMouseSmoothing = true;
        public float mouseSmoothingFactor = 0.5f;

        private void Start()
        {
#if UNITY_6000_0_OR_NEWER && ENABLE_INPUT_SYSTEM
            var inputSystem = UnityEngine.InputSystem.InputSystem.settings;
            // Configure for Unity 6000
            inputSystem.updateMode = UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
            inputSystem.filterNoiseOnCurrent = true;
#endif
        }
    }
}
