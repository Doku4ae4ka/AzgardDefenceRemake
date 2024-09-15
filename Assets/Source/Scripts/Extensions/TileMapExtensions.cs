using System.Collections.Generic;
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
        
    }
}