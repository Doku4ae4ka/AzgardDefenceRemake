using System.Collections.Generic;
using System.Linq;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Leopotam.SpaceHash;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class TargetingSystem : EcsGameSystem
    {
        private EcsFilter _towerFilter;
        private List<SpaceHashHit<int>> _result;
        
        protected override void Initialize()
        {
            _towerFilter = InGameMask.Inc<EcsData.Tower>().End();
        }

        protected override void Update()
        {
            _towerFilter.Foreach(OnUpdate);
        }

        private void OnUpdate(int towerEntity)
        {
            ref var towerData = ref Pooler.Tower.Get(towerEntity);

            var enemyEntities = new List<int>();
            var originPosition = Pooler.GetPosition(towerEntity);
            _result = SpaceHash.GetAllInRadius(originPosition, towerData.Radius);
            foreach (var spaceHashHit in _result)
            {
                var foundedEntity = spaceHashHit.Id;
                if (foundedEntity == towerEntity) continue;
                if(!Pooler.Enemy.Has(foundedEntity)) continue;

                ref var enemyData = ref Pooler.Enemy.Get(foundedEntity);
                if (!IsEnemyCompatible(towerData.EnemyType, enemyData.EnemyType)) continue;
                
                enemyEntities.Add(foundedEntity);
            }

            ref var targetData = ref Pooler.Target.Get(towerEntity);
            targetData.PackedTarget = SelectTarget(enemyEntities, towerData.TargetingType);
        }
        
        private EcsPackedEntity SelectTarget(List<int> entities, TargetingType targetingType)
        {
            switch (targetingType)
            {
                case TargetingType.Closest:
                    return World.PackEntity(entities.OrderBy(GetDistanceToCastle).First());
                case TargetingType.Weakest:
                    return World.PackEntity(entities.OrderBy(GetHealth).First());
                case TargetingType.Random:
                    int randomIndex = Random.Range(0, entities.Count);
                    return World.PackEntity(entities[randomIndex]);

                default:
                    return World.PackEntity(entities[0]);
            }
        }
        
        private float GetDistanceToCastle(int enemyEntity)
        {
            return Pooler.Movable.Get(enemyEntity).DistanceToCastle;
        }

        // Получение здоровья из компонента HealthData
        private float GetHealth(int enemyEntity)
        {
            return Pooler.Health.Get(enemyEntity).Current;
        }
        
        private bool IsEnemyCompatible(EnemyType towerEnemyType, EnemyType enemyType)
        {
            return towerEnemyType == EnemyType.Both || towerEnemyType == enemyType;
        }
    }
}