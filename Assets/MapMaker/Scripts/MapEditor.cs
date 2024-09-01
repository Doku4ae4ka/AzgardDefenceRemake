
using Exerussus._1Extensions;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.Libraries;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace MapMaker.Scripts
{
    public class MapEditor : MonoBehaviour
    {
        public string slotName = "1";
        public string locationName = "home";
        [HideInInspector] public GameConfigurations gameConfigurations;
        [HideInInspector] public ViewLibrary viewLibrary;
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
        
        private Transform _globals;
        private Transform _globalsConfigs;
        private Transform _globalsPrototypes;
        
        private Transform _locals;
        private Transform _localsEnvironments;
        private Transform _localsItems;
        private Transform _localsCharacters;

        [Button]
        public void SaveLevel()
        {
            _index = 0;
            var entitiesCount = 0;

            var config = GetConfig();
            var quests = GetAllQuests();
            var playerCharacter = GetPlayerCharacter();
            var playerInfo = GetPlayerInfo();
            var characters = GetAllCharacters();
            var environments = GetAllEnvironments();
            var items = GetAllItems();

            entitiesCount += quests.Length + characters.Length + environments.Length + items.Length;
            if (playerCharacter != null) entitiesCount++;
            
            if (!saveEmptyMap && entitiesCount == 0) return;
            
            var memorySlot = GetSlot();
            memorySlot.Initialize();
            var location = memorySlot.GetOrCreateLocation(locationName);
            
            memorySlot.global.Clear();
            location.Clear();
            
            SaveEntities(characters, memorySlot, location, $"{location.ID}.character");
            SaveEntities(items, memorySlot, location, $"{location.ID}.item");
            SaveEntities(environments, memorySlot, location, $"{location.ID}.environment");
            SaveEntities(quests, memorySlot, location, "global.quest");
            config.Save(SavePath.Config.ID, memorySlot, location);
            
            if (playerCharacter != null) playerCharacter.Save($"global.player.character" ,memorySlot, location);
            if (playerInfo != null) playerInfo.Save($"global.player.info", memorySlot, location);

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

        private void SaveEntities(IEntityObject[] entities, Slot slot, Location location, string prefix)
        {
            if (entities is { Length: > 0 })
            {
                foreach (var entity in entities)
                {
                    entity.Save($"{prefix}.{Increment}", slot, location);
                }
            }
        }

        private ConfigEntity GetConfig()
        {
            return FindObjectOfType<ConfigEntity>();
        }

        private QuestEntity[] GetAllQuests()
        {
            return FindObjectsOfType<QuestEntity>();
        }

        private CharacterEntity[] GetAllCharacters()
        {
            return FindObjectsOfType<CharacterEntity>();
        }

        private ItemEntity[] GetAllItems()
        {
            return FindObjectsOfType<ItemEntity>();
        }

        private EnvironmentEntity[] GetAllEnvironments()
        {
            return FindObjectsOfType<EnvironmentEntity>();
        }

        private PlayerCharacterEntity GetPlayerCharacter()
        {
            return FindObjectOfType<PlayerCharacterEntity>();
        }

        private PlayerInfoEntity GetPlayerInfo()
        {
            return FindObjectOfType<PlayerInfoEntity>();
        }

        private void InitializeTransforms(Location location)
        {
            Clear();

            _globals = new GameObject
            {
                name = "Global"
            }.transform;
            _globals.gameObject.AddComponent<GlobalEntities>();
            
            _globalsConfigs = new GameObject
            {
                name = "Configs",
                transform = { parent = _globals}
            }.transform;
            
            _globalsQuests = new GameObject
            {
                name = "Quests",
                transform = { parent = _globals}
            }.transform;
            
            
            _globalsPrototypes = new GameObject
            {
                name = "Prototypes",
                transform = { parent = _globals}
            }.transform;
            
            _locals = new GameObject
            {
                name = $"Local.{location.ID}"
            }.transform;
            _locals.gameObject.AddComponent<LocalEntities>();
            
            _localsEnvironments = new GameObject
            {
                name = "Environments",
                transform = { parent = _locals}
            }.transform;
            
            _localsItems = new GameObject
            {
                name = "Items",
                transform = { parent = _locals}
            }.transform;

            (_localsCharacters) = new GameObject
            {
                name = "Characters",
                transform = { parent = _locals}
            }.transform;
        }
        
        [Button]
        public void LoadLevel()
        {
            var memorySlot = GetSlot();
            memorySlot.Initialize();
            var location = memorySlot.GetOrCreateLocation(locationName);
            
            InitializeTransforms(location);
            LoadConfigs(memorySlot, location);
            LoadEnvironments(memorySlot, location);
            LoadItems(memorySlot, location);
            LoadCharacters(memorySlot, location);
            LoadPlayerCharacter(memorySlot, location);
            LoadPlayerInfo(memorySlot, location);
        }

        private void LoadCharacters(Slot slot, Location location)
        {
            foreach (var entity in location.Characters)
            {
                if (entity.category == EntityCategory.Character || 
                    entity.category == EntityCategory.Prototype && 
                    entity.GetField(SavePath.Prototype.Category).ParseEntityCategory() == EntityCategory.Character)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = _localsCharacters}}.AddComponent<CharacterEntity>();
                    entityObject.Load(entity, slot, location);
                }
            }
        }

        private void LoadConfigs(Slot slot, Location location)
        {
            if (slot.global.Configs == null) slot.global.CreateConfig(new Entity(SavePath.Config.ID, EntityCategory.Config));
            var entityObject = new GameObject { name = slot.global.Configs.id, transform = { parent = _globalsConfigs}}.AddComponent<ConfigEntity>();
            entityObject.Load(slot.global.Configs, slot, location);
        }

        private void LoadEnvironments(Slot slot, Location location)
        {
            foreach (var entity in location.Environment)
            {
                if (entity.category == EntityCategory.Environment)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = (_localsEnvironments)}}.AddComponent<EnvironmentEntity>();
                    entityObject.Load(entity, slot, location);
                }
            }
        }

        private void LoadItems(Slot slot, Location location)
        {
            foreach (var entity in location.Items)
            {
                if (entity.category == EntityCategory.Item)
                {
                    var entityObject = new GameObject { name = entity.id, transform = { parent = (_localsItems)}}.AddComponent<ItemEntity>();
                    entityObject.Load(entity, slot, location);
                }
            }
        }

        private void LoadPlayerCharacter(Slot slot, Location location)
        {
            var entityObject = new GameObject { name = slot.global.Player.character.id, transform = { parent = _globals}}.AddComponent<PlayerCharacterEntity>();
            entityObject.Load(slot.global.Player.character, slot, location);
        }

        private void LoadPlayerInfo(Slot slot, Location location)
        {
            var entityObject = new GameObject { name = slot.global.Player.info.id, transform = { parent = _globals}}.AddComponent<PlayerInfoEntity>();
            entityObject.Load(slot.global.Player.info, slot, location);
        }

        [Button]
        public void Clear()
        {
            var globals = FindObjectsOfType<GlobalEntities>();
            if (globals != null) foreach (var foundObject in globals) DestroyImmediate(foundObject.gameObject);
            var locals = FindObjectsOfType<LocalEntities>();
            if (locals != null) foreach (var foundObject in locals) DestroyImmediate(foundObject.gameObject);
            var playerCharacterEntities = FindObjectsOfType<PlayerCharacterEntity>();
            if (playerCharacterEntities != null) foreach (var foundObject in playerCharacterEntities) DestroyImmediate(foundObject.gameObject);
            var playerInfoEntities = FindObjectsOfType<PlayerInfoEntity>();
            if (playerInfoEntities != null) foreach (var foundObject in playerInfoEntities) DestroyImmediate(foundObject.gameObject);
            var envs = GetAllEnvironments();
            if (envs != null) foreach (var foundObject in envs) DestroyImmediate(foundObject.gameObject);
            var characters = GetAllCharacters();
            if (characters != null) foreach (var foundObject in characters) DestroyImmediate(foundObject.gameObject);
            var configs = FindObjectsOfType<ConfigEntity>();
            if (configs != null) foreach (var foundObject in configs) DestroyImmediate(foundObject.gameObject);
            var items = GetAllItems();
            if (items != null) foreach (var foundObject in items) DestroyImmediate(foundObject.gameObject);
            var quests = GetAllQuests();
            if (quests != null) foreach (var foundObject in quests) DestroyImmediate(foundObject.gameObject);
        }

        private void TryDel(ref MonoBehaviour[] array)
        {
            
        }
        
        [Button]
        public void Validate()
        {
            var entities = FindObjectsOfType<CharacterEntity>();
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
            ConfigLoader.TryGetConfigIfNull(ref viewLibrary, "GameCore");
        }
    }
}