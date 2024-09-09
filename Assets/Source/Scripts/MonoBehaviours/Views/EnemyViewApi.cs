using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.MonoBehaviours.AssetApis;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Source.Scripts.MonoBehaviours.Views
{
    public class EnemyViewApi
    {
        private AsyncOperationHandle<GameObject> _handle;
        private EnemyAsset _enemyAsset;
        private Signal _signal;
        private EcsPackedEntity _packedEntity;

        private bool _isAssetLoaded = false;
        private Dictionary<string, Action> _pendingActions = new Dictionary<string, Action>();

        public EnemyAsset EnemyAsset => _enemyAsset;

        public bool IsActivated { get; private set; }

        // Если ассет не загружен, откладываем действия
        private void ExecuteOrEnqueue(string actionKey, Action action)
        {
            if (_isAssetLoaded)
            {
                action?.Invoke();
            }
            else
            {
                _pendingActions[actionKey] = action;
            }
        }

        // Выполняем все отложенные действия
        private void ExecutePendingActions()
        {
            foreach (var action in _pendingActions.Values)
            {
                action();
            }
            _pendingActions.Clear();
        }
        
        public void Hide()
        {
            ExecuteOrEnqueue("Hide",() =>
            {
                IsActivated = false;
                _enemyAsset.Deactivate();
            });
        }

        public void Show(string viewId, Signal signal, EcsPackedEntity packedEntity)
        {
            if (_isAssetLoaded)
            {
                IsActivated = true;
                _enemyAsset.Activate();
            }
            else
            {
                LoadView(viewId, signal, packedEntity);
            }
        }

        public void SetName(string name)
        {
            ExecuteOrEnqueue("SetName",() =>
            {
                _enemyAsset.transform.name = name;
            });
        }

        private void InvokeReadySignal()
        {
            _signal.RegistryRaise(new Signals.OnViewAssetLoaded()
            {
                PackedEntity = _packedEntity,
                Transform = _enemyAsset.transform
            });
        }

        public async void LoadView(string viewId, Signal signal, EcsPackedEntity packedEntity)
        {
            _signal = signal;
            _packedEntity = packedEntity;
            
            var loadResult = await LoadAndInstantiateAsync(viewId);
            _handle = loadResult.handle;

            if ( loadResult.instance == null) return;
            _enemyAsset = loadResult.instance.GetComponent<EnemyAsset>();

            _isAssetLoaded = true;
            InvokeReadySignal();
            ExecutePendingActions();
        }

        public void Unload()
        {
            if (_enemyAsset != null && _enemyAsset.gameObject != null)
            {
                Object.Destroy(_enemyAsset.gameObject);
                _enemyAsset = null;
            }

            if (_handle.IsValid())
            {
                Addressables.Release(_handle);
                _handle = default;
            }
        }

        private async Task<(GameObject instance, AsyncOperationHandle<GameObject> handle)> LoadAndInstantiateAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                Debug.LogError("Address is null or empty.");
                return (null, default);
            }

            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var prefab = handle.Result;
                var instance = Object.Instantiate(prefab);
                return (instance, handle);
            }
            else
            {
                Debug.LogError($"Failed to load asset with address: {address}");
                return (null, default);
            }
        }
    }
}