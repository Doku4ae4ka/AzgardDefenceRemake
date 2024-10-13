using System;
using ECS.Modules.Exerussus.Health;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.Enemies
{
    public class EnemyBuilder : EntityBuilder
    {
        public override SlotCategory Category { get; } = SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask { get; }
        private HealthPooler _healthPooler;
        private EnemyPooler _enemyPooler;
        
        private const string EnemyType = "enemy";
        
        public override void Initialize(GameShare gameShare)
        {
             gameShare.GetSharedObject(ref _healthPooler);
             gameShare.GetSharedObject(ref _enemyPooler);
        }

        public override bool CheckPrototypeProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.type == EnemyType;
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i =>
            {
                _enemyPooler.EnemyMark.Add(i);
            };

            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            
        }
    }
}