using System;
using Sirenix.OdinInspector;
using Source.Scripts.Extensions;
using Source.Scripts.Libraries;
using Source.Scripts.SaveSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MapMaker.Scripts
{
    [Serializable, Toggle("enabled")]
    public class ViewSettings
    {
        public bool enabled;
        public string viewPath;
        [SerializeField, HideInInspector] private GameObject spawnedView;

        public void TryLoad(Entity entity, Transform transform, ViewLibrary viewLibrary)
        {
            if (entity.TryGetField(SavePath.View, out var viewField))
            {
                enabled = true;
                viewPath = viewField;
                if (entity.TryGetField(SavePath.Position, out var positionField))
                {
                    transform.position = positionField.ParseVector3();
                }
                
                if (entity.TryGetField(SavePath.Rotation, out var rotationField))
                {
                    transform.rotation = rotationField.ParseQuaternion();
                }

                LoadView(transform, viewLibrary);
            }
            else enabled = false;
        }

        public void TrySave(Entity entity, Transform transform)
        {
            if (!enabled) return;
            
            entity.SetField(SavePath.View, $"{viewPath}");
            entity.SetField(SavePath.Position, $"{transform.position.ToString()}");
            if ("(0.00000, 0.00000, 0.00000, 1.00000)" != transform.rotation.ToString())
            {
                entity.SetField(SavePath.Rotation, $"{transform.rotation.ToString()}");
            }
        }

        public void Validate(Transform transform, ViewLibrary viewLibrary)
        {
            if (spawnedView != null) Object.DestroyImmediate(spawnedView);
            LoadView(transform, viewLibrary);
        }

        private void LoadView(Transform parent, ViewLibrary viewLibrary)
        {
            spawnedView = Object.Instantiate(viewLibrary.GetViewByID(viewPath).gameObject, parent);
            spawnedView.transform.localRotation = Quaternion.identity;
            spawnedView.transform.localPosition = Vector3.zero;
        }
    }
}