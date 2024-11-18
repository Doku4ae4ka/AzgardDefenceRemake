using Source.Scripts.ECS.Groups.Enemies;
using Source.Scripts.ECS.Groups.GameCore.DataBuilder;
using Source.Scripts.ECS.Groups.HealthSaver;
using Source.Scripts.ECS.Groups.SlotSaver;

namespace Source.Scripts.Core
{
    public static class StartingConfigurations
    {
        public static SlotSaverGroup SetSlotSaverSettings(this SlotSaverGroup group)
        {
            group
                //.SetBuilder(new EnemyBuilder())
                .SetBuilder(new ConfigBuilder())
                .SetBuilder(new HealthBuilder())
                .SetBuilder(new MovableBuilder())
                ;
            return group;
        }
    }
}