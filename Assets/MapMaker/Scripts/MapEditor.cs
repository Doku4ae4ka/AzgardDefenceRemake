using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Ecs.Modules.PauldokDev.SlotSaver.Core;
using UnityEngine;

namespace MapMaker.Scripts
{
     public class MapEditor : MonoBehaviour
    {
        public string slotName = "1";
        [HideInInspector] public GameConfigurations gameConfigurations;
        [SerializeField] private bool saveEmptyMap;
        private int _index;

        public int Increment
        {
            get
            {
                _index++;
                return _index;
            }
        }

        private Transform _all;
        private Transform _config;
        private Transform _prototypes;
        private Transform _towers;
        private Transform _enemies;
        private Transform _camera;
        private Transform _level;
        private Transform _environments;
        private Transform _waves;

        [Button]
        public void SaveLevel()
        {
            _index = 0;
            var entitiesCount = 0;

            var config = GetConfig();
            var environments = GetAllEnvironments();
            var level = GetLevel();
            var enemies = GetAllEnemies();
            var towers = GetAllTowers();
            var waves = GetAllWaves();
            //var cameraEntity = GetCamera();

            entitiesCount += environments.Length + enemies.Length + towers.Length + waves.Length;
            //if (cameraEntity != null) entitiesCount++;
            if (level != null) entitiesCount++;

            if (!saveEmptyMap && entitiesCount == 0) return;

            var memorySlot = GetSlot();
            memorySlot.Initialize();
            
            memorySlot.Clear();
            
            SaveEntities(environments, memorySlot, "environment");
            SaveEntities(towers, memorySlot, "towers");
            SaveEntities(enemies, memorySlot, "enemies");
            SaveEntities(waves, memorySlot, "waves");
            level.Save(SavePath.Level.ID, memorySlot);
            //cameraEntity.Save(SavePath.Camera.ID, memorySlot);
            config.Save(SavePath.Config.ID, memorySlot);

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
        
        private EnemyEntity[] GetAllEnemies()
        {
            return FindObjectsByType<EnemyEntity>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        }

        private TowerEntity[] GetAllTowers()
        {
            return FindObjectsByType<TowerEntity>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        }

        private EnvironmentEntity[] GetAllEnvironments()
        {
            return FindObjectsByType<EnvironmentEntity>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        }
        
        private WavesEntity[] GetAllWaves()
        {
            return FindObjectsByType<WavesEntity>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        }

        private CameraEntity GetCamera()
        {
            return FindObjectOfType<CameraEntity>();
        }
        
        private LevelEntity GetLevel()
        {
            return FindObjectOfType<LevelEntity>();
        }
        
        private ConfigEntity GetConfig()
        {
            return FindObjectOfType<ConfigEntity>();
        }


        private void InitializeTransforms(Slot slot)
        {
            var entities = FindObjectOfType<Entities>();
            if (entities != null) DestroyImmediate(entities.gameObject);

            _all = new GameObject
            {
                name = "Level"
            }.transform;
            _all.gameObject.AddComponent<Entities>();

            _config = new GameObject
            {
                name = "Config",
                transform = { parent = _all}
            }.transform;

            _level = new GameObject
            {
                name = "Level",
                transform = { parent = _all}
            }.transform;

            _prototypes = new GameObject
            {
                name = "Prototypes",
                transform = { parent = _all}
            }.transform;

            _towers = new GameObject
            {
                name = "Towers",
                transform = { parent = _all}
            }.transform;

            _enemies = new GameObject
            {
                name = "Enemies",
                transform = { parent = _all}
            }.transform;
            
            _camera = new GameObject
            {
                name = "Camera",
                transform = { parent = _all}
            }.transform;
            
            _environments = new GameObject
            {
                name = "Environments",
                transform = { parent = _all}
            }.transform;
            
            _waves = new GameObject
            {
                name = "Waves",
                transform = { parent = _all}
            }.transform;
        }

        [Button]
        public void LoadLevel()
        {
            var memorySlot = GetSlot();
            memorySlot.Initialize();

            InitializeTransforms(memorySlot);
            LoadConfigs(memorySlot);
            LoadEnvironments(memorySlot);
            LoadTowers(memorySlot);
            LoadLevel(memorySlot);
            //LoadCamera(memorySlot);
            LoadWaves(memorySlot);
            LoadEnemies(memorySlot);
        }

        private void LoadEnemies(Slot slot)
        {
            foreach (var entity in slot.Dynamics)
            {
                if (entity.category == SavePath.EntityCategory.Enemy)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = _enemies}}.AddComponent<EnemyEntity>();
                    entityObject.Load(entity, slot, this);
                }
            }
            
