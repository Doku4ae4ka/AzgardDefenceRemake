using Components;
using Components.Tags;
using Infrastructure;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Services;
using UnityEngine;

namespace Systems
{
    sealed class TowerPreviewCheck : IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<InputUtils> _inputUtils = default;
        
        private readonly EcsFilterInject<Inc<TowerPreview, IsBuildModeTag>> _towerPreview = default;
        
        private readonly EcsPoolInject<TowerPreview> _towerPreviewPool = default;
        private readonly EcsPoolInject<IsBuildValidTag> _IsBuildValidTagPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _towerPreview.Value)
            {
                ref var towerPreview = ref _towerPreviewPool.Value.Get(entity);
                var exclusionTilemap = _sceneData.Value.exclusionTilemap;

                Vector3Int currentPos = _inputUtils.Value.GetMouseOnGridPos(exclusionTilemap);
                var currentTile = exclusionTilemap.GetTile(currentPos);

                if (currentTile.name == "CyanEmpty")
                {
                    UpdateTowerColor(towerPreview.Transform, currentPos, new Color(0f, 1f, 0f, 0.6f));
                    if(!_IsBuildValidTagPool.Value.Has(entity)) 
                        _IsBuildValidTagPool.Value.Add(entity);

                }
                else if (currentTile.name == "PurpleExclusion")
                {
                    UpdateTowerColor(towerPreview.Transform, currentPos, new Color(1f, 0f, 0f, 0.6f));
                    if(_IsBuildValidTagPool.Value.Has(entity)) 
                        _IsBuildValidTagPool.Value.Del(entity);
                }
                
            }
            
        }
        
        private void UpdateTowerColor(Transform towerPreview, Vector3Int newPosition, UnityEngine.Color color)
        {
            Transform towerSelectTransform = towerPreview.Find("TowerSelect");
            towerSelectTransform.gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}