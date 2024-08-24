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

                Entity savingEntity;
                
                if (slot.TryGetEntity(entityData.EntityID, out var foundEntity)) savingEntity = foundEntity;
                else
                {
                    savingEntity = new Entity(entityData.EntityID, entityData.Category);
                    if (pooler.Enemy.Has(entity)) slot.AddEnemy(savingEntity);
                }
                
                savingEntity.SetField(SavePath.HealthMax, $"{healthData.Max}");
        
                if (!Mathf.Approximately(healthData.Current, healthData.Max))
                {
                    savingEntity.SetField(SavePath.HealthCurrent, $"{healthData.Current}");
                }
            }
        }
    }
}