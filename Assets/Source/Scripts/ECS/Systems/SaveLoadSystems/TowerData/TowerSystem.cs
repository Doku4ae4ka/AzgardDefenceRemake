using Leopotam.EcsLite;
using Source.Scripts.Core;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems.TowerData
{
    public class TowerSystem : EcsGameSystem
    {
        private EcsFilter _towerFilter;

        protected override void Initialize()
        {
            _towerFilter = InGameMask.Inc<EcsData.Tower>().End();
            Memory.save.OnDynamic += TowerSaver.TryAddData;
            Memory.load.OnDynamic += TowerLoader.TryLoadDynamic;
            Memory.load.OnPrototypes += TowerLoader.TryLoadPrototype;
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            Memory.save.OnDynamic -= TowerSaver.TryAddData;
            Memory.load.OnDynamic -= TowerLoader.TryLoadDynamic;
            Memory.load.OnPrototypes -= TowerLoader.TryLoadPrototype;
        }
    }
}