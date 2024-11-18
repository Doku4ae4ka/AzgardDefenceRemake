using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MapMaker.Scripts.EntitySettings.Level
{
    [Serializable, Toggle("enabled")]
    public class BuildingTilemapSettings
    {
        public bool enabled;
        public Tilemap tilemap;
        
        public void TryLoad(SlotEntity slotEntity)
        {
            if (slotEntity.TryGetTileEntriesField(SavePath.BuildingTilemap.Tilemap, TileMapExtensions.CacheAllTiles(), out var loadedList))
            {
                enabled = true;
                tilemap = InstantiateTilemapGameObject();
                tilemap.FillTilemap(loadedList);
                
            }
            else enabled = false;
        }
        
        public void TrySave(SlotEntity slotEntity)
        {
            if (!enabled) return;
            slotEntity.SetField(SavePath.BuildingTilemap.Tilemap, tilemap.SerializeTilemap());
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
        
    }
}