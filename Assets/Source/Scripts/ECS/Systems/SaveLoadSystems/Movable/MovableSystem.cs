using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.ECS.Systems.SaveLoadSystems.Health;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.Movable
{
    public class MovableSystem : EcsGameSystem
    {
        private EcsFilter _movableFilter;

        protected override void Initialize()
        {
            _movableFilter = InGameMask.Inc<EcsData.Movable>().End();
            Memory.save.OnDynamic += MovableSaver.TryAddData;
            Memory.load.OnDynamic += MovableLoader.TryLoadDynamic;
            Memory.load.OnPrototypes += MovableLoader.TryLoadPrototype;
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            Memory.save.OnDynamic -= MovableSaver.TryAddData;
            Memory.load.OnDynamic -= MovableLoader.TryLoadDynamic;
            Memory.load.OnPrototypes -= MovableLoader.TryLoadPrototype;
        }
    }
}