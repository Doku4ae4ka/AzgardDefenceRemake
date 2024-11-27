using System;
using ECS.Modules.Exerussus.Movement;
using ECS.Modules.Exerussus.TransformRelay;
using ECS.Modules.Exerussus.ViewCreator;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.Enemies.MonoBehaviours;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore.DataBuilder.Dynamic
{
    public class EnemyViewBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<ViewCreatorData.AssetViewApi>().Inc<EcsData.Enemy>().Exc<ViewCreatorData.AssetLoadingMark>();
        
        private ViewCreatorPooler _viewCreatorPooler;
        private MovementPooler _movementPooler;
        private TransformRelayPooler _transformPooler;
        private GameCorePooler _corePooler;
        private EcsWorld _world;
        public override void Initialize(GameShare gameShare)
        {
            gameShare.GetSharedObject(ref _viewCreatorPooler);
            gameShare.GetSharedObject(ref _movementPooler);
            gameShare.GetSharedObject(ref _transformPooler);
            gameShare.GetSharedObject(ref _corePooler);
            _world = gameShare.GetSharedObject<Componenter>().World;
        }

        public override bool CheckProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.type == SavePath.EntityType.Enemy;
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i => { };
            
            if(!slotEntity.TryGetField(SavePath.View.Enemy, out var viewValue)) return resultAction;
            var position = slotEntity.TryGetVector3Field(SavePath.Movement.Position, out var positionValue) ? positionValue : Vector3.zero;
            
            
            resultAction += i =>
            {
                _movementPooler.Position.Add(i);
                _viewCreatorPooler.CreateView(i, slotEntity.id, viewValue.ParseToAssetReference(), position, (api) =>
                {
                    ref var transformData = ref _transformPooler.Transform.Add(i);
                    transformData.Value = api.transform;
                    
                    ref var enemyView = ref _corePooler.EnemyView.Add(i);
                    enemyView.ViewId = viewValue.ParseToAssetReference();
                    enemyView.Value = (EnemyViewApi)api;
                    enemyView.Value.Hide();
                });
            };
            
            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if(!slotEntity.TryGetField(SavePath.View.Enemy, out var viewValue)) return;
            var position = slotEntity.TryGetVector3Field(SavePath.Movement.Position, out var positionValue) ? positionValue : Vector3.zero;
            
            ref var positionData = ref _movementPooler.Position.Add(entity);
            positionData.Value = position;
            
            _viewCreatorPooler.CreateView(entity, slotEntity.id, viewValue.ParseToAssetReference(), position, (api) =>
            {
                ref var transformData = ref _transformPooler.Transform.Add(entity);
                transformData.Value = api.transform;
                    
                ref var enemyView = ref _corePooler.EnemyView.Add(entity);
                enemyView.ViewId = viewValue.ParseToAssetReference();
                enemyView.Value = (EnemyViewApi)api;
            });
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            ref var enemyView = ref _corePooler.EnemyView.Get(entity);
            slotEntity.SetField(SavePath.View.Enemy, $"{enemyView.ViewId}");
            ref var transformData = ref _transformPooler.Transform.Get(entity);
            slotEntity.SetField(SavePath.WorldSpace.Position, $"{transformData.Value.position}");
        }
        
        public override void OnUnloadSlotProcess(int entity)
        {
            _viewCreatorPooler.ReleaseView(entity);
        }
    }
}