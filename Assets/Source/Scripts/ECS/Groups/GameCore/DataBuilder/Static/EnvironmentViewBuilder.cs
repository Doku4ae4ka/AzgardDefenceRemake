using System;
using ECS.Modules.Exerussus.Movement;
using ECS.Modules.Exerussus.TransformRelay;
using ECS.Modules.Exerussus.ViewCreator;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.Extensions;
using Source.Scripts.MonoBehaviours.Views;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore.DataBuilder.Static
{
    public class EnvironmentViewBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Static;
        public override EcsWorld.Mask FilterMask => _world.Filter<ViewCreatorData.AssetViewApi>().Inc<EcsData.Environment>().Exc<ViewCreatorData.AssetLoadingMark>();
        
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
            return slotEntity.type == SavePath.EntityType.Environment;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if(!slotEntity.TryGetField(SavePath.View.Environment, out var viewValue)) return;
            var position = slotEntity.TryGetVector3Field(SavePath.Movement.Position, out var positionValue) ? positionValue : Vector3.zero;
            
            ref var positionData = ref _movementPooler.Position.Add(entity);
            positionData.Value = position;
            
            _viewCreatorPooler.CreateView(entity, slotEntity.id, viewValue.ParseToAssetReference(), position, (api) =>
            {
                ref var transformData = ref _transformPooler.Transform.Add(entity);
                transformData.Value = api.transform;
                    
                ref var environmentView = ref _corePooler.EnvironmentView.Add(entity);
                environmentView.ViewId = viewValue.ParseToAssetReference();
                environmentView.Value = (EnvironmentViewApi)api;
            });
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            ref var environmentView = ref _corePooler.EnvironmentView.Get(entity);
            slotEntity.SetField(SavePath.View.Enemy, $"{environmentView.ViewId}");
            ref var transformData = ref _transformPooler.Transform.Get(entity);
            slotEntity.SetField(SavePath.WorldSpace.Position, $"{transformData.Value.position}");
        }
        
        public override void OnUnloadSlotProcess(int entity)
        {
            _viewCreatorPooler.ReleaseView(entity);
        }
    }
}