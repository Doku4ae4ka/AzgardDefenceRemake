// using Leopotam.EcsLite;
// using Source.Scripts.Core;
//
// namespace Source.Scripts.ECS.Systems.BuildingTilemap
// {
//     public class BuildingTilemapSystem : EcsGameSystem
//     {
//         private EcsFilter _tilemapFilter;
//
//         protected override void Initialize()
//         {
//             _tilemapFilter = InGameMask.Inc<EcsData.BuildingTileMap>().End();
//             Memory.save.OnDynamic += BuildingTilemapSaver.TryAddData;
//             Memory.load.OnDynamic += BuildingTilemapLoader.TryLoadDynamic;
//             
//         }
//
//         public override void Destroy(IEcsSystems systems)
//         {
//             base.Destroy(systems);
//             Memory.save.OnDynamic -= BuildingTilemapSaver.TryAddData;
//             Memory.load.OnDynamic -= BuildingTilemapLoader.TryLoadDynamic;
//         }
//     }
// }