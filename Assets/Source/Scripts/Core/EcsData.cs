
using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.MonoBehaviours.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;

namespace Source.Scripts.Core
{
    public static class EcsData
    {
        #region Core
        
        public struct Transform : IGameEcsComponent
        {
            public UnityEngine.Transform Value;
        }
        
        public struct TilePosition : IGameEcsComponent
        {
            public Vector3Int Value;
        }
        
        public struct Position : IGameEcsComponent
        {
            public Vector3 Value;
        }
        
        public struct Rotation : IGameEcsComponent
        {
            public Quaternion Value;
        }
        
        public struct DeadMark : IGameEcsComponent
        {
            
        }
        
        #endregion

        #region Views
        
        public struct TowerView : IGameEcsComponent
        {
            public AssetReference ViewId;
            public TowerViewApi Value;
        }
        
        public struct EnemyView : IGameEcsComponent
        {
            public AssetReference ViewId;
            public EnemyViewApi Value;
        }
        
        public struct EnvironmentView : IGameEcsComponent
        {
            public AssetReference ViewId;
            public EnvironmentViewApi Value;
        }
        
        public struct BuildingTilemapView : IGameEcsComponent
        {
            public AssetReference ViewId;
            public BuildingTilemapViewApi Value;
        }
        
        #endregion

        #region SaveSystem
        
        public struct Entity : IGameEcsComponent
        {
            public string EntityID;
            public string Category;
        }
        
        public struct Prototype : IGameEcsComponent
        {
            public string Category;
            public List<Action<int>> DataBuilder;
        }
        
        /// <summary>
        /// Сущность не статична (персонаж, предмет, игрок)
        /// </summary>
        public struct DynamicMark : IGameEcsComponent
        {
            
        }
        
        /// <summary>
        /// Сущность статична (Environment)
        /// </summary>
        public struct StaticMark : IGameEcsComponent
        {
            
        }
        
        #endregion
        
        public struct TowerPreview : IGameEcsComponent
        {
            public Tilemap Tilemap;
            public Dictionary<string, TileBase> CachedTiles;
        }
        
        public struct BuildValidMark : IGameEcsComponent
        {
            
        }
        
        public struct BuildingTileMap : IGameEcsComponent
        {
            public Tilemap Value;
            public List<KeyValuePair<Vector3Int, TileBase>> RawValue;
            public Dictionary<string, TileBase> CachedTiles;
        }

        public struct Health : IGameEcsComponent
        {
            public int Max;
            public float Current;
        }

        public struct Movable : IGameEcsComponent
        {
            
        }

        public struct Config : IGameEcsComponent
        {
            
        }

        public struct Tower : IGameEcsComponent
        {
            
        }

        public struct Enemy : IGameEcsComponent
        {
            
        }

        public struct Camera : IGameEcsComponent
        {
            
        }

        public struct Environment : IGameEcsComponent
        {
            
        }
        
        public struct Level : IGameEcsComponent
        {
            
        }
        
    }

    public interface IGameEcsComponent : IEcsComponent
    {
        
    }
}