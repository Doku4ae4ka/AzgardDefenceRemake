using System;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MapMaker.Scripts.EntitySettings.Configs
{
    [Serializable, Toggle("enabled")]
    public class RoutesSettings
    {
        public bool enabled;
        public Tilemap tilemap;

        public void TryLoad(Entity entity)
        {
            if (entity.TryGetRoutesField(SavePath.Config.Routes, out var dictionary))
            {
                tilemap = GameObject.Find("PathTilemap").GetComponent<Tilemap>();
                enabled = true;
            }
            else enabled = false;
            
        }
         
        public void TrySave(Entity entity)
        {            
            if (!enabled) return;
            entity.SetField(SavePath.Config.Routes, SerializePaths(ParsePathsTilemap(tilemap)));
        }
        
        public static List<(int id, List<Vector2> waypoints)> ParsePathsTilemap(Tilemap pathsTilemap)
        {
            var pathData = new List<(int id, List<Vector2> waypoints)>();
            var entryPoints = GetEntryPoints(pathsTilemap);
            var visited = new Dictionary<Vector3Int, bool>();

            int pathId = 0;

            foreach (var entryPoint in entryPoints)
            {
                var waypoints = GetPathWaypoints(pathsTilemap, entryPoint, visited);
                if (waypoints.Count > 0)
                {
                    pathData.Add((pathId++, waypoints));
                }
                else
                {
                    Debug.LogWarning($"Не удалось найти выход для маршрута с входной точкой {entryPoint}");
                }
            }

            return pathData;
        }

        private static List<Vector3Int> GetEntryPoints(Tilemap pathsTilemap)
        {
            var entryPoints = new List<Vector3Int>();

            foreach (var position in pathsTilemap.cellBounds.allPositionsWithin)
            {
                var tile = pathsTilemap.GetTile(position);
                if (tile != null && tile.name == Constants.Tiles.Portal)
                {
                    entryPoints.Add(position);
                }
            }

            return entryPoints;
        }

        private static List<Vector2> GetPathWaypoints(Tilemap pathsTilemap, Vector3Int startPosition, Dictionary<Vector3Int, bool> visited)
        {
            var waypoints = new List<Vector2>();
            var currentPosition = startPosition;
            
            var cellCenterOffset = new Vector3(pathsTilemap.cellSize.x / 2f, pathsTilemap.cellSize.y / 2f, 0);
            
            waypoints.Add(pathsTilemap.CellToWorld(currentPosition) + cellCenterOffset);
            visited.TryAdd(currentPosition, true);

            while (true)
            {
                var nextPosition = GetNextWaypoint(pathsTilemap, currentPosition, visited);
                var nextTile = pathsTilemap.GetTile(nextPosition);

                if (nextTile != null && nextTile.name == Constants.Tiles.Castle)
                {
                    waypoints.Add(pathsTilemap.CellToWorld(nextPosition) + cellCenterOffset);
                    break;
                }

                if (nextTile != null && nextTile.name == Constants.Tiles.Road)
                {
                    waypoints.Add(pathsTilemap.CellToWorld(nextPosition) + cellCenterOffset);
                    visited.TryAdd(nextPosition, true);
                    currentPosition = nextPosition;
                }
                else
                {
                    Debug.LogWarning("Не удалось найти следующий путь");
                    break;
                }
            }

            return waypoints;
        }

        private static Vector3Int GetNextWaypoint(Tilemap tilemap, Vector3Int currentPosition, Dictionary<Vector3Int, bool> visited)
        {
            var directions = new Vector3Int[]
            {
                Vector3Int.up,
                Vector3Int.down,
                Vector3Int.left,
                Vector3Int.right,
            };

            foreach (var direction in directions)
            {
                var nextPosition = currentPosition + direction;
                var nextTile = tilemap.GetTile(nextPosition);
                
                if (nextTile != null && nextTile.name != Constants.Tiles.Portal && !visited.ContainsKey(nextPosition))
                {
                    return nextPosition;
                }
            }
            return Vector3Int.zero;
        }
        
        private string SerializePaths(List<(int, List<Vector2>)> data)
        {
            StringBuilder serializedData = new StringBuilder();

            foreach (var entry in data)
            {
                serializedData.Append(entry.Item1);
                serializedData.Append(":");

                for (int i = 0; i < entry.Item2.Count; i++)
                {
                    var point = entry.Item2[i];
                    serializedData.Append($"({point.x},{point.y})");

                    if (i < entry.Item2.Count - 1)
                    {
                        serializedData.Append("/");
                    }
                }

                serializedData.Append(";");
            }

            return serializedData.ToString();
        }
        
        
        // private static Tilemap InstantiateTilemapGameObject(string gridName = "RoutesGrid", string tilemapName = "RoutesTilemap")
        // {
        //     GameObject gridObject = new GameObject(gridName);
        //     gridObject.AddComponent<Grid>();
        //     
        //     GameObject tilemapObject = new GameObject(tilemapName);
        //     tilemapObject.transform.parent = gridObject.transform;
        //     
        //     Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
        //     TilemapRenderer tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
        //     tilemapRenderer.enabled = false;
        //     tilemapRenderer.sortingOrder = 4;
        //     
        //     return tilemap;
        // }
        
        private Dictionary<string, TileBase> CacheAllTiles()
        {
            var dict = new Dictionary<string, TileBase>();
            
            var portal = Resources.Load<TileBase>(Constants.Resources.TilePaths.Portal);
            var castle = Resources.Load<TileBase>(Constants.Resources.TilePaths.Castle);
            var road = Resources.Load<TileBase>(Constants.Resources.TilePaths.Road);

            if (portal != null) dict.TryAdd(Constants.Tiles.Portal, portal);
            else Debug.LogError($"Tile '{Constants.Resources.TilePaths.Portal}' not found in Resources.");

            if (castle != null) dict.TryAdd(Constants.Tiles.Castle, castle);
            else Debug.LogError($"Tile '{Constants.Resources.TilePaths.Castle}' not found in Resources.");
            
            if (road != null) dict.TryAdd(Constants.Tiles.Road, road);
            else Debug.LogError($"Tile '{Constants.Resources.TilePaths.Road}' not found in Resources.");

            return dict;
        }

    }
}