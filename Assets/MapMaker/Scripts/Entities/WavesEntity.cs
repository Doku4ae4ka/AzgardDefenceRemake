using Source.Scripts.ECS.Groups.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/WavesEntity"), SelectionBase]
    public class WavesEntity : MonoBehaviour, IEntityObject
    {
        public void Save(string entityID, Slot slot)
        {
        }

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor, bool isPrototype)
        {
        }
        
    }
}