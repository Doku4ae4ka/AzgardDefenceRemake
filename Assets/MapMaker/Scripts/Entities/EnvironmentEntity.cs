using MapMaker.Scripts.EntitySettings.Environment;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Core.SaveManager;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/EnvironmentEntity"), SelectionBase]
    public class EnvironmentEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public EnvironmentViewSettings view;
        [SerializeField] public EnvironmentSettings environment;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = new Entity(entityID, SavePath.EntityCategory.Environment);
            
            slot.AddStatic(entity);
            
            if (view.enabled) view.TrySaveView(entity, transform);

            this.SerializeObject(entity);
        }

        public void Load(Entity entity, Slot slot, MapEditor mapEditor)
        {
            environment = new ();
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