using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class BaseExtensions
    {
        public static TI Pop<TK, TI>(this Dictionary<TK, TI> dictionary, TK key)
        {
            var item = dictionary[key];
            dictionary.Remove(key);
            return item;
        }
        
        public static T Pop<T>(this List<T> collection, int index)
        {
            var item = collection[index];
            collection.RemoveAt(index);
            return item;
        }
        
        public static T PopFirst<T>(this List<T> collection)
        {
            var item = collection[0];
            collection.RemoveAt(0);
            return item;
        }
        
        public static T PopLast<T>(this List<T> collection)
        {
            var item = collection[^1];
            collection.Remove(item);
            return item;
        }
        
        public static T GetRandomItem<T>(this T[] collection)
        {
            if (collection.Length == 0) Debug.LogError("Empty array");
            if (collection.Length == 1) return collection[0];
            var rValue = Random.Range(0, collection.Length);
            return collection[rValue];
        }
        
        public static T GetRandomItem<T>(this List<T> collection)
        {
            var rValue = Random.Range(0, collection.Count);
            return collection[rValue];
        }
        
        public static List<T> AddUnique<T>(this List<T> list, T[] other)
        {
            foreach (var item in other) if (item != null && !list.Contains(item)) list.Add(item);
            return list;
        }
        
        public static List<T> AddUnique<T>(this List<T> list, T item)
        {
            if (item != null && !list.Contains(item)) list.Add(item);
            return list;
        }
        
    }
}