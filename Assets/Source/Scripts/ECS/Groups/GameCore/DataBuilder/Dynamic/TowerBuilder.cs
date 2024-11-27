using System;
using ECS.Modules.Exerussus.Health;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.GameCore.DataBuilder
{
    public class TowerBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<HealthData.Health>();
        private GameCorePooler _corePooler;
        private EcsWorld _world;
        
        public override void Initialize(GameShare gameShare)
        {
            gameShare.GetSharedObject(ref _corePooler);
            _world = gameShare.GetSharedObject<Componenter>().World;
        }

        public override bool CheckProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.type == SavePath.EntityType.Tower;
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i => { };
            
            if(!slotEntity.TryGetFloatField(SavePath.Tower.Damage, out var damage)) return resultAction;
            if(!slotEntity.TryGetFloatField(SavePath.Tower.AttackSpeed, out var attackSpeed)) return resultAction;
            if(!slotEntity.TryGetFloatField(SavePath.Tower.Radius, out var radius)) return resultAction;
            if(!slotEntity.TryGetIntField(SavePath.Tower.BaseCost, out var baseCost)) return resultAction;
            if(!slotEntity.TryGetEnumField(SavePath.Tower.EnemyType, out EnemyType enemyType)) return resultAction;
            
            resultAction += i =>
            {
                 _corePooler.TowerMark.Add(i);
                 
                ref var attacker = ref _corePooler.Attacker.Add(i);
                attacker.Damage = damage;
                attacker.AttackSpeed = attackSpeed;
                attacker.Radius = radius;
                attacker.EnemyType = enemyType;
                attacker.TargetingType = TargetingType.Closest;
                
                ref var towerPrices = ref _corePooler.TowerPrices.Add(i);
                towerPrices.BaseCost = baseCost;
                
                ref var towerLevel = ref _corePooler.TowerLevel.Add(i);
                towerLevel.Value = 1;
                
                ref var target = ref _corePooler.Target.Add(i);
                target.HasTarget = false;
                
                ref var upgradable = ref _corePooler.Upgradable.Add(i);
            };
            
            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if(!slotEntity.TryGetFloatField(SavePath.Tower.Damage, out var damage)) return;
            if(!slotEntity.TryGetFloatField(SavePath.Tower.AttackSpeed, out var attackSpeed)) return;
            if(!slotEntity.TryGetFloatField(SavePath.Tower.Radius, out var radius)) return;
            if(!slotEntity.TryGetIntField(SavePath.Tower.BaseCost, out var baseCost)) return;
            if(!slotEntity.TryGetEnumField(SavePath.Tower.EnemyType, out EnemyType enemyType)) return;
            if (!slotEntity.TryGetFloatField(SavePath.Health.Max, out var healthMax)) return;
            
            ref var attacker = ref _corePooler.Attacker.Add(entity);
            attacker.Damage = damage;
            attacker.AttackSpeed = attackSpeed;
            attacker.Radius = radius;
            attacker.EnemyType = enemyType;
            attacker.TargetingType = 
                slotEntity.TryGetEnumField(SavePath.Tower.TargetingType, out TargetingType targeting) ? targeting : TargetingType.Closest;
                
            ref var towerPrices = ref _corePooler.TowerPrices.Add(entity);
            towerPrices.BaseCost = baseCost;
                
            ref var towerLevel = ref _corePooler.TowerLevel.Add(entity);
            towerLevel.Value = slotEntity.TryGetIntField(SavePath.TowerLevel.Level,  out var level) ? level : 1;
                
            ref var target = ref _corePooler.Target.Add(entity);
            target.HasTarget = false;
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        { 
            ref var attacker = ref _corePooler.Attacker.Get(entity);
            ref var towerPrices = ref _corePooler.TowerPrices.Get(entity);
            ref var towerLevel = ref _corePooler.TowerLevel.Get(entity);
            
            slotEntity.SetField(SavePath.Tower.BaseCost, $"{towerPrices.BaseCost}");
            slotEntity.SetField(SavePath.Tower.Damage, $"{attacker.Damage}");
            slotEntity.SetField(SavePath.Tower.AttackSpeed, $"{attacker.AttackSpeed}");
            slotEntity.SetField(SavePath.Tower.Radius, $"{attacker.Radius}");
            slotEntity.SetField(SavePath.Tower.EnemyType, $"{attacker.EnemyType}");
            slotEntity.SetField(SavePath.Tower.TargetingType, $"{attacker.TargetingType}");
            slotEntity.SetField(SavePath.TowerLevel.Level, $"{towerLevel.Value}");
        }
    }
}