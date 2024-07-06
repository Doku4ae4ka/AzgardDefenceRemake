
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.Managers.ProjectSettings;
using Source.Scripts.SignalSystem;
using Source.SignalSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.EasyECS.Core
{
    public sealed class EcsMonoBehavior : MonoBehaviour
    {
        #region SerializedFields
        
        [SerializeField, ReadOnly] private int entity;
        [SerializeField, ReadOnly] private bool isAlive = true;
        [SerializeField, ReadOnly] private bool isInitialized;
        [SerializeField, ReadOnly] private int components;
        [SerializeField, HideInInspector] private EcsComponent[] ecsComponents;
        [SerializeField, HideInInspector] private Signal signal;
        private bool _isReused;
        
        #endregion

        #region Members
        
        public int Entity => entity;
        public bool IsAlive => isAlive;
        public Componenter Componenter { get; private set; }

        #endregion

        #region InitAndDestroy
        
        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            if (_isReused) Initialize();
        }

        private void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;
            isAlive = true;
            Componenter = EasyNode.EcsComponenter;
            entity = Componenter.GetNewEntity();
            ref var transformData = ref Componenter.Add<TransformData>(entity);
            transformData.InitializeValues(transform);
            foreach (var ecsComponent in ecsComponents) ecsComponent.Initialize(entity, Componenter);
            ref var ecsMonoBehData = ref Componenter.Add<EcsMonoBehaviorData>(entity);
            ecsMonoBehData.InitializeValues(this);
            signal.RegistryRaise(new OnEcsMonoBehaviorInitializedSignal { EcsMonoBehavior = this });
        }
        
        public void DestroyEcsMonoBehavior()
        {
            if (!IsAlive) return;
            isAlive = false;
            isInitialized = false;
            _isReused = true;
            foreach (var ecsComponent in ecsComponents) ecsComponent.Destroy(entity, Componenter);
            signal.RegistryRaise(new OnEcsMonoBehaviorStartDestroySignal { EcsMonoBehavior = this });
            ref var destroyingData = ref Componenter.AddOrGet<OnDestroyData>(entity);
            destroyingData.InitializeValues(gameObject, Constants.Ecs.EntityDestroyDelay);
        }

        #endregion

        #region Editor

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (signal == null) signal = Project.Loader.GetAssetByTypeOnValidate<Signal>();

            ecsComponents = GetComponents<EcsComponent>();
            components = ecsComponents.Length;
        }
#endif
        
        #endregion
    }
    
    public interface IEcsComponentInitialize
    {
        public void Initialize(int entity, Componenter componenter);
    }
    
    public interface IEcsComponentDestroy
    {
        public void Destroy(int entity, Componenter componenter);
    }

    public struct EcsMonoBehaviorData : IEcsData<EcsMonoBehavior>
    {
        public EcsMonoBehavior Value;
        
        public void InitializeValues(EcsMonoBehavior value)
        {
            Value = value;
        }
    }
}