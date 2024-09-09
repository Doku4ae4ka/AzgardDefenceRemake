using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    public class WavesEntity : MonoBehaviour, IEntityObject
    {
        public void Save(string entityID, Slot slot)
        {
            throw new System.NotImplementedException();
        }

        public void Load(Entity entity, Slot slot, MapEditor mapEditor)
        {
            throw new System.NotImplementedException();
        }
        
    }
}