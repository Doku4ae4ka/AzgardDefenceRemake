using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/CameraEntity"), SelectionBase]
    public class CameraEntity : MonoBehaviour, IEntityObject
    {
        public void Save(string entityID, Slot slot)
        {
        }

        public void Load(Entity entity, Slot slot, MapEditor mapEditor)
        {
        }
    }
}