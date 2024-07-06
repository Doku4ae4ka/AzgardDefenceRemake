
using System;
using Source.Scripts.Data.GamesConfigurations;
using Source.SignalSystem;
using UnityEngine;

namespace Source.EasyECS
{
    public abstract class EasySystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private bool _isInitialized = false;
        private GameShare _gameShare;
        protected EcsWorld World;
        protected Componenter Componenter;
        protected GameConfiguration GameConfiguration;
        private Signal _signal;
        private float _deltaTime;
        protected float DeltaTime => _deltaTime;
        protected float TickTime { get; private set; }
        private InitializeType _initializeType;
        
        public virtual void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            if (_isInitialized) return;
            _gameShare = gameShare;
            Componenter = _gameShare.GetSharedObject<Componenter>();
            GameConfiguration = _gameShare.GetSharedObject<GameConfiguration>();
            _signal = _gameShare.GetSharedObject<Signal>();
            TickTime = tickTime;
            _initializeType = initializeType;
            _deltaTime = GetCurrentTime();
            _isInitialized = true;
        }

        private float GetCurrentTime()
        {
            switch (_initializeType)
            {
                case InitializeType.None:
                    return 0;
                
                case InitializeType.FixedUpdate:
                    return Time.fixedDeltaTime;
                
                case InitializeType.Tick:
                    return TickTime;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void RegistrySignal<T>(T data) where T : struct, ISignal
        {
            _signal.RegistryRaise(data);
        }

        public void SubscribeSignal<T>(Action<T> action) where T : struct, ISignal
        {
            _signal.Subscribe(action);
        }

        public void UnsubscribeSignal<T>(Action<T> action) where T : struct, ISignal
        {
            _signal.Unsubscribe(action);
        }

        public void Init(IEcsSystems systems)
        {
            World = systems.GetWorld();
            Initialize();
        }

        public void Run(IEcsSystems systems)
        {
            Update();
        }
        
        
        
        protected virtual void Initialize(){}
        protected virtual void Update(){}

        public virtual InitializeType DefaultInitializeType()
        {
            return InitializeType.None;
        }

        public virtual string Description()
        {
            return "";
        }

        public virtual void Destroy(IEcsSystems systems)
        {
            
        }
    }
    
    public enum InitializeType
    {
        None,
        Start,
        FixedUpdate,
        Tick
    }
}