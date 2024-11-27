using MapMaker.Scripts.EntitySettings.Environment;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
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
            var entity = new SlotEntity(entityID, SlotCategory.Static, SavePath.EntityType.Environment);
            
            slot.AddStatic(entity);
            
            if (view.enabled) view.TrySaveView(entity, transform);

            this.SerializeObject(entity);
        }

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor, bool isPrototype)
        {
            environment = new ();
            view = new();
            
            view.TryLoadView(slotEntity, transform);
            this.DeserializeObject(slotEntity);
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