using System.Collections.Generic;
using System.Text;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Source.Scripts.Extensions
{
    public static class TileMapExtensions
    {
        public static Vector3Int GetMouseOnGridPos(this Tilemap tilemap)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var mouseCellPos = tilemap.WorldToCell(mousePos);
            mouseCellPos.z = 0;
 
            return mouseCellPos;
        }
        
        public static void FillTilemap(this Tilemap tilemap, List<KeyValuePair<Vector3Int, TileBase>> tileEntries)
        {
            tilemap.ClearAllTiles();

            foreach (var tileEntry in tileEntries)
            {
                Vector3Int position = tileEntry.Key;
                TileBase tile = tileEntry.Value;

                tilemap.SetTile(position, tile);
            }
        }
        
        public static string SerializeTilemap(this Tilemap tilemap)
        {
            var sb = new StringBuilder();

            var bounds = tilemap.cellBounds;
            foreach (var position in bounds.allPositionsWithin)
            {
                TileBase tile = tilemap.GetTile(position);
                if (tile != null) sb.Append($"({position.x},{position.y},{tile.name});");
            }

            return sb.ToString();
        }
        
        public static Dictionary<string, TileBase> CacheAllTiles()
        {
            var dict = new Dictionary<string, TileBase>();
            
            var exclude = Resources.Load<TileBase>(Constants.Resources.TilePaths.Exclude);
            var empty = Resources.Load<TileBase>(Constants.Resources.TilePaths.Empty);

            if (exclude != null) dict.TryAdd(Constants.Tiles.Exclude, exclude);
            else Debug.LogError($"Tile '{Constants.Resources.TilePaths.Exclude}' not found in Resources.");

            if (empty != null) dict.TryAdd(Constants.Tiles.Empty, empty);
            else Debug.LogError($"Tile '{Constants.Resources.TilePaths.Empty}' not found in Resources.");

            return dict;
        }
        
    }
}