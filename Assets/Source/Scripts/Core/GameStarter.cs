
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using Source.Scripts.ProjectLibraries;
using Source.Scripts.SaveSystem;
using Source.Scripts.Systems;
using Source.Scripts.Systems.Health;
using Source.Scripts.Systems.View;
using UnityEngine;

namespace Source.Scripts.Core
{
    [AddComponentMenu("1Game/GameStarter")]
    public class GameStarter : EcsStarter<Pooler>
    {
        [SerializeField] private bool autoLoad;
        [SerializeField] private string locationName = "home";
        [SerializeField] private GameStatus gameStatus;
        [SerializeField, HideInInspector] private SignalHandler signalHandler;
        [SerializeField, HideInInspector] private GameConfigurations gameConfigurations;
        [SerializeField, HideInInspector] private Libraries libraries;
        [SerializeField, HideInInspector] private Memory memory;
        
        public GameStatus GameStatus => gameStatus;
        public SignalHandler SignalHandler => signalHandler;
        public GameConfigurations GameConfigurations => gameConfigurations;
        public Libraries Libraries => libraries;
        public Memory Memory => memory;

        public Pooler Pooler { get; private set; }


        private void Start()
        {
            Initialize();
            if (autoLoad) Load();
        }
        
        [Button]
        public void Save()
        {
            gameStatus.currentState = GameStatus.State.Loading;
            
            var slot = gameConfigurations.slot;
            slot.Initialize();
            memory.save.Invoke(_world, _pooler, slot);
            gameStatus.currentState = GameStatus.State.Game;
        }

        [Button]
        public void Load()
        {
            gameStatus.currentState = GameStatus.State.Loading;
            
            var slot = gameConfigurations.slot;
            slot.Initialize();
            memory.load.Invoke(_world, _pooler, slot);
            
            gameStatus.currentState = GameStatus.State.Game;
        }

        [Button]
        public void Pause()
        {
            if (gameStatus.currentState == GameStatus.State.Game) gameStatus.currentState = GameStatus.State.Pause;
            else if (gameStatus.currentState == GameStatus.State.Pause) gameStatus.currentState = GameStatus.State.Game;
        }

        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            initSystems
                    
                .Add(new EnemyViewSystem())
                .Add(new HealthSystem());
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems
                    
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
        }

        protected override void SetUpdateSystems(IEcsSystems updateSystems)
        {
            updateSystems
                
                .Add(new CloneCreatorSystem());
        }

        protected override void SetLateUpdateSystems(IEcsSystems lateUpdateSystems)
        {
            
        }

        protected override void SetTickUpdateSystems(IEcsSystems tickUpdateSystems)
        {
            
        }

        protected override void SetSharingData(EcsWorld world, GameShare gameShare)
        {
            gameShare.AddSharedObject(gameConfigurations);
            gameShare.AddSharedObject(libraries);
            gameShare.AddSharedObject(gameStatus);
            gameShare.AddSharedObject(memory);
        }

        protected override Signal GetSignal()
        {
            return signalHandler.Signal;
        }

        protected override Pooler GetPooler(EcsWorld world)
        {
            Pooler = new Pooler(world);
            return Pooler;
        }

        private void OnValidate()
        {
            ConfigLoader.TryGetConfigIfNull(ref signalHandler,"Signals");
            ConfigLoader.TryGetConfigIfNull(ref gameConfigurations, "GameCore");
            ProjectTask.TestCode(() =>
            {
                var towerLibrary = Libraries.TowerLibrary;
                ConfigLoader.TryGetConfigIfNull(ref towerLibrary, "Libraries");
            });
        }
    }
}