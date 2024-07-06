// ----------------------------------------------------------------------------
// The Proprietary or MIT-Red License
// Copyright (c) 2012-2023 Leopotam <leopotam@yandex.ru>
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Source.EasyECS {
    public interface IEcsSystem { }

    public interface IEcsPreInitSystem : IEcsSystem {
        void PreInit (IEcsSystems systems);
    }

    public interface IEcsInitSystem : IEcsSystem {
        void Init (IEcsSystems systems);
    }

    public interface IEcsRunSystem : IEcsSystem {
        void Run (IEcsSystems systems);
        string GetName() => GetType().Name;
    }

    public interface IEcsPostRunSystem : IEcsSystem {
        void PostRun (IEcsSystems systems);
    }

    public interface IEcsDestroySystem : IEcsSystem {
        void Destroy (IEcsSystems systems);
    }

    public interface IEcsPostDestroySystem : IEcsSystem {
        void PostDestroy (IEcsSystems systems);
    }
    
    [Serializable]
    public class SystemState
    {
        public SystemState(string systemName)
        {
            this.systemName = systemName;
            IsActive = true;
        }
        
        [SerializeField] private string systemName;
        public bool IsActive;

    }
    
    public interface IEcsSystems {
        T GetObsoleteShare<T> () where T : class;
        IEcsSystems AddWorld (EcsWorld world, string name);
        EcsWorld GetWorld (string name = null);
        Dictionary<string, EcsWorld> GetAllNamedWorlds ();
        IEcsSystems Add (IEcsSystem system);
        List<IEcsSystem> GetAllSystems ();
        List<SystemState> SystemsState  { get; }
        
        void Init ();
        void Run ();
        void Destroy ();
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public class EcsSystems : IEcsSystems {
        readonly EcsWorld _defaultWorld;
        readonly Dictionary<string, EcsWorld> _worlds;
        readonly List<IEcsSystem> _allSystems;
        readonly List<IEcsRunSystem> _runSystems;
        readonly List<IEcsPostRunSystem> _postRunSystems;
        private Dictionary<string, SystemState> _systemsSwitcher;
        private GameShare _gameShare;

        public List<SystemState> SystemsState { get; private set; }
        readonly object _shared;
#if DEBUG
        protected bool _inited;
#endif

        public EcsSystems (EcsWorld defaultWorld, GameShare gameShare, object shared = null) {
            _defaultWorld = defaultWorld;
            _shared = shared;
            _worlds = new Dictionary<string, EcsWorld> (8);
            _allSystems = new List<IEcsSystem> (128);
            _runSystems = new List<IEcsRunSystem> (128);
            _postRunSystems = new List<IEcsPostRunSystem> (128);
            _systemsSwitcher = new Dictionary<string, SystemState>();
            SystemsState = new List<SystemState>();
            _gameShare = gameShare;
            
        }
        
        public virtual T GetObsoleteShare<T> () where T : class {
            return _shared as T;
        }

        public virtual IEcsSystems AddWorld (EcsWorld world, string name) {
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
            if (_inited) { throw new System.Exception ("Cant add world after initialization."); }
            if (world == null) { throw new System.Exception ("World cant be null."); }
            if (string.IsNullOrEmpty (name)) { throw new System.Exception ("World name cant be null or empty."); }
            if (_worlds.ContainsKey (name)) { throw new System.Exception ($"World with name \"{name}\" already added."); }
#endif
            _worlds[name] = world;
            return this;
        }

        public virtual EcsWorld GetWorld (string name = null) {
            if (name == null) {
                return _defaultWorld;
            }
            _worlds.TryGetValue (name, out var world);
            return world;
        }

        public virtual Dictionary<string, EcsWorld> GetAllNamedWorlds () {
            return _worlds;
        }
        
        public virtual IEcsSystems Add (IEcsSystem system) {
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
            if (_inited) { throw new System.Exception ("Cant add system after initialization."); }
#endif
            _allSystems.Add (system);
            if (system is IEcsRunSystem runSystem)
            {
                var newSystemState = new SystemState(runSystem.GetName());
                _systemsSwitcher[runSystem.GetName()] = newSystemState;
                SystemsState.Add(newSystemState);
                _runSystems.Add (runSystem);
            }
            if (system is IEcsPostRunSystem postRunSystem) {
                _postRunSystems.Add (postRunSystem);
            }
            return this;
        }

        public virtual List<IEcsSystem> GetAllSystems () {
            return _allSystems;
        }

        public virtual void Init () {
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
            if (_inited) { throw new System.Exception ("Already initialized."); }
#endif
            foreach (var system in _allSystems) {
                if (system is IEcsPreInitSystem initSystem) {
                    initSystem.PreInit (this);
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
                    var worldName = CheckForLeakedEntities (this);
                    if (worldName != null) { throw new System.Exception ($"Empty entity detected in world \"{worldName}\" after {initSystem.GetType ().Name}.PreInit()."); }
#endif
                }
            }
            foreach (var system in _allSystems) {
                if (system is IEcsInitSystem initSystem) {
                    initSystem.Init (this);
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
                    var worldName = CheckForLeakedEntities (this);
                    if (worldName != null) { throw new System.Exception ($"Empty entity detected in world \"{worldName}\" after {initSystem.GetType ().Name}.Init()."); }
#endif
                }
            }
#if DEBUG
            _inited = true;
#endif
        }

        public virtual void Run () {
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
            if (!_inited) { throw new System.Exception ("Cant run without initialization."); }
#endif
            for (int i = 0, iMax = _runSystems.Count; i < iMax; i++) {
                if (_systemsSwitcher.ContainsKey(_runSystems[i].GetName()) && _systemsSwitcher[_runSystems[i].GetName()].IsActive)
                _runSystems[i].Run (this);
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
                var worldName = CheckForLeakedEntities (this);
                if (worldName != null) { throw new System.Exception ($"Empty entity detected in world \"{worldName}\" after {_runSystems[i].GetType ().Name}.Run()."); }
#endif
            }
            for (int i = 0, iMax = _postRunSystems.Count; i < iMax; i++) {
                _postRunSystems[i].PostRun (this);
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
                var worldName = CheckForLeakedEntities (this);
                if (worldName != null) { throw new System.Exception ($"Empty entity detected in world \"{worldName}\" after {_postRunSystems[i].GetType ().Name}.PostRun()."); }
#endif
            }
        }

        public virtual void Destroy () {
            for (var i = _allSystems.Count - 1; i >= 0; i--) {
                if (_allSystems[i] is IEcsDestroySystem destroySystem) {
                    destroySystem.Destroy (this);
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
                    var worldName = CheckForLeakedEntities (this);
                    if (worldName != null) { throw new System.Exception ($"Empty entity detected in world \"{worldName}\" after {destroySystem.GetType ().Name}.Destroy()."); }
#endif
                }
            }
            for (var i = _allSystems.Count - 1; i >= 0; i--) {
                if (_allSystems[i] is IEcsPostDestroySystem postDestroySystem) {
                    postDestroySystem.PostDestroy (this);
#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
                    var worldName = CheckForLeakedEntities (this);
                    if (worldName != null) { throw new System.Exception ($"Empty entity detected in world \"{worldName}\" after {postDestroySystem.GetType ().Name}.PostDestroy()."); }
#endif
                }
            }
            _worlds.Clear ();
            _allSystems.Clear ();
            _runSystems.Clear ();
            _postRunSystems.Clear ();
#if DEBUG
            _inited = false;
#endif
        }

#if DEBUG && !LEOECSLITE_NO_SANITIZE_CHECKS
        public static string CheckForLeakedEntities (IEcsSystems systems) {
            if (systems.GetWorld ().CheckForLeakedEntities ()) { return "default"; }
            foreach (var pair in systems.GetAllNamedWorlds ()) {
                if (pair.Value.CheckForLeakedEntities ()) {
                    return pair.Key;
                }
            }
            return null;
        }
#endif
    }
}