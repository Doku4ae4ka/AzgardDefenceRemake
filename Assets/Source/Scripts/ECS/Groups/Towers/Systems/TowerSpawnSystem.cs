using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.Core;

namespace Source.Scripts.ECS.Groups.Towers.Systems
{
    public class TowerSpawnSystem : EasySystem<TowerPooler>
    {
        // protected override void OnSignal(Signals.CommandSpawnTower data)
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
        //     ref var tilePositionData = ref Pooler.TilePosition.Get(newEntity);
        //     tilePositionData.Value = data.TilePosition;
        //     ref var positionData = ref Pooler.Position.Get(newEntity);
        //     positionData.Value = data.TilePosition;
        //     if (Pooler.Transform.Has(newEntity))
        //     {
        //         ref var transformData = ref Pooler.Transform.Get(newEntity);
        //         transformData.Value.position = data.TilePosition;
        //     }
        //     
        //     ref var viewData = ref Pooler.TowerView.Get(newEntity);
        //     viewData.Value.Show();
        // }
    }
}