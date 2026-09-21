
using UnityEngine;
using UnityEditor;
using BLINK.RPGBuilder.Managers;
using BLINK.RPGBuilder.Templates;

namespace BLINK.RPGBuilder.Editor
{
    public class RPGBuilderEditorShopModule : RPGBuilderEditorModule
    {
        public override void DrawView()
        {
            base.DrawView();
            EditorGUILayout.LabelField("Shop Module - Unity 6000.7.0b1", EditorStyles.boldLabel);
            if (GameDatabase.Instance == null) return;
            var entries = GameDatabase.Instance.GetShops();
            EditorGUILayout.LabelField($"Total Shops: {entries.Count}");
            // Full editor implementation would go here
        }

        public override void Save()
        {
            base.Save();
            Debug.Log("[RPG Builder] Saved Shop module");
        }
    }
}
