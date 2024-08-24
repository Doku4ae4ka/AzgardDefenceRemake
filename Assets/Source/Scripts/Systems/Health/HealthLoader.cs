using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.Systems.Health
{
    public static class HealthLoader
    {
        public static void TryLoadDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (savingEntity.TryGetField(SavePath.HealthMax, out var maxValue))
                {
                    var maxHealth = int.Parse(maxValue);
                    var currentHealth = (float)maxHealth;
                    
                    if (savingEntity.TryGetField(SavePath.HealthCurrent, out var currentValue)) currentHealth = float.Parse(currentValue);
                    ref var healthData = ref pooler.Health.AddOrGet(entity);
                    healthData.Max = maxHealth;
                    healthData.Current = currentHealth;
                }
            }
        }
    }
}