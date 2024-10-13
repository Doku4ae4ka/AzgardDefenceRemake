using MapMaker.Scripts.EntitySettings.Level;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/LevelEntity"), SelectionBase]
    public class LevelEntity : MonoBehaviour, IEntityObject
    {
        [SerializeField] public LevelSettings level;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = new SlotEntity(entityID, SlotCategory.Dynamic, "",SavePath.EntityCategory.Level);
            
            slot.AddDynamic(entity);
            this.SerializeObject(entity);
        }

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor, bool isPrototype)
        {
            level = new ();
            
            this.DeserializeObject(slotEntity);
        }
        
    }
}