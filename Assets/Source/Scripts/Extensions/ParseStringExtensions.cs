using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using MapMaker.Scripts.EntitySettings.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;

namespace Source.Scripts.Extensions
{
    public static class ParserExtensions
    {
        public static bool TryParseVector2(this string vectorString, out Vector2 result)
        {
            result = Vector2.zero;

            var match = Regex.Match(vectorString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                
                bool xParsed = float.TryParse(cleanX, NumberStyles.Float, CultureInfo.InvariantCulture, out float x);
                bool yParsed = float.TryParse(cleanY, NumberStyles.Float, CultureInfo.InvariantCulture, out float y);

                if (xParsed && yParsed)
                {
                    result = new Vector2(x, y);
                    return true;
                }
            }

            return false;
        }
        
        public static Vector2 ParseVector2(this string vectorString)
        {

            var match = Regex.Match(vectorString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                
                bool xParsed = float.TryParse(cleanX, NumberStyles.Float, CultureInfo.InvariantCulture, out float x);
                bool yParsed = float.TryParse(cleanY, NumberStyles.Float, CultureInfo.InvariantCulture, out float y);

                if (xParsed && yParsed)
                {
                    return new Vector2(x, y);
                }
            }

            return default;
        }
        
        public static Vector2Int ParseVector2Int(this string vectorString)
        {

            var match = Regex.Match(vectorString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                
                bool xParsed = int.TryParse(cleanX, out var x);
                bool yParsed = int.TryParse(cleanY, out var y);

                if (xParsed && yParsed)
                {
                    return new Vector2Int(x, y);
                }
            }

            return default;
        }
        
        public static bool TryParseVector3(this string vectorString, out Vector3 result)
        {
            result = Vector3.zero;

            var match = Regex.Match(vectorString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                string cleanZ = match.Groups[5].Value.Trim();
                
                bool xParsed = float.TryParse(cleanX, NumberStyles.Float, CultureInfo.InvariantCulture, out float x);
                bool yParsed = float.TryParse(cleanY, NumberStyles.Float, CultureInfo.InvariantCulture, out float y);
                bool zParsed = float.TryParse(cleanZ, NumberStyles.Float, CultureInfo.InvariantCulture, out float z);

                if (xParsed && yParsed && zParsed)
                {
                    result = new Vector3(x, y, z);
                    return true;
                }
            }

            return false;
        }
        
        public static Vector3 ParseVector3(this string vectorString)
        {

            var match = Regex.Match(vectorString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                string cleanZ = match.Groups[5].Value.Trim();
                
                bool xParsed = float.TryParse(cleanX, NumberStyles.Float, CultureInfo.InvariantCulture, out float x);
                bool yParsed = float.TryParse(cleanY, NumberStyles.Float, CultureInfo.InvariantCulture, out float y);
                bool zParsed = float.TryParse(cleanZ, NumberStyles.Float, CultureInfo.InvariantCulture, out float z);

                if (xParsed && yParsed && zParsed)
                {
                    return new Vector3(x, y, z);
                }
            }

            return default;
        }
        
        public static bool TryParseVector4(this string vectorString, out Vector4 result)
        {
            var match = Regex.Match(vectorString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                string cleanZ = match.Groups[5].Value.Trim();
                string cleanW = match.Groups[7].Value.Trim();

                bool xParsed = float.TryParse(cleanX, NumberStyles.Float, CultureInfo.InvariantCulture, out float x);
                bool yParsed = float.TryParse(cleanY, NumberStyles.Float, CultureInfo.InvariantCulture, out float y);
                bool zParsed = float.TryParse(cleanZ, NumberStyles.Float, CultureInfo.InvariantCulture, out float z);
                bool wParsed = float.TryParse(cleanW, NumberStyles.Float, CultureInfo.InvariantCulture, out float w);

                if (xParsed && yParsed && zParsed && wParsed)
                {
                    result = new Vector4(x, y, z, w);
                    return true;
                }
            }

            result = default;
            return false;
        }
        
        public static Vector4 ParseVector4(this string vectorString)
        {
            var match = Regex.Match(vectorString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                string cleanZ = match.Groups[5].Value.Trim();
                string cleanW = match.Groups[7].Value.Trim();

                bool xParsed = float.TryParse(cleanX, NumberStyles.Float, CultureInfo.InvariantCulture, out float x);
                bool yParsed = float.TryParse(cleanY, NumberStyles.Float, CultureInfo.InvariantCulture, out float y);
                bool zParsed = float.TryParse(cleanZ, NumberStyles.Float, CultureInfo.InvariantCulture, out float z);
                bool wParsed = float.TryParse(cleanW, NumberStyles.Float, CultureInfo.InvariantCulture, out float w);

                if (xParsed && yParsed && zParsed && wParsed)
                {
                    return new Vector4(x, y, z, w);
                }
            }

            return default;
        }
        
