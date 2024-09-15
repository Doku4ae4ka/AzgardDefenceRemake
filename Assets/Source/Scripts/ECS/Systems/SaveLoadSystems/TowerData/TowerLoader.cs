using System;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.TowerData
{
    public static class TowerLoader
    {
        public static void TryLoadPrototype(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().Inc<EcsData.Tower>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                
                if (!savingEntity.TryGetIntField(SavePath.Tower.BaseCost, out var baseCost)) continue;
                if (!savingEntity.TryGetFloatField(SavePath.Tower.Damage, out var damage)) continue;
                if (!savingEntity.TryGetFloatField(SavePath.Tower.AttackSpeed, out var attackSpeed)) continue;
                if (!savingEntity.TryGetFloatField(SavePath.Tower.Radius, out var radius)) continue;
                if (!savingEntity.TryGetEnumField(SavePath.Tower.EnemyType, out EnemyType enemyType)) continue;

                var levelUpCost = baseCost;
                if (savingEntity.TryGetIntField(SavePath.TowerLevel.Cost, out var cost))
                    levelUpCost = cost;
                
                Action<int> buildAction = (int newEntity) =>
                {
                    ref var tower = ref pooler.Tower.Get(newEntity);
                    tower.BaseCost = baseCost;
                    tower.Damage = damage;
                    tower.AttackSpeed = attackSpeed;
                    tower.Radius = radius;
                    tower.EnemyType = enemyType;
                    tower.TargetingType = TargetingType.Closest;
                    
                    ref var towerLevel = ref pooler.TowerLevel.Add(newEntity);
                    towerLevel.Value = 1;
                    towerLevel.Cost = levelUpCost;
                    
                    ref var target = ref pooler.Target.Add(newEntity);
                    target.HasTarget = false;
                };
                
                buildAction.Invoke(entity);
                prototypeData.DataBuilder.Add(buildAction);
            }
        }
        
        public static void TryLoadDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Inc<EcsData.Tower>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetIntField(SavePath.Tower.BaseCost, out var baseCost)) return;
                if (!savingEntity.TryGetFloatField(SavePath.Tower.Damage, out var damage)) return;
                if (!savingEntity.TryGetFloatField(SavePath.Tower.AttackSpeed, out var attackSpeed)) return;
                if (!savingEntity.TryGetFloatField(SavePath.Tower.Radius, out var radius)) return;
                if (!savingEntity.TryGetEnumField(SavePath.Tower.EnemyType, out EnemyType enemyType)) return;

                var targetingType = TargetingType.Closest;
                if (savingEntity.TryGetEnumField(SavePath.Tower.TargetingType, out TargetingType targeting))
                    targetingType = targeting;

                if (savingEntity.TryGetIntField(SavePath.TowerLevel.Cost, out var cost)) return;
                if (savingEntity.TryGetIntField(SavePath.TowerLevel.Level, out var level)) return;
                
                ref var tower = ref pooler.Tower.Add(entity);
                tower.BaseCost = baseCost;
                tower.Damage = damage;
                tower.AttackSpeed = attackSpeed;
                tower.Radius = radius;
                tower.EnemyType = enemyType;
                tower.TargetingType = targetingType;
                    
                ref var towerLevel = ref pooler.TowerLevel.Add(entity);
                towerLevel.Value = level;
                towerLevel.Cost = cost;
                    
                ref var target = ref pooler.Target.Add(entity);
                target.HasTarget = false;
                
            }
        }
    }
}