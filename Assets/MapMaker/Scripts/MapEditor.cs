using Exerussus._1Extensions;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;
using UnityEngine;
using FilePathAttribute = UnityEditor.FilePathAttribute;

namespace MapMaker.Scripts
{
    public class MapEditor : MonoBehaviour
    {
        public string slotName = "default";
        [HideInInspector] public GameConfigurations gameConfigurations;
        [SerializeField] private bool saveEmptyMap;
        private int _index;

        private int Increment
        {
            get
            {
                _index++;
                return _index;
            }
        }
        
        private Transform _all;
        private Transform _configs;
        private Transform _prototypes;
        private Transform _environments;
        private Transform _towers;
        private Transform _enemies;

        [Button]
        public void SaveLevel()
        {
            _index = 0;
            var entitiesCount = 0;

            var configs = GetAllConfigs();
            var environments = GetAllEnvironments();
            var enemies = GetAllEnemies();
            var towers = GetAllTowers();

            entitiesCount += configs.Length + environments.Length + enemies.Length + towers.Length;
            
            if (!saveEmptyMap && entitiesCount == 0) return;
            
            var memorySlot = GetSlot();
            memorySlot.Initialize();
            
            memorySlot.global.Clear();
            memorySlot.Clear();
            
            SaveEntities(configs, memorySlot, "global.config");
            SaveEntities(environments, memorySlot, $"environment");
            SaveEntities(towers, memorySlot, "towers");
            SaveEntities(enemies, memorySlot, "enemies");

            // var prototypes = new List<CharacterEntity>();
            //
            // for (var index = entities.Count - 1; index >= 0; index--)
            // {
            //     var gameEntity = entities[index];
            //     if (gameEntity.category == EntityCategory.Prototype)
            //     {
            //         prototypes.Add(gameEntity);
            //         entities.Remove(gameEntity);
            //     }
            // }
            //
            // var newEntityID = 0;
            //
            // foreach (var gameEntity in prototypes)
            // {
            //     newEntityID++;
            //     gameEntity.entityID = newEntityID;
            //     gameEntity.Save(memorySlot, location);
            // }
            //
            // foreach (var gameEntity in entities)
            // {
            //     newEntityID++;
            //     gameEntity.entityID = newEntityID;
            //     gameEntity.Save(memorySlot, location);
            // }
        }

        private void SaveEntities(IEntityObject[] entities, Slot slot, string prefix)
        {
            if (entities is { Length: > 0 })
            {
                foreach (var entity in entities)
                {
                    entity.Save($"{prefix}.{Increment}", slot);
                }
            }
        }

        private ConfigEntity[] GetAllConfigs()
        {
            return FindObjectsOfType<ConfigEntity>();
        }

        private EnemyEntity[] GetAllEnemies()
        {
            return FindObjectsOfType<EnemyEntity>();
        }

        private TowerEntity[] GetAllTowers()
        {
            return FindObjectsOfType<TowerEntity>();
        }

        private EnvironmentEntity[] GetAllEnvironments()
        {
            return FindObjectsOfType<EnvironmentEntity>();
        }

        private void InitializeTransforms(Slot slot)
        {
            var entities = FindObjectOfType<Entities>();
            if (entities != null) DestroyImmediate(entities.gameObject);

            _all = new GameObject
            {
                name = "Global"
            }.transform;
            _all.gameObject.AddComponent<Entities>();
            
            _configs = new GameObject
            {
                name = "Configs",
                transform = { parent = _all}
            }.transform;
            
            
            _prototypes = new GameObject
            {
                name = "Prototypes",
                transform = { parent = _all}
            }.transform;
            
            _environments = new GameObject
            {
                name = "Environments",
                transform = { parent = _all}
            }.transform;
            
            _towers = new GameObject
            {
                name = "Towers",
                transform = { parent = _all}
            }.transform;

            (_enemies) = new GameObject
            {
                name = "Enemies",
                transform = { parent = _all}
            }.transform;
        }
        
        [Button]
        public void LoadLevel()
        {
            var memorySlot = GetSlot();
            memorySlot.Initialize();
            
            InitializeTransforms(memorySlot);
            
            LoadEnemies(memorySlot);
            ProjectTask.AddCode("Load All other Entities");
        }

        private void LoadEnemies(Slot slot)
        {
            foreach (var entity in slot.Enemies)
            {
                if (entity.category == EntityCategory.Prototype && 
                    entity.GetField(SavePath.PrototypeCategory).ParseEntityCategory() == EntityCategory.Enemy)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = _enemies}}.AddComponent<EnemyEntity>();
                    entityObject.Load(entity, slot);
                }
            }
        }

        [Button]
        public void Clear()
        {
            DestroyImmediate(_all.gameObject);
        }

        [Button]
        public void Validate()
        {
            var entities = FindObjectsOfType<EnemyEntity>();
            foreach (var gameEntity in entities) gameEntity.Validate();
        }

        private Slot GetSlot()
        {
            if (gameConfigurations.slot == null) gameConfigurations.slot = new Slot(slotName);
            
            return gameConfigurations.slot;
        }
        
        private void OnValidate()
        {
            ConfigLoader.TryGetConfigIfNull(ref gameConfigurations, "GameCore");
        }
    }
}