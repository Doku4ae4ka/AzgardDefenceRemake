using System;
using System.Collections.Generic;
using Exerussus._1Extensions;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace Source.Scripts.ECS.Systems.View
{
    public class ViewSystem : EcsGameSystem<Signals.OnViewAssetLoaded>
    {
        private EcsFilter _viewFilter;
        
        protected override void Initialize()
        {
            _viewFilter = InGameMask.Inc<EcsData.TowerView>().Inc<EcsData.EnemyView>().Inc<EcsData.EnvironmentView>()
                .Inc<EcsData.Transform>().Inc<EcsData.Position>().End();
            Memory.save.OnDynamic += ViewSaver.TrySaveDynamic;
            Memory.save.OnStatic += ViewSaver.TrySaveStatic;
            
            Memory.load.OnPrototypes += TryLoadPrototypes;
            Memory.load.OnDynamic += TryLoadDynamic;
            Memory.load.OnStatic += TryLoadStatic;
        }

        public override void Destroy(IEcsSystems systems)
        {
            base.Destroy(systems);
            Memory.save.OnDynamic -= ViewSaver.TrySaveDynamic;
            Memory.save.OnStatic -= ViewSaver.TrySaveStatic;
            
            Memory.load.OnPrototypes -= TryLoadPrototypes;
            Memory.load.OnDynamic -= TryLoadDynamic;
            Memory.load.OnStatic -= TryLoadStatic;
        }

        protected override void OnSignal(Signals.OnViewAssetLoaded data)
        {
            if (!data.PackedEntity.Unpack(World, out var unpackedEntity))
            {
                // сделать возврат в пул вместо уничтожения
                ProjectTask.TestCode(() => { Object.Destroy(data.Transform.gameObject); });
                return;
            }

            if (Pooler.Tower.Has(unpackedEntity) ||
                Pooler.Enemy.Has(unpackedEntity) ||
                Pooler.Level.Has(unpackedEntity) ||
                Pooler.Environment.Has(unpackedEntity))
            {
                ref var transformData = ref Pooler.Transform.Add(unpackedEntity);
                transformData.Value = data.Transform;
            
                ref var position = ref Pooler.Position.Get(unpackedEntity);
                ref var rotation = ref Pooler.Rotation.Get(unpackedEntity);
                transformData.Value.position = position.Value;
                transformData.Value.rotation = rotation.Value;
            }

            if (Pooler.Level.Has(unpackedEntity))
            {
                ref var viewData = ref Pooler.BuildingTilemapView.Get(unpackedEntity);
                ref var buildingTilemapData = ref Pooler.BuildingTilemap.Get(unpackedEntity);
                buildingTilemapData.Value = viewData.Value.GetTilemap();

                buildingTilemapData.Value.FillTilemap(buildingTilemapData.RawValue);
            }
        }

        private void TryLoadDynamic(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        {
            ViewLoader.LoadViewDynamic(ecsWorld, pooler, slot, Signal);
        }

        private void TryLoadStatic(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        {
            ViewLoader.LoadViewStatic(ecsWorld, pooler, slot, Signal);
        }

        private void TryLoadPrototypes(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        {
            ViewLoader.LoadViewPrototype(ecsWorld, pooler, slot, Signal);
        }
    }
}