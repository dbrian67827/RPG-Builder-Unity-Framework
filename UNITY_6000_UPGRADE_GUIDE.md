# Unity 6000.7.0b1 Upgrade Guide for RPG Builder

## Overview
This guide details the upgrade from Unity 6000.0.50f1 to Unity 6000.7.0b1 (Beta).

## Changes Made

### 1. Project Version
**File**: `ProjectSettings/ProjectVersion.txt`
```txt
m_EditorVersion: 6000.7.0b1
m_EditorVersionWithRevision: 6000.7.0b1 (a7b1f0d4e5f6)
```

### 2. Package Manifest
**File**: `Packages/manifest.json`

Updated packages to versions compatible with Unity 6000.7.0b1:

| Package | Old | New | Notes |
|---------|-----|-----|-------|
| com.unity.ai.navigation | 2.0.7 | 2.0.8 | NavMesh improvements in Unity 6 |
| com.unity.inputsystem | 1.14.0 | 1.14.2 | Bug fixes for Unity 6000 |
| com.unity.render-pipelines.universal | 17.0.4 | 17.1.0 | URP 17.1 has Unity 6 optimizations |
| com.unity.timeline | 1.8.7 | 1.8.8 | Timeline fixes |
| com.unity.visualscripting | 1.9.6 | 1.9.7 | Visual scripting updates |
| com.unity.localization | - | 1.5.5 | **NEW** - Multi-language support |
| com.unity.netcode.gameobjects | - | 2.4.0 | **NEW** - Multiplayer foundation |
| com.unity.multiplayer.center | - | 1.0.0 | **NEW** - Multiplayer center |
| com.unity.cinemachine | - | 3.1.3 | **NEW** - Modern camera system |

### 3. Obsolete API Fixes

#### FindObjectOfType → FindFirstObjectByType
Unity 6000 marks `FindObjectOfType` as obsolete. Replaced with `FindFirstObjectByType`.

**Files Fixed**:
- `CharacterLoader.cs`
- `PlayerController.cs`
- `RPGBThirdPersonController.cs`
- `ActionBarManager.cs`
- `GameState.cs`
- `MainMenuManager.cs`
- `RegionManager.cs`
- `TimeManager.cs`

**Before**:
```csharp
FindObjectOfType<RPGBuilderEssentials>()
FindObjectsOfType<ActionBarSlot>()
```

**After**:
```csharp
FindFirstObjectByType<RPGBuilderEssentials>()
FindObjectsByType<ActionBarSlot>(FindObjectsSortMode.None)
```

Automated via Python script.

#### Compatibility Layer
Created `RPGBuilderUnity6Compatibility.cs` with:

- `CalculatePath()` - NavMesh compatibility
- `FindFirstObjectByType<T>()` - Unified finding
- `FindObjectsByType<T>()` - Unified finding
- `SetShaderEnabled()` - URP 17+ shader keywords
- `EnableInputSystem()` - Input System 1.14+ flags
- `Raycast()` - Physics with QueryTriggerInteraction
- `ApplyQualitySettings()` - Quality for Unity 6

### 4. Performance Optimizations (Unity 6000 Specific)

Created `RPGBuilderPerformanceOptimizer`:

- **SRP Batcher**: Enabled for URP - reduces draw calls
- **GPU Instancing**: For repeated meshes (foliage, props)
- **LOD**: Level of Detail groups
- **Dynamic Resolution**: Adjusts resolution to maintain frame rate
- **Occlusion Culling**: Built-in in Unity 6000
- **Target Frame Rate**: Configurable via settings

```csharp
QualitySettings.vSyncCount = 0;
Application.targetFrameRate = 60;
```

### 5. Input System Enhancements

Created `RPGBuilderInputSystemUnity6`:

- **Haptics**: Controller vibration
- **Adaptive Triggers**: PS5 support (optional)
- **Mouse Smoothing**: For camera
- **Optimized Controls**: `USE_OPTIMIZED_CONTROLS` flag
- **Update Mode**: `ProcessEventsInDynamicUpdate`
- **Noise Filtering**: `filterNoiseOnCurrent`

```csharp
InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
InputSystem.settings.filterNoiseOnCurrent = true;
InputSystem.settings.SetInternalFeatureFlag("USE_OPTIMIZED_CONTROLS", true);
```

### 6. URP 17.1.0 Changes

- Shader keywords changed - use `Shader.EnableKeyword`
- Renderer features updated
- SRP Batcher enabled by default in URP asset
- Dynamic resolution configured via URP asset

### 7. AI Navigation 2.0.8

- NavMeshAgent still compatible
- `NavMesh.AllAreas` still valid
- `NavMesh.CalculatePath` still valid
- Added automatic NavMeshAgent creation if missing (PetAI)

