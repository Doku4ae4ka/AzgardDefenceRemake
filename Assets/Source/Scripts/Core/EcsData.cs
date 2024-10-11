
using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.ECS.Groups.Enemies.MonoBehaviours;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.ECS.Groups.Towers.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;

namespace Source.Scripts.Core
{
    public static class EcsData
    {
        #region Core
        
        public struct TransformData : IGameEcsComponent, ISpaceHashTransform
        {
            public Transform Value;
            public Transform Transform => Value;
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
        
        #endregion

        #region SaveSystem
        
        public struct Entity : IGameEcsComponent
        {
            public string EntityID;
            public SlotCategory Category;
            public string Type;
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
        
        public struct BuildValidMark : IGameEcsComponent
        {
            
        }
        
        public struct BuildingTileMap : IGameEcsComponent
        {
            public Tilemap Value;
            public List<KeyValuePair<Vector3Int, TileBase>> RawValue;
            public Dictionary<string, TileBase> CachedTiles;
        }

        public struct Config : IGameEcsComponent
        {
            
        }

        #region TowerData
        
        public struct TowerPreview : IGameEcsComponent
        {
            public Tilemap Tilemap;
            public Dictionary<string, TileBase> CachedTiles;
        }
        
        public struct Tower : IGameEcsComponent
        {
            public int BaseCost;
            public float Damage;
            public float AttackSpeed;
            public float Radius;
            public EnemyType EnemyType;
            public TargetingType TargetingType;
        }
        
        public struct TowerLevel : IGameEcsComponent
        {
            public int Value;
            public int Cost;
        }
        
        public struct Upgradable : IGameEcsComponent
        {
            public string[] Variants;
            public int Cost;
        }
        
        public struct Target : IGameEcsComponent
        {
            public bool HasTarget;
            public int TargetEntity;
            public float TickRemaining;
        }
        
        #endregion

        #region EnemyData
        
        public struct Enemy : IGameEcsComponent
        {
            public int DamageToCastle;
            public EnemyType EnemyType;
        }
        
        public struct Health : IGameEcsComponent
        {
            public int Max;
            public float Current;
        }

        public struct Movable : IGameEcsComponent
        {
            public int CurrentWaypointIndex; 
            public float PassedDistance;
            public float Speed;
            public Vector2 LastDirection;
        }

        #endregion

        #region PathSystem
        
        public struct Route : IGameEcsComponent
        {
            public int RouteId;
            public List<Vector2> Waypoints;
        }

        #endregion

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

    public enum EnemyType
    {
        Ground,
        Air,
        Both
    }
    
    public enum TargetingType
    {
        Closest,
        Weakest,
        Random
    }
    
    

    public interface IGameEcsComponent : IEcsComponent
    {
        
    }
}