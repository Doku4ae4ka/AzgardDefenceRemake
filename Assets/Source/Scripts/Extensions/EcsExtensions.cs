using ECS.Modules.Exerussus.Movement;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.GameCore;
using Source.Scripts.ECS.Groups.SlotSaver;
using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class EcsExtensions
    {
        public static bool TryGetBuildingTilemapEntity(this GameCorePooler pooler, EcsWorld world, out int tilemapEntity)
        {
            tilemapEntity = 0;
            foreach (var entity in world.Filter<SlotSaverData.SlotEntity>().Inc<EcsData.BuildingTileMap>().End())
                tilemapEntity = entity;
            
            if (tilemapEntity == default) return false;
            return true;
        }
        
        public static float GetDistance(this MovementPooler pooler, int firstEntity, int secondEntity)
        {
            ref var firstPos = ref pooler.Position.Get(firstEntity);
            ref var secondPos = ref pooler.Position.Get(secondEntity);

            return Vector2.Distance(firstPos.Value, secondPos.Value);
        }

        public static float GetDistance(this MovementPooler pooler, int firstEntity, int secondEntity,
            out Vector2 firstPosition, out Vector2 secondPosition)
        {
            ref var firstPos = ref pooler.Position.Get(firstEntity);
            ref var secondPos = ref pooler.Position.Get(secondEntity);
            firstPosition = firstPos.Value;
            secondPosition = secondPos.Value;
            return Vector2.Distance(firstPosition, secondPosition);
        }

        public static float GetDistanceSquared(this MovementPooler pooler, int firstEntity, int secondEntity)
        {
            ref var firstPos = ref pooler.Position.Get(firstEntity);
            ref var secondPos = ref pooler.Position.Get(secondEntity);

            return (firstPos.Value - secondPos.Value).sqrMagnitude;
        }

        public static float GetDistanceSquared(this MovementPooler pooler, int firstEntity, int secondEntity,
            out Vector2 firstPosition, out Vector2 secondPosition)
        {
            ref var firstPos = ref pooler.Position.Get(firstEntity);
            ref var secondPos = ref pooler.Position.Get(secondEntity);
            firstPosition = firstPos.Value;
            secondPosition = secondPos.Value;
            return (firstPos.Value - secondPos.Value).sqrMagnitude;
        }

        public static Vector2 GetPosition(this MovementPooler pooler, int entity)
        {
            ref var position = ref pooler.Position.Get(entity);
            return position.Value;
        }
    }
}