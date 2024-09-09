using System;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.MonoBehaviours.Views;
using Source.Scripts.SaveSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Systems.View
{
    public static class ViewLoader
    {
        #region Prototypes
        
        public static void LoadViewPrototype(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            LoadTowerViewPrototype(world, pooler, slot, signal);
            LoadEnemyViewPrototype(world, pooler, slot, signal);
        }
        public static void LoadTowerViewPrototype(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().Inc<EcsData.Tower>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);

                if (!savingEntity.TryGetField(SavePath.View.Tower, out var viewValue)) continue;
                
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                
                Action<int> buildAction = (int newEntity) =>
                {
                    ref var entityData = ref pooler.Entity.Get(newEntity);
                    ref var viewData = ref pooler.TowerView.Add(newEntity);
                    pooler.Position.Add(newEntity);
                    pooler.Rotation.Add(newEntity);
                    viewData.ViewId = viewValue;
                    viewData.Value = new TowerViewApi();
                    viewData.Value.LoadView(viewValue, signal, world.PackEntity(newEntity));
                    viewData.Value.SetName(entityData.EntityID);
                };
                
                buildAction.Invoke(entity);
                ref var viewData = ref pooler.TowerView.Get(entity);
                
                Object.Destroy(viewData.Value.TowerAsset.gameObject);
                
                prototypeData.DataBuilder.Add(buildAction);
            }
        }
        
        public static void LoadEnemyViewPrototype(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().Inc<EcsData.Enemy>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);

                if (!savingEntity.TryGetField(SavePath.View.Enemy, out var viewValue)) continue;
                
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                
                Action<int> buildAction = (int newEntity) =>
                {
                    ref var entityData = ref pooler.Entity.Get(newEntity);
                    ref var viewData = ref pooler.EnemyView.Add(newEntity);
                    pooler.Position.Add(newEntity);
                    pooler.Rotation.Add(newEntity);
                    viewData.ViewId = viewValue;
                    viewData.Value = new EnemyViewApi();
                    viewData.Value.LoadView(viewValue, signal, world.PackEntity(newEntity));
                    viewData.Value.SetName(entityData.EntityID);
                };
                
                buildAction.Invoke(entity);
                ref var viewData = ref pooler.EnemyView.Get(entity);
                
                Object.Destroy(viewData.Value.EnemyAsset.gameObject);
                
                prototypeData.DataBuilder.Add(buildAction);
            }
        }
        
        #endregion

        #region Dynamic
        
        public static void LoadViewDynamic(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            LoadTowerView(world, pooler, slot, signal);
            LoadEnemyView(world, pooler, slot, signal);
        }
        
        public static void LoadTowerView(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Tower>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetField(SavePath.View.Tower, out var viewValue)) continue;
                
                ref var viewData = ref pooler.TowerView.Add(entity);
                pooler.Position.Add(entity);
                pooler.Rotation.Add(entity);
                viewData.ViewId = viewValue;
                viewData.Value = new TowerViewApi();
                viewData.Value.LoadView(viewValue, signal, world.PackEntity(entity));
                viewData.Value.SetName(entityData.EntityID);
                
                ref var positionData = ref pooler.Position.Add(entity);
                ref var rotationData = ref pooler.Rotation.Add(entity);
                if (savingEntity.TryGetVector3Field(SavePath.WorldSpace.Position, out Vector3 positionValue)) 
                {
                    positionData.Value = positionValue;
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.WorldSpace.Rotation, out var rotationValue))
                {
                    rotationData.Value = rotationValue;
                }
            }
        }
        
        public static void LoadEnemyView(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Enemy>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetField(SavePath.View.Enemy, out var viewValue)) continue;
                
                ref var viewData = ref pooler.EnemyView.Add(entity);
                pooler.Position.Add(entity);
                pooler.Rotation.Add(entity);
                viewData.ViewId = viewValue;
                viewData.Value = new EnemyViewApi();
                viewData.Value.LoadView(viewValue, signal, world.PackEntity(entity));
                viewData.Value.SetName(entityData.EntityID);
                
                ref var positionData = ref pooler.Position.Add(entity);
                ref var rotationData = ref pooler.Rotation.Add(entity);
                if (savingEntity.TryGetVector3Field(SavePath.WorldSpace.Position, out Vector3 positionValue)) 
                {
                    positionData.Value = positionValue;
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.WorldSpace.Rotation, out var rotationValue))
                {
                    rotationData.Value = rotationValue;
                }
            }
        }
        
        #endregion

        #region Static

        public static void LoadViewStatic(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            LoadEnvironmentView(world, pooler, slot, signal);
        }
        
        private static void LoadEnvironmentView(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Environment>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetField(SavePath.View.Environment, out var viewValue)) continue;
                
                ref var viewData = ref pooler.EnvironmentView.Add(entity);
                pooler.Position.Add(entity);
                pooler.Rotation.Add(entity);
                viewData.ViewId = viewValue;
                viewData.Value = new EnvironmentViewApi();
                viewData.Value.LoadView(viewValue, signal, world.PackEntity(entity));
                viewData.Value.SetName(entityData.EntityID);
                
                ref var positionData = ref pooler.Position.Add(entity);
                ref var rotationData = ref pooler.Rotation.Add(entity);
                if (savingEntity.TryGetVector3Field(SavePath.WorldSpace.Position, out Vector3 positionValue)) 
                {
                    positionData.Value = positionValue;
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.WorldSpace.Rotation, out var rotationValue))
                {
                    rotationData.Value = rotationValue;
                }
            }
        }
        
        #endregion
        
    }
}