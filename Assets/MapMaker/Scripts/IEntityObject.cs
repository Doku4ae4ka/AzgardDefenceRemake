using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace MapMaker.Scripts
{
    public interface IEntityObject
    {
        public void Save(string entityID, Slot slot);

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor, bool isPrototype);
    }
}