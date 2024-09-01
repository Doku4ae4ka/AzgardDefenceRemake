﻿
using System.Globalization;
using System.Text.RegularExpressions;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class ParserExtensions
    {
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
        
        public static EntityCategory ParseEntityCategory(this string value)
        {
            if (int.TryParse(value, out int result))
            {
                return (EntityCategory)result;
            }
            
            return default;
        }
    }
}