            foreach (var entity in slot.Prototypes)
            {
                if (entity.category == SavePath.EntityCategory.Prototype && 
                    entity.GetField(SavePath.Prototype.Category) == SavePath.EntityCategory.Enemy)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = _prototypes}}.AddComponent<EnemyEntity>();
                    entityObject.Load(entity, slot, this);
                }
            }
        }

        private void LoadTowers(Slot slot)
        {
            foreach (var entity in slot.Dynamics)
            {
                if (entity.category == SavePath.EntityCategory.Tower)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = _towers}}.AddComponent<TowerEntity>();
                    entityObject.Load(entity, slot, this);
                }
            }
            
            foreach (var entity in slot.Prototypes)
            {
                if (entity.category == SavePath.EntityCategory.Prototype && 
                    entity.GetField(SavePath.Prototype.Category) == SavePath.EntityCategory.Tower)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = _prototypes}}.AddComponent<TowerEntity>();
                    entityObject.Load(entity, slot, this);
                }
            }
        }

        private void LoadConfigs(Slot slot)
        {
            if (slot.Configs == null) slot.CreateConfig(new SlotEntity(SavePath.Config.ID, SavePath.EntityCategory.Config));
            var entityObject = new GameObject { name = slot.Configs.id, transform = { parent = _config}}.AddComponent<ConfigEntity>();
            entityObject.Load(slot.Configs, slot, this);
        }
        
        private void LoadLevel(Slot slot)
        {
            foreach (var entity in slot.Dynamics)
            {
                if (entity.category == SavePath.EntityCategory.Level)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = _level}}.AddComponent<LevelEntity>();
                    entityObject.Load(entity, slot, this);
                }
            }
        }
        
        private void LoadCamera(Slot slot)
        {
            foreach (var entity in slot.Dynamics)
            {
                if (entity.category == SavePath.EntityCategory.Config)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = _camera}}.AddComponent<CameraEntity>();
                    entityObject.Load(entity, slot, this);
                }
            }
        }
        
        private void LoadWaves(Slot slot)
        {
            foreach (var entity in slot.Statics)
            {
                if (entity.category == SavePath.EntityCategory.Waves)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = (_waves)}}.AddComponent<WavesEntity>();
                    entityObject.Load(entity, slot, this);
                }
            }
        }

        private void LoadEnvironments(Slot slot)
        {
            foreach (var entity in slot.Statics)
            {
                if (entity.category == SavePath.EntityCategory.Environment)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = (_environments)}}.AddComponent<EnvironmentEntity>();
                    entityObject.Load(entity, slot, this);
                }
            }
        }
        
        [Button]
        public void Clear()
        {
            //DestroyImmediate(_all.gameObject);
            var globals = FindObjectsOfType<Entities>();
            if (globals != null) foreach (var foundObject in globals) DestroyImmediate(foundObject.gameObject);
            var envs = GetAllEnvironments();
            if (envs != null) foreach (var foundObject in envs) DestroyImmediate(foundObject.gameObject);
            var enemies = GetAllEnemies();
            if (enemies != null) foreach (var foundObject in enemies) DestroyImmediate(foundObject.gameObject);
            var towers = GetAllTowers();
            if (towers != null) foreach (var foundObject in towers) DestroyImmediate(foundObject.gameObject);
            var configs = FindObjectsByType<ConfigEntity>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (configs != null) foreach (var foundObject in configs) DestroyImmediate(foundObject.gameObject);
            var cameras = FindObjectsByType<CameraEntity>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (cameras != null) foreach (var foundObject in cameras) DestroyImmediate(foundObject.gameObject);
            var waves = GetAllWaves();
            if (waves != null) foreach (var foundObject in waves) DestroyImmediate(foundObject.gameObject);
        }
        
        private void TryDel(ref MonoBehaviour[] array)
        {
            
        }

        [Button]
        public void Validate()
        {
            var enemyEntities = FindObjectsOfType<EnemyEntity>();
            foreach (var gameEntity in enemyEntities) gameEntity.Validate();
            
            var towerEntities = FindObjectsOfType<TowerEntity>();
            foreach (var gameEntity in towerEntities) gameEntity.Validate();
            
            var evnironment = FindObjectsOfType<EnvironmentEntity>();
            foreach (var gameEntity in evnironment) gameEntity.Validate();
        }

        private Slot GetSlot()
        {
            if (gameConfigurations.slot == null) gameConfigurations.slot = new Slot(slotName);
            
            return gameConfigurations.slot;
        }
    }
}