
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.Libraries;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [SelectionBase]
    public class CharacterEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public bool isPrototype;
        [HideInInspector] public ViewLibrary viewLibrary;
        [HideInInspector] public PrototypeSettings prototype;
        public HealthSettings health;
        public ViewSettings view;
        
        public void Save(string entityID, Slot slot, Location location)
        {
            var entity = new Entity(entityID, EntityCategory.Character);

            if (isPrototype)
            {
                slot.global.AddPrototype(entity);
                prototype.Set(entity, slot);
            }
            else location.AddCharacter(entity);

            if (health.enabled) health.TrySave(entity);
            if (view.enabled) view.TrySave(entity, transform);
        }

        public void Load(Entity entity, Slot slot, Location location)
        {
            GameCore.TryGetConfig(ref viewLibrary);
            isPrototype = entity.category == EntityCategory.Prototype;
            
            health = new HealthSettings();
            view = new ViewSettings();
            prototype = new PrototypeSettings();
            
            health.TryLoad(entity);
            view.TryLoad(entity, transform, viewLibrary);
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
