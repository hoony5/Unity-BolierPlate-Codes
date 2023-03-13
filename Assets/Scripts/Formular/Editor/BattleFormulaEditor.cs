using System;
using Codice.Client.Commands.CheckIn;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(BattleFormula))]
public class BattleFormulaEditor : Editor
{
    private SerializedProperty formulaNameProp;
    private SerializedProperty formulaInfoListProp;
    private SerializedProperty descriptionProp;
    private ReorderableList formulaInfoList;
    private Vector2 descriptionScrollPosition;
    private float propertyWidth = (EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth) * 0.4f;
    private float labelWidth = EditorGUIUtility.labelWidth;
    private void OnEnable()
    {
        formulaNameProp = serializedObject.FindProperty("formulaName");
        formulaInfoListProp = serializedObject.FindProperty("formulaInfoList");
        descriptionProp = serializedObject.FindProperty("description");

        formulaInfoListProp = serializedObject.FindProperty("formulaInfoList");

        formulaInfoList = new ReorderableList(serializedObject, formulaInfoListProp, true, true, true, true);
        formulaInfoList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Formula Info List");
        formulaInfoList.drawElementCallback = DrawFormulaInfoListElement;
        formulaInfoList.elementHeightCallback = ElementHeightCallback;
        formulaInfoList.onAddCallback = OnAddCallback;
    }

