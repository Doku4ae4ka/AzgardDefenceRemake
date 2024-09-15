using Leopotam.EcsLite;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class EcsExtensions
    {
        public static bool TryGetBuildingTilemapEntity(this Pooler pooler, EcsWorld world, out int tilemapEntity)
        {
            tilemapEntity = 0;
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.BuildingTileMap>().End())
                tilemapEntity = entity;
            
            if (tilemapEntity == default) return false;
            return true;
        }
        
        public static float GetDistance(this Pooler pooler, int firstEntity, int secondEntity)
        {
            ref var firstTransform = ref pooler.Transform.Get(firstEntity);
            ref var secondTransform = ref pooler.Transform.Get(secondEntity);

            return Vector2.Distance(firstTransform.Value.position, secondTransform.Value.position);
        }

        public static float GetDistance(this Pooler pooler, int firstEntity, int secondEntity,
            out Vector2 firstPosition, out Vector2 secondPosition)
        {
            ref var firstTransform = ref pooler.Transform.Get(firstEntity);
            ref var secondTransform = ref pooler.Transform.Get(secondEntity);
            firstPosition = firstTransform.Value.position;
            secondPosition = secondTransform.Value.position;
            return Vector2.Distance(firstPosition, secondPosition);
        }

        public static float GetDistanceSquared(this Pooler pooler, int firstEntity, int secondEntity)
        {
            ref var firstTransform = ref pooler.Transform.Get(firstEntity);
            ref var secondTransform = ref pooler.Transform.Get(secondEntity);

            return (firstTransform.Value.position - secondTransform.Value.position).sqrMagnitude;
        }

        public static float GetDistanceSquared(this Pooler pooler, int firstEntity, int secondEntity,
            out Vector2 firstPosition, out Vector2 secondPosition)
        {
            ref var firstTransform = ref pooler.Transform.Get(firstEntity);
            ref var secondTransform = ref pooler.Transform.Get(secondEntity);
            firstPosition = firstTransform.Value.position;
            secondPosition = secondTransform.Value.position;
            return (firstTransform.Value.position - secondTransform.Value.position).sqrMagnitude;
        }

        public static Vector2 GetPosition(this Pooler pooler, int entity)
        {
            ref var transformData = ref pooler.Transform.Get(entity);
            return transformData.Value.position;
        }
    }
}