using System;
using System.Reflection;
using MapMaker.Scripts;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.ECS.Core.SaveManager
{
    public static class EditorSerializer
    {
        public static void SerializeObject(this IEntityObject obj, Entity entity)
        {
            Type type = obj.GetType();
            SerializeFieldsRecursively(obj, type, entity);
        }

        private static void SerializeFieldsRecursively(object obj, Type type, Entity entity)
        {
            if (type.BaseType != null)
            {
                SerializeFieldsRecursively(obj, type.BaseType, entity);
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object fieldValue = field.GetValue(obj);
                if (fieldValue != null)
                {
                    FieldInfo enabledField = fieldValue.GetType().GetField("enabled", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (enabledField != null && (bool)enabledField.GetValue(fieldValue))
                    {
                        MethodInfo setMethod = fieldValue.GetType().GetMethod("Set", BindingFlags.Public | BindingFlags.Instance);
                        setMethod?.Invoke(fieldValue, new object[] { entity });
                    }

                    if (field.FieldType.IsClass && field.FieldType != typeof(string) && !field.FieldType.IsArray)
                    {
                        SerializeFieldsRecursively(fieldValue, field.FieldType, entity);
                    }
                }
            }
        }
        
        
        public static void DeserializeObject(this IEntityObject obj, Entity entity)
        {
            Type type = obj.GetType();
            DeserializeFieldsRecursively(obj, type, entity);
        }

        private static void DeserializeFieldsRecursively(object obj, Type type, Entity entity)
        {
            if (type.BaseType != null)
            {
                DeserializeFieldsRecursively(obj, type.BaseType, entity);
            }

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object fieldValue = field.GetValue(obj);
                if (fieldValue != null)
                {
                    MethodInfo tryGetMethod = fieldValue.GetType().GetMethod("TryGet", BindingFlags.Public | BindingFlags.Instance);
                    tryGetMethod?.Invoke(fieldValue, new object[] { entity });

                    if (field.FieldType.IsClass && field.FieldType != typeof(string) && !field.FieldType.IsArray)
                    {
                        DeserializeFieldsRecursively(fieldValue, field.FieldType, entity);
                    }
                }
            }
        }
    }
}