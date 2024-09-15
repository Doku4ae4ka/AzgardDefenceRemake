using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.Movable
{
    public static class MovableSaver
    {
        public static void TryAddData(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Movable>().End())
            {
                ref var movable = ref pooler.Movable.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);

                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.Movable.CurrentWaypointIndex, $"{movable.CurrentWaypointIndex}");
                savingEntity.SetField(SavePath.Movable.DistanceToCastle, $"{movable.PassedDistance}");
                savingEntity.SetField(SavePath.Movable.Speed, $"{movable.Speed}");
            }
        }
    }
}