using System.Collections;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace DebugConsole
{

    [CustomEditor(typeof(DebugConsole))]
    public class DebugConsoleEditor : Editor
    {
        private void OnEnable()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(BuildPlayerHandler);
        }

        private void OnDisable()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(null);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Find all items (will also run on build)"))
                FindAllItems();
        }

        private void FindAllItems()
        {
            var guids = AssetDatabase.FindAssets($"t:{nameof(DebugCommand)}");

            var allItems = guids.Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<DebugCommand>)
                .ToList();

            var prop = serializedObject.FindProperty("commands");
            prop.arraySize = allItems.Count;
            for (int i = 0; i < prop.arraySize; ++i)
                prop.GetArrayElementAtIndex(i).objectReferenceValue = allItems[i];

            serializedObject.ApplyModifiedProperties();
        }

        private IEnumerator WaitAndBuildPlayer(BuildPlayerOptions options)
        {
            yield return new WaitForSeconds(1);
            BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
        }

        private void BuildPlayerHandler(BuildPlayerOptions options)
        {
            FindAllItems();
            EditorCoroutineUtility.StartCoroutineOwnerless(WaitAndBuildPlayer(options));
        }
    }
}