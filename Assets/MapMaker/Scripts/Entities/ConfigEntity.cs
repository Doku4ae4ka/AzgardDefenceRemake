
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [SelectionBase]
    public class ConfigEntity : MonoBehaviour, IEntityObject
    {
        public void Save(string entityID, Slot slot, Location location)
        {
            var entity = new Entity(entityID, EntityCategory.Config);
            slot.global.CreateConfig(entity);
            var lastEntityID = FindObjectOfType<MapEditor>().Increment;
            
            entity.SetField(SavePath.Config.FreeEntityID, $"{lastEntityID}");
        }

        public void Load(Entity entity, Slot slot, Location location)
        {
            
        }
    }
}