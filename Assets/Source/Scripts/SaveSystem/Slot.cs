using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public class Slot
    {
        public Slot(string slotName)
        {
            this.slotName = slotName;
            statics = new List<Entity>();
            dynamics = new List<Entity>();
        }

        public string slotName;
        [SerializeField] private Entity configs;
        [SerializeField] private List<Entity> prototypes;
        [SerializeField] private List<Entity> statics;
        [SerializeField] private List<Entity> dynamics;
        private Dictionary<string, Entity> _allEntities;
        
        public Entity Configs => configs;
        public IReadOnlyList<Entity> Prototypes => prototypes;
        public IReadOnlyList<Entity> Statics => statics;
        public IReadOnlyList<Entity> Dynamics => dynamics;
        
        public void Initialize()
        {
            _allEntities = new();
            
            configs.Initialize();
            _allEntities[configs.id] = configs;
            
            foreach (var entity in prototypes)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
            }
            
            foreach (var entity in statics)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
            }
            
            foreach (var entity in dynamics)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
            }
        }
        
        public void CreateConfig(Entity entity)
        {
            _allEntities[entity.id] = entity;
            configs = entity;
        }

        public void AddPrototype(Entity entity)
        {
            _allEntities[entity.id] = entity;
            prototypes.Add(entity);
        }
        
        public void AddStatic(Entity entity)
        {
            _allEntities[entity.id] = entity;
            statics.Add(entity);
        }

        public void AddDynamic(Entity entity)
        {
            _allEntities[entity.id] = entity;
            dynamics.Add(entity);
        }
        
        public void Clear()
        {
            prototypes.Clear();
            statics.Clear();
            dynamics.Clear();
            
            _allEntities.Clear();
        }

        public bool TryGetEntity(string entityID, out Entity entity)
        {
            if (_allEntities.TryGetValue(entityID, out var result))
            {
                entity = result;
                return true;
            }
            
            entity = default;
            return false;
        }

        public Entity GetEntity(string entityID)
        {
            return _allEntities[entityID];
        }
        
        [Button]
        public void Validate()
        {

            if (Configs != null)
            {
                Configs.id = SavePath.Config.ID;
                Configs.category = EntityCategory.Config;
            }
            if (Prototypes != null) foreach (var entity in Prototypes) entity.category = EntityCategory.Prototype;
            if (Dynamics != null) foreach (var entity in Dynamics) entity.category = EntityCategory.Dynamic;
            if (Statics != null) foreach (var entity in Statics) entity.category = EntityCategory.Static;
        }
    }
}