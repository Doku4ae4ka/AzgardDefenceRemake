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
    public class TowerViewApi
    {
        private AsyncOperationHandle<GameObject> _handle;
        private TowerAsset _towerAsset;
        private Signal _signal;
        private EcsPackedEntity _packedEntity;

        private bool _isAssetLoaded = false;
        private Dictionary<string, Action> _pendingActions = new Dictionary<string, Action>();

        public TowerAsset TowerAsset => _towerAsset;

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

        public void SetTowerPreviewView(float radius)
        {
            ExecuteOrEnqueue("SetTowerPreviewView",() =>
            {
                _towerAsset.SetTowerPreviewView();
                _towerAsset.SetRadius(radius);
            });
        }
        
        public void SetTowerPreviewView()
        {
            ExecuteOrEnqueue("SetTowerPreviewView",() =>
            {
                _towerAsset.SetTowerPreviewView();
            });
        }

        public void SetDefaultView(float radius)
        {
            ExecuteOrEnqueue("SetDefaultView",() =>
            {
                _towerAsset.SetDefault();
                _towerAsset.SetRadius(radius);
            });
        }
        
        public void SetTowerSelectValid()
        {
            ExecuteOrEnqueue("SetTowerSelectColor",() =>
            {
                _towerAsset.SetTowerSelectValid();
            });
        }
        
        public void SetTowerSelectInvalid()
        {
            ExecuteOrEnqueue("SetTowerSelectColor",() =>
            {
                _towerAsset.SetTowerSelectInvalid();
            });
        }
        
        public void SetTowerSelectSelected()
        {
            ExecuteOrEnqueue("SetTowerSelectColor",() =>
            {
                _towerAsset.SetTowerSelectSelected();
            });
        }

        public void SetRadius(float radius)
        {
            ExecuteOrEnqueue("SetRadius",() =>
            {
                _towerAsset.SetRadius(radius);
            });
        }

        public void Hide()
        {
            ExecuteOrEnqueue("Hide",() =>
            {
                IsActivated = false;
                _towerAsset.Deactivate();
            });
        }

        public void Show()
        {
            ExecuteOrEnqueue("Show",() =>
            {
                IsActivated = true;
                _towerAsset.Activate();
            });
        }

        public void SetName(string name)
        {
            ExecuteOrEnqueue("SetName",() =>
            {
                _towerAsset.transform.name = name;
            });
        }

        private void InvokeReadySignal()
        {
            _signal.RegistryRaise(new Signals.OnViewAssetLoaded()
            {
                PackedEntity = _packedEntity,
                Transform = _towerAsset.transform
            });
        }

        public async void LoadView(AssetReference viewId, Signal signal, EcsPackedEntity packedEntity)
        {
            _signal = signal;
            _packedEntity = packedEntity;
            
            var loadResult = await LoadAndInstantiateAsync(viewId);
            _handle = loadResult.handle;

            if ( loadResult.instance == null) return;
            _towerAsset = loadResult.instance.GetComponent<TowerAsset>();
            if (_towerAsset == null)
            {
                Debug.LogError("Component TowerAsset not set to prefab.");
                return;
            }
            
            _isAssetLoaded = true;
            InvokeReadySignal();
            ExecutePendingActions();
        }

        public void Unload()
        {
            if (_towerAsset != null && _towerAsset.gameObject != null)
            {
                Object.Destroy(_towerAsset.gameObject);
                _towerAsset = null;
            }

            if (_handle.IsValid())
            {
                Addressables.Release(_handle);
                _handle = default;
            }
        }

        private async Task<(GameObject instance, AsyncOperationHandle<GameObject> handle)> LoadAndInstantiateAsync(AssetReference address)
        {
            if (string.IsNullOrEmpty(address.AssetGUID))
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