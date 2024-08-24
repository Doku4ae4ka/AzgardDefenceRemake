
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace Source.Scripts.Core
{
    public class Pooler 
    {
        public Pooler(EcsWorld ecsWorld)
        {
            Transform = new PoolerModule<EcsData.Transform>(ecsWorld);
            Entity = new PoolerModule<EcsData.Entity>(ecsWorld);
            View = new PoolerModule<EcsData.View>(ecsWorld);
            Prototype = new PoolerModule<EcsData.Prototype>(ecsWorld);
            DeadMark = new PoolerModule<EcsData.DeadMark>(ecsWorld);
            Movable = new PoolerModule<EcsData.Movable>(ecsWorld);
            Config = new PoolerModule<EcsData.Config>(ecsWorld);
            Health = new PoolerModule<EcsData.Health>(ecsWorld);
            PlayerInfo = new PoolerModule<EcsData.PlayerInfo>(ecsWorld);
            Environment = new PoolerModule<EcsData.Environment>(ecsWorld);
            Tower = new PoolerModule<EcsData.Tower>(ecsWorld);
            Enemy = new PoolerModule<EcsData.Enemy>(ecsWorld);
            DynamicMark = new PoolerModule<EcsData.DynamicMark>(ecsWorld);
            StaticMark = new PoolerModule<EcsData.StaticMark>(ecsWorld);
            GlobalMark = new PoolerModule<EcsData.GlobalMark>(ecsWorld);
        }

        public readonly PoolerModule<EcsData.Transform> Transform;
        public readonly PoolerModule<EcsData.Entity> Entity;
        public readonly PoolerModule<EcsData.View> View;
        public readonly PoolerModule<EcsData.Prototype> Prototype;
        public readonly PoolerModule<EcsData.DeadMark> DeadMark;
        public readonly PoolerModule<EcsData.Movable> Movable;
        public readonly PoolerModule<EcsData.Config> Config;
        public readonly PoolerModule<EcsData.Health> Health;
        public readonly PoolerModule<EcsData.PlayerInfo> PlayerInfo;
        public readonly PoolerModule<EcsData.Environment> Environment;
        public readonly PoolerModule<EcsData.Tower> Tower;
        public readonly PoolerModule<EcsData.Enemy> Enemy;
        public readonly PoolerModule<EcsData.DynamicMark> DynamicMark;
        public readonly PoolerModule<EcsData.StaticMark> StaticMark;
        public readonly PoolerModule<EcsData.GlobalMark> GlobalMark;
    }
}