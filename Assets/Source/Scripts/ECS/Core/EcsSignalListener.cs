using Exerussus._1EasyEcs.Scripts.Core;

namespace Source.Scripts.ECS.Core
{
    public abstract class EcsListener<T1> : EcsSignalListener<Pooler, T1>
        where T1 : struct
    {
        protected GameConfiguration Configuration;
        
        public override void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Configuration);
        }
    }
    
    public abstract class EcsListener<T1, T2> : EcsSignalListener<Pooler, T1, T2> 
        where T1 : struct 
        where T2 : struct 
    {
        protected GameConfiguration Configuration;
        
        public override void PreInit(GameShare gameShare, float tickTime,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Configuration);
        }
    }

    public abstract class EcsListener<T1, T2, T3> : EcsSignalListener<Pooler, T1, T2, T3>
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        protected GameConfiguration Configuration;
        
        public override void PreInit(GameShare gameShare, float tickTime,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Configuration);
        }
    }

    public abstract class EcsListener<T1, T2, T3, T4> : EcsSignalListener<Pooler, T1, T2, T3, T4>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        protected GameConfiguration Configuration;
        
        public override void PreInit(GameShare gameShare, float tickTime,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Configuration);
        }
    }

    public abstract class EcsListener<T1, T2, T3, T4, T5> : EcsSignalListener<Pooler, T1, T2, T3, T4, T5>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        protected GameConfiguration Configuration;
        
        public override void PreInit(GameShare gameShare, float tickTime,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Configuration);
        }
    }

    public abstract class EcsListener<T1, T2, T3, T4, T5, T6> : EcsSignalListener<Pooler, T1, T2, T3, T4, T5, T6>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        protected GameConfiguration Configuration;
        
        public override void PreInit(GameShare gameShare, float tickTime,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Configuration);
        }
    }
}