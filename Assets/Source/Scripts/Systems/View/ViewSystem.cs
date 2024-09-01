using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.Systems.View
{
    public class ViewSystem : EcsGameSystem
    {
        private EcsFilter _viewFilter;
        
        protected override void Initialize()
        {
            _viewFilter = InGameMask.Inc<EcsData.View>().Inc<EcsData.Transform>().End();
            Memory.save.OnDynamic += ViewSaver.TrySave;
            Memory.load.OnPrototypes += TryLoadPrototypes;
            Memory.load.OnDynamic += TryLoadDynamic;
            Memory.load.OnStatic += TryLoadStatic;
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            Memory.save.OnDynamic -= ViewSaver.TrySave;
            Memory.load.OnPrototypes -= TryLoadPrototypes;
            Memory.load.OnDynamic -= TryLoadDynamic;
            Memory.load.OnStatic -= TryLoadStatic;
        }

        private void TryLoadDynamic(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        {
            ViewLoader.LoadViewDynamic(ecsWorld, pooler, slot, ViewLibrary);
        }

        private void TryLoadStatic(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        {
            ViewLoader.LoadViewStatic(ecsWorld, pooler, slot, ViewLibrary);
        }

        private void TryLoadPrototypes(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        {
            ViewLoader.LoadViewPrototype(ecsWorld, pooler, slot, ViewLibrary);
        }
    }
}