
using UnityEngine;
using UnityEditor;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Editor
{
    public class RPGBuilderEditorPetModule : RPGBuilderEditorModule
    {
        public override void DrawView()
        {
            base.DrawView();
            EditorGUILayout.LabelField("Pet Module - Unity 6000.7.0b1", EditorStyles.boldLabel);
            if (GameDatabase.Instance == null) return;
            var entries = GameDatabase.Instance.GetPets();
            EditorGUILayout.LabelField($"Total Pets: {entries.Count}");
            // Full editor implementation would go here
        }

        public override void Save()
        {
            base.Save();
            Debug.Log("[RPG Builder] Saved Pet module");
        }
    }
}
