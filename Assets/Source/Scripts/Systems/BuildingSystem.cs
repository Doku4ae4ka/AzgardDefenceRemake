using Components;
using Components.Tags;
using Infrastructure;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    sealed class BuildingSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        
        private readonly EcsFilterInject<Inc<IsBuildModeTag, IsBuildValidTag>> _isBuildingValidTagFilter = default;
        private readonly EcsFilterInject<Inc<IsBuildModeTag>> _isBuildingModeTagFilter = default;
        
        private readonly EcsPoolInject<IsBuildModeTag> _isBuildModeTagPool = default;
        private readonly EcsPoolInject<IsBuildValidTag> _isBuildingValidTagPool = default;
        
        private readonly EcsPoolInject<TowerPreview> _towerPreviewPool = default;
        private readonly EcsPoolInject<BuildRequest> _buildRequestPool = default;
        
        public void Run(IEcsSystems systems)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                foreach (var entity in _isBuildingValidTagFilter.Value)
                {
                    CreateTowerEntity(entity);
                }

                foreach (var entity in _isBuildingModeTagFilter.Value)
                    DestroyTowerPreview(entity);
            }
            
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                foreach (var entity in _isBuildingModeTagFilter.Value)
                    DestroyTowerPreview(entity);
            }
        }

        private void CreateTowerEntity(int entity)
        {
            var newEntity = _buildRequestPool.Value.GetWorld ().NewEntity ();
            ref var buildRequest = ref _buildRequestPool.Value.Add (newEntity);
                    
            ref var towerPreview = ref _towerPreviewPool.Value.Get(entity);

            buildRequest.Parent = _sceneData.Value.TowersParent;
            buildRequest.TowerType = towerPreview.Type;
            buildRequest.Position = towerPreview.tilePosition;
            
            var exclusionTilemap = _sceneData.Value.exclusionTilemap;
            exclusionTilemap.SetTile(towerPreview.tilePosition, _sceneData.Value.exclusionTile);
            
        }

        private void DestroyTowerPreview(int entity)
        {
            ref var towerPreview = ref _towerPreviewPool.Value.Get (entity);
            GameObject.Destroy(towerPreview.Transform.gameObject);
            _towerPreviewPool.Value.GetWorld().DelEntity(entity);
        }
    }
}