using ArtificerPro.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Stats
{
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, label, EditorStyles.boldLabel);
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.indentLevel++;
            
            EditorGUI.PropertyField(position, property, new GUIContent("Stat"));
            
            // get the current SO reference and find the index of it, so we can display the currently selected one properly
            var current = (Stat) property.objectReferenceValue;
            if (current == null)
                return;
            
            // create a new serialized object for the current property
            // this way, we can find the `initialValue` property more easily
            var serialized = new SerializedObject(current);
            
            // update the serialized object so we can see the most recent value
            serialized.Update();
            
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            
            // update the the initial value with a FloatField
            serialized.FindProperty("initialValue").floatValue = EditorGUI.FloatField(position, "Initial Value", serialized.FindProperty("initialValue").floatValue);
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.LabelField(position, "Range");
            position.x += EditorGUIUtility.labelWidth - 13;
            position.width /= 2;
            position.width = 100;
            serialized.FindProperty("minValue").floatValue = EditorGUI.FloatField(position, serialized.FindProperty("minValue").floatValue);
            position.x += position.width + 12;
            EditorGUI.LabelField(position, "to");
            position.x += 38;
            serialized.FindProperty("maxValue").floatValue = EditorGUI.FloatField(position, serialized.FindProperty("maxValue").floatValue);
            
            serialized.ApplyModifiedProperties();
            
            // write the changes to this property
            property.objectReferenceValue = current;
            EditorGUI.indentLevel--;
        }
        
        override public float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing)*3;
        }
    }
}