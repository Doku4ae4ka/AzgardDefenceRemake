using MapMaker.Scripts.EntitySettings.Level;
using Source.Scripts.ECS.Core.SaveManager;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/LevelEntity"), SelectionBase]
    public class LevelEntity : MonoBehaviour, IEntityObject
    {
        [SerializeField] public LevelSettings level;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = new Entity(entityID, SavePath.EntityCategory.Level);
            
            slot.AddDynamic(entity);
            this.SerializeObject(entity);
        }

        public void Load(Entity entity, Slot slot, MapEditor mapEditor)
        {
            level = new ();
            
            this.DeserializeObject(entity);
        }
        
    }
}