        public static bool TryParseQuaternion(this string quaternionString, out Quaternion result)
        {
            var match = Regex.Match(quaternionString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                string cleanZ = match.Groups[5].Value.Trim();
                string cleanW = match.Groups[7].Value.Trim();

                bool xParsed = float.TryParse(cleanX, NumberStyles.Float, CultureInfo.InvariantCulture, out float x);
                bool yParsed = float.TryParse(cleanY, NumberStyles.Float, CultureInfo.InvariantCulture, out float y);
                bool zParsed = float.TryParse(cleanZ, NumberStyles.Float, CultureInfo.InvariantCulture, out float z);
                bool wParsed = float.TryParse(cleanW, NumberStyles.Float, CultureInfo.InvariantCulture, out float w);

                if (xParsed && yParsed && zParsed && wParsed)
                {
                    result = new Quaternion(x, y, z, w);
                    return true;
                }
            }

            result = default;
            return false;
        }
        
        public static Quaternion ParseQuaternion(this string quaternionString)
        {
            var match = Regex.Match(quaternionString, @"^\((-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?),\s*(-?\d+(\.\d+)?)\)$");

            if (match.Success)
            {
                string cleanX = match.Groups[1].Value.Trim();
                string cleanY = match.Groups[3].Value.Trim();
                string cleanZ = match.Groups[5].Value.Trim();
                string cleanW = match.Groups[7].Value.Trim();

                bool xParsed = float.TryParse(cleanX, NumberStyles.Float, CultureInfo.InvariantCulture, out float x);
                bool yParsed = float.TryParse(cleanY, NumberStyles.Float, CultureInfo.InvariantCulture, out float y);
                bool zParsed = float.TryParse(cleanZ, NumberStyles.Float, CultureInfo.InvariantCulture, out float z);
                bool wParsed = float.TryParse(cleanW, NumberStyles.Float, CultureInfo.InvariantCulture, out float w);

                if (xParsed && yParsed && zParsed && wParsed)
                {
                    return new Quaternion(x, y, z, w);
                }
            }

            return default;
        }
        
        public static bool TryParseTileEntries(this string serializedData, Dictionary<string, TileBase> tileDictionary, out List<KeyValuePair<Vector3Int, TileBase>> result)
        {
            result = new List<KeyValuePair<Vector3Int, TileBase>>();
            var tileEntries = serializedData.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var entry in tileEntries)
            {
                var tileInfo = entry.Trim('(', ')').Split(',');
                if (tileInfo.Length == 3)
                {
                    bool xParsed = int.TryParse(tileInfo[0].Trim(), out int x);
                    bool yParsed = int.TryParse(tileInfo[1].Trim(), out int y);
                    string tileName = tileInfo[2].Trim();

                    if (xParsed && yParsed)
                    {
                        if (tileDictionary.TryGetValue(tileName, out var tile))
                        {
                            result.Add(new KeyValuePair<Vector3Int, TileBase>(new Vector3Int(x, y, 0), tile));
                        }
                        else
                        {
#if UNITY_EDITOR
                            Debug.LogWarning($"Tile with name {tileName} not found in the dictionary.");
#endif
                            result = default;
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        
        public static bool TryParseRoutes(this string listString, out Dictionary<int, List<Vector2>> result)
        {
            result = new Dictionary<int, List<Vector2>>();
            if (string.IsNullOrEmpty(listString))
            {
                return false;
            }
    
            var entries = listString.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var entry in entries)
            {
                var idAndPoints = entry.Split(':');
        
                // Проверяем, что idAndPoints содержит хотя бы 2 элемента
                if (idAndPoints.Length != 2)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"Некорректный формат маршрута: {entry}");
#endif
                    continue;
                }

                // Пробуем распарсить ID маршрута
                if (!int.TryParse(idAndPoints[0].Trim(), out var id))
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"Не удалось распарсить ID маршрута: {idAndPoints[0]}");
#endif
                    continue;
                }
        
                // Пробуем распарсить точки маршрута
                var points = idAndPoints[1].Split('/');
                var waypoints = new List<Vector2>();

                foreach (var point in points)
                {
                    var vectorStr = point.Trim('(', ')').Split(',');
                    if (vectorStr.Length == 2)
                    {
                        bool xParsed = int.TryParse(vectorStr[0].Trim(), out int x);
                        bool yParsed = int.TryParse(vectorStr[1].Trim(), out int y);
                        if (xParsed && yParsed)
                        {
                            waypoints.Add(new Vector2(x, y));
                        }
#if UNITY_EDITOR
                        else
                        {
                            Debug.LogWarning($"Не удалось распарсить точку маршрута: {point}");
                        }
#endif
                    }
                }

                // Если удалось распарсить хотя бы одну точку, добавляем маршрут
                if (waypoints.Count > 0)
                {
                    result[id] = waypoints;
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogWarning($"Маршрут с ID {id} не содержит валидных точек");
                }
#endif
            }

            return result.Count > 0;
        }
        
        public static AssetReference ParseToAssetReference(this string assetGuidOrPath)
        {
            return new AssetReference(assetGuidOrPath);
        }
        
        public static float ParseFloat(this string value)
        {
            // if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
            // {
            //     return result;
            // }            
            
            if (float.TryParse(value, out float result))
            {
                return result;
            }
            
            return default;
        }
        
        public static int ParseInt(this string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            
            return default;
        }
        
        
        public static void PrintDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
        }
        
        public static void PrintDictionary<TKey>(this Dictionary<TKey, List<Vector2Int>> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                Debug.Log($"Key: {kvp.Key}, Value: ");
                foreach (var vector in kvp.Value)
                {
                    Debug.Log(vector);
                }
            }
        }
    }
}
