using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CustomAttributes
{
    [CustomPropertyDrawer(typeof(ValueDropdown))]
    public class CustomDropdownDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ValueDropdown dropdown = (ValueDropdown)attribute;
            string methodName = dropdown.MethodName;

            // Получаем объект, на котором будет вызван метод
            object target = property.serializedObject.targetObject;
            MethodInfo method = target.GetType().GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

            if (method != null && method.ReturnType == typeof(string[]))
            {
                // Вызываем метод и получаем список опций
                string[] options = (string[])method.Invoke(target, null);

                if (options != null && options.Length > 0)
                {
                    int index = Mathf.Max(0, Array.IndexOf(options, property.stringValue));
                    index = EditorGUI.Popup(position, label.text, index, options);
                    property.stringValue = options[index];
                }
                else
                {
                    EditorGUI.LabelField(position, label.text, "No options available");
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, $"Method '{methodName}' not found or invalid return type");
            }
        }
    }

    public class ValueDropdown : PropertyAttribute
    {
        public string MethodName { get; private set; }

        public ValueDropdown(string methodName)
        {
            MethodName = methodName;
        }
    }
}