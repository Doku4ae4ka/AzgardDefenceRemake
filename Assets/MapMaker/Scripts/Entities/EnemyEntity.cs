using System;
using MapMaker.Scripts.EntitySettings;
using MapMaker.Scripts.EntitySettings.Enemy;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Ecs.Modules.PauldokDev.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/EnemyEntity"), SelectionBase]
    public class EnemyEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public bool isPrototype;
        [ShowIf("isPrototype"), CustomAttributes.ValueDropdown("Dropdown")] public string prototypeID;
        private PrototypeSettings prototype = new();
        public EnemyViewSettings view;
        [SerializeField] public EnemySettings enemy;
        
        public static string[] Dropdown() => Constants.PrototypesId.Enemies.All;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = isPrototype ?
                         new SlotEntity(prototypeID, SavePath.EntityCategory.Tower) :
                         new SlotEntity(entityID, SavePath.EntityCategory.Tower);

            if (isPrototype)
            {
                entity.category = SavePath.EntityCategory.Prototype;
                slot.AddPrototype(entity);
                prototype.Set(entity, slot, SavePath.EntityCategory.Enemy);
            }
            else slot.AddDynamic(entity);
            
            if (view.enabled) view.TrySaveView(entity, transform);

            this.SerializeObject(entity);
        }

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor)
        {
            isPrototype = slotEntity.category == SavePath.EntityCategory.Prototype;
            prototypeID = 
                Dropdown()[Array.IndexOf(Dropdown(), slotEntity.id)];
            
            prototype = new ();
            enemy = new ();
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
