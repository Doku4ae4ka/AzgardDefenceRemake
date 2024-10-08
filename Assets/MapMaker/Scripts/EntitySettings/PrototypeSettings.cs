using Ecs.Modules.PauldokDev.SlotSaver.Core;

namespace MapMaker.Scripts.EntitySettings
{
    public class PrototypeSettings
    {
        public void Set(SlotEntity slotEntity, Slot slot, string category)
        {
            slotEntity.SetField(SavePath.Prototype.Category, category);
        }
        
    }
}