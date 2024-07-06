using Components;
using Components.Commands;
using Components.Tags;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Services;
using UnityEngine;

namespace Systems
{
    sealed class SpawnTowerPreview : IEcsRunSystem
    {
        private readonly EcsCustomInject<TowerUtils> _towerUtils = default;
        
        private readonly EcsFilterInject<Inc<TowerPreview, SpawnCommand>> _filter = default;
        
        private readonly EcsPoolInject<TowerPreview> _towerPreviewPool = default;
        private readonly EcsPoolInject<SpawnCommand> _spawnCommandPool = default;
        private readonly EcsPoolInject<IsBuildModeTag> _IsBuildModeTagPool = default;
        
        
    
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var towerPreview = ref _towerPreviewPool.Value.Get(entity);

                var towerPreviewData = _towerUtils.Value.GetTowerData(towerPreview.Type);
                var towerPreviewGo =
                    Object.Instantiate(towerPreviewData.prefab, Vector2.zero, Quaternion.identity);

                towerPreview.Transform = towerPreviewGo.transform;
                
                Transform towerSelectTransform = towerPreview.Transform.Find("TowerSelect");
                if (towerSelectTransform != null)
                {
                    towerSelectTransform.gameObject.SetActive(true);
                }
                else { Debug.LogError("TowerSelect not found"); }
                Transform towerRangeTransform = towerPreview.Transform.Find("Range");
                if (towerRangeTransform != null)
                {
                    towerRangeTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                else { Debug.LogError("Range object not found"); }
                Transform towerModelTransform = towerPreview.Transform.Find("TowerModel");
                if (towerModelTransform != null)
                {
                    towerModelTransform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.45f, 0.6f);
                }
                else { Debug.LogError("TowerModel not found"); }
                
                _spawnCommandPool.Value.Del(entity);
                _IsBuildModeTagPool.Value.Add(entity);

            }
        }
        
    }
}