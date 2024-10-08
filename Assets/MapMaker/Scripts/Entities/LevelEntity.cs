using MapMaker.Scripts.EntitySettings.Level;
using Ecs.Modules.PauldokDev.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/LevelEntity"), SelectionBase]
    public class LevelEntity : MonoBehaviour, IEntityObject
    {
        [SerializeField] public LevelSettings level;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = new SlotEntity(entityID, SavePath.EntityCategory.Level);
            
            slot.AddDynamic(entity);
            this.SerializeObject(entity);
        }

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor)
        {
            level = new ();
            
            this.DeserializeObject(slotEntity);
        }
        
    }
}