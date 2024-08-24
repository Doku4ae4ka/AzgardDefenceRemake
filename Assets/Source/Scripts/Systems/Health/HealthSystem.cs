using Leopotam.EcsLite;
using Source.Scripts.Core;

namespace Source.Scripts.Systems.Health
{
    public class HealthSystem : EcsGameSystem
    {
        private EcsFilter _healthFilter;

        protected override void Initialize()
        {
            _healthFilter = InGameMask.Inc<EcsData.Health>().End();
            Memory.save.OnDynamic += HealthSaver.TryAddData;
            Memory.load.OnDynamic += HealthLoader.TryLoadDynamic;
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            Memory.save.OnDynamic -= HealthSaver.TryAddData;
            Memory.load.OnDynamic -= HealthLoader.TryLoadDynamic;
        }
    }
}