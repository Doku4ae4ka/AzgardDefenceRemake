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

        public static SlotEntity GetOrCreateEntityOnSave(SlotSaverPooler pooler, Slot slot, ref SlotSaverData.SlotEntity entityData, int entity, Action onCreate = null)
        {
            if (slot.TryGetEntity(entityData.EntityID, out var foundEntity)) return foundEntity;

            var savingEntity = new SlotEntity(entityData.EntityID, entityData.Category, entityData.Type, entityData.SubType);

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
        public void Invoke(EcsWorld world, SlotSaverPooler pooler, Slot slot, Signal signal)
        {
            signal.RegistryRaise(new SlotSaverSignals.OnStartSaving() { Slot = slot });

            slot.Clear();

            SerializeAll(world, pooler, slot);

            foreach (var entityBuilder in pooler.AllCreators)
            {
                foreach (var entity in entityBuilder.FilterMask.Inc<SlotSaverData.SlotEntity>().Inc<SlotSaverData.SavingProcess>().End())
                {
                    ref var savingProcessData = ref pooler.SavingProcess.Get(entity);
                    entityBuilder.TrySaveData(entity, savingProcessData.SlotEntity);
                }
            }

            foreach (var entity in world.Filter<SlotSaverData.SlotEntity>().Inc<SlotSaverData.SavingProcess>().End()) pooler.SavingProcess.Del(entity);
            
            signal.RegistryRaise(new SlotSaverSignals.OnFinishSaving() { Slot = slot });
        }


        private void SerializeAll(EcsWorld world, SlotSaverPooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<SlotSaverData.SlotEntity>().Exc<SlotSaverData.SavingProcess>().End())
            {
                ref var entityData = ref pooler.SlotEntity.Get(entity);
                var newEntitySlot = new SlotEntity(entityData.EntityID, entityData.Category, entityData.Type, entityData.SubType);

                if (pooler.Prototype.Has(entity)) slot.AddPrototype(newEntitySlot);
                else if (pooler.ConfigMark.Has(entity)) slot.AddConfig(newEntitySlot);
                else if (pooler.StaticMark.Has(entity)) slot.AddStatic(newEntitySlot);
                else if (pooler.PlayerMark.Has(entity)) slot.AddPlayer(newEntitySlot);
                else if (pooler.DynamicMark.Has(entity)) slot.AddDynamic(newEntitySlot);
                
                ref var savingProcessData = ref pooler.SavingProcess.Add(entity);
                savingProcessData.SlotEntity = newEntitySlot;
            }
        }
    }

    [Serializable]
    public class Loader
    {
        public void Invoke(EcsWorld world, SlotSaverPooler pooler, Slot slot, Prototypes prototypes, Signal signal)
        {
            signal.RegistryRaise(new SlotSaverSignals.OnStartLoading { Slot = slot });

            DeserializeConfigs(world, pooler, slot);
            DeserializePrototypes(world, pooler, slot, prototypes);
            DeserializePlayer(world, pooler, slot);
            DeserializeStatic(world, pooler, slot);
            DeserializeDynamic(world, pooler, slot);
            
            LoadAllData(world, pooler);
            
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
        
        private static void LoadAllData(EcsWorld world, SlotSaverPooler pooler)
        {
            foreach (var entity in world.Filter<SlotSaverData.SlotEntity>().Inc<SlotSaverData.LoadingProcess>().End())
            {
                ref var loadingProcessData = ref pooler.LoadingProcess.Get(entity);
                switch (loadingProcessData.SlotEntity.category)
                {
                    case SlotCategory.Config:
                        foreach (var builder in pooler.ConfigDataCreators) builder.TrySetData(entity, loadingProcessData.SlotEntity);
                        break;
                    case SlotCategory.Player:
                        foreach (var builder in pooler.PlayerDataCreators) builder.TrySetData(entity, loadingProcessData.SlotEntity);
                        break;
                    case SlotCategory.Static:
                        foreach (var builder in pooler.StaticDataCreators) builder.TrySetData(entity, loadingProcessData.SlotEntity);
                        break;
                    case SlotCategory.Dynamic:
                        foreach (var builder in pooler.DynamicDataCreators) builder.TrySetData(entity, loadingProcessData.SlotEntity);
                        break;
                }
                pooler.LoadingProcess.Del(entity);
            }
            
        }

        #endregion
    }
}