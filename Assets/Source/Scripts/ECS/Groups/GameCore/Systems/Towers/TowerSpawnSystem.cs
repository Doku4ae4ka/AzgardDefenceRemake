using ECS.Modules.Exerussus.Movement;
using ECS.Modules.Exerussus.TransformRelay;
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.GameCore;
using Source.Scripts.ECS.Groups.SlotSaver;

namespace Source.Scripts.ECS.Groups.Towers.Systems
{
    public class TowerSpawnSystem : EcsSignalListener<GameCorePooler, Signals.CommandSpawnTower>
    {
        [InjectSharedObject] private SlotSaverPooler _saverPooler;
        [InjectSharedObject] private MovementPooler _movementPooler;
        [InjectSharedObject] private TransformRelayPooler _transformRelayPooler;
        protected override void OnSignal(Signals.CommandSpawnTower data)
        {
            var newEntity = _saverPooler.CreatePrototype(World, data.PrototypeEntity);
            //ref var prototypeSlotEntity = ref _saverPooler.SlotEntity.Get(data.PrototypeEntity);
            //ref var entityData = ref _saverPooler.SlotEntity.Get(newEntity);
            //entityData.EntityID = $"{prototypeSlotEntity.EntityID}.{Pooler.Configs.FreeID}";
            
            ref var tilePositionData = ref Pooler.TilePosition.Get(newEntity);
            tilePositionData.Value = data.TilePosition;
            ref var positionData = ref _movementPooler.Position.Get(newEntity);
            positionData.Value = data.TilePosition;
            
            ref var viewData = ref Pooler.TowerView.Get(newEntity);
            viewData.Value.Show();
        }
    }
}