using Exerussus._1EasyEcs.Scripts.Core;

namespace Source.Scripts.ECS.Groups.SlotSaver.Core
{
    public abstract class TypeBuilder
    {
        public abstract string Type { get; }
        public abstract SlotCategory Category { get; }
        public int Entity { get; private set; }
        public SlotEntity SlotEntity { get; private set; }

        public abstract void Initialize(GameShare gameShare);
        
        public bool TryAddData(int entity, SlotEntity slotEntity)
        {
            if (slotEntity.type != Type) return false;
            
            AddDataProcess(entity, slotEntity);
            return true;
        }
        
        protected abstract void AddDataProcess(int entity, SlotEntity slotEntity);
    }
}