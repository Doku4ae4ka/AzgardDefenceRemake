using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace Source.Scripts.ECS.Groups.Enemies
{
    public class EnemyPooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            EnemyMark = new(world);
        }

        public PoolerModule<EnemyData.EnemyMark> EnemyMark;
    }
}