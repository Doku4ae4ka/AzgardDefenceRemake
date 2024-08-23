using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace Source.Scripts.ECS.Core
{
    public class Pooler
    {
        public Pooler(EcsWorld world)
        {
            #region Init
            
            Transform = new PoolerModule<TransformData>(world);
            OnDestroy = new PoolerModule<OnDestroyData>(world);
            EcsMonoBehavior = new PoolerModule<EcsMonoBehaviorData>(world);
            Tower = new PoolerModule<TowerData>(world);
            
            #endregion
        }
        
        #region Properties

        public readonly PoolerModule<TransformData> Transform;
        public readonly PoolerModule<OnDestroyData> OnDestroy;
        public readonly PoolerModule<EcsMonoBehaviorData> EcsMonoBehavior;
        public readonly PoolerModule<TowerData> Tower;
        
        #endregion
    }
}