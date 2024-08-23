using System;
using System.ComponentModel;

namespace Source.Scripts.Extensions
{
    public static class KeysHolderExtensions
    {
        public static string GetDescription<T>(this T value) where T : Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute.Description;
        }
    }
}