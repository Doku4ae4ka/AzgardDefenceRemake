using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.GameCore;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.Enemies
{
    public class EnemyBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<EcsData.Enemy>();
        private GameCorePooler _corePooler;
        private EcsWorld _world;
        
        public override void Initialize(GameShare gameShare)
        {
            _world = gameShare.GetSharedObject<Componenter>().World;
            gameShare.GetSharedObject(ref _corePooler);
        }

        public override bool CheckProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.type == SavePath.EntityType.Enemy;
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i => { };
            
            if(!slotEntity.TryGetIntField(SavePath.Enemy.DamageToCastle, out var damage)) return resultAction;
            if(!slotEntity.TryGetEnumField(SavePath.Enemy.EnemyType, out EnemyType type)) return resultAction;
            
            resultAction += i =>
            {
                ref var enemy = ref _corePooler.Enemy.Add(i);
                enemy.EnemyType = type;
                enemy.DamageToCastle = damage;
            };

            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if(!slotEntity.TryGetIntField(SavePath.Enemy.DamageToCastle, out var damage)) return;
            if(!slotEntity.TryGetEnumField(SavePath.Enemy.EnemyType, out EnemyType type)) return;
            
            ref var enemy = ref _corePooler.Enemy.Add(entity);
            enemy.EnemyType = type;
            enemy.DamageToCastle = damage;
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            ref var enemy = ref _corePooler.Enemy.Get(entity);
            slotEntity.SetField(SavePath.Enemy.DamageToCastle, $"{enemy.DamageToCastle}");
            slotEntity.SetField(SavePath.Enemy.EnemyType, $"{enemy.EnemyType}");
        }
    }
}