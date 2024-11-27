using ECS.Modules.Exerussus.Movement;
using ECS.Modules.Exerussus.TransformRelay;
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore.Systems
{
    public class EnemySpawnSystem : EcsSignalListener<GameCorePooler, Signals.CommandSpawnEnemy>
    {
        [InjectSharedObject] private SlotSaverPooler _saverPooler;
        [InjectSharedObject] private MovementPooler _movementPooler;
        [InjectSharedObject] private TransformRelayPooler _transformPooler;
        protected override void OnSignal(Signals.CommandSpawnEnemy data)
        {
            var newEntity = _saverPooler.CreatePrototype(World, data.PrototypeEntity);
            //ref var prototypeSlotEntity = ref _saverPooler.SlotEntity.Get(data.PrototypeEntity);
            //ref var entityData = ref _saverPooler.SlotEntity.Get(newEntity);
            // entityData.EntityID = $"{prototypeSlotEntity.EntityID}.{Pooler.Configs.FreeID}";
            
            ref var viewData = ref Pooler.EnemyView.Get(newEntity);
            viewData.Value.Show();
        
            ref var routeData = ref Pooler.Route.Add(newEntity);
            if (Pooler.Configs.Routes.TryGetValue(data.RouteId, out var waypoints))
            {
                routeData.RouteId = data.RouteId;
                routeData.Waypoints = waypoints;
            }
                        
            ref var positionData = ref _movementPooler.Position.Get(newEntity);
            positionData.Value = routeData.Waypoints[0];
            if (_transformPooler.Transform.Has(newEntity))
            {
                ref var transformData = ref _transformPooler.Transform.Get(newEntity);
                var direction = (routeData.Waypoints[1] - routeData.Waypoints[0]).normalized;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transformData.Value.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}