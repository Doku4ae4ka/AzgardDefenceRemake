// using System;
// using System.Collections.Generic;
// using System.Text;
// using Sirenix.OdinInspector;
// using Source.Scripts.Core;
// using Source.Scripts.SaveSystem;
// using UnityEngine;
// using UnityEngine.Tilemaps;
//
// namespace MapMaker.Scripts.EntitySettings.Level
// {
//     [Serializable, Toggle("enabled")]
//     public class BuildingTilemapSettings
//     {
//         public bool enabled;
//         public Tilemap tilemap;
//         
//         public void TryLoad(Entity entity)
//         {
//             if (entity.TryGetTilemapField(SavePath.BuildingTilemap.Tilemap, CacheAllTiles(), tilemap, out var loadedTilemap))
//             {
//                 enabled = true;
//                 tilemap = loadedTilemap;
//             }
//             else enabled = false;
//         }
//         
//         public void TrySave(Entity entity)
//         {
//             if (!enabled) return;
//             entity.SetField(SavePath.BuildingTilemap.Tilemap, SerializeTilemap(tilemap));
//         }
//         
//         private Dictionary<string, TileBase> CacheAllTiles()
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
//         
//         private string SerializeTilemap(Tilemap tilemap)
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