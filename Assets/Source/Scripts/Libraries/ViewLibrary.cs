
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.MonoBehaviours;
using UnityEngine;

namespace Source.Scripts.Libraries
{
    public class ViewLibrary : ScriptableObject, IGameCoreConfig
    {
        [SerializeField] private List<View> views;
        private Dictionary<string, View> _viewDict;
        private bool _isInitialized;
        
        private void Initialize()
        {
            _isInitialized = true;
            _viewDict = new();
            
            if (views.Count > 0)
            {
                foreach (var view in views) _viewDict[view.viewId] = view;
            }
        }

        public View GetViewByID(string viewValue)
        {
            if (!_isInitialized || _viewDict == null) Initialize();
            if (_viewDict.TryGetValue(viewValue, out var value)) return value;
            var log = $"Невозможно найти {viewValue} в view библиотеке.\nКлючи в библиотеке: ";
            var count = 0;
            foreach (var key in _viewDict.Keys)
            {
                if (count > 0) log += ", ";
                log += key;
                count++;
            }
            Debug.Log(log);
            return _viewDict[viewValue];
        }

        [Button]
        public void ReInitialized()
        {
            Initialize();
        }
    }
}