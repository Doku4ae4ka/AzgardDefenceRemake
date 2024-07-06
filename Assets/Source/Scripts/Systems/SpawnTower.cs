using Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Services;
using UnityEngine;

namespace Systems
{
    sealed class SpawnTower : IEcsRunSystem
    {
        private readonly EcsCustomInject<TowerUtils> _towerUtils = default;
        
        private readonly EcsFilterInject<Inc<BuildRequest>> _buildRequest = default;
        
        private readonly EcsPoolInject<Tower> _towerPool = default;
        private readonly EcsPoolInject<BuildRequest> _buildRequestPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _buildRequest.Value)
            {
                ref var buildRequest = ref _buildRequestPool.Value.Get(entity);
                ref var tower = ref _towerPool.Value.Add (entity);

                var towerData = _towerUtils.Value.GetTowerData(buildRequest.TowerType);
                
                var towerGo =
                    Object.Instantiate(towerData.prefab, Vector2.zero, Quaternion.identity);

                tower.Transform = towerGo.transform;
                tower.Transform.position = buildRequest.Position;
                tower.Transform.parent = buildRequest.Parent;
                
                tower.Type = buildRequest.TowerType;
                tower.Level = 1;
                
                tower.Damage = towerData.damage;
                tower.RateOfFire = towerData.rateOfFire;
                tower.Range = towerData.range;
                
                tower.PlaceCost = towerData.placeCost;
                tower.BaseUpgradeCost = towerData.baseUpgradeCost;
                tower.BaseLevelUpCost = towerData.baseLevelUpCost;

                tower.Category = towerData.category;
                tower.DamageType = towerData.damageType;
                
                
                _buildRequestPool.Value.Del(entity);
                
            }
        }
    }
}