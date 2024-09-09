using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.Systems.View
{
    public static class ViewSaver
    {
        #region Dynamic

        public static void TrySaveView(EcsWorld world, Pooler pooler, Slot slot)
        {
            TrySaveTower(world, pooler, slot);
            TrySaveEnemy(world, pooler, slot);
        }
        public static void TrySaveTower(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.TowerView>().Inc<EcsData.Transform>().End())
            {
                ref var viewData = ref pooler.TowerView.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);
                ref var transformData = ref pooler.Transform.Get(entity);
                
                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.View.Tower, viewData.ViewId);
                
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
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.EnemyView>().Inc<EcsData.Transform>().End())
            {
                ref var viewData = ref pooler.EnemyView.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);
                ref var transformData = ref pooler.Transform.Get(entity);
                
                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.View.Enemy, viewData.ViewId);
                
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

        

        #endregion
    }
}