using System;
using ECS.Modules.Exerussus.Movement;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.GameCore;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.HealthSaver
{
    public class PathFollowerBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<EcsData.PathFollower>().Inc<MovementData.Speed>();
        private MovementPooler _movementPooler;
        private GameCorePooler _corePooler;
        private EcsWorld _world;
        
        public override void Initialize(GameShare gameShare)
        {
            gameShare.GetSharedObject(ref _movementPooler);
            gameShare.GetSharedObject(ref _corePooler);
            _world = gameShare.GetSharedObject<Componenter>().World;
        }

        public override bool CheckProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.TryGetField(SavePath.PathFollower.Speed, out var value);
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i => { };
            if (!slotEntity.TryGetFloatField(SavePath.PathFollower.Speed, out var speed)) return resultAction;
            
            resultAction += i =>
            {
                ref var speedData = ref _movementPooler.Speed.Add(i);
                speedData.Value = speed;
                _corePooler.PathFollower.Add(i);
            };
            
            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if (!slotEntity.TryGetFloatField(SavePath.PathFollower.Speed, out var speed)) return;
            
            ref var speedData = ref _movementPooler.Speed.Add(entity);
            speedData.Value = speed;
            
            ref var pathFollower = ref _corePooler.PathFollower.Add(entity);
            pathFollower.CurrentWaypointIndex =
                slotEntity.TryGetIntField(SavePath.PathFollower.CurrentWaypointIndex, out var index) ? index : 0;
            pathFollower.PassedDistance = 
                slotEntity.TryGetFloatField(SavePath.PathFollower.DistanceToCastle, out var distance) ? distance : 0f;
                
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            if (!_movementPooler.Speed.Has(entity)) return;
            if (!_corePooler.PathFollower.Has(entity)) return;
            
            ref var speedData = ref _movementPooler.Speed.Get(entity);
            slotEntity.SetField(SavePath.PathFollower.Speed, $"{speedData.Value}");
            
            ref var pathFollower = ref _corePooler.PathFollower.Get(entity);
            slotEntity.SetField(SavePath.PathFollower.CurrentWaypointIndex, $"{pathFollower.CurrentWaypointIndex}");
            slotEntity.SetField(SavePath.PathFollower.CurrentWaypointIndex, $"{pathFollower.PassedDistance}");
        }
    }
}