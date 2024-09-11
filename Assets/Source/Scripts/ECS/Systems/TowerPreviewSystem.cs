using System.Collections.Generic;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Scripts.ECS.Systems
{
    public class TowerPreviewSystem : EcsGameSystem<Signals.CommandSpawnTowerPreview>
    {
        private EcsFilter _towerPreviewFilter;
        protected override void Initialize()
        {
            _towerPreviewFilter = PrototypeMask.Inc<EcsData.TowerPreview>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _towerPreviewFilter)
            {
                ref var towerPreview = ref Pooler.TowerPreview.Get(entity);
                var exclusionTilemap = towerPreview.Tilemap;
                
                Vector3Int currentPos = exclusionTilemap.GetMouseOnGridPos();
                ref var tilePositionData = ref Pooler.TilePosition.Get(entity);
                tilePositionData.Value = currentPos;
                ref var positionData = ref Pooler.Position.Get(entity);
                positionData.Value = currentPos;
                if (Pooler.Transform.Has(entity))
                {
                    ref var transformData = ref Pooler.Transform.Get(entity);
                    transformData.Value.position = currentPos;
                }

                var currentTile = exclusionTilemap.GetTile(currentPos);
                if (currentTile.name == "CyanEmpty")
                {
                    ref var towerViewData = ref Pooler.TowerView.Get(entity);
                    towerViewData.Value.SetTowerSelectValid();
                    if(!Pooler.BuildValidMark.Has(entity)) 
                        Pooler.BuildValidMark.Add(entity);

                }
                else if (currentTile.name == "PurpleExclusion")
                {
                    ref var towerViewData = ref Pooler.TowerView.Get(entity);
                    towerViewData.Value.SetTowerSelectInvalid();
                    if(Pooler.BuildValidMark.Has(entity)) 
                        Pooler.BuildValidMark.Del(entity);
                }
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (Pooler.BuildValidMark.Has(entity))
                    {
                        var exclusionTile = GetTile(towerPreview.CachedTiles, "PurpleExclusion");
                        SpawnTower(entity, tilePositionData.Value);
                        exclusionTilemap.SetTile(tilePositionData.Value, exclusionTile);
                    }
                }
            }
        }

        private TileBase GetTile(Dictionary<string, TileBase> tiles, string key)
        {
            if (!tiles.TryGetValue(key, out var tileBase)) return null;
            return tileBase;
        }
        
        public void SpawnTower(int prototypeEntity, Vector3Int position)
        {
            Signal.RegistryRaise(new Signals.CommandSpawnTower
            {
                PrototypeEntity = prototypeEntity,
                Position = position
            });
        }

        protected override void OnSignal(Signals.CommandSpawnTowerPreview data)
        {
            if (!Prototypes.TryGet(data.TowerId, out var prototypeEntity)) return;
            if (!Pooler.TryGetBuildingTilemapEntity(World, out var tilemapEntity)) return;
            
            ref var towerView = ref Pooler.TowerView.Get(prototypeEntity);
            towerView.Value.SetTowerPreviewView();
            towerView.Value.Show();
            ref var tilemapData = ref Pooler.BuildingTilemap.Get(tilemapEntity);
            ref var towerPreviewData = ref Pooler.TowerPreview.Add(prototypeEntity);
            towerPreviewData.Tilemap = tilemapData.Value;
            towerPreviewData.CachedTiles = tilemapData.CachedTiles;
        }
    }
}