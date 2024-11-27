using System;
using ECS.Modules.Exerussus.Health;
using ECS.Modules.Exerussus.Movement;
using ECS.Modules.Exerussus.SpaceHash;
using ECS.Modules.Exerussus.TransformRelay;
using ECS.Modules.Exerussus.ViewCreator;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Groups.AzgardView;
using Source.Scripts.ECS.Groups.Debug;
using Source.Scripts.ECS.Groups.GameCore;
using Source.Scripts.ECS.Groups.SlotSaver;
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
        [SerializeField] private SignalHandler signalHandler;
        [SerializeField] private GameConfigurations gameConfigurations;
        [SerializeField] private Memory memory;
        [SerializeField, ReadOnly] private MemoryLoadingProcess memoryLoadingProcess;
        [SerializeField, ReadOnly] private bool isLoading;
        private SlotSaverPooler _slotSaverPooler;
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

        [Button]
        public void LoadGame()
        {
            gameConfigurations.slot.Initialize();
            memoryLoadingProcess = MemoryLoadingProcess.Pre;
        }

        protected override void SetSharingDataAfterInitialized(EcsWorld world, GameShare gameShare)
        {
            gameShare.GetSharedObject(ref _slotSaverPooler);
        }
        
        protected override GameContext GetGameContext(GameShare gameShare)
        {
            gameShare.AddSharedObject(azgardGameContext);
            return azgardGameContext;
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
                new HealthGroup(),
                new MovementGroup(),
                new TransformRelayGroup(),
                new ViewCreatorGroup(),
                new SlotSaverGroup().SetSlotSaverSettings(),
                CreateSpaceHash(),
                new GameCoreGroup(),
                new DebugGroup(),
            };
        }

        protected override void SetSharingDataOnStart(EcsWorld world, GameShare gameShare)
        {
            gameShare.AddSharedObject(gameConfigurations);
            gameShare.AddSharedObject(gameStatus);
            gameShare.AddSharedObject(memory);
            gameShare.AddSharedObject(prototypes);
            gameShare.AddSharedObject(configs);
            gameShare.AddSharedObject(this);
        }

        public override void FixedUpdate()
        {
            UpdateMemoryLoadingProcess(gameConfigurations.slot);
            if(memoryLoadingProcess != MemoryLoadingProcess.None) return;
            base.FixedUpdate();
        }

        public override void Update()
        {
            if(memoryLoadingProcess != MemoryLoadingProcess.None) return;
            base.Update();
        }

        public override void LateUpdate()
        {
            if(memoryLoadingProcess != MemoryLoadingProcess.None) return;
            base.LateUpdate();
        }

        private void UpdateMemoryLoadingProcess(Slot slot)
        {
            switch (memoryLoadingProcess)
            {
                case MemoryLoadingProcess.None:
                    break;
                case MemoryLoadingProcess.Pre:
                    memory.load.PreLoading(_world, _slotSaverPooler, slot, Signal);
                    memoryLoadingProcess = MemoryLoadingProcess.Process;
                    break;
                case MemoryLoadingProcess.Process:
                    memory.load.Load(_world, _slotSaverPooler, slot, prototypes);
                    memoryLoadingProcess = MemoryLoadingProcess.Post;
                    break;
                case MemoryLoadingProcess.Post:
                    memory.load.PostLoading(_world, _slotSaverPooler, slot, Signal);
                    memoryLoadingProcess = MemoryLoadingProcess.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }   
            
        }
        
        private SpaceHashGroup CreateSpaceHash()
        {
            return new SpaceHashGroup()
                .SetMask(_world.Filter<EcsData.Enemy>().Exc<HealthData.DeadMark>())
                .SetMinMaxPoints(new Vector2(-40, -40), new Vector2(45, 45))
                .SetCellSize(4f);
        }
    }

    public enum MemoryLoadingProcess
    {
        None,
        Pre,
        Process,
        Post
    }
}