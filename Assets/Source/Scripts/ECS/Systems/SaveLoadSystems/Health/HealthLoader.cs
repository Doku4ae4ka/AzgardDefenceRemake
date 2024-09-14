using System;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.Health
{
    public static class HealthLoader
    {
        public static void TryLoadPrototype(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().Inc<EcsData.Enemy>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);

                if (!savingEntity.TryGetIntField(SavePath.Health.Max, out var maxValue)) continue;
                
                ref var prototypeData =ref pooler.Prototype.Get(entity);

                var maxHealth = maxValue;
                float currentHealth;

                if (savingEntity.TryGetFloatField(SavePath.Health.Current, out var current))
                    currentHealth = current;
                else currentHealth = maxHealth;
                
                Action<int> buildAction = (int newEntity) =>
                {
                    ref var healthData = ref pooler.Health.AddOrGet(newEntity);
                    healthData.Max = maxHealth;
                    healthData.Current = currentHealth;
                };
                
                buildAction.Invoke(entity);
                prototypeData.DataBuilder.Add(buildAction);
            }
        }
        
        public static void TryLoadDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Inc<EcsData.Enemy>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (savingEntity.TryGetIntField(SavePath.Health.Max, out var maxValue))
                {
                    var maxHealth = maxValue;
                    float currentHealth;

                    if (savingEntity.TryGetFloatField(SavePath.Health.Current, out var current)) currentHealth = current;
                    else currentHealth = maxHealth;
                    
                    ref var healthData = ref pooler.Health.AddOrGet(entity);
                    healthData.Max = maxHealth;
                    healthData.Current = currentHealth;
                }
            }
        }
        
        // protected override void OnSignal(Signals.CommandLoadGame data)
        // {  
        //     foreach (var entity in AllEntitiesMask.End())
        //     {
        //         ref var entityData = ref Pooler.Entity.Get(entity);
        //         
        //         if (GameConfigurations.memory.TryGetField(
        //                 data.SlotName, 
        //                 entityData.EntityID, 
        //                 SavePath.HealthMax, 
        //                 out var maxValue))
        //         {
        //             var maxHealth = int.Parse(maxValue);
        //             var currentHealth = (float)maxHealth;
        //
        //             if (GameConfigurations.memory.TryGetField(
        //                     data.SlotName,
        //                     entityData.EntityID,
        //                     SavePath.HealthCurrent,
        //                     out var currentValue))
        //             {
        //                 currentHealth = float.Parse(currentValue);
        //             }
        //
        //             Action<int> addDataAction = ent =>
        //             {
        //                 ref var healthData = ref Pooler.Health.AddOrGet(ent);
        //                 healthData.Max = maxHealth;
        //                 healthData.Current = currentHealth;
        //             };
        //             
        //             addDataAction.Invoke(entity);
        //             
        //             if (Pooler.Prototype.Has(entity))
        //             {
        //                 ref var prototypeData = ref Pooler.Prototype.Get(entity);
        //                 prototypeData.DataBuilder.Add(addDataAction);
        //             }
        //         }
        //     }
        // }
    }
}