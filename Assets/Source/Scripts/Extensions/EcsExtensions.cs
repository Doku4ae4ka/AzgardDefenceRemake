using Leopotam.EcsLite;
using Source.Scripts.Core;

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
    }
}