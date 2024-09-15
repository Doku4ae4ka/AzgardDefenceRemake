

using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 GetDirection(this Vector2 firstPos, Vector2 secondPos)
        {
            return (secondPos - firstPos).normalized;
        }
        
        public static float GetDistanceSquared(this Vector2 firstPos, Vector2 secondPos)
        {
            return (firstPos - secondPos).sqrMagnitude;
        }
        
        public static float GetDistanceSquared(this Vector2Int firstPos, Vector2Int secondPos)
        {
            return (firstPos - secondPos).sqrMagnitude;
        }
    }
}