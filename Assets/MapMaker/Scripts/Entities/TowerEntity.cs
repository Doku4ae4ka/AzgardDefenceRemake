using MapMaker.Scripts.EntitySettings.Tower;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Core.SaveManager;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [SelectionBase]
    public class TowerEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public bool isPrototype;
        [HideInInspector] public PrototypeSettings prototype;
        public TowerSettings tower;
        public TowerViewSettings view;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = new Entity(entityID, SavePath.EntityCategory.Tower);

            if (isPrototype)
            {
                entity.category = SavePath.EntityCategory.Prototype;
                slot.AddPrototype(entity);
                prototype.Set(entity, slot, SavePath.EntityCategory.Tower);
            }
            else slot.AddDynamic(entity);
            
            if (view.enabled) view.TrySaveView(entity, transform);

            this.SerializeObject(entity);
        }

        public void Load(Entity entity, Slot slot, MapEditor mapEditor)
        {
            isPrototype = entity.category == SavePath.EntityCategory.Prototype;
            
            prototype = new ();
            tower = new ();
            view = new();
            
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
