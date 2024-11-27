using ECS.Modules.Exerussus.Movement;
using ECS.Modules.Exerussus.TransformRelay;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore.Systems
{
    public class MovementSystem : EasySystem<GameCorePooler>
    {
        private EcsFilter _enemyFilter;
        [InjectSharedObject] private MovementPooler _movementPooler;
        [InjectSharedObject] private TransformRelayPooler _transformPooler;
        
        protected override void Initialize()
        {
            _enemyFilter = Pooler.InGameMask.Inc<EcsData.PathFollower>().Inc<EcsData.Route>().End();
        }
        
        protected override void Update()
        {
            _enemyFilter.Foreach(OnUpdate);
        }
        
        private void OnUpdate(int entity)
        {
            ref var pathFollower = ref Pooler.PathFollower.Get(entity);
            ref var movement = ref _movementPooler.Speed.Get(entity);
            ref var route = ref Pooler.Route.Get(entity);
            ref var position = ref _movementPooler.Position.Get(entity);
            var waypoints = route.Waypoints;
            var index = pathFollower.CurrentWaypointIndex;
            var currentPosition = (Vector2)position.Value;
        
            if (index >= waypoints.Count - 1) return;
            
            ref var transform = ref _transformPooler.Transform.Get(entity);
            var nextWaypoint = waypoints[index + 1];
            var direction = (nextWaypoint - currentPosition).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.Value.rotation = Quaternion.Euler(0, 0, angle);
            
            var step = DeltaTime * movement.Value;
            var newPos = Vector2.MoveTowards(currentPosition, nextWaypoint, step);
            position.Value = newPos;
            
            if (newPos.GetDistanceSquared(nextWaypoint) < 0.01f)
            {
                pathFollower.CurrentWaypointIndex++;
                pathFollower.PassedDistance += (waypoints[index] - nextWaypoint).sqrMagnitude;
            }
        }
    }
}