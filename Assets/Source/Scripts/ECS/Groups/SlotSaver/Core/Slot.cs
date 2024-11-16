using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.SlotSaver.Core
{
    [Serializable]
    public class Slot
    {
        public string slotName;
        [SerializeField] private List<SlotEntity> configs;
        [SerializeField] private List<SlotEntity> prototypes;
        [SerializeField] private List<SlotEntity> player;
        [SerializeField] private List<SlotEntity> statics;
        [SerializeField] private List<SlotEntity> dynamics;
        private Dictionary<string, SlotEntity> _allEntities;

        public Slot(string slotName)
        {
            this.slotName = slotName;
            statics = new List<SlotEntity>();
            dynamics = new List<SlotEntity>();
        }

        public IReadOnlyList<SlotEntity> Configs => configs;
        public IReadOnlyList<SlotEntity> Prototypes => prototypes;
        public IReadOnlyList<SlotEntity> Player => player;
        public IReadOnlyList<SlotEntity> Statics => statics;
        public IReadOnlyList<SlotEntity> Dynamics => dynamics;

        public void Initialize()
        {
            _allEntities = new Dictionary<string, SlotEntity>();
            
            foreach (var entity in configs)
            {
                entity.Initialize();
                _allEntities[entity.id] = entity;
            }
            
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
        
        public void AddConfig(SlotEntity slotEntity)
        {
            _allEntities[slotEntity.id] = slotEntity;
            configs.Add(slotEntity);
        }

        public void AddPrototype(SlotEntity slotEntity)
        {
            _allEntities[slotEntity.id] = slotEntity;
            prototypes.Add(slotEntity);
        }

        public void AddPlayer(SlotEntity slotEntity)
        {
            _allEntities[slotEntity.id] = slotEntity;
            player.Add(slotEntity);
        }

        public void AddStatic(SlotEntity slotEntity)
        {
            _allEntities[slotEntity.id] = slotEntity;
            statics.Add(slotEntity);
        }

        public void AddDynamic(SlotEntity slotEntity)
        {
            _allEntities[slotEntity.id] = slotEntity;
            dynamics.Add(slotEntity);
        }

        public void Clear()
        {
            configs.Clear();
            prototypes.Clear();
            statics.Clear();
            dynamics.Clear();
            player.Clear();

            _allEntities.Clear();
        }

        public bool TryGetEntity(string entityID, out SlotEntity slotEntity)
        {
            if (_allEntities.TryGetValue(entityID, out var result))
            {
                slotEntity = result;
                return true;
            }

            slotEntity = default;
            return false;
        }

        public SlotEntity GetEntity(string entityID)
        {
            return _allEntities[entityID];
        }
    }
}