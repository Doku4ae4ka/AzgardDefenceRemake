using System;
using System.Collections.Generic;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.MonoBehaviours.Views;
using Source.Scripts.SaveSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Scripts.ECS.Systems.View
{
    public static class ViewLoader
    {
        #region Prototypes
        
        public static void LoadViewPrototype(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            LoadTowerViewPrototype(world, pooler, slot, signal);
            LoadEnemyViewPrototype(world, pooler, slot, signal);
        }
        public static void LoadTowerViewPrototype(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().Inc<EcsData.Tower>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);

                if (!savingEntity.TryGetField(SavePath.View.Tower, out var viewValue)) continue;
                
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                
                Action<int> buildAction = (int newEntity) =>
                {
                    ref var entityData = ref pooler.Entity.Get(newEntity);
                    
                    pooler.Position.Add(newEntity);
                    pooler.Rotation.Add(newEntity);
                    pooler.TilePosition.Add(newEntity);
                    ref var viewData = ref pooler.TowerView.Add(newEntity);
                    viewData.ViewId = viewValue.ParseToAssetReference();
                    viewData.Value = new TowerViewApi();
                    viewData.Value.LoadView(viewData.ViewId, signal, world.PackEntity(newEntity));
                    viewData.Value.SetName(entityData.EntityID);
                    viewData.Value.Hide();
                };
                
                buildAction.Invoke(entity);
                //ref var viewData = ref pooler.TowerView.Get(entity);
                
                //Object.Destroy(viewData.Value.TowerAsset.gameObject);
                
                prototypeData.DataBuilder.Add(buildAction);
            }
        }
        
        public static void LoadEnemyViewPrototype(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Prototype>().Inc<EcsData.Enemy>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);

                if (!savingEntity.TryGetField(SavePath.View.Enemy, out var viewValue)) continue;
                
                ref var prototypeData = ref pooler.Prototype.Get(entity);
                
                Action<int> buildAction = (int newEntity) =>
                {
                    ref var entityData = ref pooler.Entity.Get(newEntity);
                    
                    pooler.Position.Add(newEntity);
                    pooler.Rotation.Add(newEntity);
                    ref var viewData = ref pooler.EnemyView.Add(newEntity);
                    viewData.ViewId = viewValue.ParseToAssetReference();
                    viewData.Value = new EnemyViewApi();
                    viewData.Value.LoadView(viewData.ViewId, signal, world.PackEntity(newEntity));
                    viewData.Value.SetName(entityData.EntityID);
                    viewData.Value.Hide();
                };
                
                buildAction.Invoke(entity);
                //ref var viewData = ref pooler.EnemyView.Get(entity);
                
                //Object.Destroy(viewData.Value.EnemyAsset.gameObject);
                
                prototypeData.DataBuilder.Add(buildAction);
            }
        }
        
        #endregion

        #region Dynamic
        
        public static void LoadViewDynamic(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            LoadTowerView(world, pooler, slot, signal);
            LoadEnemyView(world, pooler, slot, signal);
            LoadBuildingTilemapView(world, pooler, slot, signal);
        }
        
        public static void LoadTowerView(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Tower>().Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetField(SavePath.View.Tower, out var viewValue)) continue;
                
                ref var viewData = ref pooler.TowerView.Add(entity);
                viewData.ViewId = viewValue.ParseToAssetReference();
                viewData.Value = new TowerViewApi();
                viewData.Value.LoadView(viewData.ViewId, signal, world.PackEntity(entity));
                viewData.Value.SetName(entityData.EntityID);
                
                ref var tilePositionData = ref pooler.TilePosition.Add(entity);
                ref var positionData = ref pooler.Position.Add(entity);
                ref var rotationData = ref pooler.Rotation.Add(entity);
                if (savingEntity.TryGetVector3Field(SavePath.WorldSpace.Position, out Vector3 positionValue)) 
                {
                    positionData.Value = positionValue;
                    tilePositionData.Value = Vector3Int.RoundToInt(positionValue);
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.WorldSpace.Rotation, out var rotationValue))
                {
                    rotationData.Value = rotationValue;
                }
            }
        }
        
        public static void LoadEnemyView(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Enemy>().Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetField(SavePath.View.Enemy, out var viewValue)) continue;
                
                ref var viewData = ref pooler.EnemyView.Add(entity);
                viewData.ViewId = viewValue.ParseToAssetReference();
                viewData.Value = new EnemyViewApi();
                viewData.Value.LoadView(viewData.ViewId, signal, world.PackEntity(entity));
                viewData.Value.SetName(entityData.EntityID);
                
                ref var positionData = ref pooler.Position.Add(entity);
                ref var rotationData = ref pooler.Rotation.Add(entity);
                if (savingEntity.TryGetVector3Field(SavePath.WorldSpace.Position, out Vector3 positionValue)) 
                {
                    positionData.Value = positionValue;
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.WorldSpace.Rotation, out var rotationValue))
                {
                    rotationData.Value = rotationValue;
                }
            }
        }
        
        public static void LoadBuildingTilemapView(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Level>().Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetField(SavePath.View.BuildingTilemap, out var viewValue)) continue;
                
                ref var viewData = ref pooler.BuildingTilemapView.Add(entity);
                viewData.ViewId = viewValue.ParseToAssetReference();
                viewData.Value = new BuildingTilemapViewApi();
                viewData.Value.LoadView(viewData.ViewId, signal, world.PackEntity(entity));
                viewData.Value.SetName(entityData.EntityID);
                
                ref var positionData = ref pooler.Position.Add(entity);
                ref var rotationData = ref pooler.Rotation.Add(entity);
                if (savingEntity.TryGetVector3Field(SavePath.WorldSpace.Position, out Vector3 positionValue)) 
                {
                    positionData.Value = positionValue;
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.WorldSpace.Rotation, out var rotationValue))
                {
                    rotationData.Value = rotationValue;
                }
                
                ref var tilemapData = ref pooler.BuildingTilemap.Add(entity);
                tilemapData.CachedTiles = CacheAllTiles();
                if (savingEntity.TryGetTileEntriesField(SavePath.BuildingTilemap.Tilemap, tilemapData.CachedTiles, out var loadedList))
                {
                    tilemapData.RawValue = loadedList;
                }
            }
        }
        
        #endregion

        #region Static

        public static void LoadViewStatic(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            LoadEnvironmentView(world, pooler, slot, signal);
        }
        
        private static void LoadEnvironmentView(EcsWorld world, Pooler pooler, Slot slot, Signal signal)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.Environment>().Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (!savingEntity.TryGetField(SavePath.View.Environment, out var viewValue)) continue;
                
                ref var viewData = ref pooler.EnvironmentView.Add(entity);
                viewData.ViewId = viewValue.ParseToAssetReference();
                viewData.Value = new EnvironmentViewApi();
                viewData.Value.LoadView(viewData.ViewId, signal, world.PackEntity(entity));
                viewData.Value.SetName(entityData.EntityID);
                
                ref var positionData = ref pooler.Position.Add(entity);
                ref var rotationData = ref pooler.Rotation.Add(entity);
                if (savingEntity.TryGetVector3Field(SavePath.WorldSpace.Position, out Vector3 positionValue)) 
                {
                    positionData.Value = positionValue;
                }
                
                if (savingEntity.TryGetQuaternionField(SavePath.WorldSpace.Rotation, out var rotationValue))
                {
                    rotationData.Value = rotationValue;
                }
            }
        }
        
        #endregion
        
        private static Dictionary<string, TileBase> CacheAllTiles()
        {
            var dict = new Dictionary<string, TileBase>();
            
            var exclude = Resources.Load<TileBase>(Constants.Resources.ExcludeTiles.Exclude);
            var empty = Resources.Load<TileBase>(Constants.Resources.ExcludeTiles.Empty);

            if (exclude != null) dict.TryAdd("PurpleExclusion", exclude);
            else Debug.LogError($"Tile '{Constants.Resources.ExcludeTiles.Exclude}' not found in Resources.");

            if (empty != null) dict.TryAdd("CyanEmpty", empty);
            else Debug.LogError($"Tile '{Constants.Resources.ExcludeTiles.Empty}' not found in Resources.");

            return dict;
        }
    }
}