using System;
using Sirenix.OdinInspector;
using Source.Scripts.Managers.ProjectSettings;
using UnityEngine;

namespace Source.SignalSystem
{
    [Tooltip("@Description")]
    [HideLabel, GUIColor("@NormalColor")]
    [Serializable]
    public abstract class SignalizeListener : MonoBehaviour
    {
        [SerializeField, ReadOnly, GUIColor("@ActivateColor")] public Signalizator.InitializeType initializeType;
        public Signalizator.InitializeType DefaultInitializeType => GetInitializeType();
        [HideLabel, GUIColor("@NormalColor")]
        [SerializeField, TextArea, ReadOnly] private string description;
        [SerializeField, HideInInspector] private Signal _signal;
        [SerializeField, HideInInspector] private Transform _canvas;
        public Signal Signal => _signal;
        private Color NormalColor => new Color(1, 1f, 1f); 
        private Color ActivateColor => initializeType == Signalizator.InitializeType.Off ? Color.red : Color.green;

        public Transform Canvas => _canvas;

        public virtual void PreInit() {}        
        
        public void Validate(Signal signal, Transform canvasTransform)
        {
            _canvas = canvasTransform;
            _signal = signal ? signal : Project.Loader.GetAssetByTypeOnValidate<Signal>();
            description = Description();
        }

        protected virtual string Description()
        {
            return "";
        }

        protected virtual Signalizator.InitializeType GetInitializeType()
        {
            return Signalizator.InitializeType.Off;
        }
        
        public virtual void Initialize() {}
    }

    [Serializable]
    public abstract class SignalizeListener<T1> : SignalizeListener 
        where T1 : struct
    {
        protected virtual void OnEnable()
        {
            Signal.Subscribe<T1>(OnSignal);
        }
        
        protected virtual void OnDisable()
        {
            Signal.Unsubscribe<T1>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
    }

    [Serializable]
    public abstract class SignalizeListener<T1, T2> : SignalizeListener 
        where T1 : struct where T2 : struct
    {
        protected virtual void OnEnable()
        {
            Signal.Subscribe<T1>(OnSignal);
            Signal.Subscribe<T2>(OnSignal);
        }
        
        protected virtual void OnDisable()
        {
            Signal.Unsubscribe<T1>(OnSignal);
            Signal.Unsubscribe<T2>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
    }

    [Serializable]
    public abstract class SignalizeListener<T1, T2, T3> : SignalizeListener 
        where T1 : struct where T2 : struct where T3 : struct
    {
        protected virtual void OnEnable()
        {
            Signal.Subscribe<T1>(OnSignal);
            Signal.Subscribe<T2>(OnSignal);
            Signal.Subscribe<T3>(OnSignal);
        }
        
        protected virtual void OnDisable()
        {
            Signal.Unsubscribe<T1>(OnSignal);
            Signal.Unsubscribe<T2>(OnSignal);
            Signal.Unsubscribe<T3>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
    }

    [Serializable]
    public abstract class SignalizeListener<T1, T2, T3, T4> : SignalizeListener 
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct
    {
        protected virtual void OnEnable()
        {
            Signal.Subscribe<T1>(OnSignal);
            Signal.Subscribe<T2>(OnSignal);
            Signal.Subscribe<T3>(OnSignal);
            Signal.Subscribe<T4>(OnSignal);
        }
        
        protected virtual void OnDisable()
        {
            Signal.Unsubscribe<T1>(OnSignal);
            Signal.Unsubscribe<T2>(OnSignal);
            Signal.Unsubscribe<T3>(OnSignal);
            Signal.Unsubscribe<T4>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
    }

    [Serializable]
    public abstract class SignalizeListener<T1, T2, T3, T4, T5> : SignalizeListener 
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct
    {
        protected virtual void OnEnable()
        {
            Signal.Subscribe<T1>(OnSignal);
            Signal.Subscribe<T2>(OnSignal);
            Signal.Subscribe<T3>(OnSignal);
            Signal.Subscribe<T4>(OnSignal);
            Signal.Subscribe<T5>(OnSignal);
        }
        
        protected virtual void OnDisable()
        {
            Signal.Unsubscribe<T1>(OnSignal);
            Signal.Unsubscribe<T2>(OnSignal);
            Signal.Unsubscribe<T3>(OnSignal);
            Signal.Unsubscribe<T4>(OnSignal);
            Signal.Unsubscribe<T5>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
    }

    [Serializable]
    public abstract class SignalizeListener<T1, T2, T3, T4, T5, T6> : SignalizeListener 
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct where T6 : struct
    {
        protected virtual void OnEnable()
        {
            Signal.Subscribe<T1>(OnSignal);
            Signal.Subscribe<T2>(OnSignal);
            Signal.Subscribe<T3>(OnSignal);
            Signal.Subscribe<T4>(OnSignal);
            Signal.Subscribe<T5>(OnSignal);
            Signal.Subscribe<T6>(OnSignal);
        }
        
        protected virtual void OnDisable()
        {
            Signal.Unsubscribe<T1>(OnSignal);
            Signal.Unsubscribe<T2>(OnSignal);
            Signal.Unsubscribe<T3>(OnSignal);
            Signal.Unsubscribe<T4>(OnSignal);
            Signal.Unsubscribe<T5>(OnSignal);
            Signal.Unsubscribe<T6>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
    } 
    
    [Serializable]
    public abstract class SignalizeListener<T1, T2, T3, T4, T5, T6, T7> : SignalizeListener
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct where T6 : struct 
        where T7 : struct
    {
        protected virtual void OnEnable()
        {
            Signal.Subscribe<T1>(OnSignal);
            Signal.Subscribe<T2>(OnSignal);
            Signal.Subscribe<T3>(OnSignal);
            Signal.Subscribe<T4>(OnSignal);
            Signal.Subscribe<T5>(OnSignal);
            Signal.Subscribe<T6>(OnSignal);
            Signal.Subscribe<T7>(OnSignal);
        }
        
        protected virtual void OnDisable()
        {
            Signal.Unsubscribe<T1>(OnSignal);
            Signal.Unsubscribe<T2>(OnSignal);
            Signal.Unsubscribe<T3>(OnSignal);
            Signal.Unsubscribe<T4>(OnSignal);
            Signal.Unsubscribe<T5>(OnSignal);
            Signal.Unsubscribe<T6>(OnSignal);
            Signal.Unsubscribe<T7>(OnSignal);
        }
        
        protected abstract void OnSignal(T1 data);
        protected abstract void OnSignal(T2 data);
        protected abstract void OnSignal(T3 data);
        protected abstract void OnSignal(T4 data);
        protected abstract void OnSignal(T5 data);
        protected abstract void OnSignal(T6 data);
        protected abstract void OnSignal(T7 data);
    }
    
}