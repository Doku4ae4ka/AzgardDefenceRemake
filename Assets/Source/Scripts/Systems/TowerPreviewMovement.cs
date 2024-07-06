using Components;
using Components.Tags;
using Infrastructure;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Services;
using UnityEngine;

namespace Systems
{
    sealed class TowerPreviewMovement : IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<InputUtils> _inputUtils = default;
        
        private readonly EcsFilterInject<Inc<TowerPreview, IsBuildModeTag>> _towerPreview = default;
        
        private readonly EcsPoolInject<TowerPreview> _towerPreviewPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _towerPreview.Value)
            {
                ref var towerPreview = ref _towerPreviewPool.Value.Get(entity);
                var exclusionTilemap = _sceneData.Value.exclusionTilemap;

                Vector3Int currentPos = _inputUtils.Value.GetMouseOnGridPos(exclusionTilemap);
                towerPreview.Transform.position = currentPos;
                towerPreview.tilePosition = currentPos;

            }
        }
        
    }
}