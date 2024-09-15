
using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts
{
    public interface IEntityObject
    {
        public void Save(string entityID, Slot slot);

        public void Load(Entity entity, Slot slot, MapEditor mapEditor);
    }
}