
using MapMaker.Scripts.EntitySettings;
using MapMaker.Scripts.EntitySettings.Enemy;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.ECS.Core.SaveManager;
using Source.Scripts.ProjectLibraries;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    [SelectionBase]
    public class EnemyEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public bool isPrototype;
        [SerializeField] public string entityID;
        [HideInInspector] public PrototypeSettings prototype;
        public EnemyViewSettings viewSettings;
        public EnemySettings enemySettings;
        
        public void Save(string entityID1, Slot slot)
        {
            entityID = entityID1;
            var entity = new Entity(entityID, EntityCategory.Enemy);

            if (isPrototype)
            {
                slot.global.AddPrototype(entity);
                prototype.Set(entity, slot, EntityCategory.Enemy);
            }
            else slot.AddEnemy(entity);

            if (viewSettings.enabled) viewSettings.Set(entity, transform);
            
            this.SerializeObject(entity);
        }

        public void Load(Entity entity, Slot slot)
        {
            isPrototype = entity.category == EntityCategory.Prototype;
            
            entityID = entity.id;
            
            prototype = new ();
            viewSettings = new();
            enemySettings = new ();

            viewSettings.TryGet(entity, transform, Libraries.EnemyLibrary);
            
            this.DeserializeObject(entity);
        }
        
        [Button]
        public void Validate()
        {
            
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
