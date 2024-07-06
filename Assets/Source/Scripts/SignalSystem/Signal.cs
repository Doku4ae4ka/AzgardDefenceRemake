using System;
using System.Collections.Generic;
using Source.EasyECS;
using UnityEngine;

namespace Source.SignalSystem
{
    [CreateAssetMenu(menuName = "SignalSystem")]
    public class Signal : ScriptableObject, IGameShareItem
    {
        private Dictionary<Type, List<object>> _listeners = new Dictionary<Type, List<object>>();
        private List<Type> _listenersBroadcast = new();

        /// <summary>
        /// Вызывает сигнал 
        /// </summary>
        public void RegistryRaise<T>(T data) where T : struct, ISignal
        {
            var type = typeof(T);
            // Debug.Log($"{typeof(T)}");
            TryCreateIfNotExist<T>();
            var actions = _listeners[type];

            for (var index = actions.Count - 1; index >= 0; index--)
            {
                var action = actions[index];
                var actionTyped = action as Action<T>;
                actionTyped?.Invoke(data);
            }
        }

        public void Subscribe<T>(Action<T> action) where T : struct
        {
            var type = typeof(T);
            TryCreateIfNotExist<T>();
            _listeners[type].Add(action);
        }
        

        public void Unsubscribe<T>(Action<T> action) where T : struct
        {
            var type = typeof(T);
            if (_listeners.TryGetValue(type, out var actions))
            {
                actions.Remove(action);
            }
        }
        
        private void TryCreateIfNotExist<T>() where T : struct
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
            {
                _listeners.Add(type, new List<object>());
            }
        }
    }

    public interface ISignal
    {
        
    }
}