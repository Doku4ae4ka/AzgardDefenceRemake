using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace Source.Scripts.ECS.Groups.GameCore
{
    public class GameCorePooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            
        }

        public Configs Configs = new();
    }
}