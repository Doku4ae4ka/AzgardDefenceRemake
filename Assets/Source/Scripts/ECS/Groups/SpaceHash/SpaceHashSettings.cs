using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.SpaceHash
{
    public class SpaceHashSettings
    {
        public float CellSize = 1f;
        public Vector2 MinPoint = Vector2.zero;
        public Vector2 MaxPoint = Vector2.one * 10;
        public EcsWorld.Mask AdditionalMask;
    }
}