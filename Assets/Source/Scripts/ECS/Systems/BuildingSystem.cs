using Source.Scripts.ECS.Core;

namespace Source.Scripts.ECS.Systems
{
    public class BuildingSystem : EcsListener<CommandSpawnTower>
    {
        protected override void OnSignal(CommandSpawnTower data)
        {
            throw new System.NotImplementedException();
        }
    }
}