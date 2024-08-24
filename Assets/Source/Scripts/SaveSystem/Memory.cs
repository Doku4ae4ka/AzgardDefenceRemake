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
        public event Action<EcsWorld, Pooler, Slot> OnGlobal;
        public event Action<EcsWorld, Pooler, Slot> OnStatic;
        public event Action<EcsWorld, Pooler, Slot> OnDynamic;
        public event Action<EcsWorld, Pooler, Slot> OnFinish;

        public void Invoke(EcsWorld world, Pooler pooler, Slot slot)
        {
            OnStart?.Invoke(world, pooler, slot);
            
            slot.global.Clear();
            slot.Clear();
            
            InitializeConfigs(world, pooler, slot);
            InitializePrototypes(world, pooler, slot);
            InitializeEnvironment(world, pooler, slot);
            InitializeTowers(world, pooler, slot);
            InitializeEnemies(world, pooler, slot);
            
            OnConfigs?.Invoke(world, pooler, slot);
            OnGlobal?.Invoke(world, pooler, slot);
            OnStatic?.Invoke(world, pooler, slot);
            OnDynamic?.Invoke(world, pooler, slot);
            
            OnFinish?.Invoke(world, pooler, slot);
        }

        #region Static Methods

        private static void InitializeEnemies(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Enemy>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddEnemy(newEntity);
            }
        }

        private static void InitializeTowers(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Tower>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddTower(newEntity);
            }
        }

        private static void InitializeEnvironment(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Environment>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.AddEnvironment(newEntity);
            }
        }

        private static void InitializePrototypes(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.global.AddPrototype(newEntity);
                
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                var field = newEntity.GetOrCreateFieldIteration(SavePath.PrototypeCategory);
                field.value = $"{(int)prototypeData.Category}";
            }
        }

        private static void InitializeConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Config>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var newEntity = new Entity(entityData.EntityID, entityData.Category);
                slot.global.AddConfig(newEntity);
            }
        }

        #endregion
    }

    [Serializable]
    public class Loader
    {
        public event Action<EcsWorld, Pooler, Slot> OnStart;
        public event Action<EcsWorld, Pooler, Slot> OnConfigs;
        public event Action<EcsWorld, Pooler, Slot> OnGlobal;
        public event Action<EcsWorld, Pooler, Slot> OnStatic;
        public event Action<EcsWorld, Pooler, Slot> OnDynamic;
        public event Action<EcsWorld, Pooler, Slot> OnFinish;
        
        public void Invoke(EcsWorld world, Pooler pooler, Slot slot)
        {
            OnStart?.Invoke(world, pooler, slot);
            
            UnloadAllEntities(world, pooler);
            InitializeConfigs(world, pooler, slot);
            OnConfigs?.Invoke(world, pooler, slot);
            
            InitializePrototypes(world, pooler, slot);
            OnGlobal?.Invoke(world, pooler, slot);
            
            InitializeEnvironment(world, pooler, slot);
            OnStatic?.Invoke(world, pooler, slot);
            
            InitializeTowers(world, pooler, slot);
            InitializeEnemies(world, pooler, slot);
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
                    ProjectTask.TestCode(() => { Object.Destroy(view.gameObject); });
                }
                
                world.DelEntity(entity);
            }
        }
        
        private static void InitializeTowers(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var itemEntity in slot.Towers)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = itemEntity.id;
                entityData.Category = itemEntity.category;

                pooler.DynamicMark.Add(entity);
                ref var towerData = ref pooler.Tower.Add(entity);
            }
        }

        private static void InitializeEnvironment(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var environmentEntity in slot.Environment)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = environmentEntity.id;
                entityData.Category = environmentEntity.category;

                pooler.StaticMark.Add(entity);
                ref var environmentData = ref pooler.Environment.Add(entity);
            }
        }

        private static void InitializePrototypes(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var prototypeEntity in slot.global.Prototypes)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = prototypeEntity.id;
                entityData.Category = prototypeEntity.category;

                pooler.GlobalMark.Add(entity);
                ref var prototypeData = ref pooler.Prototype.Add(entity);
                prototypeData.Category = (EntityCategory)prototypeEntity.GetField(SavePath.PrototypeCategory).ParseInt();
                prototypeData.DataBuilder = new();
            }
        }
        
        private static void InitializeConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var configEntity in slot.global.Configs)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = configEntity.id;
                entityData.Category = configEntity.category;

                ref var configData = ref pooler.Config.Add(entity);
            }
        }

        private static void InitializeEnemies(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var configEntity in slot.Enemies)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.Entity.Add(entity);
                entityData.EntityID = configEntity.id;
                entityData.Category = configEntity.category;

                pooler.DynamicMark.Add(entity);
                ref var enemyData = ref pooler.Enemy.Add(entity);
            }
        }
        
        #endregion
    }
}