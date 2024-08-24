using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public class Slot
    {
        public Slot(string slotName)
        {
            this.slotName = slotName;
            global = new();
            environment = new List<Entity>();
            towers = new List<Entity>();
            enemies = new List<Entity>();
        }

        public string slotName;
        public Global global;
        [SerializeField] private List<Entity> environment;
        [SerializeField] private List<Entity> towers;
        [SerializeField] private List<Entity> enemies;
        private Dictionary<string, Entity> _allEntities;
        
        public IReadOnlyList<Entity> Environment => environment;
        public IReadOnlyList<Entity> Towers => towers;
        public IReadOnlyList<Entity> Enemies => enemies;
        
        public void Initialize()
        {
            _allEntities = new();
            
            global.Initialize(_allEntities);
            
            foreach (var entity in environment)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
            }
            
            foreach (var entity in towers)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
            }
            
            foreach (var entity in enemies)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
            }
        }
        
        public void AddEnvironment(Entity entity)
        {
            _allEntities[entity.id] = entity;
            environment.Add(entity);
        }

        public void AddTower(Entity entity)
        {
            _allEntities[entity.id] = entity;
            towers.Add(entity);
        }

        public void AddEnemy(Entity entity)
        {
            _allEntities[entity.id] = entity;
            enemies.Add(entity);
        }
        
        public void Clear()
        {
            environment.Clear();
            towers.Clear();
            enemies.Clear();
            
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
    }
}