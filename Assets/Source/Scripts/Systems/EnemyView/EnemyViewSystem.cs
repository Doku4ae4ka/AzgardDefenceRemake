using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.Systems.View
{
    public class EnemyViewSystem : EcsGameSystem
    {
        private EcsFilter _viewFilter;
        
        protected override void Initialize()
        {
            _viewFilter = InGameMask.Inc<EcsData.View>().Inc<EcsData.Transform>().End();
            Memory.save.OnDynamic += EnemyViewSaver.TrySave;
            Memory.load.OnDynamic += TryLoadDynamic;
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            Memory.save.OnDynamic -= EnemyViewSaver.TrySave;
            Memory.load.OnDynamic -= TryLoadDynamic;
        }

        private void TryLoadDynamic(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        {
            EnemyViewLoader.LoadViewDynamic(ecsWorld, pooler, slot);
        }
    }
}