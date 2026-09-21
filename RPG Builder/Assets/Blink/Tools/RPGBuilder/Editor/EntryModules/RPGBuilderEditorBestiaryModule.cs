
using UnityEngine;
using UnityEditor;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Editor
{
    public class RPGBuilderEditorBestiaryModule : RPGBuilderEditorModule
    {
        public override void DrawView()
        {
            base.DrawView();
            EditorGUILayout.LabelField("Bestiary Module - Unity 6000.7.0b1", EditorStyles.boldLabel);
            if (GameDatabase.Instance == null) return;
            var entries = GameDatabase.Instance.GetBestiarys();
            EditorGUILayout.LabelField($"Total Bestiarys: {entries.Count}");
            // Full editor implementation would go here
        }

        public override void Save()
        {
            base.Save();
            Debug.Log("[RPG Builder] Saved Bestiary module");
        }
    }
}
