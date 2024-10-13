using System;
using ECS.Modules.Exerussus.Health;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Exerussus.EasyEcsModules.ViewCreator;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Groups.AzgardView;
using Source.Scripts.ECS.Groups.BuildingTilemap;
using Source.Scripts.ECS.Groups.Debug;
using Source.Scripts.ECS.Groups.Enemies;
using Source.Scripts.ECS.Groups.GameCore;
using Source.Scripts.ECS.Groups.GameCore.DataBuilder;
using Source.Scripts.ECS.Groups.SlotSaver;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.ECS.Groups.Towers;
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

        protected override Func<float> FixedUpdateDelta { get; } = () => Time.fixedDeltaTime;
        protected override Func<float> UpdateDelta { get; } = () => Time.deltaTime;
        protected override Signal Signal => signalHandler.Signal;
        
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
        }

        [Button]
        public void Pause()
        {
            if (gameStatus.currentState == GameStatus.State.Game) gameStatus.currentState = GameStatus.State.Pause;
            else if (gameStatus.currentState == GameStatus.State.Pause) gameStatus.currentState = GameStatus.State.Game;
        }

        protected override EcsGroup[] GetGroups()
        {
            return new EcsGroup[]
            {
                new AzgardViewGroup(),
                new TileMapGroup(),
                new HealthGroup(),
                new ViewCreatorGroup(),
                new SlotSaverGroup().SetSlotSaverSettings(),
                new TowerGroup(),
                new GameCoreGroup(),
                new DebugGroup(),
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
    }
}