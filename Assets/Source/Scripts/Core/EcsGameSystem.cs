
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.ProjectLibraries;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.Core
{
    public abstract class EcsGameSystem : EasySystem<Pooler>
    {
        protected GameConfigurations GameConfigurations;
        protected Libraries Libraries;
        protected GameStatus GameStatus;
        protected Memory Memory;
        protected EcsWorld.Mask InGameMask => World.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Exc<EcsData.DeadMark>();
        protected EcsWorld.Mask PrototypeMask => World.Filter<EcsData.Entity>().Inc<EcsData.Prototype>();
        protected EcsWorld.Mask AllEntitiesMask => World.Filter<EcsData.Entity>();
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref GameConfigurations);
            gameShare.GetSharedObject(ref Libraries);
            gameShare.GetSharedObject(ref GameStatus);
            gameShare.GetSharedObject(ref Memory);
        }
    }
    
    public abstract class EcsGameSystem<T1> : EcsSignalListener<Pooler, T1>
        where T1 : struct
    {
        protected GameConfigurations GameConfigurations;
        protected Libraries Libraries;
        protected GameStatus GameStatus;
        protected Memory Memory;
        protected EcsWorld.Mask InGameMask => World.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Exc<EcsData.DeadMark>();
        protected EcsWorld.Mask PrototypeMask => World.Filter<EcsData.Entity>().Inc<EcsData.Prototype>();
        protected EcsWorld.Mask AllEntitiesMask => World.Filter<EcsData.Entity>();
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref GameConfigurations);
            gameShare.GetSharedObject(ref Libraries);
            gameShare.GetSharedObject(ref GameStatus);
            gameShare.GetSharedObject(ref Memory);
        }
    }
    
    public abstract class EcsGameSystem<T1, T2> : EcsSignalListener<Pooler, T1, T2>
        where T1 : struct
        where T2 : struct
    {
        protected GameConfigurations GameConfigurations;
        protected Libraries Libraries;
        protected GameStatus GameStatus;
        protected Memory Memory;
        protected EcsWorld.Mask InGameMask => World.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Exc<EcsData.DeadMark>();
        protected EcsWorld.Mask PrototypeMask => World.Filter<EcsData.Entity>().Inc<EcsData.Prototype>();
        protected EcsWorld.Mask AllEntitiesMask => World.Filter<EcsData.Entity>();
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref GameConfigurations);
            gameShare.GetSharedObject(ref Libraries);
            gameShare.GetSharedObject(ref GameStatus);
            gameShare.GetSharedObject(ref Memory);
        }
    }
    
    public abstract class EcsGameSystem<T1, T2, T3> : EcsSignalListener<Pooler, T1, T2, T3>
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        protected GameConfigurations GameConfigurations;
        protected Libraries Libraries;
        protected GameStatus GameStatus;
        protected Memory Memory;
        protected EcsWorld.Mask InGameMask => World.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Exc<EcsData.DeadMark>();
        protected EcsWorld.Mask PrototypeMask => World.Filter<EcsData.Entity>().Inc<EcsData.Prototype>();
        protected EcsWorld.Mask AllEntitiesMask => World.Filter<EcsData.Entity>();
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref GameConfigurations);
            gameShare.GetSharedObject(ref Libraries);
            gameShare.GetSharedObject(ref GameStatus);
            gameShare.GetSharedObject(ref Memory);
        }
    }
    
    public abstract class EcsGameSystem<T1, T2, T3, T4> : EcsSignalListener<Pooler, T1, T2, T3, T4>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        protected GameConfigurations GameConfigurations;
        protected Libraries Libraries;
        protected GameStatus GameStatus;
        protected Memory Memory;
        protected EcsWorld.Mask InGameMask => World.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Exc<EcsData.DeadMark>();
        protected EcsWorld.Mask PrototypeMask => World.Filter<EcsData.Entity>().Inc<EcsData.Prototype>();
        protected EcsWorld.Mask AllEntitiesMask => World.Filter<EcsData.Entity>();
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref GameConfigurations);
            gameShare.GetSharedObject(ref Libraries);
            gameShare.GetSharedObject(ref GameStatus);
            gameShare.GetSharedObject(ref Memory);
        }
    }
    
    public abstract class EcsGameSystem<T1, T2, T3, T4, T5> : EcsSignalListener<Pooler, T1, T2, T3, T4, T5>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        protected GameConfigurations GameConfigurations;
        protected Libraries Libraries;
        protected GameStatus GameStatus;
        protected Memory Memory;
        protected EcsWorld.Mask InGameMask => World.Filter<EcsData.Entity>().Exc<EcsData.Prototype>().Exc<EcsData.DeadMark>();
        protected EcsWorld.Mask PrototypeMask => World.Filter<EcsData.Entity>().Inc<EcsData.Prototype>();
        protected EcsWorld.Mask AllEntitiesMask => World.Filter<EcsData.Entity>();
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref GameConfigurations);
            gameShare.GetSharedObject(ref Libraries);
            gameShare.GetSharedObject(ref GameStatus);
            gameShare.GetSharedObject(ref Memory);
        }
    }
}
