using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/WavesEntity"), SelectionBase]
    public class WavesEntity : MonoBehaviour, IEntityObject
    {
        public void Save(string entityID, Slot slot)
        {
        }

        public void Load(Entity entity, Slot slot, MapEditor mapEditor)
        {
        }
        
    }
}