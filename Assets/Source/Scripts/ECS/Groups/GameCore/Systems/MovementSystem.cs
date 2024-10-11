using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore.Systems
{
    public class MovementSystem : EasySystem<GameCorePooler>
    {
        private EcsFilter _enemyFilter;
        
        // protected override void Initialize()
        // {
        //     _enemyFilter = InGameMask.Inc<EcsData.Movable>().Inc<EcsData.Route>().End();
        // }
        //
        // protected override void Update()
        // {
        //     _enemyFilter.Foreach(OnUpdate);
        // }
        //
        // private void OnUpdate(int entity)
        // {
        //     ref var movable = ref Pooler.Movable.Get(entity);
        //     ref var route = ref Pooler.Route.Get(entity);
        //     ref var transform = ref Pooler.Transform.Get(entity);
        //     var waypoints = route.Waypoints;
        //     var index = movable.CurrentWaypointIndex;
        //     var currentPosition = (Vector2)transform.Value.position;
        //
        //     if (index >= waypoints.Count - 1) return;
        //
        //     var nextWaypoint = waypoints[index + 1];
        //     var direction = (nextWaypoint - currentPosition).normalized;
        //     var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //     transform.Value.rotation = Quaternion.Euler(0, 0, angle);
        //     
        //     var step = DeltaTime * movable.Speed;
        //     var newPos = Vector2.MoveTowards(currentPosition, nextWaypoint, step);
        //     transform.Value.position = newPos;
        //     
        //     if (newPos.GetDistanceSquared(nextWaypoint) < 0.01f)
        //     {
        //         movable.CurrentWaypointIndex++;
        //         movable.PassedDistance += (waypoints[index] - nextWaypoint).sqrMagnitude;
        //     }
        // }
    }
}