using System;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;

namespace Source.Scripts.ECS.Groups.SlotSaver.Core
{
    public abstract class EntityBuilder
    {
        public abstract SlotCategory Category { get; }
        public abstract EcsWorld.Mask FilterMask { get; }
        public int Entity { get; private set; }
        public SlotEntity SlotEntity { get; private set; }
        
        public abstract void Initialize(GameShare gameShare);

        public bool Check(int entity, SlotEntity slotEntity)
        {
            Entity = entity;
            SlotEntity = slotEntity;
            return CheckProcess(entity, slotEntity);
        }
        
        public Action<int> GetDataBuilder(int entity, SlotEntity slotEntity)
        {
            Entity = entity;
            SlotEntity = slotEntity;
            return SetDataBuilderForPrototype(entity, slotEntity);
        }

        public void TrySetData(int entity, SlotEntity slotEntity)
        {
            Entity = entity;
            SlotEntity = slotEntity;
            TrySetDataForStandardEntity(entity, slotEntity);
        }

        public void TrySaveData(int entity, SlotEntity slotEntity)
        {
            Entity = entity;
            SlotEntity = slotEntity;
            TrySaveDataProcess(entity, slotEntity);
        }

        public void OnUnloadSlot(int entity)
        {
            Entity = entity;
            OnUnloadSlotProcess(entity);
        }

        public virtual bool CheckProcess(int entity, SlotEntity slotEntity) => false;
        public virtual Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity) => null;
        public abstract void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity);
        public abstract void TrySaveDataProcess(int entity, SlotEntity slotEntity);
        public virtual void OnUnloadSlotProcess(int entity) { }
    }
}