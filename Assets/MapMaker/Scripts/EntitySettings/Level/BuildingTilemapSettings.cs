using System;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MapMaker.Scripts.EntitySettings.Level
{
    [Serializable, Toggle("enabled")]
    public class BuildingTilemapSettings
    {
        public bool enabled;
        public Tilemap tilemap;
        
        public void TryLoad(Entity entity)
        {
            if (entity.TryGetTileEntriesField(SavePath.BuildingTilemap.Tilemap, CacheAllTiles(), out var loadedList))
            {
                enabled = true;
                tilemap = InstantiateTilemapGameObject();
                tilemap.FillTilemap(loadedList);
                
            }
            else enabled = false;
        }
        
        public void TrySave(Entity entity)
        {
            if (!enabled) return;
            entity.SetField(SavePath.BuildingTilemap.Tilemap, SerializeTilemap(tilemap));
        }
        
        private static Tilemap InstantiateTilemapGameObject(string gridName = "TowerGrid", string tilemapName = "BuildingTilemap")
        {
            GameObject gridObject = new GameObject(gridName);
            gridObject.AddComponent<Grid>();
            
            GameObject tilemapObject = new GameObject(tilemapName);
            tilemapObject.transform.parent = gridObject.transform;
            tilemapObject.transform.position = new Vector3(-0.5f, -0.5f, 0);
            
            Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
            TilemapRenderer tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
            tilemapRenderer.enabled = false;
            tilemapRenderer.sortingOrder = 4;
            
            return tilemap;
        }
        
        private Dictionary<string, TileBase> CacheAllTiles()
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
        
        private string SerializeTilemap(Tilemap tilemap)
        {
            var sb = new StringBuilder();

            BoundsInt bounds = tilemap.cellBounds;
            foreach (var position in bounds.allPositionsWithin)
            {
                TileBase tile = tilemap.GetTile(position);
                if (tile != null) sb.Append($"({position.x},{position.y},{tile.name});");
            }

            return sb.ToString();
        }
    }
}