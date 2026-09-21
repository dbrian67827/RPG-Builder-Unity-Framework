using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RPGBuilderEditorParagonModule : RPGBuilderEditorModule
{
    private Dictionary<int, RPGParagon> entries = new Dictionary<int, RPGParagon>();
    private RPGParagon currentEntry;

    public override void Initialize()
    {
        LoadEntries();
        if (entries.Count != 0)
        {
            currentEntry = Instantiate(entries[RPGBuilderEditor.Instance.CurrentEntryIndex]);
            RPGBuilderEditor.Instance.CurrentEntry = currentEntry;
        }
        else
        {
            CreateNewEntry();
        }

        // Filters not yet implemented for new modules, use generic
        // RPGBuilderEditor.Instance.InitializeFilters(RPGBuilderEditor.Instance.EditorFilters.paragonFilters);
    }

    public override void InstantiateCurrentEntry(int index)
    {
        if (entries.Count == 0) return;
        currentEntry = Instantiate(entries[index]);
        RPGBuilderEditor.Instance.CurrentEntry = currentEntry;
    }

    public override void LoadEntries()
    {
        Dictionary<int, RPGParagon> dictionary = new Dictionary<int, RPGParagon>();
        databaseEntries.Clear();
        var allEntries =
            Resources.LoadAll<RPGParagon>(RPGBuilderEditor.Instance.EditorData.RPGBDatabasePath + AssetFolderName);
        for (var index = 0; index < allEntries.Length; index++)
        {
            var entry = allEntries[index];
            dictionary.Add(index, entry);
            databaseEntries.Add(entry);
        }

        entries = dictionary;
    }

    public override void CreateNewEntry()
    {
        if (EditorApplication.isCompiling)
        {
            Debug.LogError("You cannot interact with the RPG Builder while the editor is compiling");
            return;
        }

        currentEntry = CreateInstance<RPGParagon>();
        RPGBuilderEditor.Instance.CurrentEntry = currentEntry;
        RPGBuilderEditor.Instance.CurrentEntryIndex = -1;
    }

    public override bool SaveConditionsMet()
    {
        if (string.IsNullOrEmpty(currentEntry.entryName))
        {
            RPGBuilderEditorUtility.DisplayDialogueWindow("Invalid Name", "Enter a valid name", "OK");
            return false;
        }
        if (ContainsInvalidCharacters(currentEntry.entryName))
        {
            RPGBuilderEditorUtility.DisplayDialogueWindow("Invalid Characters", "The Name contains invalid characters", "OK");
            return false;
        }
        
        return true;
    }

    public override void UpdateEntryData(RPGBuilderDatabaseEntry updatedEntry)
    {
        RPGParagon entryFile = (RPGParagon) updatedEntry;
        entryFile.UpdateEntryData(currentEntry);
    }

    public override void ClearEntries()
    {
        databaseEntries.Clear();
        entries.Clear();
        currentEntry = null;
    }

    public override void DrawView()
    {
        if (currentEntry == null)
        {
            if (entries.Count > 0 && entries[0] != null)
            {
                RPGBuilderEditor.Instance.SelectDatabaseEntry(0, true);
                currentEntry = Instantiate(entries[RPGBuilderEditor.Instance.CurrentEntryIndex]);
            }
            else
            {
                CreateNewEntry();
            }
        }

        RPGBuilderEditorUtility.UpdateViewAndFieldData();

        ScriptableObject scriptableObj = currentEntry;
        var serialObj = new SerializedObject(scriptableObj);

        float topSpace = RPGBuilderEditor.Instance.ButtonHeight + 5;
        GUILayout.Space(topSpace);
        
        RPGBuilderEditor.Instance.ViewScroll = EditorGUILayout.BeginScrollView(RPGBuilderEditor.Instance.ViewScroll,
            false, false,
            GUILayout.Width(RPGBuilderEditor.Instance.ViewWidth),
            GUILayout.MaxWidth(RPGBuilderEditor.Instance.ViewWidth),
            GUILayout.ExpandHeight(true));

        GUILayout.Space(10);
        // BASE INFO
        GUILayout.Label("BASE INFO", EditorStyles.boldLabel);
        GUILayout.Space(10);
        RPGBuilderEditorUtility.StartHorizontalMargin(RPGBuilderEditor.Instance.LongHorizontalMargin, false);
        currentEntry.entryIcon =
            RPGBuilderEditorFields.DrawIcon(currentEntry.entryIcon, 100, 100);
        GUILayout.BeginVertical();
        RPGBuilderEditorFields.DrawID(currentEntry.ID);
        currentEntry.entryName =
            RPGBuilderEditorFields.DrawHorizontalTextField("Name", "", RPGBuilderEditor.Instance.FieldHeight,
                currentEntry.entryName);
        currentEntry.entryDisplayName = RPGBuilderEditorFields.DrawHorizontalTextField(
            "Display Name", "",
            RPGBuilderEditor.Instance.FieldHeight,
            currentEntry.entryDisplayName);
        currentEntry.entryFileName = RPGBuilderEditorFields.DrawFileNameField(
            "File Name", "",
            RPGBuilderEditor.Instance.FieldHeight,
            currentEntry.entryName + AssetNameSuffix);
        currentEntry.entryDescription =
            RPGBuilderEditorFields.DrawHorizontalDescriptionField("Description",
                "", RPGBuilderEditor.Instance.FieldHeight,
                currentEntry.entryDescription);
        GUILayout.EndVertical();
        RPGBuilderEditorUtility.EndHorizontalMargin(RPGBuilderEditor.Instance.LongHorizontalMargin, false);

        GUILayout.Space(10);
        GUILayout.Label("RPGParagon - Full RPG Extended Features", EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.Label("This entry has been expanded with full RPG features. Configure via inspector or code.", EditorStyles.wordWrappedLabel);
        GUILayout.Space(10);

        // Draw default inspector for remaining fields
        serialObj.Update();
        var iterator = serialObj.GetIterator();
        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren))
        {
            if (iterator.name == "m_Script" || iterator.name == "ID" || iterator.name == "entryName" || iterator.name == "entryFileName" || iterator.name == "entryDisplayName" || iterator.name == "entryIcon" || iterator.name == "entryDescription") continue;
            EditorGUILayout.PropertyField(iterator, true);
            enterChildren = false;
        }
        serialObj.ApplyModifiedProperties();

        EditorGUILayout.EndScrollView();
    }
}
