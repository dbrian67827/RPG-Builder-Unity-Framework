
using UnityEngine;
using UnityEditor;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Editor
{
    public class RPGBuilderEditorLoreModule : RPGBuilderEditorModule
    {
        public override void DrawView()
        {
            base.DrawView();
            EditorGUILayout.LabelField("Lore Module - Unity 6000.7.0b1", EditorStyles.boldLabel);
            if (GameDatabase.Instance == null) return;
            var entries = GameDatabase.Instance.GetLores();
            EditorGUILayout.LabelField($"Total Lores: {entries.Count}");
            // Full editor implementation would go here
        }

        public override void Save()
        {
            base.Save();
            Debug.Log("[RPG Builder] Saved Lore module");
        }
    }
}
