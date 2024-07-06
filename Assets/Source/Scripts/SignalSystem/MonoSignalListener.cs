using Source.Scripts.Data.GamesConfigurations;
using Source.Scripts.Managers.ProjectSettings;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.SignalSystem
{
    public abstract class MonoSignalListener : MonoBehaviour
    {
        [SerializeField, HideInInspector] private Signal signal;
        [SerializeField, HideInInspector] private GameConfiguration gameConfiguration;
        public Signal Signal => signal;
        public GameConfiguration GameConfiguration => gameConfiguration;

        protected virtual void OnValidate()
        {
            if (signal == null) signal = Project.Loader.GetAssetByTypeOnValidate<Signal>();
            if (gameConfiguration == null) gameConfiguration = Project.Loader.GetAssetByTypeOnValidate<GameConfiguration>();
            
        }
    }
    
    public abstract class MonoSignalListener<T> : MonoSignalListener where T : struct
    {
        protected virtual void OnEnable()
        {
            Signal.Subscribe<T>(OnSignal);
        }

        protected virtual void OnDisable()
        {
            Signal.Unsubscribe<T>(OnSignal);
        }

        protected abstract void OnSignal(T data);
    }
    
    public abstract class MonoSignalListener<T1, T2> : MonoSignalListener where T1 : struct where T2 : struct
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
    
    public abstract class MonoSignalListener<T1, T2, T3> : MonoSignalListener where T1 : struct where T2 : struct where T3 : struct
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
    
    public abstract class MonoSignalListener<T1, T2, T3, T4> : MonoSignalListener where T1 : struct where T2 : struct where T3 : struct where T4 : struct
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
    
    public abstract class MonoSignalListener<T1, T2, T3, T4, T5> : MonoSignalListener where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct
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
    
    public abstract class MonoSignalListener<T1, T2, T3, T4, T5, T6> : MonoSignalListener 
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
}