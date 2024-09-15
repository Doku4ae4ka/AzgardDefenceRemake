
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace Source.Scripts.Core
{
    public class Pooler 
    {
        public Pooler(EcsWorld ecsWorld)
        {
            Transform = new PoolerModule<EcsData.TransformData>(ecsWorld);
            TilePosition = new PoolerModule<EcsData.TilePosition>(ecsWorld);
            Position = new PoolerModule<EcsData.Position>(ecsWorld);
            Rotation = new  PoolerModule<EcsData.Rotation>(ecsWorld);
            DeadMark = new PoolerModule<EcsData.DeadMark>(ecsWorld);
            
            TowerView = new PoolerModule<EcsData.TowerView>(ecsWorld);
            EnemyView = new PoolerModule<EcsData.EnemyView>(ecsWorld);
            EnvironmentView = new PoolerModule<EcsData.EnvironmentView>(ecsWorld);
            
            TowerPreview = new PoolerModule<EcsData.TowerPreview>(ecsWorld);
            Tower = new PoolerModule<EcsData.Tower>(ecsWorld);
            TowerLevel = new PoolerModule<EcsData.TowerLevel>(ecsWorld);
            Upgradable = new PoolerModule<EcsData.Upgradable>(ecsWorld);
            Target = new PoolerModule<EcsData.Target>(ecsWorld);
            
            Enemy = new PoolerModule<EcsData.Enemy>(ecsWorld);
            Health = new PoolerModule<EcsData.Health>(ecsWorld);
            Movable = new PoolerModule<EcsData.Movable>(ecsWorld);
            Route = new PoolerModule<EcsData.Route>(ecsWorld);
            
            Config = new PoolerModule<EcsData.Config>(ecsWorld);
            Environment = new PoolerModule<EcsData.Environment>(ecsWorld);
            Camera = new PoolerModule<EcsData.Camera>(ecsWorld);
            Level = new PoolerModule<EcsData.Level>(ecsWorld);
            
            Entity = new PoolerModule<EcsData.Entity>(ecsWorld);
            Prototype = new PoolerModule<EcsData.Prototype>(ecsWorld);
            DynamicMark = new PoolerModule<EcsData.DynamicMark>(ecsWorld);
            StaticMark = new PoolerModule<EcsData.StaticMark>(ecsWorld);
            
            BuildValidMark = new PoolerModule<EcsData.BuildValidMark>(ecsWorld);
            BuildingTilemap = new PoolerModule<EcsData.BuildingTileMap>(ecsWorld);
        }

        public readonly PoolerModule<EcsData.TransformData> Transform;
        public readonly PoolerModule<EcsData.TilePosition> TilePosition;
        public readonly PoolerModule<EcsData.Position> Position;
        public readonly PoolerModule<EcsData.Rotation> Rotation;
        public readonly PoolerModule<EcsData.DeadMark> DeadMark;
        
        public readonly PoolerModule<EcsData.TowerView> TowerView;
        public readonly PoolerModule<EcsData.EnemyView> EnemyView;
        public readonly PoolerModule<EcsData.EnvironmentView> EnvironmentView;
        
        public readonly PoolerModule<EcsData.Tower> Tower;
        public readonly PoolerModule<EcsData.TowerPreview> TowerPreview;
        public readonly PoolerModule<EcsData.TowerLevel> TowerLevel;
        public readonly PoolerModule<EcsData.Upgradable> Upgradable;
        public readonly PoolerModule<EcsData.Target> Target;
        
        public readonly PoolerModule<EcsData.Enemy> Enemy;
        public readonly PoolerModule<EcsData.Health> Health;
        public readonly PoolerModule<EcsData.Movable> Movable;
        public readonly PoolerModule<EcsData.Route> Route;
        
        public readonly PoolerModule<EcsData.Config> Config;
        public readonly PoolerModule<EcsData.Environment> Environment;
        public readonly PoolerModule<EcsData.Camera> Camera;
        public readonly PoolerModule<EcsData.Level> Level;
        
        public readonly PoolerModule<EcsData.Entity> Entity;
        public readonly PoolerModule<EcsData.Prototype> Prototype;
        public readonly PoolerModule<EcsData.DynamicMark> DynamicMark;
        public readonly PoolerModule<EcsData.StaticMark> StaticMark;
        
        public readonly PoolerModule<EcsData.BuildValidMark> BuildValidMark;
        public readonly PoolerModule<EcsData.BuildingTileMap> BuildingTilemap;
    }
}