using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.EasyECS.Interfaces;
using Source.Scripts.Data.GamesConfigurations;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.Managers.ProjectSettings;
using Source.SignalSystem;
using UnityEngine;

namespace Source.EasyECS
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private List<BootstrapComponent> _awake = new List<BootstrapComponent>();
        [SerializeField] private List<BootstrapComponent> _start = new List<BootstrapComponent>();
        [ReadOnly]
        [SerializeField] private List<DataPack> bootQueue;
        private Dictionary<Type, DataPack> _sharedData;
        [ReadOnly]
        [SerializeField] private GameShare gameShare;
        [SerializeField] public Signal signal;
        [SerializeField] private GameConfiguration gameConfiguration;
        private Componenter _componenter = new Componenter();

        public IGameShareItem[] GameShareItems => new IGameShareItem[] 
            { signal, _componenter, gameConfiguration };
        
        private Action _onUpdate;
        private Action _onFixedUpdate;
        private Action _onLateUpdate;
        
        private void PreInit(BootstrapComponent component)
        {
            if (!_sharedData.ContainsKey(component.GetType()))
            {
                var newPack = new DataPack(component.GetType(), component);
                component.PreInit(gameShare, newPack);
                _sharedData[component.GetType()] = newPack;

                if (component is IEasyUpdate easyUpdate) _onUpdate += easyUpdate.EasyUpdate;
                if (component is IEasyFixedUpdate easyFixedUpdate) _onFixedUpdate += easyFixedUpdate.EasyFixedUpdate;
                if (component is IEasyLateUpdate easyLateUpdate) _onLateUpdate += easyLateUpdate.EasyLateUpdate;
            }
            
        }

        private void PreInitAll()
        {
            _sharedData = new Dictionary<Type, DataPack>();
            bootQueue = new List<DataPack>();
            gameShare = new GameShare(_sharedData);
            
            foreach (var gameShareItem in GameShareItems)
            {
                gameShare.AddSharedObject(gameShareItem.GetType(), gameShareItem);
            }
            
            foreach (var monoBeh in _awake)
            {
                PreInit(monoBeh);
            }
            
            foreach (var monoBeh in _start)
            {
                PreInit(monoBeh);
            }
        }
        
        private void Awake()
        {
            PreInitAll();
            
            foreach (var initMonoBeh in _awake)
            {
                initMonoBeh.Initialize();
                bootQueue.Add(_sharedData[initMonoBeh.GetType()]);
            }
            
            foreach (var initMonoBeh in _start)
            {
                initMonoBeh.Initialize();
                bootQueue.Add(_sharedData[initMonoBeh.GetType()]);
            }
        }

        private void Update()
        {
            _onUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            _onFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            _onLateUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            _onUpdate = null;
            _onFixedUpdate = null;
            _onLateUpdate = null;
        }

        private void OnValidate()
        {
            gameObject.name = "Bootstrapper";
            transform.position = Vector3.zero;
            if (signal == null) signal = Project.Loader.GetAssetByTypeOnValidate<Signal>();
            if (gameConfiguration == null) gameConfiguration = Project.Loader.GetAssetByTypeOnValidate<GameConfiguration>();
        }
    }
}