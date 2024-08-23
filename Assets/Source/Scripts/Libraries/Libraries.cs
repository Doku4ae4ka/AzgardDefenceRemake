using System;
using Exerussus._1Extensions.SignalSystem;
using UnityEngine;

namespace Source.Scripts.ProjectLibraries
{
    public class Libraries : MonoBehaviour
    {
        [SerializeField] private TowerLibrary towerLibrary;
        
        [SerializeField] private Signal signal;
        
        private KeysHolder _keysHolder = new();
        private ILibrary[] AllLibraries => new ILibrary[] 
            { 
                towerLibrary, 
            };

        public static KeysHolder KeysHolder => Instance._keysHolder;

        public static TowerLibrary TowerLibrary => Instance.towerLibrary;

        public static Signal Signal => Instance.signal;

        public static Libraries Instance { get; private set; }

        public Action OnConfigurationsUpdate;
        
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                UpdateConfigurations();
                _keysHolder.Initialize();
                foreach (var library in AllLibraries) library.Initialize();
            }
            else Destroy(this);
        }

        public void UpdateConfigurations()
        {
            OnConfigurationsUpdate?.Invoke();
        }

        // private void OnValidate()
        // {
        //     if (signal == null) signal = Project.Loader.GetAssetByTypeOnValidate<Signal>();
        //     if (towerLibrary == null) towerLibrary = Project.Loader.GetAssetByTypeOnValidate<TowerLibrary>();
        // }
    }
}