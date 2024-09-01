using Source.Scripts.Core;

namespace Source.Scripts.ECS.Systems
{
    public class BuildingSystem : EcsGameSystem<Signals.CommandSpawnTower>
    {
        protected override void OnSignal(Signals.CommandSpawnTower data)
        {
            throw new System.NotImplementedException();
        }
    }
}