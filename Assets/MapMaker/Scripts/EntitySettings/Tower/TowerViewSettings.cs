using System;
using Sirenix.OdinInspector;
using Ecs.Modules.PauldokDev.SlotSaver.Core;
using Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace MapMaker.Scripts.EntitySettings.Tower
{
    [Serializable, Toggle("enabled")]
    public class TowerViewSettings
    {
        public bool enabled;
        public AssetReference viewPath;
        [SerializeField, HideInInspector] private GameObject spawnedView;

        public void TryLoadView(SlotEntity slotEntity, Transform transform)
        {
            if (slotEntity.TryGetField(SavePath.View.Tower, out var viewField))
            {
                enabled = true;
                viewPath = viewField.ParseToAssetReference();
                if (slotEntity.TryGetField(SavePath.WorldSpace.Position, out var positionField))
                {
                    transform.position = positionField.ParseVector3();
                }
                
                if (slotEntity.TryGetField(SavePath.WorldSpace.Rotation, out var rotationField))
                {
                    transform.rotation = rotationField.ParseQuaternion();
                }

                LoadView(transform);
            }
            else enabled = false;
        }

        public void TrySaveView(SlotEntity slotEntity, Transform transform)
        {
            if (!enabled) return;
            
            slotEntity.SetField(SavePath.View.Tower, viewPath.AssetGUID);
            slotEntity.SetField(SavePath.WorldSpace.Position, $"{transform.position}");
            if ("(0.00000, 0.00000, 0.00000, 1.00000)" != transform.rotation.ToString())
            {
                slotEntity.SetField(SavePath.WorldSpace.Rotation, $"{transform.rotation}");
            }
        }

        public void Validate(Transform transform)
        {
            if (spawnedView != null) Object.DestroyImmediate(spawnedView);
            LoadView(transform);
        }

        private void LoadView(Transform parent)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(viewPath);
            handle.WaitForCompletion();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var prefab = handle.Result;
                spawnedView = Object.Instantiate(prefab, parent, true);
                spawnedView.transform.localRotation = Quaternion.identity;
                spawnedView.transform.localPosition = Vector3.zero;
            }
            else
            {
                Debug.LogError($"Failed to load asset with address: {viewPath}");
            }
        }
    }
}