using UnityEditor;
using UnityEngine;

namespace Utility.ExcelReader.Editor
{
    [CustomEditor(typeof(ExcelCsvReader))]
    public class ExcelCsvReaderEditor : UnityEditor.Editor
    {
        private ExcelCsvReader _reader;
        private void OnEnable()
        {
            _reader = (ExcelCsvReader)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            _reader.path = _reader.excelFile is not null ? AssetDatabase.GetAssetPath(_reader.excelFile) : string.Empty;
            EditorGUILayout.Space(10);
            if (GUILayout.Button("Load"))
            {
                _reader.LoadDocument(_reader.path, _reader.sheetName);
            }
        }
    }
}