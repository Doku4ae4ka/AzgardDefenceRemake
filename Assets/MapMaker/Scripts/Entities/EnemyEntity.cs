using System;
using MapMaker.Scripts.EntitySettings.Enemy;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/EnemyEntity"), SelectionBase]
    public class EnemyEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public bool isPrototype;
        [ShowIf("isPrototype"), CustomAttributes.ValueDropdown("Dropdown")] public string prototypeID;
        public EnemyViewSettings view;
        [SerializeField] public EnemySettings enemy;
        
        public static string[] Dropdown() => Constants.PrototypesId.Enemies.All;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = isPrototype ? new SlotEntity(prototypeID, SlotCategory.Dynamic, SavePath.EntityType.Enemy) :
                         new SlotEntity(entityID, SlotCategory.Dynamic, SavePath.EntityType.Enemy);

            if (isPrototype) slot.AddPrototype(entity);
            else slot.AddDynamic(entity);
            
            if (view.enabled) view.TrySaveView(entity, transform);

            this.SerializeObject(entity);
        }

        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor, bool isPrototype)
        {
            this.isPrototype = isPrototype;
            prototypeID = Dropdown()[Array.IndexOf(Dropdown(), slotEntity.id)];
            
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
