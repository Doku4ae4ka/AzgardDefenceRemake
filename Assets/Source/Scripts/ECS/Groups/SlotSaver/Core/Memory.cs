using System;
using System.Collections.Generic;
using Exerussus._1Extensions;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Object = UnityEngine.Object;

namespace Source.Scripts.ECS.Groups.SlotSaver.Core
{
    [Serializable]
    public class Memory
    {
        public Saver save;
        public Loader load;

        public static SlotEntity GetOrCreateEntityOnSave(SlotSaverPooler pooler, Slot slot, ref EcsData.Entity entityData, int entity, Action onCreate = null)
        {
            if (slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return foundEntity;

            var savingEntity = new SlotEntity(entityData.EntityID, entityData.Category, entityData.Type);

            onCreate?.Invoke();
            // if (pooler.Tower.Has(entity) ||
            //     pooler.Enemy.Has(entity) ||
            //     pooler.Camera.Has(entity)) slot.AddDynamic(savingEntity);
            // else if (pooler.Environment.Has(entity)) slot.AddStatic(savingEntity);

            return savingEntity;
        }
    }

    [Serializable]
    public class Saver
    {
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnStart;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnConfigs;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnPrototypes;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnStatic;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnDynamic;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnFinish;

        public void Invoke(EcsWorld world, SlotSaverPooler pooler, Slot slot)
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

        private void InitializeDynamic(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.DynamicMark>()
                         .Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.SlotEntity.Get(entity);
                var newEntity = new SlotEntity(entityData.EntityID, entityData.Category, entityData.Type);
                slot.AddDynamic(newEntity);
            }
        }

        private void InitializeStatic(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.StaticMark>()
                         .Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.SlotEntity.Get(entity);
                var newEntity = new SlotEntity(entityData.EntityID, entityData.Category, entityData.Type);
                slot.AddStatic(newEntity);
            }
        }

