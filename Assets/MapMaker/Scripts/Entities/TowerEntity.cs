using System;
using MapMaker.Scripts.EntitySettings.Tower;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
    [AddComponentMenu("ADR/TowerEntity"), SelectionBase]
    public class TowerEntity : MonoBehaviour, IEntityObject
    {
        public bool autoValidate;
        public bool isPrototype;
        [ShowIf("isPrototype"), CustomAttributes.ValueDropdown("Dropdown")] public string prototypeID;
        public TowerViewSettings view;
        [SerializeField] public TowerSettings tower;
        
        private static string[] Dropdown() => Constants.PrototypesId.Towers.All;
        
        public void Save(string entityID, Slot slot)
        {
            var entity = isPrototype ?
                        new SlotEntity(prototypeID, SlotCategory.Dynamic, "",SavePath.EntityCategory.Tower) :
                        new SlotEntity(entityID, SlotCategory.Dynamic, "",SavePath.EntityCategory.Tower);
            
            if (isPrototype) slot.AddPrototype(entity);
            else slot.AddDynamic(entity);
            
            if (view.enabled) view.TrySaveView(entity, transform);
        
            this.SerializeObject(entity);
        }
        
        public void Load(SlotEntity slotEntity, Slot slot, MapEditor mapEditor, bool isPrototype)
        {
            this.isPrototype = isPrototype;
            prototypeID = Dropdown()[Array.IndexOf(Dropdown(), slotEntity.id)];
            
            tower = new ();
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
