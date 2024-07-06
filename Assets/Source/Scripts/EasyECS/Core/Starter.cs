
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using UnityEngine;

namespace Source.EasyECS
{
    public abstract class Starter : BootstrapComponent, IEasyFixedUpdate
    {
        [SerializeField] private float tickTime = 1f;
        [ReadOnly]
        [SerializeField] private List<string> _bootQueue;
        
        private EcsWorld _world;
        private Componenter _componenter;
        private IEcsSystems _stepByStepSystems;
        private IEcsSystems _cardViewRefreshSystems;
        private IEcsSystems _coreSystems;
        private IEcsSystems _initSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _tickUpdateSystems;
        private float _tickTimer;
        
        protected override void OnPreInit()
        {
            _world = new EcsWorld();
            PrepareCoreSystems();
            PrepareInitSystems();
            PrepareFixedUpdateSystems();
            PrepareTickUpdateSystems();
            DependencyInject();
        }

        private void DependencyInject()
        {
            InjectSystems(_coreSystems);
            InjectSystems(_initSystems);
            InjectSystems(_fixedUpdateSystems, InitializeType.FixedUpdate);
            InjectSystems(_tickUpdateSystems, InitializeType.Tick);
        }
        
        private void InjectSystems(IEcsSystems systems, InitializeType initializeType = InitializeType.None)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EasySystem easySystem)
                {
                    easySystem.PreInit(GameShare, tickTime, initializeType);
                }
            }
        }
        
        public override void Initialize()
        {
            InitBootInfo();
            
            _coreSystems.Init();
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _tickUpdateSystems.Init();
 
        }
        
        protected abstract void SetInitSystems(IEcsSystems initSystems);
        protected abstract void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems);
        protected abstract void SetTickUpdateSystems(IEcsSystems tickUpdateSystems);

        private void InitBootInfo()
        {
            _bootQueue = new List<string>();
            AddToBoot(_coreSystems);
            AddToBoot(_initSystems);
            AddToBoot(_fixedUpdateSystems);
            AddToBoot(_tickUpdateSystems);
        }

        private void AddToBoot(IEcsSystems systems)
        {
            foreach (var system in systems.GetAllSystems())
            {
                _bootQueue.Add(system.GetType().Name);
            }
        }

        private void TryInvokeTick()
        {
            _tickTimer += Time.fixedDeltaTime;
            if (!(_tickTimer >= tickTime)) return;
            _tickTimer -= tickTime;
            _tickUpdateSystems?.Run();
        }
        
        private void PrepareCoreSystems()
        {
            _componenter = GameShare.GetSharedObject<Componenter>();
            _componenter.PreInit(_world);
            _coreSystems = new EcsSystems(_world, GameShare);
            _coreSystems.Inject();
        }
        
        private void PrepareInitSystems()
        {
            _initSystems = new EcsSystems(_world, GameShare);
            SetInitSystems(_initSystems);
            _initSystems.Inject();
        }
        
        private void PrepareFixedUpdateSystems()
        {
            _fixedUpdateSystems = new EcsSystems(_world, GameShare);
            SetFixedUpdateSystems(_fixedUpdateSystems);
#if UNITY_EDITOR
                // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                _fixedUpdateSystems.Add(new UnityEditor.EcsWorldDebugSystem());
#endif
            
            _fixedUpdateSystems.Inject();
        }
        
        private void PrepareTickUpdateSystems()
        {
            _tickUpdateSystems = new EcsSystems(_world, GameShare);
            SetTickUpdateSystems(_tickUpdateSystems);
            _tickUpdateSystems.Inject();
        }
        
        private void OnDestroy() 
        {
            _coreSystems?.Destroy();
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _tickUpdateSystems?.Destroy();
        }

        public void EasyFixedUpdate()
        {
            _fixedUpdateSystems?.Run();
            TryInvokeTick();
        }
    }
}
