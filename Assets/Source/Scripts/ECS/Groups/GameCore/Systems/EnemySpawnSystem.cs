using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore.Systems
{
    public class EnemySpawnSystem : EasySystem<GameCorePooler>
    {
        // protected override void OnSignal(Signals.CommandSpawnEnemy data)
        // {
        //     ref var prototypeData = ref Pooler.Prototype.Get(data.PrototypeEntity);
        //     
        //     var newEntity = World.NewEntity();
        //     ref var entityData = ref Pooler.Entity.Add(newEntity);
        //     entityData.EntityID = $"{prototypeData.Category}.{Configs.FreeID}";
        //     entityData.Category = prototypeData.Category;
        //
        //     foreach (var action in prototypeData.DataBuilder) action.Invoke(newEntity);
        //     
        //     ref var viewData = ref Pooler.EnemyView.Get(newEntity);
        //     viewData.Value.Show();
        //
        //     ref var routeData = ref Pooler.Route.Add(newEntity);
        //     if (Configs.Routes.TryGetValue(data.RouteId, out var waypoints))
        //     {
        //         routeData.RouteId = data.RouteId;
        //         routeData.Waypoints = waypoints;
        //     }
        //                 
        //     ref var positionData = ref Pooler.Position.Get(newEntity);
        //     positionData.Value = routeData.Waypoints[0];
        //     if (Pooler.Transform.Has(newEntity))
        //     {
        //         ref var transformData = ref Pooler.Transform.Get(newEntity);
        //         transformData.Value.position = positionData.Value;
        //         
        //         var direction = (routeData.Waypoints[1] - routeData.Waypoints[0]).normalized;
        //         var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //         transformData.Value.rotation = Quaternion.Euler(0, 0, angle);
        //     }
        // }
    }
}