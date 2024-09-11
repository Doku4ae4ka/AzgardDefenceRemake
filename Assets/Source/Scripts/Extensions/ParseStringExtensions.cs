
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Source.Scripts.Core;
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
        
        public static Vector3 ParseVector2(this string vectorString)
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
                            Debug.LogWarning($"Tile with name {tileName} not found in the dictionary.");
                            result = default;
                            return false;
                        }
                    }
                }
            }

            return true;
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
    }
}
