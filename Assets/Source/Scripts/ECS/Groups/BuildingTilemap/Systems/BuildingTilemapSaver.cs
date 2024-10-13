// using System.Text;
// using Leopotam.EcsLite;
// using Source.Scripts.Core;
// using Source.Scripts.ECS.Groups.SlotSaver.Core;
// using UnityEngine;
// using UnityEngine.Tilemaps;
//
// namespace Source.Scripts.ECS.Groups.BuildingTilemap.Systems
// {
//     public static class BuildingTilemapSaver
//     {
//         public static void TrySaveDynamic(EcsWorld world, Pooler pooler, Slot slot)
//         {
//             foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.BuildingTileMap>().End())
//             {
//                 ref var buildingTilemapData = ref pooler.BuildingTilemap.Get(entity);
//                 ref var entityData = ref pooler.Entity.Get(entity);
//
//                 if (!slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return;
//                 var savingEntity = foundEntity;
//                 
//                 savingEntity.SetField(SavePath.BuildingTilemap.Tilemap, SerializeTilemap(buildingTilemapData.Value));
//                 
//             }
//         }
//
//         private static string SerializeTilemap(Tilemap tilemap)
//         {
//             var sb = new StringBuilder();
//
//             BoundsInt bounds = tilemap.cellBounds;
//             foreach (var position in bounds.allPositionsWithin)
//             {
//                 TileBase tile = tilemap.GetTile(position);
//                 if (tile != null) sb.Append($"({position.x},{position.y},{tile.name});");
//             }
//
//             return sb.ToString();
//         }
//     }
// }