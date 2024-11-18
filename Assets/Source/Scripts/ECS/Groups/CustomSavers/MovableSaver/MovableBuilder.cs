using System;
using ECS.Modules.Exerussus.Movement;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.HealthSaver
{
    public class MovableBuilder : EntityBuilder
    {
        public override SlotCategory Category { get; } = SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<EcsData.Movable>();
        private MovementPooler _movementPooler;
        private EcsWorld _world;
        
        public override void Initialize(GameShare gameShare)
        {
            gameShare.GetSharedObject(ref _movementPooler);
            _world = gameShare.GetSharedObject<Componenter>().World;
        }

        public override bool CheckPrototypeProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.TryGetField(SavePath.Movable.Speed, out var value);
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i => { };
            if (!slotEntity.TryGetFloatField(SavePath.Movable.Speed, out var speed)) return resultAction;

            resultAction += i =>
            {
                ref var speedData = ref _movementPooler.Speed.Add(i);
                speedData.Value = speed;
            };
            
            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if (slotEntity.TryGetFloatField(SavePath.Movable.Speed, out var speed))
            {
                ref var speedData = ref _movementPooler.Speed.Add(entity);
                speedData.Value = speed;
            }
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            if (_movementPooler.Speed.Has(entity))
            {
                ref var speedData = ref _movementPooler.Speed.Get(entity);
                slotEntity.SetField(SavePath.Movable.Speed, $"{speedData.Value}");
            }
        }
    }
}