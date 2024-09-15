using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.TowerData
{
    public static class TowerSaver
    {
        public static void TryAddData(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Tower>().End())
            {
                ref var towerData = ref pooler.Tower.Get(entity);
                ref var entityData = ref pooler.Entity.Get(entity);

                if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
                var savingEntity = foundEntity;
                
                savingEntity.SetField(SavePath.Tower.BaseCost, $"{towerData.BaseCost}");
                savingEntity.SetField(SavePath.Tower.Damage, $"{towerData.Damage}");
                savingEntity.SetField(SavePath.Tower.AttackSpeed, $"{towerData.AttackSpeed}");
                savingEntity.SetField(SavePath.Tower.Radius, $"{towerData.Radius}");
                savingEntity.SetField(SavePath.Tower.EnemyType, $"{towerData.EnemyType}");
                savingEntity.SetField(SavePath.Tower.TargetingType, $"{towerData.TargetingType}");
            }
        }
    }
}