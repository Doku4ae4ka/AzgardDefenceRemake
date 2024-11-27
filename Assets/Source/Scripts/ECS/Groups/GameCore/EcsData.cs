using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.ECS.Groups.Enemies.MonoBehaviours;
using Source.Scripts.ECS.Groups.Towers.MonoBehaviours;
using Source.Scripts.MonoBehaviours.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;

namespace Source.Scripts.ECS.Groups.GameCore
{
    public static class EcsData
    {
        #region Views
        
        public struct TowerView : IEcsComponent
        {
            public AssetReference ViewId;
            public TowerViewApi Value;
        }
        
        public struct EnemyView : IEcsComponent
        {
            public AssetReference ViewId;
            public EnemyViewApi Value;
        }
        
        public struct EnvironmentView : IEcsComponent
        {
            public AssetReference ViewId;
            public EnvironmentViewApi Value;
        }
        
        public struct TilePosition : IEcsComponent
        {
            public Vector3Int Value;
        }
        
        #endregion
        
        public struct BuildValidMark : IEcsComponent
        {
            
        }
        
        public struct BuildingTileMap : IEcsComponent
        {
            public Tilemap Value;
            public List<KeyValuePair<Vector3Int, TileBase>> RawValue;
            public Dictionary<string, TileBase> CachedTiles;
        }

        public struct Config : IEcsComponent
        {
            
        }

        #region TowerData
        
        public struct TowerPreview : IEcsComponent
        {
            public Tilemap Tilemap;
            public Dictionary<string, TileBase> CachedTiles;
        }
        
        public struct TowerMark : IEcsComponent { }
        
        public struct Attacker : IEcsComponent
        {
            public float Damage;
            public float AttackSpeed;
            public float Radius;
            public EnemyType EnemyType;
            public TargetingType TargetingType;
        }
        
        public struct TowerLevel : IEcsComponent
        {
            public int Value;
        }
        
        public struct TowerPrices : IEcsComponent
        {
            public int BaseCost;
            public int CurrentCost;
            public int LevelUpCost;
            public int UpgradeCost;
        }
        
        public struct Upgradable : IEcsComponent
        {
            public string[] Variants;
        }
        
        public struct Target : IEcsComponent
        {
            public bool HasTarget;
            public int TargetEntity;
            public float TickRemaining;
        }
        
        #endregion

        #region EnemyData
        
        public struct Enemy : IEcsComponent
        {
            public int DamageToCastle;
            public EnemyType EnemyType;
        }

        #endregion

        #region PathSystem
        
        public struct Route : IEcsComponent
        {
            public int RouteId;
            public List<Vector2> Waypoints;
        }
        
        public struct PathFollower : IEcsComponent
        {
            public int CurrentWaypointIndex; 
            public float PassedDistance;
            public Vector2 LastDirection;
        }

        #endregion

        public struct Camera : IEcsComponent
        {
            
        }

        public struct Environment : IEcsComponent
        {
            
        }
        
        public struct Level : IEcsComponent
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
}