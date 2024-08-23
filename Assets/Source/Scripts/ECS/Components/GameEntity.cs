using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Source.Scripts.ECS.Core.Interfaces;
using UnityEngine;

namespace Source.Scripts.ECS.Core
{
    [SelectionBase]
    public abstract class GameEntity : MonoBehaviour, IEcsMonoBehavior
    {
        #region SerializedFields
        
        [SerializeField, HideInInspector] private int entity;
        [SerializeField, HideInInspector] private bool isAlive = true;
        [SerializeField, HideInInspector] private bool isInitialized;
        [SerializeField, HideInInspector] private EcsComponent[] ecsComponents;
        
        #endregion

        #region Members
        
        public int Entity => entity;
        public bool IsAlive => isAlive;
        public Pooler Pooler { get; private set; }
        public Signal Signal { get; private set; }
        
        public event Action OnInitialized;

        #endregion

        #region InitAndDestroy

        public void Initialize(int newEntity, Signal signal, Pooler pooler)
        {
            if (isInitialized) return;
            
            isInitialized = true;
            isAlive = true;
            Pooler = pooler;
            Signal = signal;
            entity = newEntity;
            
            ref var transformData = ref Pooler.Transform.AddOrGet(entity);
            transformData.InitializeValues(transform);
            
            foreach (var ecsComponent in ecsComponents) ecsComponent.PreInitialize(entity, Pooler);
            foreach (var ecsComponent in ecsComponents) ecsComponent.Initialize();
            
            ref var ecsMonoBehData = ref Pooler.EcsMonoBehavior.AddOrGet(entity);
            ecsMonoBehData.InitializeValues(this);
            
            Signal.RegistryRaise(new OnGameEntityInitializedSignal { EcsMonoBehavior = this });
            OnInitialized?.Invoke();
            OnInitialized = null;
        }
        
        public void DestroyEcsMonoBehavior(float delay)
        {
            if (!IsAlive) return;
            isAlive = false;
            isInitialized = false;
            foreach (var ecsComponent in ecsComponents) ecsComponent.Destroy();
            Signal.RegistryRaise(new OnGameEntityStartDestroySignal { EcsMonoBehavior = this });
            ref var destroyingData = ref Pooler.OnDestroy.AddOrGet(entity);
            destroyingData.InitializeValues(gameObject, delay);
        }

        #endregion

        #region Methods

        public void SwitchActivated()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        #endregion

        #region Editor
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            ecsComponents = GetComponents<EcsComponent>();
        }

#endif

        #endregion
        
    }
}