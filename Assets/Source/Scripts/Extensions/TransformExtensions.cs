using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class TransformExtensions
    {
        public static Transform WithPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var vector = transform.position;
            vector = new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
            transform.position = vector;
            return transform;
        } 
        
        public static Transform AddPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            var vector = transform.position;
            vector = new Vector3(vector.x + (x ?? 0),vector.y + ( y ?? 0), vector.z + ( z ?? 0));
            transform.position = vector;
            return transform;
        } 
    }
}