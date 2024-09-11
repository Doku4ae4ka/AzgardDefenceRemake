using MapMaker.Scripts.EntitySettings.Level;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Core.SaveManager;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/LevelEntity"), SelectionBase]
    public class LevelEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public BuildingTilemapViewSettings view;
        [SerializeField] public LevelSettings level;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = new Entity(entityID, SavePath.EntityCategory.Level);
            
            if (view.enabled) view.TrySaveView(entity, transform);
            
            slot.AddDynamic(entity);
            this.SerializeObject(entity);
        }

        public void Load(Entity entity, Slot slot, MapEditor mapEditor)
        {
            view = new();
            level = new ();
            view.TryLoadView(entity, transform);
            this.DeserializeObject(entity);
        }
        
        [Button]
        public void Validate()
        {
            if (view.enabled) view.Validate(transform);
        }

        private void OnValidate()
        {
            if (autoValidate) Validate();
        }
    }
}