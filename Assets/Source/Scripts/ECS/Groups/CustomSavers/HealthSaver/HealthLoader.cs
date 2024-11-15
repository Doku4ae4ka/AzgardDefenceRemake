// using System;
// using Leopotam.EcsLite;
// using Source.Scripts.Core;
// using Source.Scripts.ECS.Groups.SlotSaver.Core;
//
// namespace Source.Scripts.ECS.Groups.HealthSaver
// {
//     public static class HealthLoader
//     {
//         public static void TryLoadPrototype(EcsWorld world, Pooler pooler, Slot slot)
//         {
//             foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().Inc<EcsData.Enemy>().End())
//             {
//                 ref var entityData = ref pooler.Entity.Get(entity);
//                 var savingEntity = slot.GetEntity(entityData.EntityID);
//
//                 if (!savingEntity.TryGetIntField(SavePath.Health.Max, out var maxValue)) continue;
//                 
//                 ref var prototypeData =ref pooler.Prototype.Get(entity);
//
//                 var maxHealth = maxValue;
//                 float currentHealth;
//
//                 if (savingEntity.TryGetFloatField(SavePath.Health.Current, out var current))
//                     currentHealth = current;
//                 else currentHealth = maxHealth;
//                 
//                 Action<int> buildAction = (int newEntity) =>
//                 {
//                     ref var healthData = ref pooler.Health.Add(newEntity);
//                     healthData.Max = maxHealth;
//                     healthData.Current = currentHealth;
//                 };
//                 
//                 buildAction.Invoke(entity);
//                 prototypeData.DataBuilder.Add(buildAction);
//             }
//         }
//         
//         public static void TryLoadDynamic(EcsWorld world, Pooler pooler, Slot slot)
//         {
//             foreach (var entity in world.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Inc<EcsData.Enemy>().End())
//             {
//                 ref var entityData = ref pooler.Entity.Get(entity);
//                 var savingEntity = slot.GetEntity(entityData.EntityID);
//                 
//                 if (savingEntity.TryGetIntField(SavePath.Health.Max, out var maxValue))
//                 {
//                     var maxHealth = maxValue;
//                     float currentHealth;
//
//                     if (savingEntity.TryGetFloatField(SavePath.Health.Current, out var current)) currentHealth = current;
//                     else currentHealth = maxHealth;
//                     
//                     ref var healthData = ref pooler.Health.AddOrGet(entity);
//                     healthData.Max = maxHealth;
//                     healthData.Current = currentHealth;
//                 }
//             }
//         }
//     }
// }