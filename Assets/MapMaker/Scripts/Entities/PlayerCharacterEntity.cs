using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.Libraries;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [SelectionBase]
    public class PlayerCharacterEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        [SerializeField] public string entityID;
        [HideInInspector] public ViewLibrary viewLibrary;
        public HealthSettings health;
        public ViewSettings view;
        
        public void Save(string entityID1, Slot slot, Location location)
        {
            entityID = entityID1;
            var entity = new Entity(entityID, EntityCategory.PlayerCharacter);
            slot.global.Player.character = entity;

            health.TrySave(entity);
            view.TrySave(entity, transform);
        }

        public void Load(Entity entity, Slot slot, Location location)
        {
            GameCore.TryGetConfig(ref viewLibrary);
            
            entityID = entity.id;
            
            health = new HealthSettings();
            view = new ViewSettings();
            
            health.TryLoad(entity);
            view.TryLoad(entity, transform, viewLibrary);
        }
        
        [Button]
        public void Validate()
        {
            GameCore.TryGetConfig(ref viewLibrary);
            if (view.enabled) view.Validate(transform, viewLibrary);
        }

        [Button]
        public void Rotate()
        {
            Debug.Log(transform.rotation);
        }

        private void OnValidate()
        {
            if (autoValidate) Validate();
        }
    }
}