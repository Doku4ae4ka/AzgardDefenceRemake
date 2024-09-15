using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts.EntitySettings
{
    public class PrototypeSettings
    {
        public void Set(Entity entity, Slot slot, string category)
        {
            entity.SetField(SavePath.Prototype.Category, category);
        }
        
    }
}