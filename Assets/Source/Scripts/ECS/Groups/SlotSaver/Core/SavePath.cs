﻿namespace Source.Scripts.ECS.Groups.SlotSaver.Core
{
    public static class SavePath
    {
        public static readonly string[] AllPathFields =
        {
            EntityType.Tower,
            EntityType.Enemy,
            EntityType.Camera,
            EntityType.Level,
            EntityType.Waves,
            EntityType.Environment,
            EntityType.Prototype,
            EntityType.Config,
            EntityType.Trigger,
            Config.ID,
            Config.FreeEntityID,
            Config.MapBounds,
            Camera.ID,
            Prototype.Category,
            View.Tower,
            View.Enemy,
            View.Environment,
            View.BuildingTilemap,
            WorldSpace.Position,
            WorldSpace.Rotation,
            Health.Max,
            Health.Current,
            Movement.Position,
            Movement.Direction,
            PathFollower.CurrentWaypointIndex,
            PathFollower.DistanceToCastle,
            Tower.BaseCost,
            Tower.Damage,
            Tower.AttackSpeed,
            Tower.Radius,
            Tower.EnemyType,
            Tower.TargetingType,
            TowerLevel.Level,
            TowerLevel.Cost,
            BuildingTilemap.Tilemap
        };
        
        

        public static class EntityType
        {
            public const string Tower = "tower";
            public const string Enemy = "enemy";
            public const string Camera = "camera";
            public const string Level = "level";
            public const string Waves = "waves";
            public const string Prototype = "prototype";
            public const string Config = "main_config";
            public const string Environment = "environment";
            public const string Trigger = "trigger";

            public static readonly string[] All =
                { Tower, Enemy, Camera, Level, Waves, Environment, Prototype, Config, Trigger };
        }

        public static class Config
        {
            public const string ID = "configs";
            public const string FreeEntityID = "config.free_entity_id";

            public const string MapBounds = "config.map_borders";
            public const string Routes = "config.routes";
        }

        public static class Camera
        {
            public const string ID = "camera";
        }

        public static class Level
        {
            public const string ID = "level";
        }

        public static class Prototype
        {
            public const string Category = "prototype.category";
            public const string Type = "prototype.type";
        }

        public static class BuildingTilemap
        {
            public const string Tilemap = "building_tilemap";
        }

        public static class View
        {
            public const string Tower = "view.tower";
            public const string Enemy = "view.enemy";
            public const string Environment = "view.environment";
            public const string BuildingTilemap = "view.building_tilemap";
        }

        public static class WorldSpace
        {
            public const string Position = "worldspace.position";
            public const string Rotation = "worldspace.rotation";
        }

        public static class Tower
        {
            public const string BaseCost = "tower.base_cost";
            public const string Damage = "tower.damage";
            public const string AttackSpeed = "tower.attack_speed";
            public const string Radius = "tower.radius";
            public const string EnemyType = "tower.enemy_type";
            public const string TargetingType = "tower.targeting_type";
        }

        public static class TowerLevel
        {
            public const string Level = "tower.level";
            public const string Cost = "tower.cost";
        }

        public static class TowerType
        {
            public const string Archer = "tower.archer";
            public const string Wind = "tower.wind";
            public const string Gun = "tower.gun";
        } 
        
        public static class Enemy
        {
            public const string DamageToCastle = "enemy.damage_to_castle";
            public const string EnemyType = "enemy.enemy_type";
        }

        public static class Health
        {
            public const string Max = "health.max";
            public const string Current = "health.current";
        }

        public static class PathFollower
        {
            public const string DistanceToCastle = "path_follower.distance_to_castle";
            public const string CurrentWaypointIndex = "path_follower.current_waypoint_index";
            public const string Speed = "path_follower.speed";
        }

        public static class Movement
        {
            public const string Position = "movement.position";
            public const string Direction = "movement.direction";
        }
    }
}