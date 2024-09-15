using System.Collections.Generic;

namespace Source.Scripts.SaveSystem
{
    public static class SavePath
    {
        public static class EntityCategory
        {
            public const string Tower = "tower";
            public const string Enemy = "enemy";
            public const string Camera = "camera";
            public const string Level = "level";
            public const string Waves = "waves";
            public const string Prototype = "prototype";
            public const string Config = "config";
            public const string Environment = "environment";
            public const string Trigger = "trigger";
            
            public static readonly string[] All = new string[] 
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
        
        public static class Health
        {
            public const string Max = "health.max";
            public const string Current = "health.current";
        }
        
        public static class Movable
        {
            public const string DistanceToCastle = "movable.distance_to_castle";
            public const string CurrentWaypointIndex = "movable.current_waypoint_index";
            public const string Speed = "movable.speed";
        }
        
        public static readonly string[] AllPathFields = new string[] {
            EntityCategory.Tower,
            EntityCategory.Enemy,
            EntityCategory.Camera,
            EntityCategory.Level,
            EntityCategory.Waves,
            EntityCategory.Environment,
            EntityCategory.Prototype,
            EntityCategory.Config,
            EntityCategory.Trigger,
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
            Movable.Speed,
            Movable.CurrentWaypointIndex,
            Movable.DistanceToCastle,
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
    }
}