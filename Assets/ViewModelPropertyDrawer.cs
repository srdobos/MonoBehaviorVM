using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[CustomPropertyDrawer(typeof(ViewModelProperty))]
public class ViewModelPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //Debug.Log("Object name " + property.serializedObject.targetObject.name);

        var propertySource = property.objectReferenceValue as ViewModelProperty;
        if (propertySource != null)
        {
            //Debug.Log(propertySource.name);

            float indent = EditorGUI.indentLevel * 15f;
            Rect labelPosition = new Rect(position.x + indent, position.y, EditorGUIUtility.labelWidth - indent,
                EditorGUIUtility.singleLineHeight);
            Rect rect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, position.width - EditorGUIUtility.labelWidth, position.height);


            position = EditorGUI.PrefixLabel(position, label);
            Rect popupRect = EditorGUI.IndentedRect(position);
            //Rect popupRect = EditorGUI.lab
            position.x += popupRect.width;
            position.width -= popupRect.width;

            var viewModelProperties = propertySource.GetComponents<ViewModelProperty>();
            //Debug.Log("Num properties " + viewModelProperties.Length);
            var labels = new string[viewModelProperties.Length];
            for (var i = 0; i < viewModelProperties.Length; i++)
            {
                var p = viewModelProperties[i];
                labels[i] = p.name + " " + i;
            }
            int currentSelection = Array.IndexOf(viewModelProperties, propertySource);
            EditorGUI.BeginChangeCheck();
            int newSelection = EditorGUI.Popup(popupRect, currentSelection, labels);
            if (EditorGUI.EndChangeCheck())
            {
                var chosenProperty = viewModelProperties[newSelection];
                property.objectReferenceValue = chosenProperty;
                Debug.Log("Chose " + chosenProperty.name);
            }
            //EditorGUIUtility.GetFlowLayoutedRects()
        }
        else
        {
            EditorGUI.ObjectField(position, property, label);
        }
    }
}