using System;
using Exerussus._1EasyEcs.Scripts.Core;

namespace Source.Scripts.ECS.Groups.SlotSaver.Core
{
    public abstract class EntityBuilder
    {
        public abstract SlotCategory Category { get; }
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
            return GetDataBuilderProcess(entity, slotEntity);
        }

        public void TrySetData(int entity, SlotEntity slotEntity)
        {
            Entity = entity;
            SlotEntity = slotEntity;
            TrySetDataProcess(entity, slotEntity);
        }
        
        public abstract bool CheckProcess(int entity, SlotEntity slotEntity);
        public abstract Action<int> GetDataBuilderProcess(int entity, SlotEntity slotEntity);
        public abstract void TrySetDataProcess(int entity, SlotEntity slotEntity);
    }
}