using Source.Scripts.ECS.Groups.Enemies;
using Source.Scripts.ECS.Groups.GameCore.DataBuilder;
using Source.Scripts.ECS.Groups.GameCore.DataBuilder.Dynamic;
using Source.Scripts.ECS.Groups.GameCore.DataBuilder.Static;
using Source.Scripts.ECS.Groups.HealthSaver;
using Source.Scripts.ECS.Groups.SlotSaver;

namespace Source.Scripts.Core
{
    public static class StartingConfigurations
    {
        public static SlotSaverGroup SetSlotSaverSettings(this SlotSaverGroup group)
        {
            group
                .SetBuilder(new EnemyBuilder())
                .SetBuilder(new ConfigBuilder())
                .SetBuilder(new HealthBuilder())
                .SetBuilder(new PathFollowerBuilder())
                .SetBuilder(new BuildingTilemapBuilder())
                .SetBuilder(new TowerBuilder())
                
                .SetBuilder(new EnemyViewBuilder())
                .SetBuilder(new TowerViewBuilder())
                .SetBuilder(new EnvironmentViewBuilder())
                ;
            return group;
        }
    }
}