using Source.Scripts.Core;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [SelectionBase]
    public class PlayerInfoEntity : MonoBehaviour, IEntityObject
    {
        public string currentLocation;
        
        public void Save(string entityID, Slot slot, Location location)
        {
            var entity = new Entity(entityID, EntityCategory.PlayerInfo);
            slot.global.Player.info = entity;
            entity.SetField(SavePath.Player.Info.Location, currentLocation);
        }

        public void Load(Entity entity, Slot slot, Location location)
        {
            currentLocation = entity.TryGetField(SavePath.Player.Info.Location, out var foundLocation) ? foundLocation : "home";
        }
    }
}