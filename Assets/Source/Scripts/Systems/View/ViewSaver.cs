using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.Systems.View
{
    public static class ViewSaver
    {
        public static void TrySave(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.View>().Inc<EcsData.Transform>().End())
            {
                ref var viewData = ref pooler.View.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);
                ref var transformData = ref pooler.Transform.Get(entity);
                
                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.View, viewData.ViewId);
                
                if (pooler.Prototype.Has(entity)) continue;
                
                savingEntity.SetField(SavePath.Position, $"{transformData.Value.position}");
                
                if ("(0.00000, 0.00000, 0.00000, 1.00000)" != $"{transformData.Value.rotation}")
                {
                    savingEntity.SetField(SavePath.Rotation, $"{transformData.Value.rotation}");
                }
            }
        }
        
    }
}