using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts
{
    public class PrototypeSettings
    {
        public void Set(Entity entity, Slot slot)
        {
            slot.global.AddPrototype(entity);
            entity.SetField(SavePath.Prototype.Category, $"{(int)EntityCategory.Item}");
        }
    }
}