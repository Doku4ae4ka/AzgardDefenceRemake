// using System.Collections.Generic;
// using Leopotam.EcsLite;
// using Source.Scripts.Core;
// using Source.Scripts.SaveSystem;
// using UnityEngine;
// using UnityEngine.Tilemaps;
//
// namespace Source.Scripts.ECS.Systems.BuildingTilemap
// {
//     public static class BuildingTilemapLoader
//     {
//         public static void TryLoadDynamic(EcsWorld world, Pooler pooler, Slot slot)
//         {
//             foreach (var entity in world.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().End())
//             {
//                 ref var entityData = ref pooler.Entity.Get(entity);
//                 var savingEntity = slot.GetEntity(entityData.EntityID);
//                 
//                 if (savingEntity.TryGetTilemapField(SavePath.BuildingTilemap.Tilemap, CacheAllTiles(), out var loadedTilemap))
//                 {
//                     ref var tilemapData = ref pooler.BuildingTilemap.AddOrGet(entity);
//                     tilemapData.Value = loadedTilemap;
//                 }
//             }
//         }
//         
//         private static Dictionary<string, TileBase> CacheAllTiles()
//         {
//             var dict = new Dictionary<string, TileBase>();
//             
//             var exclude = Resources.Load<TileBase>(Constants.Resources.ExcludeTiles.Exclude);
//             var empty = Resources.Load<TileBase>(Constants.Resources.ExcludeTiles.Empty);
//             
//             if (exclude != null) dict.TryAdd("PurpleExclusion", exclude);
//             else Debug.LogError($"Tile '{Constants.Resources.ExcludeTiles.Exclude}' not found in Resources.");
//
//             if (empty != null) dict.TryAdd("CyanEmpty", empty);
//             else Debug.LogError($"Tile '{Constants.Resources.ExcludeTiles.Empty}' not found in Resources.");
//
//             return dict;
//         }
//     }
// }