using ECS.Modules.Exerussus.Movement;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace Source.Scripts.ECS.Groups.GameCore.Systems.Towers
{
    public class TowerAttackSystem : EasySystem<GameCorePooler>
    {
        [InjectSharedObject] private MovementPooler _movementPooler;
        private EcsFilter _towerFilter;
        
        protected override void Initialize()
        {
            _towerFilter = Pooler.InGameMask.Inc<EcsData.TowerMark>().Inc<EcsData.Target>().End();
        }
        
        protected override void Update()
        {
            _towerFilter.Foreach(OnUpdate);
        }
        
        private void OnUpdate(int entity)
        {
            ref var target = ref Pooler.Target.Get(entity);
            if(!target.HasTarget) return;
            
            ref var towerView = ref Pooler.TowerView.Get(entity);
        
            towerView.Value.SetTarget(_movementPooler.GetPosition(target.TargetEntity));
        }
    }
}