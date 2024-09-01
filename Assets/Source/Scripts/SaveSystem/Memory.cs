using System;
using Exerussus._1Extensions;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Object = UnityEngine.Object;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public class Memory
    {
        public Saver save;
        public Loader load;
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

        private static void InitializeDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.DynamicMark>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddDynamic(newEntity);
            }
        }

        private static void InitializeStatic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.StaticMark>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddStatic(newEntity);
            }
        }

        private static void InitializePrototypes(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddPrototype(newEntity);
                
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                var field = newEntity.GetOrCreateFieldIteration(SavePath.Prototype.Category);
                field.value = $"{(int)prototypeData.Category}";
            }
        }

        private static void InitializeConfigs(EcsWorld world, Pooler pooler, Slot slot)
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
                if (pooler.View.Has(entity))
                {
                    var view = pooler.View.Get(entity).Value;
                    // сделать возврат в пул вместо уничтожения
                    if (view != null) ProjectTask.TestCode(() => { Object.Destroy(view.gameObject); });
                }
                
                world.DelEntity(entity);
            }
        }
        
        private static void InitializeDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var itemEntity in slot.Dynamics)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = itemEntity.id;
                entityData.Category = itemEntity.category;

                pooler.DynamicMark.Add(entity);
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
                prototypeData.Category = (EntityCategory)prototypeEntity.GetField(SavePath.Prototype.Category).ParseInt();
                prototypeData.DataBuilder = new();
                
                Action<int> buildAction = null;
                switch (prototypeData.Category)
                {
                    case EntityCategory.Dynamic:
                        buildAction = (int newEntity) =>
                        {
                            pooler.DynamicMark.Add(newEntity);
                        };
                        break;
                    case EntityCategory.Static:
                        buildAction = (int newEntity) =>
                        {
                            pooler.StaticMark.Add(newEntity);
                        };
                        break;
                    case EntityCategory.Prototype:
                        break;
                    case EntityCategory.Config:
                        break;
                    case EntityCategory.Trigger:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (buildAction != null) prototypeData.DataBuilder.Add(buildAction);
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