using Source.Scripts.Managers.ProjectSettings;
using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class GameObjectExtensions
    {
        public static T AddOrGet<T>(this GameObject gameObject)
            where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null) component = gameObject.AddComponent<T>();
            return component;
        }

        public static T TryGetIfNull<T>(this GameObject gameObject, ref T component)
            where T : Component
        {
            if (component == null)
            {
                component = gameObject.GetComponent<T>();
            }

            return component;
        }

        public static T TrySetDataIfNull<T>(this Object gameObject, ref T component)
            where T : ScriptableObject
        {
            if (component == null)
            {
                component = Project.Loader.GetAssetByTypeOnValidate<T>();
            }

            return component;
        }

        public static T OrNull<T>(this T obj) 
            where T : Object
        {
            return obj ? obj : null;
        }
    }
}