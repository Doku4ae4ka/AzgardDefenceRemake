
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace Source.Scripts.Core
{
    public class Pooler 
    {
        public Pooler(EcsWorld ecsWorld)
        {
            Transform = new PoolerModule<EcsData.Transform>(ecsWorld);
            Position = new PoolerModule<EcsData.Position>(ecsWorld);
            Rotation = new  PoolerModule<EcsData.Rotation>(ecsWorld);
            DeadMark = new PoolerModule<EcsData.DeadMark>(ecsWorld);
            
            TowerView = new PoolerModule<EcsData.TowerView>(ecsWorld);
            EnemyView = new PoolerModule<EcsData.EnemyView>(ecsWorld);
            EnvironmentView = new PoolerModule<EcsData.EnvironmentView>(ecsWorld);
            
            Config = new PoolerModule<EcsData.Config>(ecsWorld);
            Tower = new PoolerModule<EcsData.Tower>(ecsWorld);
            Enemy = new PoolerModule<EcsData.Enemy>(ecsWorld);
            Environment = new PoolerModule<EcsData.Environment>(ecsWorld);
            Camera = new PoolerModule<EcsData.Camera>(ecsWorld);
            
            Entity = new PoolerModule<EcsData.Entity>(ecsWorld);
            Prototype = new PoolerModule<EcsData.Prototype>(ecsWorld);
            DynamicMark = new PoolerModule<EcsData.DynamicMark>(ecsWorld);
            StaticMark = new PoolerModule<EcsData.StaticMark>(ecsWorld);
            
            Health = new PoolerModule<EcsData.Health>(ecsWorld);
            Movable = new PoolerModule<EcsData.Movable>(ecsWorld);
        }

        public readonly PoolerModule<EcsData.Transform> Transform;
        public readonly PoolerModule<EcsData.Position> Position;
        public readonly PoolerModule<EcsData.Rotation> Rotation;
        public readonly PoolerModule<EcsData.DeadMark> DeadMark;
        
        public readonly PoolerModule<EcsData.TowerView> TowerView;
        public readonly PoolerModule<EcsData.EnemyView> EnemyView;
        public readonly PoolerModule<EcsData.EnvironmentView> EnvironmentView;
        
        public readonly PoolerModule<EcsData.Config> Config;
        public readonly PoolerModule<EcsData.Tower> Tower;
        public readonly PoolerModule<EcsData.Enemy> Enemy;
        public readonly PoolerModule<EcsData.Environment> Environment;
        public readonly PoolerModule<EcsData.Camera> Camera;
        
        public readonly PoolerModule<EcsData.Entity> Entity;
        public readonly PoolerModule<EcsData.Prototype> Prototype;
        public readonly PoolerModule<EcsData.DynamicMark> DynamicMark;
        public readonly PoolerModule<EcsData.StaticMark> StaticMark;
        
        public readonly PoolerModule<EcsData.Health> Health;
        public readonly PoolerModule<EcsData.Movable> Movable;
    }
}