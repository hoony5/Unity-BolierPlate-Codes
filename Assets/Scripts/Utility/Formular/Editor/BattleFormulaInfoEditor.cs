using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BattleFormulaInfo))]
public class BattleFormulaInfoEditor : UnityEditor.Editor
{
    private SerializedProperty statusNameProp;
    private SerializedProperty statusReferenceTargetProp;
    private SerializedProperty calculationTargetTypeProp;
    private SerializedProperty modifyBattleFormulaInfoProp;
    private SerializedProperty calculationTypeProp;
    private SerializedProperty modifyValueProp;
    private SerializedProperty descriptionProp;

    private GUIStyle boldLabelStyle;
    private GUIStyle smallLabelStyle;

    private Vector2 scrollPos;

    private void OnEnable()
    {
        statusNameProp = serializedObject.FindProperty("statusName");
        statusReferenceTargetProp = serializedObject.FindProperty("statusReferenceTarget");
        calculationTargetTypeProp = serializedObject.FindProperty("calculationTargetType");
        modifyBattleFormulaInfoProp = serializedObject.FindProperty("modifyBattleFormulaInfo");
        calculationTypeProp = serializedObject.FindProperty("formulaCalculationType");
        modifyValueProp = serializedObject.FindProperty("modifyValue");
        descriptionProp = serializedObject.FindProperty("description");
    }

    public override void OnInspectorGUI()
    {
        boldLabelStyle ??= new GUIStyle(EditorStyles.boldLabel) { fontSize = 12 };
        smallLabelStyle ??= new GUIStyle(GUI.skin.label) { fontSize = 10 };
        serializedObject.Update();

        // Show the status name field
        EditorGUILayout.Space(8f);
        EditorGUILayout.PropertyField(statusNameProp);

        // Show the Reference Target field
        EditorGUILayout.Space(8f);
        EditorGUILayout.PropertyField(statusReferenceTargetProp);
        // Show the calculation type field
        EditorGUILayout.Space(8f);
        EditorGUILayout.PropertyField(calculationTypeProp);

        // Show the calculation target type field
        EditorGUILayout.Space(8f);
        EditorGUILayout.PropertyField(calculationTargetTypeProp);

        // Show the modify value field or modify formula info field
        EditorGUILayout.Space(8f);
        EditorGUILayout.BeginHorizontal();
        switch ((CalculationTargetType)calculationTargetTypeProp.enumValueIndex)
        {
            case CalculationTargetType.UseModifyValue:
                EditorGUILayout.PropertyField(modifyValueProp);
                break;
            case CalculationTargetType.UseFormulaInfoValue:
                EditorGUILayout.PropertyField(modifyBattleFormulaInfoProp);
                break;
        }

        EditorGUILayout.EndHorizontal();

        // Show the description field with scroll view and text area
        EditorGUILayout.Space(8f);
        EditorGUILayout.LabelField("Description:", boldLabelStyle);
        EditorGUILayout.BeginVertical(GUI.skin.box);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(100f));
        descriptionProp.stringValue =
            EditorGUILayout.TextArea(descriptionProp.stringValue, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
