# Unity 6000.7.0b1 Upgrade Guide

## Package Upgrades
| Package | Old | New |
|---------|-----|-----|
| ai.navigation | 2.0.7 | 2.0.8 |
| inputsystem | 1.14.0 | 1.14.2 |
| universal RP | 17.0.4 | 17.1.0 |
| timeline | 1.8.7 | 1.8.8 |
| visualscripting | 1.9.6 | 1.9.7 |
| localization | - | 1.5.5 |
| netcode.gameobjects | - | 2.4.0 |
| multiplayer.center | - | 1.0.0 |
| cinemachine | - | 3.1.3 |

## Obsolete API Fixes

### 1. EndNameEditAction -> AssetCreationEndAction (xNode)
**Error**: `Assets\RPG Builder\...\NodeEditorUtilities.cs(277,41): error CS0619: 'EndNameEditAction' is obsolete: 'EndNameEditAction is obsolete. Use AssetCreationEndAction that uses EntityId instead of int for instance IDs.'`

**Fix**:
```csharp
#if UNITY_6000_4_OR_NEWER
    ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
        EntityId.None, // was 0
        CreateInstance<DoCreateCodeFile>(), ...)
    public class DoCreateCodeFile : AssetCreationEndAction {
        public override void Action(EntityId instanceId, ...) // was int
#else
    // old API
#endif
```

### 2. InstanceIDToObject -> EntityIdToObject
```csharp
#if UNITY_6000_4_OR_NEWER
    EditorUtility.EntityIdToObject(entityId)
#else
    EditorUtility.InstanceIDToObject(instanceID)
#endif
```

### 3. FindObjectOfType -> FindFirstObjectByType
```csharp
#if UNITY_6000_0_OR_NEWER
    FindFirstObjectByType<T>()
#else
    FindObjectOfType<T>()
#endif
```

### 4. FindObjectsOfType -> FindObjectsByType
```csharp
#if UNITY_6000_0_OR_NEWER
    FindObjectsByType<T>(FindObjectsSortMode.None)
#else
    FindObjectsOfType<T>()
#endif
```

### 5. GetInstanceID -> GetEntityId
```csharp
#if UNITY_6000_4_OR_NEWER
    GetEntityId()
#else
    GetInstanceID()
#endif
```

## Performance
- URP 17.1 SRP Batcher enabled by default
- LOD and Dynamic Resolution support
- GPU Instancing for VFX
- Object pooling for projectiles and damage numbers

## Testing
- Open project in Unity 6000.7.0b1
- Check console for no CS0619 errors
- Test xNode graph creation (Create -> xNode -> Node)
- Test FindFirstObjectByType in play mode
- Verify all new managers initialize
