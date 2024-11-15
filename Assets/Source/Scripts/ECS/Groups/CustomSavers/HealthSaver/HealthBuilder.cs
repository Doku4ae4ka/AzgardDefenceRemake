using System;
using ECS.Modules.Exerussus.Health;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.HealthSaver
{
    public class HealthBuilder : EntityBuilder
    {
        public override SlotCategory Category { get; } = SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<HealthData.Health>();
        private HealthPooler _healthPooler;
        private EcsWorld _world;
        private const string HealthMax = "health.max";
        private const string HealthCurrent = "health.current";
        private const string HealthRegeneration = "health.regeneration";
        
        public override void Initialize(GameShare gameShare)
        {
            gameShare.GetSharedObject(ref _healthPooler);
            _world = gameShare.GetSharedObject<Componenter>().World;
        }

        public override bool CheckPrototypeProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.TryGetField(HealthMax, out var healthMax);
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i => { };
            var hasMax = slotEntity.TryGetFloatField(HealthMax, out var healthMax);
            var hasCurrent = slotEntity.TryGetFloatField(HealthCurrent, out var healthCurrent);
            
            if (hasMax)
            {
                if (hasCurrent)
                {
                    resultAction += i =>
                    {
                        ref var healthData = ref _healthPooler.Health.Add(i);
                        healthData.Max = healthMax;
                        healthData.Current = healthCurrent;
                    };
                }
                else
                {
                    resultAction += i =>
                    {
                        ref var healthData = ref _healthPooler.Health.Add(i);
                        healthData.Max = healthMax;
                        healthData.Current = healthMax;
                    };
                }
            }

            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if (slotEntity.TryGetFloatField(HealthMax, out var healthMax))
            {
                ref var healthData = ref _healthPooler.Health.Add(entity);
                healthData.Max = healthMax;
                healthData.Current = slotEntity.TryGetFloatField(HealthCurrent, out var healthCurrent) ? healthCurrent : healthMax;
            }
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            if (_healthPooler.Health.Has(entity))
            {
                ref var healthData = ref _healthPooler.Health.Get(entity);
                slotEntity.SetField(HealthMax, $"{healthData.Max}");
                slotEntity.SetField(HealthCurrent, $"{healthData.Current}");
            }
        }
    }
}