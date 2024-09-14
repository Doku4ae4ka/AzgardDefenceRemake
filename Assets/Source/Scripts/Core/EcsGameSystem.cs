using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.Core
{
    public abstract class EcsGameSystem : EasySystem<Pooler>
    {
        protected GameConfigurations GameConfigurations;
        protected GameStatus GameStatus;
        protected Memory Memory;
        protected GameStarter GameStarter;
        protected EcsWorld.Mask InGameMask => World.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Exc<EcsData.DeadMark>();
        protected EcsWorld.Mask PrototypeMask => World.Filter<EcsData.Entity>().Inc<EcsData.Prototype>();
        protected EcsWorld.Mask AllEntitiesMask => World.Filter<EcsData.Entity>();
        protected Prototypes Prototypes;
        protected Configs Configs;
        protected SpaceHash<EcsData.TransformData, EcsData.Tower> SpaceHash;
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref GameConfigurations);
            gameShare.GetSharedObject(ref GameStatus);
            gameShare.GetSharedObject(ref Memory);
            gameShare.GetSharedObject(ref GameStarter);
            gameShare.GetSharedObject(ref Prototypes);
            gameShare.GetSharedObject(ref Configs);
            gameShare.GetSharedObject(ref SpaceHash);
        }
    }
    
    public abstract class EcsGameSystem<T1> : EcsGameSystem
        where T1 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
    }
    
    public abstract class EcsGameSystem<T1, T2> : EcsGameSystem
        where T1 : struct
        where T2 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
    }
    
    public abstract class EcsGameSystem<T1, T2, T3> : EcsGameSystem
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            SubscribeSignal<T1>(OnSignal);
            SubscribeSignal<T2>(OnSignal);
            SubscribeSignal<T3>(OnSignal);
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            UnsubscribeSignal<T1>(OnSignal);
            UnsubscribeSignal<T2>(OnSignal);
            UnsubscribeSignal<T3>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
    }
}