    private void OnAddCallback(ReorderableList list)
    {
        if (formulaInfoListProp.arraySize <= 0) formulaInfoListProp.arraySize = 0;
        
        formulaInfoListProp.InsertArrayElementAtIndex(formulaInfoListProp.arraySize);
        formulaInfoListProp.GetArrayElementAtIndex(formulaInfoListProp.arraySize - 1).objectReferenceValue = null;
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

    private float ElementHeightCallback(int index)
    {
        SerializedProperty element = formulaInfoListProp.GetArrayElementAtIndex(index);
        if (element.objectReferenceValue is null) return EditorGUIUtility.singleLineHeight;

        SerializedObject relativeObject = new SerializedObject(element.objectReferenceValue);
        float height = EditorGUIUtility.singleLineHeight * 6 + 12;
        CalculationTargetType calculationTargetType = (CalculationTargetType)relativeObject.FindProperty("calculationTargetType").enumValueIndex;
        switch (calculationTargetType)
        {
            case CalculationTargetType.UseModifyValue:
                height += EditorGUIUtility.singleLineHeight;
                break;
            case CalculationTargetType.UseFormulaInfoValue:
                height += EditorGUIUtility.singleLineHeight * 2;
                break;
        }

        return height;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space(8f);
        EditorGUILayout.PropertyField(formulaNameProp);

        EditorGUILayout.Space(8f);
        formulaInfoList.DoLayoutList();

        EditorGUILayout.Space(8f);
        
        EditorGUILayout.LabelField("Description:");
        BattleFormulaInfo resultInfo = (BattleFormulaInfo)formulaInfoListProp.GetArrayElementAtIndex(formulaInfoListProp.arraySize - 1).objectReferenceValue;
        
        if(resultInfo is null)
        {
            EditorGUILayout.HelpBox("Please add a result info to the formula info list.", MessageType.Warning);
        }

        {
            EditorGUILayout.SelectableLabel($"<color=lime><b>{(string.IsNullOrEmpty(formulaNameProp.stringValue) ? "It" : formulaNameProp.stringValue)}</b></color>'s Formula Finalized Value -> <color=lime>{resultInfo.GetEditorCalculatedValue()}</color>", new GUIStyle(GUI.skin.box)
            {
                richText = true,
                stretchWidth = true,
                fontSize = 20
            });
        }
        
        EditorGUILayout.Space(8f);

        serializedObject.ApplyModifiedProperties();
    }

private void DrawFormulaInfoListElement(Rect rect, int index, bool isActive, bool isFocused)
{
    SerializedProperty element = formulaInfoListProp.GetArrayElementAtIndex(index);
    if (element.objectReferenceValue is null)
    {
        element.objectReferenceValue = EditorGUI.ObjectField(rect, GUIContent.none, null, typeof(BattleFormulaInfo), false);
        return;
    }

    BattleFormulaInfo formulaInfo = (BattleFormulaInfo)element.objectReferenceValue;

    rect.y += 2;
    rect.height = EditorGUIUtility.singleLineHeight;

    SerializedObject relativeObject = new SerializedObject(element.objectReferenceValue);
    SerializedProperty statusNameProp = relativeObject.FindProperty("statusName");
    SerializedProperty calculationTypeProp = relativeObject.FindProperty("calculationType");
    SerializedProperty calculationTargetTypeProp = relativeObject.FindProperty("calculationTargetType");
    SerializedProperty useInputModeProp = relativeObject.FindProperty("useInputMode");
    SerializedProperty useClmapProp = relativeObject.FindProperty("useClamp");
    SerializedProperty clampRangeVector2Prop = relativeObject.FindProperty("minMaxRange");

    // Draw the status name field
    EditorGUI.LabelField(rect, "Status Name");
    Rect labelPos = rect;
    rect.x += labelWidth;
    rect.width = propertyWidth;
    element.objectReferenceValue =
        EditorGUI.ObjectField(rect, element.objectReferenceValue, typeof(BattleFormulaInfo), false);
    rect.x += rect.width + 5;
    rect.width = propertyWidth;
    EditorGUI.PropertyField(rect, statusNameProp, GUIContent.none);
    rect.x -= labelWidth + rect.width + 5;
    rect.width = propertyWidth;

    // Draw Test Value
    rect.y += EditorGUIUtility.singleLineHeight + 5;
    labelPos.y = rect.y;
    EditorGUI.LabelField(rect, $"{(useInputModeProp.boolValue ? "<color=lime>Input Directly Base Value Mode On</color>" : "<color=yellow>Using Input Directly Base Value Mode ?</color>")}", new GUIStyle(GUI.skin.label){richText = true});
    rect.x += labelWidth;
    rect.width =  propertyWidth;
    useInputModeProp.boolValue = EditorGUI.Toggle(rect, useInputModeProp.boolValue);
    rect.x += rect.width;
    rect.width = propertyWidth;
    if (useInputModeProp.boolValue)
    {
        formulaInfo.baseValue = EditorGUI.FloatField(rect, formulaInfo.baseValue);
        rect.x -= rect.width;
    }
    else
    {
        rect.x -= rect.width * 0.5f;
        EditorGUI.SelectableLabel(rect, "<color=lime>On runtime, by searching statusName</color>, baseValue is set. ", new GUIStyle(GUI.skin.box){richText = true, fontSize = 12});
        formulaInfo.calculatedValue = 0;
        formulaInfo.baseValue = 0;
        rect.x -= rect.width * 0.5f;
    }
    // Draw the calculation type and calculation target type fields
    rect.x -= labelWidth;
    rect.y += EditorGUIUtility.singleLineHeight + 5;
    labelPos.y = rect.y;
    EditorGUI.LabelField(labelPos, "Calculation Method");
    rect.x += labelWidth;
    rect.width *= 0.5f;
    EditorGUI.PropertyField(rect, calculationTypeProp, GUIContent.none);
    rect.x += rect.width + 5;
    EditorGUI.PropertyField(rect, calculationTargetTypeProp, GUIContent.none);
    rect.x += rect.width + 5;

    // Draw the target property field (either a float field or a BattleFormulaInfo field)
    CalculationTargetType calculationTargetType = (CalculationTargetType)calculationTargetTypeProp.enumValueIndex;
    rect.width = propertyWidth;
    switch (calculationTargetType)
    {
        case CalculationTargetType.UseModifyValue:
            SerializedProperty modifyValueProp = relativeObject.FindProperty("modifyValue");
            modifyValueProp.floatValue = EditorGUI.FloatField(rect, modifyValueProp.floatValue);
            formulaInfo.calculatedValue = modifyValueProp.floatValue;
            break;
        case CalculationTargetType.UseFormulaInfoValue:
            SerializedProperty modifyBattleFormulaInfoProp = relativeObject.FindProperty("modifyBattleFormulaInfo");
            modifyBattleFormulaInfoProp.objectReferenceValue = EditorGUI.ObjectField(rect, modifyBattleFormulaInfoProp.objectReferenceValue, typeof(BattleFormulaInfo), false);
            if (modifyBattleFormulaInfoProp.objectReferenceValue != null)
            {
                BattleFormulaInfo subFormulaInfo = (BattleFormulaInfo)modifyBattleFormulaInfoProp.objectReferenceValue;
                formulaInfo.calculatedValue = subFormulaInfo.GetPreCalculatedValue();
            }
            break;
    }

    // Draw the Clamp Mode label
    rect.x -= labelWidth + rect.width + 10;
    rect.y += EditorGUIUtility.singleLineHeight + 5;
    rect.width = (EditorGUIUtility.currentViewWidth - labelWidth) * 0.2f;
    labelPos.y = rect.y;
    EditorGUI.LabelField(labelPos, $"{(useClmapProp.boolValue ? "Clamp Mode <color=lime>On</color>" : "Clamp Mode <color=yellow>Off</color>")}", new GUIStyle(GUI.skin.label){richText = true});
    rect.x += labelWidth;
    useClmapProp.boolValue = EditorGUI.Toggle(rect,useClmapProp.boolValue);
    if (useClmapProp.boolValue)
    {
        rect.x += rect.width;
        rect.width = propertyWidth;
        clampRangeVector2Prop.vector2Value = EditorGUI.Vector2Field(rect, GUIContent.none, clampRangeVector2Prop.vector2Value);   
        rect.x -= rect.width * 0.35f;
    }
    else
    {
        clampRangeVector2Prop.vector2Value  = Vector2.zero;
    }
    rect.x -= rect.width * 0.3f;
    rect.y += EditorGUIUtility.singleLineHeight + 5;
    labelPos.y = rect.y;
    // Draw the calculated value label
    EditorGUI.LabelField(labelPos, "Calculated Result");
    rect.width =(EditorGUIUtility.currentViewWidth - labelWidth) * 0.8f;
    rect.x += rect.width * 0.3f;
    float preCalculatedResult = formulaInfo.GetEditorCalculatedValue();
    
    if ((CalculationType)calculationTypeProp.enumValueIndex is CalculationType.Logarithmic 
        && (preCalculatedResult < 0 
            || formulaInfo.baseValue <= 0 
            || float.IsNaN(preCalculatedResult) 
            || float.IsInfinity(preCalculatedResult)))
    {
        
            EditorGUI.LabelField(rect, "<color=yellow><b>Invalid input value</b></color>. Please, check.", new GUIStyle(GUI.skin.box){richText = true, alignment = TextAnchor.MiddleLeft});
    }
    else
    {
        string currentNumber = index.ToString();
        string numberFormat = (currentNumber == "0" ? "." :
            currentNumber[^1] == '1' ? "st." :
            currentNumber[^2] == '2' ? "nd." :
            currentNumber[^3] == '3' ? "rd." : "th.");
        
        EditorGUI.SelectableLabel(rect,
            $"<b>{index}{numberFormat}</b> <color=lime><b>{GetDescription(element)} = {preCalculatedResult}</b></color>"
            , new GUIStyle(GUI.skin.box) { richText = true, alignment = TextAnchor.MiddleLeft });
    }
    
    relativeObject.ApplyModifiedProperties();
}

private string GetDescription(SerializedProperty element)
{
    if (formulaInfoListProp is null || formulaInfoListProp.arraySize == 0)
    {
        return "There is no Formula";
    }

    if (element.objectReferenceValue is null)
    {
        return "Please, assign a FormulaInfo";
    }
    BattleFormulaInfo formulaInfo = (BattleFormulaInfo)element.objectReferenceValue;
    string description = "";
    SerializedObject relativeObject = new SerializedObject(element.objectReferenceValue);
    SerializedProperty statusNameProp = relativeObject.FindProperty("statusName");
    SerializedProperty calculationTypeProp = relativeObject.FindProperty("calculationType");
    SerializedProperty calculationTargetTypeProp = relativeObject.FindProperty("calculationTargetType");
    SerializedProperty modifyValueProp = relativeObject.FindProperty("modifyValue");
    SerializedProperty modifyBattleFormulaInfoProp = relativeObject.FindProperty("modifyBattleFormulaInfo");

    if (statusNameProp is null) return "There is no Formula";

    string formulaDescription = $"{statusNameProp.stringValue} => ";

    CalculationType calculationType = (CalculationType)calculationTypeProp.enumValueIndex;
    switch (calculationType)
    {
        case CalculationType.None:
            break;
        case CalculationType.Additive:
            formulaDescription += $"{formulaInfo.baseValue} +";
            break;
        case CalculationType.Multiply:
            formulaDescription += $"{formulaInfo.baseValue} ×";
            break;
        case CalculationType.Logarithmic:
            formulaDescription += $" Log({formulaInfo.baseValue})";
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }

    CalculationTargetType calculationTargetType =
        (CalculationTargetType)calculationTargetTypeProp.enumValueIndex;
    switch (calculationTargetType)
    {
        case CalculationTargetType.UseModifyValue:
            formulaDescription += $" {modifyValueProp.floatValue}";
            break;
        case CalculationTargetType.UseFormulaInfoValue
            when modifyBattleFormulaInfoProp.objectReferenceValue != null:
            BattleFormulaInfo subFormulaInfo = (BattleFormulaInfo)
                modifyBattleFormulaInfoProp.objectReferenceValue;
            formulaDescription += $" <color=cyan>({subFormulaInfo.ToFormulaString()})</color>";
            break;
        case CalculationTargetType.UseFormulaInfoValue:
            formulaDescription += " <color=yellow>[Missing formula reference]</color>";
            break;
        case CalculationTargetType.UseSelfValue:
            formulaDescription += $" {statusNameProp.stringValue}";
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }

    formulaDescription += ",";
    description += formulaDescription;

    // Remove the last comma
    description = description.TrimEnd(',');

    return description;
}

}