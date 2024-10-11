using System;
using System.Reflection;
using MapMaker.Scripts;

namespace Source.Scripts.ECS.Groups.SlotSaver.Core
{
    public static class EditorSerializer
    {
        public static void SerializeObject(this IEntityObject obj, SlotEntity slotEntity)
        {
            var type = obj.GetType();
            SerializeFieldsRecursively(obj, type, slotEntity);
        }

        private static void SerializeFieldsRecursively(object obj, Type type, SlotEntity slotEntity)
        {
            if (type.BaseType != null) SerializeFieldsRecursively(obj, type.BaseType, slotEntity);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var fieldValue = field.GetValue(obj);
                if (fieldValue != null)
                {
                    var enabledField = fieldValue.GetType().GetField("enabled",
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (enabledField != null && (bool)enabledField.GetValue(fieldValue))
                    {
                        var saveMethod = fieldValue.GetType()
                            .GetMethod("TrySave", BindingFlags.Public | BindingFlags.Instance);
                        saveMethod?.Invoke(fieldValue, new object[] { slotEntity });
                    }

                    if (field.FieldType.IsClass && field.FieldType != typeof(string) && !field.FieldType.IsArray)
                        SerializeFieldsRecursively(fieldValue, field.FieldType, slotEntity);
                }
            }
        }


        public static void DeserializeObject(this IEntityObject obj, SlotEntity slotEntity)
        {
            var type = obj.GetType();
            DeserializeFieldsRecursively(obj, type, slotEntity);
        }

        private static void DeserializeFieldsRecursively(object obj, Type type, SlotEntity slotEntity)
        {
            if (type.BaseType != null) DeserializeFieldsRecursively(obj, type.BaseType, slotEntity);

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var fieldValue = field.GetValue(obj);
                if (fieldValue != null)
                {
                    var loadMethod = fieldValue.GetType()
                        .GetMethod("TryLoad", BindingFlags.Public | BindingFlags.Instance);
                    loadMethod?.Invoke(fieldValue, new object[] { slotEntity });

                    if (field.FieldType.IsClass && field.FieldType != typeof(string) && !field.FieldType.IsArray)
                        DeserializeFieldsRecursively(fieldValue, field.FieldType, slotEntity);
                }
            }
        }
    }
}