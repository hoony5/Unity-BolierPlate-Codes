using UnityEditor;
using UnityEngine;
using Utility.ExcelReader;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class ExcelCsvReaderEditor : EditorWindow
{
    private Dictionary<(string title , string docPath) , string> excelData = new Dictionary<(string, string), string>();
    private Object newFile;
    private ExcelDataSO newExcelDataSO;
    private string newFileTitle;

    private readonly string DATA_SAVE_KEY = "ExcelCsvReaderEditor_excelData";

    [MenuItem("Window/Excel CSV Reader")]
    public static void ShowWindow()
    {
        GetWindow<ExcelCsvReaderEditor>("Excel CSV Reader");
    }

    private void OnEnable()
    {
        // Load excelData from EditorPrefs
        string json = EditorPrefs.GetString(DATA_SAVE_KEY, "");
        if (string.IsNullOrEmpty(json)) return;
        excelData = JsonConvert.DeserializeObject<Dictionary<(string, string), string>>(json);
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();

        // Fields to assign a new File and ExcelDataSO.
        Object oldFile = newFile;
        newFile = EditorGUILayout.ObjectField("File", newFile, typeof(Object), false);
        newExcelDataSO = (ExcelDataSO)EditorGUILayout.ObjectField("Excel Data SO", newExcelDataSO, typeof(ExcelDataSO), false);
        newFileTitle = EditorGUILayout.TextField("Title", newFileTitle);

        // Add the new File and ExcelDataSO to excelData when they are both set and have changed.
        if (newFile != null && newExcelDataSO != null && newFile != oldFile)
        {
            string filePath = AssetDatabase.GetAssetPath(newFile);
            string extension = Path.GetExtension(filePath).ToLower();
            newFileTitle = newFileTitle ?? newFile.name;

            // Check if file extension is .csv or .xlsx
            if (extension is ".csv" or ".xlsx")
            {
                string fileGUID = AssetDatabase.AssetPathToGUID(filePath);
                string soPath = AssetDatabase.GetAssetPath(newExcelDataSO);
                (string newFileTitle, string fileGUID) key = (newFileTitle, fileGUID);
                if (!excelData.ContainsKey(key))
                {
                    excelData.Add(key, AssetDatabase.AssetPathToGUID(soPath));
                    Debug.Log("Added file: " + newFile.name);
                    // Save excelData to EditorPrefs
                    string json = JsonConvert.SerializeObject(excelData);
                    EditorPrefs.SetString(DATA_SAVE_KEY, json);
                }
                else
                {
                    excelData[key] = AssetDatabase.AssetPathToGUID(soPath);
                    Debug.Log("Updated file: " + newFile.name);
                }
            }
            else
            {
                Debug.LogWarning("Invalid file type. Please select a .csv or .xlsx file.");
            }
        }

        // Display the keys and values of excelData.
        EditorGUILayout.LabelField("Loaded Files and Data Objects:", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box"); // Begin box-styled vertical group

        foreach (KeyValuePair<(string title, string docPath), string> entry in excelData)
        {
            string title = entry.Key.title;
            string filePath = AssetDatabase.GUIDToAssetPath(entry.Key.docPath);
            string soPath = AssetDatabase.GUIDToAssetPath(entry.Value);

            // Create a row for each title, file, and SO.
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Title: " + title, EditorStyles.boldLabel);
            EditorGUILayout.Space(5);
            EditorGUILayout.HelpBox("File: " + filePath + "\nExcel Data SO: " + soPath, MessageType.None);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical(); // End box-styled vertical group
        GUILayout.EndVertical();

    }
}
