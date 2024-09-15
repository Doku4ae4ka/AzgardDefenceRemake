using System;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.Movable
{
    public static class MovableLoader
    {
        public static void TryLoadPrototype(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().Inc<EcsData.Enemy>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);

                if (!savingEntity.TryGetFloatField(SavePath.Movable.Speed, out var speedValue)) continue;
                
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                
                Action<int> buildAction = (int newEntity) =>
                {
                    ref var movable = ref pooler.Movable.Add(newEntity);
                    movable.Speed = speedValue;
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
                
                if (savingEntity.TryGetFloatField(SavePath.Movable.Speed, out var speedValue))
                {
                    int currentIndex;
                    if (savingEntity.TryGetIntField(SavePath.Movable.CurrentWaypointIndex, out var current))
                        currentIndex = current;
                    else currentIndex = 0;
                
                    float distanceToCastle;
                    if (savingEntity.TryGetIntField(SavePath.Movable.DistanceToCastle, out var distance))
                        distanceToCastle = distance;
                    else distanceToCastle = 0;
                    
                    ref var movable = ref pooler.Movable.Add(entity);
                    movable.Speed = speedValue;
                    movable.CurrentWaypointIndex = currentIndex;
                    movable.PassedDistance = distanceToCastle;
                }
            }
        }
    }
}