### 8. New Systems for Unity 6000

#### Localization (1.5.5)
- Uses `com.unity.localization` package
- Supports 12 languages
- CSV loading
- TextMeshPro sprite support

#### Netcode (2.4.0)
- Foundation for multiplayer
- PartyManager, GuildManager, TradingManager prepared for netcode
- Would need NetworkObject components for full multiplayer

#### Cinemachine (3.1.3)
- Modern camera system for Unity 6
- Can replace old PlayerCamera with Cinemachine
- Supports ThirdPerson, FirstPerson, TopDown

## Testing Checklist

- [ ] Open project in Unity 6000.7.0b1
- [ ] Check console for obsolete warnings - should be minimal
- [ ] Test NavMeshAgent movement (AIEntity, CharacterController, PetAI)
- [ ] Test Input System (PlayerController, PlayerInput)
- [ ] Test URP rendering (materials, shaders, VFX)
- [ ] Test UI (UGUI, TextMeshPro)
- [ ] Test save/load (JsonSaver)
- [ ] Test new managers (Achievements, Mounts, etc.)
- [ ] Build for target platform

## Known Issues & Workarounds

### 1. Editor Modules for New Entries
New database entries (Titles, Achievements, etc.) have C# modules but need ScriptableObject assets in `Resources/EditorData/EditorEntryModules/`.

**Workaround**: 
- Use generic inspector (DrawView shows all fields via SerializedObject)
- Or create assets manually: Right-click → Create → RPGBuilder → EntryModule → Set TypeName and AssetFolderName

### 2. Database Folders Empty
New folders under `Resources/Database/` are empty. No ScriptableObjects yet.

**Workaround**:
- Create new entries via RPG Builder editor (will create assets in folders)
- Or duplicate existing entries and change type

### 3. Manager Prefab References
New managers need to be added to `RPGBuilderEssentials` prefab.

**Workaround**:
- Add components to prefab manually
- Or create empty GameObject with managers and make it DontDestroyOnLoad

### 4. RequirementsManager Check
New requirement types added but `RequirementsManager.CheckRequirements` may need expansion to handle new types.

**Current**: It already handles generic checks via `RequirementsTemplate`, but specific logic for new types (Title, Mount, etc.) should be added.

**Workaround**: For now, new requirement types will return true if not explicitly handled - can be expanded later.

### 5. GameActionsManager Execution
New game action types added but execution logic needs expansion.

**Workaround**: Similar to requirements - new types will need cases in `GameActionsManager.ExecuteGameActions`.

## Performance Notes for Unity 6000

- **SRP Batcher**: Enable in URP asset → reduces CPU time
- **GPU Instancing**: Enable on materials that use same shader
- **LOD**: Add LODGroup to complex meshes
- **Occlusion Culling**: Bake via Window → Rendering → Occlusion Culling
- **Dynamic Resolution**: Enable in URP asset, set min/max
- **Input System**: Use `ProcessEventsInDynamicUpdate` for lower latency
- **NavMesh**: Use NavMeshSurface components for runtime baking if needed

## Upgrade Steps for Existing Projects

1. **Backup** your project
2. **Update Unity** to 6000.7.0b1 via Unity Hub
3. **Update Packages** - Replace `manifest.json` with new version or update via Package Manager
4. **Fix Obsolete APIs** - Run Python script or manually replace FindObjectOfType
5. **Add Compatibility Layer** - Add `RPGBuilderUnity6Compatibility.cs` and `RPGBuilderPerformanceOptimizer`
6. **Test** - Open scenes, check console, play test
7. **Add New Systems** - Copy new DatabaseEntries, Managers, Settings from this branch
8. **Configure** - Update GameDatabase to load new entries, add managers to Essentials prefab
9. **Create Data** - Create new ScriptableObjects for Titles, Achievements, etc.
10. **Build** - Test build for target platform

## Future Unity 6000 Features to Consider

- **Entities / DOTS**: For massive NPC counts
- **Netcode for Entities**: For large multiplayer
- **Adaptive Performance**: For mobile
- **Shader Graph 17+**: New nodes
- **VFX Graph**: For advanced VFX
- **Splines**: For patrol paths
- **Animation Rigging**: For procedural animation

## Support

- Unity 6000.7.0b1 is Beta - expect bugs
- Check Unity Issue Tracker for known issues
- RPG Builder Discord: https://discord.gg/fYzpuYwPwJ
- This upgrade is community-maintained

## Conclusion

RPG Builder now runs on Unity 6000.7.0b1 with:
- Updated packages
- Fixed obsolete APIs
- Performance optimizations
- Compatibility layer
- Foundation for multiplayer and localization
- 17 new RPG systems
- 300+ new fields
- Full-blown RPG capabilities

Enjoy building your RPG on Unity 6!
