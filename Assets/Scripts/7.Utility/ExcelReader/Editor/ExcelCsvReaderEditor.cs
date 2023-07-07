using UnityEditor;
using UnityEngine;
using Utility.ExcelReader;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class ExcelCsvReaderEditor : EditorWindow
{
    private Object newFile;
    private ExcelDataSO newExcelDataSO;

    [MenuItem("Window/Excel CSV Reader")]
    public static void ShowWindow()
    {
        GetWindow<ExcelCsvReaderEditor>("Excel CSV Reader");
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        // Fields to assign a new File and ExcelDataSO.
        Object oldFile = newFile;
        newFile = EditorGUILayout.ObjectField("File", newFile, typeof(Object), false);
        newExcelDataSO = (ExcelDataSO)EditorGUILayout.ObjectField("Excel Data SO", newExcelDataSO, typeof(ExcelDataSO), false);
        
        EditorGUILayout.Space(10);

        // Add the new File and ExcelDataSO to excelData when they are both set and have changed.
        if (newFile != null && newExcelDataSO != null && newFile != oldFile)
        {
            string filePath = AssetDatabase.GetAssetPath(newFile);
            string extension = Path.GetExtension(filePath).ToLower();

            // Check if file extension is .csv or .xlsx
            if (extension is ".csv" or ".xlsx")
            {
                newExcelDataSO.Set(ExcelCsvReader.Read(filePath));
                Debug.Log("Load Success.");
            }
            else
            {
                Debug.LogWarning("Invalid file type. Please select a .csv or .xlsx file.");
            }
        }
        GUILayout.EndVertical();
    }
}
