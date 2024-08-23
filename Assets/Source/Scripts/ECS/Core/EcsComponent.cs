using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Extensions.SignalSystem;
using Source.Scripts.ECS.Core.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.ECS.Core
{
    [RequireComponent(typeof(IEcsMonoBehavior))]
    public abstract class EcsComponent : MonoSignalListener
    {
        [FormerlySerializedAs("ecsMonoBehavior")] [SerializeField, HideInInspector] private GameEntity gameEntity;
        private int _entity;
        private Pooler _pooler;

        public GameEntity GameEntity => gameEntity;
        public int Entity => _entity;
        public Pooler Pooler => _pooler;

        public void PreInitialize(int entity, Pooler pooler)
        {
            _entity = entity;
            _pooler = pooler;
        }
        
        public virtual void Initialize()
        {
            
        }

        public virtual void Destroy()
        {
            
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            gameEntity = gameObject.TryGetIfNull(ref gameEntity);
        }
    }
}