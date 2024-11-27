using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.GameCore.Systems;
using Source.Scripts.ECS.Groups.GameCore.Systems.Towers;
using Source.Scripts.ECS.Groups.Towers.Systems;

namespace Source.Scripts.ECS.Groups.GameCore
{
    public class GameCoreGroup : EcsGroup<GameCorePooler>
    {
        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            initSystems.Add(new EnemySpawnSystem());
            initSystems.Add(new TowerSpawnSystem());
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new MovementSystem());
            fixedUpdateSystems.Add(new LoaderSystem());
            fixedUpdateSystems.Add(new TowerPreviewSystem());
            fixedUpdateSystems.Add(new TargetingSystem());
            fixedUpdateSystems.Add(new TowerAttackSystem());
        }
    }
}