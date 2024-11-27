using System;
using ECS.Modules.Exerussus.Health;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.HealthSaver
{
    public class HealthBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<HealthData.Health>();
        private HealthPooler _healthPooler;
        private EcsWorld _world;
        
        public override void Initialize(GameShare gameShare)
        {
            gameShare.GetSharedObject(ref _healthPooler);
            _world = gameShare.GetSharedObject<Componenter>().World;
        }

        public override bool CheckProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.TryGetField(SavePath.Health.Max, out var healthMax);
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i => { };
            if (!slotEntity.TryGetFloatField(SavePath.Health.Max, out var maxHealth)) return resultAction;
            
            // var currentHealth = slotEntity.TryGetFloatField(SavePath.Health.Current, out var current) ? current : maxHealth;

            resultAction += i =>
            {
                ref var healthData = ref _healthPooler.Health.Add(i);
                healthData.Max = maxHealth;
                //healthData.Current = currentHealth;
            };
            
            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if (!slotEntity.TryGetFloatField(SavePath.Health.Max, out var healthMax)) return;
            
            ref var healthData = ref _healthPooler.Health.Add(entity);
            healthData.Max = healthMax;
            healthData.Current = slotEntity.TryGetFloatField(SavePath.Health.Current, out var healthCurrent) ? healthCurrent : healthMax;
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            if (!_healthPooler.Health.Has(entity)) return;
            
            ref var healthData = ref _healthPooler.Health.Get(entity);
            slotEntity.SetField(SavePath.Health.Max, $"{healthData.Max}");
            if (!Mathf.Approximately(healthData.Current, healthData.Max))
                slotEntity.SetField(SavePath.Health.Current, $"{healthData.Current}");
        }
    }
}