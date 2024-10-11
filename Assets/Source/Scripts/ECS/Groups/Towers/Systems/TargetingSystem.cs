using System.Collections.Generic;
using System.Linq;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Leopotam.SpaceHash;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.Towers.Systems
{
    public class TargetingSystem : EasySystem<TowerPooler>
    {
        private EcsFilter _towerFilter;
        private List<SpaceHashHit<int>> _result;
        
        // protected override void Initialize()
        // {
        //     _towerFilter = InGameMask.Inc<EcsData.Tower>().Inc<EcsData.Target>().End();
        // }
        //
        // protected override void Update()
        // {
        //     _towerFilter.Foreach(OnUpdate);
        // }
        //
        // private void OnUpdate(int towerEntity)
        // {
        //     ref var targetData = ref Pooler.Target.Get(towerEntity);
        //     
        //     targetData.TickRemaining -= DeltaTime;
        //     if (targetData.TickRemaining > 0) return;
        //
        //     ref var towerData = ref Pooler.Tower.Get(towerEntity);
        //
        //     var enemyEntities = new List<int>();
        //     var originPosition = Pooler.GetPosition(towerEntity);
        //     _result = SpaceHash.GetAllInRadius(originPosition, towerData.Radius);
        //     foreach (var spaceHashHit in _result)
        //     {
        //         var foundedEntity = spaceHashHit.Id;
        //         if (foundedEntity == towerEntity) continue;
        //         if(!Pooler.Enemy.Has(foundedEntity)) continue;
        //
        //         ref var enemyData = ref Pooler.Enemy.Get(foundedEntity);
        //         if (!IsEnemyCompatible(towerData.EnemyType, enemyData.EnemyType)) continue;
        //     
        //         enemyEntities.Add(foundedEntity);
        //     }
        //     
        //     targetData.HasTarget = !(enemyEntities.Count < 1);
        //
        //     if(!targetData.HasTarget) return;
        //     targetData.TargetEntity = SelectTarget(enemyEntities, towerData.TargetingType);
        //     targetData.TickRemaining = Constants.Main.TickTime;
        // }
        //
        // private int SelectTarget(List<int> entities, TargetingType targetingType)
        // {
        //     switch (targetingType)
        //     {
        //         case TargetingType.Closest:
        //             return entities.OrderBy(GetDistanceToCastle).Last();
        //         case TargetingType.Weakest:
        //             return entities.OrderBy(GetHealth).First();
        //         case TargetingType.Random:
        //             int randomIndex = Random.Range(0, entities.Count);
        //             return entities[randomIndex];
        //
        //         default:
        //             return entities[0];
        //     }
        // }
        //
        // private float GetDistanceToCastle(int enemyEntity)
        // {
        //     return Pooler.Movable.Get(enemyEntity).PassedDistance;
        // }
        //
        // // Получение здоровья из компонента HealthData
        // private float GetHealth(int enemyEntity)
        // {
        //     return Pooler.Health.Get(enemyEntity).Current;
        // }
        //
        // private bool IsEnemyCompatible(EnemyType towerEnemyType, EnemyType enemyType)
        // {
        //     return towerEnemyType == EnemyType.Both || towerEnemyType == enemyType;
        // }
    }
}