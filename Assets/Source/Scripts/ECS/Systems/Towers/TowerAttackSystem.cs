using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;

namespace Source.Scripts.ECS.Systems.Towers
{
    public class TowerAttackSystem : EcsGameSystem
    {
        private EcsFilter _towerFilter;
        
        protected override void Initialize()
        {
            _towerFilter = InGameMask.Inc<EcsData.Tower>().Inc<EcsData.Target>().End();
        }

        protected override void Update()
        {
            _towerFilter.Foreach(OnUpdate);
        }

        private void OnUpdate(int entity)
        {
            ref var target = ref Pooler.Target.Get(entity);
            if(!target.HasTarget) return;
            
            ref var towerData = ref Pooler.Tower.Get(entity);
            ref var towerView = ref Pooler.TowerView.Get(entity);

            towerView.Value.SetTarget(Pooler.GetPosition(target.TargetEntity));
        }
    }
}