using System;
using Sirenix.OdinInspector;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace MapMaker.Scripts.EntitySettings.Environment
{
    [Serializable, Toggle("enabled")]
    public class EnvironmentViewSettings
    {
        public bool enabled;
        public AssetReference viewPath;
        [SerializeField, HideInInspector] private GameObject spawnedView;

        public void TryLoadView(Entity entity, Transform transform)
        {
            if (entity.TryGetField(SavePath.View.Environment, out var viewField))
            {
                enabled = true;
                viewPath = viewField.ParseToAssetReference();
                if (entity.TryGetField(SavePath.WorldSpace.Position, out var positionField))
                {
                    transform.position = positionField.ParseVector3();
                }
                
                if (entity.TryGetField(SavePath.WorldSpace.Rotation, out var rotationField))
                {
                    transform.rotation = rotationField.ParseQuaternion();
                }

                LoadView(transform);
            }
            else enabled = false;
        }

        public void TrySaveView(Entity entity, Transform transform)
        {
            if (!enabled) return;
            
            entity.SetField(SavePath.View.Environment, viewPath.AssetGUID);
            entity.SetField(SavePath.WorldSpace.Position, $"{transform.position}");
            if ("(0.00000, 0.00000, 0.00000, 1.00000)" != transform.rotation.ToString())
            {
                entity.SetField(SavePath.WorldSpace.Rotation, $"{transform.rotation}");
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