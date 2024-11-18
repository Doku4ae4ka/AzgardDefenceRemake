﻿using System;
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

        public static SlotEntity GetOrCreateEntityOnSave(SlotSaverPooler pooler, Slot slot, ref SlotSaverData.SlotEntity entityData, int entity, Action onCreate = null)
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

    /// <summary>
    /// Saver is a class that contains methods for saving data from the ECS world to the slot.
    /// </summary>
    [Serializable]
    public class Saver
    {
        private Dictionary<int, SlotEntity> _entityToSlotEntity = new();
        public void Invoke(EcsWorld world, SlotSaverPooler pooler, Slot slot, Signal signal)
        {
            signal.RegistryRaise(new SlotSaverSignals.OnStartSaving() { Slot = slot });

            slot.Clear();
            _entityToSlotEntity.Clear();

            SerializeAll(world, pooler, slot);

            foreach (var entityBuilder in pooler.AllCreators)
            {
                foreach (var entity in entityBuilder.FilterMask.Inc<SlotSaverData.SlotEntity>().End())
                {
                    if(!_entityToSlotEntity.TryGetValue(entity, out var slotEntity)) continue;
                    entityBuilder.TrySaveData(entity, slotEntity);
                }
            }
            
            signal.RegistryRaise(new SlotSaverSignals.OnFinishSaving() { Slot = slot });
        }


        private void SerializeAll(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<SlotSaverData.SlotEntity>().Exc<SlotSaverData.SavingProcess>().End())
            {
                ref var entityData = ref pooler.SlotEntity.Get(entity);
                var newEntitySlot = new SlotEntity(entityData.EntityID, entityData.Category, entityData.Type);

                if (pooler.Prototype.Has(entity)) slot.AddPrototype(newEntitySlot);
                else if (pooler.ConfigMark.Has(entity)) slot.AddConfig(newEntitySlot);
                else if (pooler.StaticMark.Has(entity)) slot.AddStatic(newEntitySlot);
                else if (pooler.PlayerMark.Has(entity)) slot.AddPlayer(newEntitySlot);
                else if (pooler.DynamicMark.Has(entity)) slot.AddDynamic(newEntitySlot);
                
                _entityToSlotEntity.Add(entity, newEntitySlot);
            }
        }
    }

    /// <summary>
    /// Loader is a class that contains methods for loading data from the slot to the ECS world.
    /// </summary>
    [Serializable]
    public class Loader
    {
        public void Invoke(EcsWorld world, SlotSaverPooler pooler, Slot slot, Prototypes prototypes, Signal signal)
        {
            PreLoading(world, pooler, slot, signal);

            Load(world, pooler, slot, prototypes);

            PostLoading(world, pooler, slot, signal);
        }
        
        public void PreLoading(EcsWorld world, SlotSaverPooler pooler, Slot slot, Signal signal)
        {
            signal.RegistryRaise(new SlotSaverSignals.OnStartLoading { Slot = slot });
            UnloadAllEntities(world, pooler);
        }

        public void Load(EcsWorld world, SlotSaverPooler pooler, Slot slot, Prototypes prototypes)
        {
            DeserializeConfigs(world, pooler, slot);
            DeserializePrototypes(world, pooler, slot, prototypes);
            DeserializePlayer(world, pooler, slot);
            DeserializeStatic(world, pooler, slot);
            DeserializeDynamic(world, pooler, slot);
        }

        public void PostLoading(EcsWorld world, SlotSaverPooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<SlotSaverData.SlotEntity>().Inc<SlotSaverData.LoadingProcess>().End()) pooler.LoadingProcess.Del(entity);
            signal.RegistryRaise(new SlotSaverSignals.OnFinishLoading() { Slot = slot });
        }

        #region Static Methods

        private static void UnloadAllEntities(EcsWorld world, SlotSaverPooler pooler)
        {
            foreach (var entityBuilder in pooler.AllCreators)
            {
                foreach (var entity in entityBuilder.FilterMask.Inc<SlotSaverData.SlotEntity>().End())
                {
                    entityBuilder.OnUnloadSlot(entity);
                }
            }
            
            foreach (var entity in world.Filter<SlotSaverData.SlotEntity>().End())
            {
                world.DelEntity(entity);
            }
        }

        private static ref SlotSaverData.SlotEntity CreateEntity(EcsWorld world, SlotSaverPooler pooler, SlotEntity slotEntity, out int entity)
        {
            entity = world.NewEntity();
            ref var entityData = ref pooler.SlotEntity.Add(entity);
            entityData.EntityID = slotEntity.id;
            entityData.Category = slotEntity.category;
                
            ref var loadingProcessData = ref pooler.LoadingProcess.Add(entity);
            loadingProcessData.SlotEntity = slotEntity;
            return ref entityData;
        }
        
        private static void DeserializeConfigs(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var slotEntity in slot.Configs)
            {
                CreateEntity(world, pooler, slotEntity, out var entity);
                pooler.ConfigMark.Add(entity);
                foreach (var builder in pooler.ConfigDataCreators) builder.TrySetData(entity, slotEntity);
            }
        }
        
        private static void DeserializePlayer(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var slotEntity in slot.Player)
            {
                CreateEntity(world, pooler, slotEntity, out var entity);
                pooler.PlayerMark.Add(entity);
                foreach (var builder in pooler.PlayerDataCreators) builder.TrySetData(entity, slotEntity);
            }
        }
        
        private static void DeserializeDynamic(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var slotEntity in slot.Dynamics)
            {
                CreateEntity(world, pooler, slotEntity, out var entity);
                pooler.DynamicMark.Add(entity);
                foreach (var builder in pooler.DynamicDataCreators) builder.TrySetData(entity, slotEntity);
            }
        }

        private static void DeserializeStatic(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var slotEntity in slot.Statics)
            {
                CreateEntity(world, pooler, slotEntity, out var entity);
                pooler.StaticMark.Add(entity);
                foreach (var builder in pooler.StaticDataCreators) builder.TrySetData(entity, slotEntity);
            }
        }

        private static void DeserializePrototypes(EcsWorld world, SlotSaverPooler pooler, Slot slot, Prototypes prototypes)
        {
            foreach (var prototypeEntity in slot.Prototypes)
            {
                ref var entitySlotData = ref CreateEntity(world, pooler, prototypeEntity, out var entity);
                
                prototypes.Add(prototypeEntity.id, entity);

                ref var prototypeData = ref pooler.Prototype.Add(entity); 

                
                switch (entitySlotData.Category)
                {
                    case SlotCategory.Config:
                        prototypeData.DataBuilder += (i => pooler.ConfigMark.Add(i));
                        foreach (var builder in pooler.ConfigDataCreators) LoadPrototypeData(builder, entity, prototypeEntity, ref prototypeData);
                        break;
                    case SlotCategory.Player:
                        prototypeData.DataBuilder += (i => pooler.PlayerMark.Add(i));
                        foreach (var builder in pooler.PlayerDataCreators) LoadPrototypeData(builder, entity, prototypeEntity, ref prototypeData);
                        break;
                    case SlotCategory.Static:
                        prototypeData.DataBuilder += (i => pooler.StaticMark.Add(i));
                        foreach (var builder in pooler.StaticDataCreators) LoadPrototypeData(builder, entity, prototypeEntity, ref prototypeData);
                        break;
                    case SlotCategory.Dynamic:
                        prototypeData.DataBuilder += (i => pooler.DynamicMark.Add(i));
                        foreach (var builder in pooler.DynamicDataCreators) LoadPrototypeData(builder, entity, prototypeEntity, ref prototypeData);
                        break;
                }
                
                prototypeData.DataBuilder.Invoke(entity);
                
                prototypeData.DataBuilder = i =>
                {
                    ref var entitySlotData = ref pooler.SlotEntity.Add(i);
                    entitySlotData.EntityID = prototypeEntity.id;
                    entitySlotData.Category = prototypeEntity.category;
                    entitySlotData.Type = prototypeEntity.type;
                };
                
            }

            void LoadPrototypeData(EntityBuilder builder, int entity, SlotEntity prototypeEntity, ref SlotSaverData.Prototype prototypeData)
            {
                if (!builder.CheckPrototype(entity, prototypeEntity)) return;
                prototypeData.DataBuilder += builder.GetDataBuilder(entity, prototypeEntity);
            }
        }

        #endregion
    }
}