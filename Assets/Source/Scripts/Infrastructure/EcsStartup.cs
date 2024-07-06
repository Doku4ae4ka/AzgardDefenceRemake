using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using Services;
using Systems;
using Systems.EcsInput;
using Unity.VisualScripting;
using UnityEngine;

namespace Infrastructure
{
    public sealed class EcsStartup : MonoBehaviour {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private Configuration _configuration;
        [SerializeField] EcsUguiEmitter _uguiEmitter;
        private EcsWorld _world;
        private IEcsSystems _systems;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            
            var inputUtils = new InputUtils();
            var towerUtils = new TowerUtils(_configuration.TowerDatas);
            
            AddSystems();
        
            _systems
                .AddWorld (new EcsWorld (), Idents.Worlds.Events)
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem (Idents.Worlds.Events))
#endif
                .Inject (_sceneData, _configuration, inputUtils, towerUtils)
                .InjectUgui (_uguiEmitter, Idents.Worlds.Events)
                .Init ();
        
        
        }
        
        private void AddSystems()
        {
            _systems
                .Add(new ShopButtonsInput())
                .Add(new SpawnTowerPreview())
                .Add(new TowerPreviewMovement())
                .Add(new TowerPreviewCheck())
                .Add(new BuildingSystem())
                .Add(new SpawnTower());
        }
    
        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
            }
        
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}