using System;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace MapMaker.Scripts.EntitySettings.Level
{
    [Serializable, Toggle("enabled")]
    public class BuildingTilemapViewSettings
    {
        public bool enabled;
        public AssetReference viewPath;
        public Tilemap tilemap;
        [SerializeField, HideInInspector] private GameObject spawnedView;

        public void TryLoadView(Entity entity, Transform transform)
        {
            if (entity.TryGetField(SavePath.View.BuildingTilemap, out var viewField))
            {
                enabled = true;
                viewPath = viewField.ParseToAssetReference();
                if (entity.TryGetField(SavePath.WorldSpace.Position, out var positionField))
                {
                    transform.position = positionField.ParseVector3();
                }
                
                if (entity.TryGetField(SavePath.WorldSpace.Rotation, out var rotationField))
                {
                    transform.rotation = rotationField.ParseQuaternion();
                }

                LoadView(transform);
                
                if (entity.TryGetTileEntriesField(SavePath.BuildingTilemap.Tilemap, CacheAllTiles(), out var loadedList))
                {
                    enabled = true;
                    tilemap.FillTilemap(loadedList);
                }
            }
            
            else enabled = false;
        }

        public void TrySaveView(Entity entity, Transform transform)
        {
            if (!enabled) return;
            entity.SetField(SavePath.BuildingTilemap.Tilemap, SerializeTilemap(tilemap));
            entity.SetField(SavePath.View.BuildingTilemap, viewPath.AssetGUID);
            entity.SetField(SavePath.WorldSpace.Position, $"{transform.position}");
            if ("(0.00000, 0.00000, 0.00000, 1.00000)" != transform.rotation.ToString())
            {
                entity.SetField(SavePath.WorldSpace.Rotation, $"{transform.rotation}");
            }
        }

        public void Validate(Transform transform)
        {
            if (spawnedView != null) Object.DestroyImmediate(spawnedView);
            LoadView(transform);
        }

        private void LoadView(Transform parent)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(viewPath);
            handle.WaitForCompletion();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var prefab = handle.Result;
                spawnedView = Object.Instantiate(prefab, parent, true);
                spawnedView.transform.localRotation = Quaternion.identity;
                spawnedView.transform.localPosition = Vector3.zero;
                tilemap = spawnedView.GetComponentInChildren<Tilemap>();
            }
            else
            {
                Debug.LogError($"Failed to load asset with address: {viewPath}");
            }
        }
        
        private Dictionary<string, TileBase> CacheAllTiles()
        {
            var dict = new Dictionary<string, TileBase>();
            
            var exclude = Resources.Load<TileBase>(Constants.Resources.ExcludeTiles.Exclude);
            var empty = Resources.Load<TileBase>(Constants.Resources.ExcludeTiles.Empty);

            if (exclude != null) dict.TryAdd("PurpleExclusion", exclude);
            else Debug.LogError($"Tile '{Constants.Resources.ExcludeTiles.Exclude}' not found in Resources.");

            if (empty != null) dict.TryAdd("CyanEmpty", empty);
            else Debug.LogError($"Tile '{Constants.Resources.ExcludeTiles.Empty}' not found in Resources.");

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