        private void InitializePrototypes(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.SlotEntity.Get(entity);
                var newEntity = new SlotEntity(entityData.EntityID, entityData.Category, entityData.Type);
                slot.AddPrototype(newEntity);

                ref var prototypeData = ref pooler.Prototype.Get(entity);
                var field = newEntity.GetOrCreateFieldIteration(SavePath.Prototype.Category);
                field.value = $"{(int)prototypeData.Category}";
            }
        }

        private void InitializeConfigs(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Config>().End())
            {
                ref var entityData = ref pooler.SlotEntity.Get(entity);
                var newEntity = new SlotEntity(entityData.EntityID, entityData.Category, entityData.Type);
                slot.CreateConfig(newEntity);
            }
        }

        #endregion
    }

    [Serializable]
    public class Loader
    {
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnStart;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnConfigs;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnPrototypes;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnStatic;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnDynamic;
        public event Action<EcsWorld, SlotSaverPooler, Slot> OnFinish;

        public void Invoke(EcsWorld world, SlotSaverPooler pooler, Slot slot, Prototypes prototypes, Signal signal)
        {
            signal.RegistryRaise(new SlotSaverSignals.OnStartLoading { Slot = slot });

            signal.RegistryRaise(new SlotSaverSignals.OnUnloadAllEntities());
            InitializeConfigs(world, pooler, slot);
            signal.RegistryRaise(new SlotSaverSignals.OnConfigsLoading() { Slot = slot });

            InitializePrototypes(world, pooler, slot, prototypes);
            signal.RegistryRaise(new SlotSaverSignals.OnPrototypesLoading() { Slot = slot });

            InitializeStatic(world, pooler, slot);
            signal.RegistryRaise(new SlotSaverSignals.OnStaticLoading() { Slot = slot });

            InitializeDynamic(world, pooler, slot);
            signal.RegistryRaise(new SlotSaverSignals.OnDynamicLoading() { Slot = slot });
            
            signal.RegistryRaise(new SlotSaverSignals.OnFinishLoading() { Slot = slot });
        }

        #region Static Methods

        // private static void UnloadAllEntities(EcsWorld world, SlotSaverPooler pooler)
        // {
        //     foreach (var entity in world.Filter<EcsData.Entity>().End())
        //     {
        //         if (pooler.TowerView.Has(entity))
        //         {
        //             var view = pooler.TowerView.Get(entity).Value;
        //             // сделать возврат в пул вместо уничтожения
        //             if (view != null) ProjectTask.TestCode(() => { Object.Destroy(view.TowerAsset.gameObject); });
        //         }
        //
        //         if (pooler.EnemyView.Has(entity))
        //         {
        //             var view = pooler.EnemyView.Get(entity).Value;
        //             // сделать возврат в пул вместо уничтожения
        //             if (view != null) ProjectTask.TestCode(() => { Object.Destroy(view.EnemyAsset.gameObject); });
        //         }
        //
        //         if (pooler.EnvironmentView.Has(entity))
        //         {
        //             var view = pooler.EnvironmentView.Get(entity).Value;
        //             // сделать возврат в пул вместо уничтожения
        //             if (view != null) ProjectTask.TestCode(() => { Object.Destroy(view.EnvironmentAsset.gameObject); });
        //         }
        //
        //         world.DelEntity(entity);
        //     }
        // }

        private static void InitializeDynamic(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var dynamicEntity in slot.Dynamics)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.SlotEntity.Add(entity);
                entityData.EntityID = dynamicEntity.id;
                entityData.Category = dynamicEntity.category;
                pooler.DynamicMark.Add(entity);
            }
        }

        private static void InitializeStatic(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var environmentEntity in slot.Statics)
            {
                var entity = world.NewEntity();

                ref var entityData = ref pooler.SlotEntity.Add(entity);
                entityData.EntityID = environmentEntity.id;
                entityData.Category = environmentEntity.category;
                pooler.StaticMark.Add(entity);
            }
        }

        private static void InitializePrototypes(EcsWorld world, SlotSaverPooler pooler, Slot slot, Prototypes prototypes)
        {
            foreach (var prototypeEntity in slot.Prototypes)
            {
                var entity = world.NewEntity();
                prototypes.Add(prototypeEntity.id, entity);

                ref var entityData = ref pooler.SlotEntity.Add(entity);
                entityData.EntityID = prototypeEntity.id;
                entityData.Category = prototypeEntity.category;

                ref var prototypeData = ref pooler.Prototype.Add(entity);
                prototypeData.Category = prototypeEntity.TryGetIntField(SavePath.Prototype.Category, out var category) ? (SlotCategory)category : SlotCategory.Dynamic;
                prototypeData.DataBuilder = new List<Action<int>>();

                //Action<int> buildActions = null;
                switch (prototypeData.Category)
                {
                    case SlotCategory.Config:
                        prototypeData.DataBuilder.Add(i => pooler.ConfigMark.Add(i));
                        foreach (var builder in pooler.ConfigDataCreators)
                        {
                            if (!builder.Check(entity, prototypeEntity)) continue;
                            var buildAction = builder.GetDataBuilder(entity, prototypeEntity);
                            prototypeData.DataBuilder.Add(buildAction);
                        }
                        break;
                    case SlotCategory.Player:
                        prototypeData.DataBuilder.Add(i => pooler.PlayerMark.Add(i));
                        foreach (var builder in pooler.PlayerDataCreators)
                        {
                            if (!builder.Check(entity, prototypeEntity)) continue;
                            var buildAction = builder.GetDataBuilder(entity, prototypeEntity);
                            prototypeData.DataBuilder.Add(buildAction);
                        }
                        break;
                    case SlotCategory.Static:
                        prototypeData.DataBuilder.Add(i => pooler.StaticMark.Add(i));
                        foreach (var builder in pooler.StaticDataCreators)
                        {
                            if (!builder.Check(entity, prototypeEntity)) continue;
                            var buildAction = builder.GetDataBuilder(entity, prototypeEntity);
                            prototypeData.DataBuilder.Add(buildAction);
                        }
                        break;
                    case SlotCategory.Dynamic:
                        prototypeData.DataBuilder.Add(i => pooler.DynamicMark.Add(i));
                        foreach (var builder in pooler.DynamicDataCreators)
                        {
                            if (!builder.Check(entity, prototypeEntity)) continue;
                            var buildAction = builder.GetDataBuilder(entity, prototypeEntity);
                            prototypeData.DataBuilder.Add(buildAction);
                        }
                        break;
                }
            }
        }

        private static int InitializeConfigs(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            var entity = world.NewEntity();

            ref var entityData = ref pooler.SlotEntity.Add(entity);
            entityData.EntityID = slot.Configs.id;
            entityData.Category = slot.Configs.category;

            return entity;
            //ref var configData = ref pooler.Config.Add(entity);
        }

        #endregion
    }
}