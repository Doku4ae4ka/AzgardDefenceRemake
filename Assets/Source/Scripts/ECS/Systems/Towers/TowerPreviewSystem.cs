using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Scripts.ECS.Systems.Towers
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
            _towerPreviewFilter.Foreach(OnUpdate);
        }

        private void OnUpdate(int entity)
        {
            ref var towerPreview = ref Pooler.TowerPreview.Get(entity);
            var exclusionTilemap = towerPreview.Tilemap;
            
            var currentPos = exclusionTilemap.GetMouseOnGridPos();
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
            if (currentTile == null) return;
            
            ref var towerView = ref Pooler.TowerView.Get(entity);
            switch (currentTile.name)
            {
                case "CyanEmpty":
                {
                    towerView.Value.SetTowerSelectValid();
                    if(!Pooler.BuildValidMark.Has(entity)) 
                        Pooler.BuildValidMark.Add(entity);
                    break;
                }
                case "PurpleExclusion":
                {
                    towerView.Value.SetTowerSelectInvalid();
                    if(Pooler.BuildValidMark.Has(entity)) 
                        Pooler.BuildValidMark.Del(entity);
                    break;
                }
            }
                
            if (Input.GetMouseButtonDown(0))
            {
                if (Pooler.BuildValidMark.Has(entity))
                {
                    var exclusionTile = GetTile(towerPreview.CachedTiles, "PurpleExclusion");
                    SpawnTower(entity, tilePositionData.Value);
                    exclusionTilemap.SetTile(tilePositionData.Value, exclusionTile);
                }
                else
                {
                    towerView.Value.Hide();
                    Pooler.TowerPreview.Del(entity);
                }
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                towerView.Value.Hide();
                Pooler.TowerPreview.Del(entity);
            }
        }

        private TileBase GetTile(Dictionary<string, TileBase> tiles, string key)
        {
            if (!tiles.TryGetValue(key, out var tileBase)) return null;
            return tileBase;
        }
        
        public void SpawnTower(int prototypeEntity, Vector3Int tilePosition)
        {
            Signal.RegistryRaise(new Signals.CommandSpawnTower
            {
                PrototypeEntity = prototypeEntity,
                TilePosition = tilePosition
            });
        }

        protected override void OnSignal(Signals.CommandSpawnTowerPreview data)
        {
            if (!Prototypes.TryGet(data.TowerId, out var prototypeEntity)) return;
            if (!Pooler.TryGetBuildingTilemapEntity(World, out var tilemapEntity)) return;
            ref var tower = ref Pooler.Tower.Get(prototypeEntity);
            
            ref var towerView = ref Pooler.TowerView.Get(prototypeEntity);
            towerView.Value.SetTowerPreviewView(tower.Radius);
            towerView.Value.Show();
            ref var tilemapData = ref Pooler.BuildingTilemap.Get(tilemapEntity);
            ref var towerPreviewData = ref Pooler.TowerPreview.Add(prototypeEntity);
            towerPreviewData.Tilemap = tilemapData.Value;
            towerPreviewData.CachedTiles = tilemapData.CachedTiles;
        }
    }
}