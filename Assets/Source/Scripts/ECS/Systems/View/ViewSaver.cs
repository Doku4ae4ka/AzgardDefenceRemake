using System.Text;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Scripts.ECS.Systems.View
{
    public static class ViewSaver
    {
        #region Dynamic

        public static void TrySaveDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            TrySaveTower(world, pooler, slot);
            TrySaveEnemy(world, pooler, slot);
            TrySaveBuildingTilemap(world, pooler, slot);
        }
        public static void TrySaveTower(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.TowerView>()
                         .Inc<EcsData.Transform>().End())
            {
                ref var viewData = ref pooler.TowerView.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);
                ref var transformData = ref pooler.Transform.Get(entity);
                
                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.View.Tower, viewData.ViewId.AssetGUID);
                
                if (pooler.Prototype.Has(entity)) continue;
                
                savingEntity.SetField(SavePath.WorldSpace.Position, $"{transformData.Value.position}");
                
                if ("(0.00000, 0.00000, 0.00000, 1.00000)" != $"{transformData.Value.rotation}")
                {
                    savingEntity.SetField(SavePath.WorldSpace.Rotation, $"{transformData.Value.rotation}");
                }
            }
        }
        
        public static void TrySaveEnemy(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.EnemyView>()
                         .Inc<EcsData.Transform>().End())
            {
                ref var viewData = ref pooler.EnemyView.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);
                ref var transformData = ref pooler.Transform.Get(entity);
                
                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.View.Enemy, viewData.ViewId.AssetGUID);
                
                if (pooler.Prototype.Has(entity)) continue;
                
                savingEntity.SetField(SavePath.WorldSpace.Position, $"{transformData.Value.position}");
                
                if ("(0.00000, 0.00000, 0.00000, 1.00000)" != $"{transformData.Value.rotation}")
                {
                    savingEntity.SetField(SavePath.WorldSpace.Rotation, $"{transformData.Value.rotation}");
                }
            }
        }
        
        public static void TrySaveBuildingTilemap(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.BuildingTilemapView>()
                         .Inc<EcsData.BuildingTileMap>().Inc<EcsData.Transform>().End())
            {
                Debug.Log("TilemapSave?");
                ref var buildingTilemapData = ref pooler.BuildingTilemap.Get(entity);
                ref var viewData = ref pooler.BuildingTilemapView.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);
                ref var transformData = ref pooler.Transform.Get(entity);
                
                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.View.BuildingTilemap, viewData.ViewId.AssetGUID);

                savingEntity.SetField(SavePath.BuildingTilemap.Tilemap, SerializeTilemap(buildingTilemapData.Value));
                
                if (pooler.Prototype.Has(entity)) continue;
                
                savingEntity.SetField(SavePath.WorldSpace.Position, $"{transformData.Value.position}");
                
                if ("(0.00000, 0.00000, 0.00000, 1.00000)" != $"{transformData.Value.rotation}")
                {
                    savingEntity.SetField(SavePath.WorldSpace.Rotation, $"{transformData.Value.rotation}");
                }
            }
        }
         
        #endregion
        
        #region Static

        public static void TrySaveStatic(EcsWorld world, Pooler pooler, Slot slot)
        {
            TrySaveEnvironment(world, pooler, slot);
        }
        
        public static void TrySaveEnvironment(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Environment>()
                         .Inc<EcsData.Transform>().End())
            {
                Debug.Log("EnvSave?");
                ref var viewData = ref pooler.EnvironmentView.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);
                ref var transformData = ref pooler.Transform.Get(entity);
                
                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.View.Environment, viewData.ViewId.AssetGUID);
                
                if (pooler.Prototype.Has(entity)) continue;
                
                savingEntity.SetField(SavePath.WorldSpace.Position, $"{transformData.Value.position}");
                
                if ("(0.00000, 0.00000, 0.00000, 1.00000)" != $"{transformData.Value.rotation}")
                {
                    savingEntity.SetField(SavePath.WorldSpace.Rotation, $"{transformData.Value.rotation}");
                }
            }
        }

        #endregion
        
        public static string SerializeTilemap(Tilemap tilemap)
        {
            var sb = new StringBuilder();

            BoundsInt bounds = tilemap.cellBounds;
            foreach (var position in bounds.allPositionsWithin)
            {
                TileBase tile = tilemap.GetTile(position);
                if (tile != null) sb.Append($"({position.x},{position.y},{tile.name});");
            }

            return sb.ToString();
        }
    }
}