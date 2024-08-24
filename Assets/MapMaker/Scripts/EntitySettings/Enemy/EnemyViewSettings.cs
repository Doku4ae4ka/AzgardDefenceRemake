using System;
using Sirenix.OdinInspector;
using Source.Scripts.Extensions;
using Source.Scripts.ProjectLibraries;
using Source.Scripts.SaveSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable, Toggle("enabled")]
    public class EnemyViewSettings
    {
        public bool enabled;
        public EnemyKeys enemyID;

        public void TryGet(Entity entity, Transform transform, EnemyLibrary viewLibrary)
        {
            if (entity.TryGetField(SavePath.View, out var viewField))
            {
                enabled = true;
                enemyID = viewField.ParseEnum<EnemyKeys>();
                if (entity.TryGetField(SavePath.Position, out var positionField))
                {
                    transform.position = positionField.ParseVector3();
                }
                
                if (entity.TryGetField(SavePath.Rotation, out var rotationField))
                {
                    transform.rotation = rotationField.ParseQuaternion();
                }

                Object.Instantiate(viewLibrary.GetByID(enemyID).Enemy.gameObject, transform);
            }
            else enabled = false;
        }

        public void Set(Entity entity, Transform transform)
        {
            entity.SetField(SavePath.View, $"{enemyID}");
            entity.SetField(SavePath.Position, $"{transform.position.ToString()}");
            if ("(0.00000, 0.00000, 0.00000, 1.00000)" != transform.rotation.ToString())
            {
                entity.SetField(SavePath.Rotation, $"{transform.rotation.ToString()}");
            }
        }
    }
}