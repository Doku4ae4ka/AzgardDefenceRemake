
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [SelectionBase]
    public class ConfigEntity : MonoBehaviour, IEntityObject
    {
        public void Save(string entityID, Slot slot)
        {
            var entity = new Entity(entityID, SavePath.EntityCategory.Config);
            slot.CreateConfig(entity);
            var lastEntityID = FindAnyObjectByType<MapEditor>().Increment;
            
            entity.SetField(SavePath.Config.FreeEntityID, $"{lastEntityID}");
        }

        public void Load(Entity entity, Slot slot, MapEditor mapEditor)
        {
            
        }
    }
}