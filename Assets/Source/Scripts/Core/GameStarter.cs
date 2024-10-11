using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Groups.Debug;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using UnityEngine;

namespace Source.Scripts.Core
{
    [AddComponentMenu("GameStarter")]
    public class GameStarter : EcsStarter
    {
        [SerializeField] private bool autoLoad;
        [SerializeField] private AzgardGameContext azgardGameContext;
        [SerializeField] private GameStatus gameStatus;
        [SerializeField, HideInInspector] private SignalHandler signalHandler;
        [SerializeField, HideInInspector] private GameConfigurations gameConfigurations;
        [SerializeField, HideInInspector] private Memory memory;
        private SpaceHash<EcsData.TransformData, EcsData.Tower> _spaceHash;
        [SerializeField] private Prototypes prototypes = new Prototypes();
        [SerializeField] private Configs configs = new Configs();
        
        public GameStatus GameStatus => gameStatus;
        public SignalHandler SignalHandler => signalHandler;
        public GameConfigurations GameConfigurations => gameConfigurations;
        public Memory Memory => memory;
        public Prototypes Prototypes => prototypes;

        public Pooler Pooler { get; private set; }


        protected override GameContext GetGameContext(GameShare gameShare)
        {
            gameShare.AddSharedObject(azgardGameContext);
            return azgardGameContext;
        }

        private void Start()
        {
            Initialize();
            // if (autoLoad) Load();
        }
        //
        // [Button]
        // public void Save()
        // {
        //     gameStatus.currentState = GameStatus.State.Loading;
        //     
        //     var slot = gameConfigurations.slot;
        //     slot.Initialize();
        //     memory.save.Invoke(_world, _pooler, slot);
        //     gameStatus.currentState = GameStatus.State.Game;
        // }
        //
        // [Button]
        // public void Load()
        // {
        //     gameStatus.currentState = GameStatus.State.Loading;
        //     prototypes.Clear();
        //     
        //     var slot = gameConfigurations.slot;
        //     slot.Initialize();
        //     memory.load.Invoke(_world, _pooler, slot, prototypes);
        //     
        //     gameStatus.currentState = GameStatus.State.Game;
        // }

        [Button]
        public void Pause()
        {
            if (gameStatus.currentState == GameStatus.State.Game) gameStatus.currentState = GameStatus.State.Pause;
            else if (gameStatus.currentState == GameStatus.State.Pause) gameStatus.currentState = GameStatus.State.Game;
        }
        
            // initSystems
            //         
            //     .Add(new ConfigSystem())
            //     .Add(new ViewSystem())
            //     .Add(new BuildingTilemapSystem())
            //     .Add(new TowerSystem())
            //     .Add(new MovableSystem())
            //     .Add(new HealthSystem())
            //     .Add(new TowerSpawnSystem())
            //     .Add(new EnemySpawnSystem());
        
        // updateSystems
        //     
        //     .Add(new TowerPreviewSystem())
        //     .Add(new TowerAttackSystem())
        //     .Add(new MovementSystem())
        //     .Add(new TargetingSystem())
        //     .Add(new LoaderSystem());

        protected override EcsGroup[] GetGroups()
        {
            return new EcsGroup[]
            {
                //asdasd,
                new DebugGroup()
            };
        }

        protected override void SetSharingData(EcsWorld world, GameShare gameShare)
        {
            _spaceHash = new SpaceHash<EcsData.TransformData, EcsData.Tower>(world, new Vector4(-40, -40, 45, 45), 2);
            gameShare.AddSharedObject(gameConfigurations);
            gameShare.AddSharedObject(gameStatus);
            gameShare.AddSharedObject(memory);
            gameShare.AddSharedObject(prototypes);
            gameShare.AddSharedObject(_spaceHash);
            gameShare.AddSharedObject(configs);
            gameShare.AddSharedObject(this);
        }

        protected override Func<float> FixedUpdateDelta { get; }
        protected override Func<float> UpdateDelta { get; }
        protected override Signal Signal { get; }
    }
}