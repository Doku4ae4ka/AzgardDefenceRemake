using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts.EntitySettings
{
    public class PrototypeSettings
    {
        public void Set(Entity entity, Slot slot, EntityCategory category)
        {
            slot.global.AddPrototype(entity);
            entity.SetField(SavePath.PrototypeCategory, $"{(int)category}");
        }
    }
}