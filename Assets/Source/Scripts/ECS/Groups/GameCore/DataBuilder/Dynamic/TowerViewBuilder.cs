using System;
using ECS.Modules.Exerussus.Movement;
using ECS.Modules.Exerussus.TransformRelay;
using ECS.Modules.Exerussus.ViewCreator;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.ECS.Groups.Towers.MonoBehaviours;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore.DataBuilder
{
    public class TowerViewBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<ViewCreatorData.AssetViewApi>().Inc<EcsData.TowerMark>().Exc<ViewCreatorData.AssetLoadingMark>();
        
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
            return slotEntity.type == SavePath.EntityType.Tower;
        }

        public override Action<int> SetDataBuilderForPrototype(int entity, SlotEntity slotEntity)
        {
            Action<int> resultAction = i => { };
            
            if(!slotEntity.TryGetField(SavePath.View.Tower, out var viewValue)) return resultAction;
            var position = slotEntity.TryGetVector3Field(SavePath.Movement.Position, out var positionValue) ? positionValue : Vector3.zero;
            
            
            resultAction += i =>
            {
                _movementPooler.Position.Add(i);
                _corePooler.TilePosition.Add(i);
                _viewCreatorPooler.CreateView(i, slotEntity.id, viewValue.ParseToAssetReference(), position, (api) =>
                {
                    ref var transformData = ref _transformPooler.Transform.Add(i);
                    transformData.Value = api.transform;
                    
                    ref var towerView = ref _corePooler.TowerView.Add(i);
                    towerView.ViewId = viewValue.ParseToAssetReference();
                    towerView.Value = (TowerViewApi)api;
                    towerView.Value.Hide();
                });
            };
            
            return resultAction;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if(!slotEntity.TryGetField(SavePath.View.Tower, out var viewValue)) return;
            var position = slotEntity.TryGetVector3Field(SavePath.Movement.Position, out var positionValue) ? positionValue : Vector3.zero;
            
            ref var positionData = ref _movementPooler.Position.Add(entity);
            positionData.Value = position;
            ref var tilePositionData = ref _corePooler.TilePosition.Add(entity);
            tilePositionData.Value = Vector3Int.RoundToInt(positionValue);
            
            _viewCreatorPooler.CreateView(entity, slotEntity.id, viewValue.ParseToAssetReference(), position, (api) =>
            {
                ref var transformData = ref _transformPooler.Transform.Add(entity);
                transformData.Value = api.transform;
                    
                ref var towerView = ref _corePooler.TowerView.Add(entity);
                towerView.ViewId = viewValue.ParseToAssetReference();
                towerView.Value = (TowerViewApi)api;
            });
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            ref var towerView = ref _corePooler.TowerView.Get(entity);
            slotEntity.SetField(SavePath.View.Tower, $"{towerView.ViewId}");
            ref var transformData = ref _transformPooler.Transform.Get(entity);
            slotEntity.SetField(SavePath.WorldSpace.Position, $"{transformData.Value.position}");
        }
        
        public override void OnUnloadSlotProcess(int entity)
        {
            _viewCreatorPooler.ReleaseView(entity);
        }
    }
}