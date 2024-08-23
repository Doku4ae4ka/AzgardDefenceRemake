using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace Source.Scripts.ECS.Core
{
    public class EcsStarter : EcsStarter<Pooler>
    {
        [SerializeField] private SignalHandler signalHandler;
        [SerializeField] private GameConfiguration gameConfiguration;
        
        public Pooler Pooler { get; private set; }
        
        protected override void SetInitSystems(IEcsSystems initSystems)
        {

        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {

        }

        protected override void SetUpdateSystems(IEcsSystems updateSystems)
        {

        }

        protected override void SetLateUpdateSystems(IEcsSystems lateUpdateSystems)
        {

        }

        protected override void SetTickUpdateSystems(IEcsSystems tickUpdateSystems)
        {

        }

        protected override void SetSharingData(EcsWorld world, GameShare gameShare)
        {
            gameShare.AddSharedObject(gameConfiguration);
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
    }
}