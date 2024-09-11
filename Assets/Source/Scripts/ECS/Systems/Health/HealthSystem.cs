using Leopotam.EcsLite;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Systems.Health
{
    public class HealthSystem : EcsGameSystem
    {
        private EcsFilter _healthFilter;

        protected override void Initialize()
        {
            _healthFilter = InGameMask.Inc<EcsData.Health>().End();
            Memory.save.OnDynamic += HealthSaver.TryAddData;
            Memory.load.OnDynamic += HealthLoader.TryLoadDynamic;
            Memory.load.OnPrototypes += HealthLoader.TryLoadPrototype;

            var prototypeID = "tower";
            var position = new Vector3(0, 0, 0);

            if (!Prototypes.TryGet(prototypeID, out var prototypeEntity)) return;

            ref var prototypeData = ref Pooler.Prototype.Get(prototypeEntity);

            var newEntity = World.NewEntity();
            
            foreach (var action in prototypeData.DataBuilder) action.Invoke(newEntity);

            if (Pooler.Transform.Has(newEntity))
            {
                ref var transformData = ref Pooler.Transform.Get(newEntity);
                transformData.Value.position = position;
            }
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            Memory.save.OnDynamic -= HealthSaver.TryAddData;
            Memory.load.OnDynamic -= HealthLoader.TryLoadDynamic;
            Memory.load.OnPrototypes -= HealthLoader.TryLoadPrototype;
        }
    }
}