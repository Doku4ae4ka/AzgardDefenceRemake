using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace Source.Scripts.Systems.Health
{
    public static class HealthSaver
    {
        public static void TryAddData(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Health>().End())
            {
                ref var healthData = ref pooler.Health.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);

                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.Health.Max, $"{healthData.Max}");
        
                if (!Mathf.Approximately(healthData.Current, healthData.Max))
                {
                    savingEntity.SetField(SavePath.Health.Current, $"{healthData.Current}");
                }
            }
        }
    }
}