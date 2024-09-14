using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.View
{
    public static class ViewSaver
    {
        #region Dynamic

        public static void TrySaveDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            TrySaveTower(world, pooler, slot);
            TrySaveEnemy(world, pooler, slot);
        }

        private static void TrySaveTower(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.TowerView>()
                         .Inc<EcsData.TransformData>().End())
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

        private static void TrySaveEnemy(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.EnemyView>()
                         .Inc<EcsData.TransformData>().End())
            {
                ref var viewData = ref pooler.EnemyView.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);
                ref var transformData = ref pooler.Transform.Get(entity);
                
                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;

                foundEntity.SetField(SavePath.View.Enemy, viewData.ViewId.AssetGUID);
                
                if (pooler.Prototype.Has(entity)) continue;
                
                foundEntity.SetField(SavePath.WorldSpace.Position, $"{transformData.Value.position}");
                
                if ("(0.00000, 0.00000, 0.00000, 1.00000)" != $"{transformData.Value.rotation}")
                {
                    foundEntity.SetField(SavePath.WorldSpace.Rotation, $"{transformData.Value.rotation}");
                }
            }
        }
         
        #endregion
        
        #region Static

        public static void TrySaveStatic(EcsWorld world, Pooler pooler, Slot slot)
        {
            TrySaveEnvironment(world, pooler, slot);
        }

        private static void TrySaveEnvironment(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Environment>()
                         .Inc<EcsData.TransformData>().End())
            {
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
    }
}