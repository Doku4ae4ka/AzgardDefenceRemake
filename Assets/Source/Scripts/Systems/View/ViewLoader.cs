
using System;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Libraries;
using Source.Scripts.SaveSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Systems.View
{
    public static class ViewLoader
    {
        public static void LoadViewPrototype(EcsWorld world, Pooler pooler, Slot slot, ViewLibrary viewLibrary)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);

                if (!savingEntity.TryGetField(SavePath.View, out var viewValue)) continue;
                
                ref var prototypeData =ref pooler.Prototype.Get(entity);
                
                Action<int> buildAction = (int newEntity) =>
                {
                    ref var entityData = ref pooler.Entity.Get(newEntity);
                    ref var viewData = ref pooler.View.Add(newEntity);
                    ref var transformData = ref pooler.Transform.Add(newEntity);
                    viewData.ViewId = viewValue;
                    viewData.Value = Object.Instantiate(viewLibrary.GetViewByID(viewValue).gameObject).GetComponent<MonoBehaviours.View>();
                    viewData.Value.gameObject.name = entityData.EntityID;
                    transformData.Value = viewData.Value.transform;
                };
                
                buildAction.Invoke(entity);
                ref var viewData = ref pooler.View.Get(entity);
                
                Object.Destroy(viewData.Value.gameObject);
                
                prototypeData.DataBuilder.Add(buildAction);
            }
        }
        
        public static void LoadViewDynamic(EcsWorld world, Pooler pooler, Slot slot, ViewLibrary viewLibrary)
        {
            var spawnOffset = new Vector3(0, 0.03f, 0);
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.DynamicMark>().Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);

                if (!savingEntity.TryGetField(SavePath.View, out var viewValue)) continue;
                
                ref var viewData = ref pooler.View.AddOrGet(entity);
                ref var transformData = ref pooler.Transform.AddOrGet(entity);
                viewData.ViewId = viewValue;
                viewData.Value = Object.Instantiate(viewLibrary.GetViewByID(viewValue).gameObject).GetComponent<MonoBehaviours.View>();
                viewData.Value.gameObject.name = entityData.EntityID;
                transformData.Value = viewData.Value.transform;
                
                if (savingEntity.TryGetVector3Field(SavePath.Position, out var positionValue)) 
                {
                    transformData.Value.position = positionValue + spawnOffset;
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.Rotation, out var rotationValue))
                {
                    transformData.Value.rotation = rotationValue;
                }
            }
        }
        
        public static void LoadViewStatic(EcsWorld world, Pooler pooler, Slot slot, ViewLibrary viewLibrary)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Enemy>().Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetField(SavePath.View, out var viewValue)) continue;
                
                ref var viewData = ref pooler.View.AddOrGet(entity);
                ref var transformData = ref pooler.Transform.AddOrGet(entity);
                viewData.ViewId = viewValue;
                viewData.Value = Object.Instantiate(viewLibrary.GetViewByID(viewValue).gameObject).GetComponent<MonoBehaviours.View>();
                viewData.Value.gameObject.name = entityData.EntityID;
                transformData.Value = viewData.Value.transform;
                
                if (savingEntity.TryGetVector3Field(SavePath.Position, out var positionValue)) 
                {
                    transformData.Value.position = positionValue;
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.Rotation, out var rotationValue))
                {
                    transformData.Value.rotation = rotationValue;
                }
            }
        }
    }
}