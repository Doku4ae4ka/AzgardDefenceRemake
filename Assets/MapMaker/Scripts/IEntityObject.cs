using Ecs.Modules.PauldokDev.SlotSaver.Core;

namespace MapMaker.Scripts
{
    public interface IEntityObject
    {
        public void Save(string entityID, Slot slot);

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor);
    }
}