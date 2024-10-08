using Ecs.Modules.PauldokDev.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/CameraEntity"), SelectionBase]
    public class CameraEntity : MonoBehaviour, IEntityObject
    {
        public void Save(string entityID, Slot slot)
        {
        }

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor)
        {
        }
    }
}