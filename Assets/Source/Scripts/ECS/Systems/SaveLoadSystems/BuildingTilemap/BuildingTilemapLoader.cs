using System.Collections.Generic;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.BuildingTilemap
{
    public static class BuildingTilemapLoader
    {
        public static void TryLoadDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Inc<EcsData.Level>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                ref var tilemapData = ref pooler.BuildingTilemap.Add(entity);
                tilemapData.CachedTiles = CacheAllTiles();
                if (savingEntity.TryGetTileEntriesField(SavePath.BuildingTilemap.Tilemap, tilemapData.CachedTiles, out var loadedList))
                {
                    tilemapData.RawValue = loadedList;
                }

                tilemapData.Value = InstantiateTilemapGameObject();
                tilemapData.Value.FillTilemap(tilemapData.RawValue);
                
            }
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
        
        private static Dictionary<string, TileBase> CacheAllTiles()
        {
            var dict = new Dictionary<string, TileBase>();
            
            var exclude = Resources.Load<TileBase>(Constants.Resources.Tiles.Exclude);
            var empty = Resources.Load<TileBase>(Constants.Resources.Tiles.Empty);
            
            if (exclude != null) dict.TryAdd("PurpleExclusion", exclude);
            else Debug.LogError($"Tile '{Constants.Resources.Tiles.Exclude}' not found in Resources.");

            if (empty != null) dict.TryAdd("CyanEmpty", empty);
            else Debug.LogError($"Tile '{Constants.Resources.Tiles.Empty}' not found in Resources.");

            return dict;
        }
    }
}