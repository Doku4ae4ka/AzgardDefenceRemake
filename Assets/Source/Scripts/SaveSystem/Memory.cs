using System;
using Exerussus._1Extensions;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public class Memory
    {
        public Saver save;
        public Loader load;
        
        public static Entity GetOrCreateEntityOnSave(Pooler pooler, Slot slot, ref EcsData.Entity entityData, int entity)
        {
            if (slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return foundEntity;

            var savingEntity = new Entity(entityData.EntityID, entityData.Category);
            if (pooler.Tower.Has(entity) ||
                pooler.Enemy.Has(entity) ||
                pooler.Camera.Has(entity)) slot.AddDynamic(savingEntity);
            else if (pooler.Environment.Has(entity)) slot.AddStatic(savingEntity);

            return savingEntity;
        }
    }

    [Serializable]
    public class Saver
    {
        public event Action<EcsWorld, Pooler, Slot> OnStart;
        public event Action<EcsWorld, Pooler, Slot> OnConfigs;
        public event Action<EcsWorld, Pooler, Slot> OnPrototypes;
        public event Action<EcsWorld, Pooler, Slot> OnStatic;
        public event Action<EcsWorld, Pooler, Slot> OnDynamic;
        public event Action<EcsWorld, Pooler, Slot> OnFinish;

        public void Invoke(EcsWorld world, Pooler pooler, Slot slot)
        {
            OnStart?.Invoke(world, pooler, slot);
            
            slot.Clear();
            
            InitializeConfigs(world, pooler, slot);
            InitializePrototypes(world, pooler, slot);
            InitializeStatic(world, pooler, slot);
            InitializeDynamic(world, pooler, slot);
            
            OnConfigs?.Invoke(world, pooler, slot);
            OnPrototypes?.Invoke(world, pooler, slot);
            OnStatic?.Invoke(world, pooler, slot);
            OnDynamic?.Invoke(world, pooler, slot);
            
            OnFinish?.Invoke(world, pooler, slot);
        }

        #region Static Methods

        private void InitializeDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.DynamicMark>()
                         .Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddDynamic(newEntity);
            }
        }

        private void InitializeStatic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.StaticMark>()
                         .Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddStatic(newEntity);
            }
        }

        private void InitializePrototypes(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddPrototype(newEntity);
                
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                var field = newEntity.GetOrCreateFieldIteration(SavePath.Prototype.Category);
                field.value = prototypeData.Category;
            }
        }

        private void InitializeConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Config>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.CreateConfig(newEntity);
            }
        }

        #endregion
    }

    [Serializable]
    public class Loader
    {
        public event Action<EcsWorld, Pooler, Slot> OnStart;
        public event Action<EcsWorld, Pooler, Slot> OnConfigs;
        public event Action<EcsWorld, Pooler, Slot> OnPrototypes;
        public event Action<EcsWorld, Pooler, Slot> OnStatic;
        public event Action<EcsWorld, Pooler, Slot> OnDynamic;
        public event Action<EcsWorld, Pooler, Slot> OnFinish;
        
        public void Invoke(EcsWorld world, Pooler pooler, Slot slot, Prototypes prototypes)
        {
            OnStart?.Invoke(world, pooler, slot);
            
            UnloadAllEntities(world, pooler);
            InitializeConfigs(world, pooler, slot);
            OnConfigs?.Invoke(world, pooler, slot);
            
            InitializePrototypes(world, pooler, slot, prototypes);
            OnPrototypes?.Invoke(world, pooler, slot);
            
            InitializeStatic(world, pooler, slot);
            OnStatic?.Invoke(world, pooler, slot);
            
            InitializeDynamic(world, pooler, slot);
            OnDynamic?.Invoke(world, pooler, slot);
            
            OnFinish?.Invoke(world, pooler, slot);
        }

        #region Static Methods

        private static void UnloadAllEntities(EcsWorld world, Pooler pooler)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().End())
            {
                if (pooler.TowerView.Has(entity))
                {
                    var view = pooler.TowerView.Get(entity).Value;
                    // сделать возврат в пул вместо уничтожения
                    if (view != null) ProjectTask.TestCode(() => { Object.Destroy(view.TowerAsset.gameObject); });
                }
                
                if (pooler.EnemyView.Has(entity))
                {
                    var view = pooler.EnemyView.Get(entity).Value;
                    // сделать возврат в пул вместо уничтожения
                    if (view != null) ProjectTask.TestCode(() => { Object.Destroy(view.EnemyAsset.gameObject); });
                }
                
                if (pooler.EnvironmentView.Has(entity))
                {
                    var view = pooler.EnvironmentView.Get(entity).Value;
                    // сделать возврат в пул вместо уничтожения
                    if (view != null) ProjectTask.TestCode(() => { Object.Destroy(view.EnvironmentAsset.gameObject); });
                }
                
                world.DelEntity(entity);
            }
        }
        
        private static void InitializeDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var dynamicEntity in slot.Dynamics)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = dynamicEntity.id;
                entityData.Category = dynamicEntity.category;

                pooler.DynamicMark.Add(entity);
                switch (entityData.Category)
                {
                    case SavePath.EntityCategory.Tower:
                        pooler.Tower.Add(entity);
                        break;
                    case SavePath.EntityCategory.Enemy:
                        pooler.Enemy.Add(entity);
                        break;
                    case SavePath.EntityCategory.Level:
                        pooler.Level.Add(entity);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void InitializeStatic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var environmentEntity in slot.Statics)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = environmentEntity.id;
                entityData.Category = environmentEntity.category;

                pooler.StaticMark.Add(entity);
                switch (entityData.Category)
                {
                    case SavePath.EntityCategory.Environment:
                        pooler.Environment.Add(entity);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void InitializePrototypes(EcsWorld world, Pooler pooler, Slot slot, Prototypes prototypes)
        {
            foreach (var prototypeEntity in slot.Prototypes)
            {
                var entity = world.NewEntity();
                prototypes.Add(prototypeEntity.id, entity);

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = prototypeEntity.id;
                entityData.Category = prototypeEntity.category;
                
                ref var prototypeData = ref pooler.Prototype.Add(entity);
                prototypeData.Category = prototypeEntity.GetField(SavePath.Prototype.Category);
                prototypeData.DataBuilder = new();
                
                Action<int> buildAction = null;
                switch (prototypeData.Category)
                {
                    case SavePath.EntityCategory.Tower:
                        buildAction = (int newEntity) =>
                        {
                            pooler.DynamicMark.Add(newEntity);
                            pooler.Tower.Add(newEntity);
                        };
                        break;
                    case SavePath.EntityCategory.Enemy:
                        buildAction = (int newEntity) =>
                        {
                            pooler.DynamicMark.Add(newEntity);
                            pooler.Enemy.Add(newEntity);
                        };
                        break;
                    case SavePath.EntityCategory.Environment:
                        buildAction = (int newEntity) =>
                        {
                            pooler.StaticMark.Add(newEntity);
                            pooler.Environment.Add(newEntity);
                        };
                        break;
                    case SavePath.EntityCategory.Camera:
                        buildAction = (int newEntity) =>
                        {
                            pooler.DynamicMark.Add(newEntity);
                            pooler.Camera.Add(newEntity);
                        };
                        break;
                    case SavePath.EntityCategory.Waves:
                        break;
                    case SavePath.EntityCategory.Prototype:
                        break;
                    case SavePath.EntityCategory.Config:
                        break;
                    case SavePath.EntityCategory.Trigger:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (buildAction != null)
                {
                    buildAction.Invoke(entity);   
                    prototypeData.DataBuilder.Add(buildAction);
                }
            }
        }
        
        private static void InitializeConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            var entity = world.NewEntity();

            ref var entityData = ref pooler.Entity.Add(entity);
            entityData.EntityID = slot.Configs.id;
            entityData.Category = slot.Configs.category;

            ref var configData = ref pooler.Config.Add(entity);
        }
        
        #endregion
    }
}