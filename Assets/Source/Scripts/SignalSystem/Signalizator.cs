
using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Source.Scripts.Managers;
using Source.Scripts.Managers.ProjectSettings;
using UnityEngine;

namespace Source.SignalSystem
{
    public class Signalizator : MonoBehaviour
    {
        [SerializeField] private bool isUpdateOnValidate;
        [SerializeField] private float tickTime = 1f;
        [Space]
        [SerializeField, GUIColor("@OffColor")] private List<SignalizeListener> off = new List<SignalizeListener>();
        [Space]
        [SerializeField, GUIColor("@OnColor")] private List<SignalizeListener> on = new List<SignalizeListener>();
        private Signal _signal;
        [SerializeField] private Canvas canvas;
        
        private Color OffColor => new Color(1, .9f, .9f); 
        private Color OnColor => new Color(.8f, 1f, .8f);
        private Color DefaultColor => new Color(.8f, .3f, .7f); 
        private Color NormalColor => new Color(1, 1f, 1f);
        private float _tickTimer;
        private ISignalizeOnUpdate[] _fixedUpdateGroup;
        private ISignalizeOnTick[] _tickGroup;
        private List<List<SignalizeListener>> AllPacks => new ()
        {
            off,
            on,
        };

        private void Awake()
        {
            foreach (var packs in AllPacks)
            {
                foreach (var signalizeListener in packs)
                {
                    signalizeListener.PreInit();
                }
            }

            UpdateGroups();
            
            foreach (var packs in AllPacks)
            {
                foreach (var signalizeListener in packs)
                {
                    signalizeListener.Initialize();
                }
            }
        }

        private void UpdateGroups()
        {
            var updateList = new List<ISignalizeOnUpdate>();
            var tickList = new List<ISignalizeOnTick>();
            foreach (var signalizeListener in on)
            {
                if (signalizeListener is ISignalizeOnUpdate updateListener) updateList.Add(updateListener);
                if (signalizeListener is ISignalizeOnTick tickListener) tickList.Add(tickListener);
            }

            _fixedUpdateGroup = updateList.ToArray();
            _tickGroup = tickList.ToArray();
        }

        private void FixedUpdate()
        {
            _tickTimer += Time.fixedDeltaTime;
            InvokeUpdate();
            
            if (_tickTimer > tickTime)
            {
                _tickTimer -= tickTime;
                InvokeTick();
            }
        }

        private void InvokeUpdate()
        {
            if (_fixedUpdateGroup.Length > 0) 
                foreach (var listener in _fixedUpdateGroup) listener.OnUpdate(Time.fixedDeltaTime);
        }

        private void InvokeTick()
        {
            if (_tickGroup.Length > 0) 
                foreach (var listener in _tickGroup) listener.OnTick(tickTime);
        }
        
        private void FindEasySystemClasses()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<string> foundTypeNames = new List<string>();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(SignalizeListener)) && !type.IsAbstract)
                    {
                        TryCreateNewListener(type);
                    }
                }
            }
        }

        private void TryCreateNewListener(Type type)
        {
            var allSignalizeListener = GetComponentsInChildren(type);
            if (allSignalizeListener != null && allSignalizeListener.Length > 0) return;
            
            var newGameObject = new GameObject
            {
                name = type.Name,
                transform = { parent = transform }
            };
            
            var listener = (SignalizeListener)newGameObject.AddComponent(type);
            listener.Validate(_signal, canvas.transform);
            AddToGroup(listener);
        }

        private void RemoveFromAll(SignalizeListener listener)
        {
            for (int ip = AllPacks.Count - 1; ip >= 0; ip--)
            {
                var pack = AllPacks[ip];
                for (int i = pack.Count - 1; i >= 0; i--)
                {
                    var signalizeListener = pack[i];
                    if (signalizeListener == listener) pack.Remove(listener);
                }
            }
        }
        
        private void AddToGroup(SignalizeListener listener)
        {
            RemoveFromAll(listener);
            switch (listener.initializeType)
            {
                case InitializeType.Off:
                    off.Add(listener);
                    break;
                case InitializeType.On:
                    on.Add(listener);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        [Button]
        private void UpdateListeners()
        {
            DelGameObjects();
            FindEasySystemClasses();
            RefreshActivated();
        }

        private void RefreshActivated()
        {
            foreach (var pack in AllPacks)
            {
                for (var index = pack.Count - 1; index >= 0; index--)
                {
                    var listener = pack[index];
                    if (listener == null) pack.Remove(listener);
                }
            }

            foreach (var signalizeListener in off) signalizeListener.initializeType = InitializeType.Off;
            foreach (var signalizeListener in on) signalizeListener.initializeType = InitializeType.On;
        }

        private void DelGameObjects()
        {
            
            var allGameObjects = GetComponentsInChildren<Transform>();

            for (var index = allGameObjects.Length - 1; index >= 0; index--)
            {
                var gObject = allGameObjects[index].gameObject;
                if (gObject.GetComponent<Signalizator>() != null) continue;
                if (gObject.GetComponent<SignalizeListener>() == null) DestroyImmediate(gObject);
            }
        }
        
        private void OnValidate()
        {
            if (_signal == null) _signal = Project.Loader.GetAssetByTypeOnValidate<Signal>();
            if (isUpdateOnValidate)
            {
                FindEasySystemClasses();
                RefreshActivated();
            }
            foreach (var packs in AllPacks)
            {
                foreach (var signalizeListener in packs)
                {
                    signalizeListener.Validate(_signal, canvas.transform);
                }
            }
        }
        public enum InitializeType
        {
            Off,
            On
        }
    }
    
    /// <summary>
    /// Вызывается на FixedUpdate
    /// </summary>
    public interface ISignalizeOnUpdate
    {
        public void OnUpdate(float deltaTime);
    }
    
    /// <summary>
    /// Вызывается на тике
    /// </summary>
    public interface ISignalizeOnTick
    {
        public void OnTick(float deltaTime);
    }
}