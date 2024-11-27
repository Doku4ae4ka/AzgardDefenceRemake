using System;
using Exerussus._1Extensions.Abstractions;
using Exerussus._1Extensions.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Core
{
    /// <summary>
    /// Контейнер для всех конфигов и библиотек.
    /// </summary>
    public class DependenciesContainer : MonoBehaviour
    {
        [SerializeField] private SignalHandler _signalHandler;

        private static DependenciesContainer _instance;
        private static bool _isInitialized;

        private static DependenciesContainer Instance
        {
            get
            {
                if (_isInitialized) return _instance;

                _instance = FindObjectOfType<DependenciesContainer>();
                if (_instance == null) throw new Exception("DependenciesContainer not found in scene.");
                _instance.Initialize();
                return _instance;
            }
        }

        private void Awake()
        {
            Initialize();
        }
        

        public static SignalHandler SignalHandler
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying) return GetAsset<SignalHandler>(Constants.Configs.AssetPath.SignalHandler);
#endif
                return Instance._signalHandler;

            }
        }

        private void Initialize()
        {
            if (_isInitialized) return;
            _instance = this;
            _isInitialized = true;
            DontDestroyOnLoad(gameObject);
            _signalHandler.Initialize();
        }

        private static T GetAsset<T>(string path) where T : ScriptableObject
        {
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
#else
            return null;
#endif
        }

        private static T GetLibrary<T>(string path) where T : ScriptableObject, IInitializable
        {
#if UNITY_EDITOR
            var library = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            library.Initialize();
            return library;
#else
            return null;
#endif
        }



#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoad]
        private class StaticCleaner
        {
            static StaticCleaner()
            {
                UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            }

            private static void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange state)
            {
                if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode ||
                    state == UnityEditor.PlayModeStateChange.ExitingEditMode)
                {
                    _instance = null;
                    _isInitialized = false;
                }
            }
        }
#endif
    }
}