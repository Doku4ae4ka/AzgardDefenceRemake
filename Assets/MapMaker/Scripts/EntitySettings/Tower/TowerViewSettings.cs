using System;
using Sirenix.OdinInspector;
using Source.Scripts.Extensions;
using Source.Scripts.ProjectLibraries;
using Source.Scripts.SaveSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MapMaker.Scripts
{
    [Serializable, Toggle("enabled")]
    public class TowerViewSettings
    {
        public bool enabled;
        public TowerKeys towerID;

        public void TryGet(Entity entity, Transform transform, TowerLibrary viewLibrary)
        {
            if (entity.TryGetField(SavePath.View, out var viewField))
            {
                enabled = true;
                towerID = viewField.ParseEnum<TowerKeys>();
                if (entity.TryGetField(SavePath.Position, out var positionField))
                {
                    transform.position = positionField.ParseVector3();
                }
                
                if (entity.TryGetField(SavePath.Rotation, out var rotationField))
                {
                    transform.rotation = rotationField.ParseQuaternion();
                }

                Object.Instantiate(viewLibrary.GetByID(towerID).Tower.gameObject, transform);
            }
            else enabled = false;
        }

        public void Set(Entity entity, Transform transform)
        {
            entity.SetField(SavePath.View, $"{towerID}");
            entity.SetField(SavePath.Position, $"{transform.position.ToString()}");
            if ("(0.00000, 0.00000, 0.00000, 1.00000)" != transform.rotation.ToString())
            {
                entity.SetField(SavePath.Rotation, $"{transform.rotation.ToString()}");
            }
        }
    }
}