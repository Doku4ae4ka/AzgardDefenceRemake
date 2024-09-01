
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.Libraries;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [SelectionBase]
    public class ItemEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public ViewSettings view;
        public HealthSettings health;
        [HideInInspector] public ViewLibrary viewLibrary;
        
        public void Save(string entityID, Slot slot, Location location)
        {
            var entity = new Entity(entityID, EntityCategory.Item);
            
            location.AddItem(entity);
            view.TrySave(entity, transform);
            health.TrySave(entity);
        }

        public void Load(Entity entity, Slot slot, Location location)
        {
            GameCore.TryGetConfig(ref viewLibrary);
            
            view = new ViewSettings();
            health = new HealthSettings();
            view.TryLoad(entity, transform, viewLibrary);
            health.TryLoad(entity);
        }
        
        [Button]
        public void Validate()
        {
            GameCore.TryGetConfig(ref viewLibrary);
            if (view.enabled) view.Validate(transform, viewLibrary);
        }

        private void OnValidate()
        {
            if (autoValidate) Validate();
        }
    }
}