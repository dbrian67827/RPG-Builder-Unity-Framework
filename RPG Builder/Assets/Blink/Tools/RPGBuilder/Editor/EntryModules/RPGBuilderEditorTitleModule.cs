
using UnityEngine;
using UnityEditor;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Editor
{
    public class RPGBuilderEditorTitleModule : RPGBuilderEditorModule
    {
        public override void DrawView()
        {
            base.DrawView();
            EditorGUILayout.LabelField("Title Module - Unity 6000.7.0b1", EditorStyles.boldLabel);
            if (GameDatabase.Instance == null) return;
            var entries = GameDatabase.Instance.GetTitles();
            EditorGUILayout.LabelField($"Total Titles: {entries.Count}");
            // Full editor implementation would go here
        }

        public override void Save()
        {
            base.Save();
            Debug.Log("[RPG Builder] Saved Title module");
        }
    }
}
