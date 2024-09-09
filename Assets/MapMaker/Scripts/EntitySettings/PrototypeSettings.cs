using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts
{
    public class PrototypeSettings
    {
        public void Set(Entity entity, Slot slot, string category)
        {
            slot.AddPrototype(entity);
            entity.SetField(SavePath.Prototype.Category, category);
        }
    }
}