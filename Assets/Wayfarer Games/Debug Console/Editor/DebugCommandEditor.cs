using System.Collections;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace DebugConsole
{

    [CustomEditor(typeof(DebugCommand), true)]
    public class DebugCommandEditor : Editor
    {
        private SerializedProperty _targetOptionsType;
        private SerializedProperty _targets;

        private void OnEnable()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(BuildPlayerHandler);
            _targetOptionsType = serializedObject.FindProperty("targetOptionsType");
            _targets = serializedObject.FindProperty("targets");
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.EnteredPlayMode)
                FindAllItems();
        }

        private void OnDisable()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(null);
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Find all items (will also run on build)"))
                FindAllItems();
        }

        private void FindAllItems()
        {
            var guids = AssetDatabase.FindAssets($"t:{_targetOptionsType.stringValue}");

            var allItems = guids.Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
                .ToList();

            _targets.arraySize = allItems.Count;
            for (int i = 0; i < _targets.arraySize; ++i)
                _targets.GetArrayElementAtIndex(i).objectReferenceValue = allItems[i];

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