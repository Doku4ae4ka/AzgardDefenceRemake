using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public class Global
    {
        [SerializeField] private List<Entity> configs;
        [SerializeField] private List<Entity> prototypes;
        private Dictionary<string, Entity> _globalEntities;
        private Dictionary<string, Entity> _allEntities;
        
        public IReadOnlyList<Entity> Configs => configs;
        public IReadOnlyList<Entity> Prototypes => prototypes;

        public void Initialize(Dictionary<string, Entity> allEntities)
        {
            _allEntities = allEntities;
            _globalEntities = new();
            
            foreach (var entity in configs)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
                _globalEntities[entity.id] = entity;
            }
            foreach (var entity in prototypes)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
                _globalEntities[entity.id] = entity;
            }
        }

        public void AddConfig(Entity entity)
        {
            _allEntities[entity.id] = entity;
            _globalEntities[entity.id] = entity;
            configs.Add(entity);
        }

        public void AddPrototype(Entity entity)
        {
            _allEntities[entity.id] = entity;
            _globalEntities[entity.id] = entity;
            prototypes.Add(entity);
        }

        public void Clear()
        {
            configs.Clear();
            prototypes.Clear();
            foreach (var keyValuePair in _globalEntities) _allEntities.Remove(keyValuePair.Key);
            _globalEntities.Clear();
        }
    }
}