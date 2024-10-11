using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace Source.Scripts.ECS.Groups.GameCore
{
    public class GameCoreGroup : EcsGroup<GameCorePooler>
    {
        protected override void OnBeforePoolInitializing(EcsWorld world, GameCorePooler pooler)
        {
            
        }
    }
}