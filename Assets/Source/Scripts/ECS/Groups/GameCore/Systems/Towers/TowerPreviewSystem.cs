using System.Collections.Generic;
using System.Runtime.InteropServices;
using ECS.Modules.Exerussus.Movement;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.GameCore;
using Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Scripts.ECS.Groups.Towers.Systems
{
    public class TowerPreviewSystem : EcsSignalListener<GameCorePooler, Signals.CommandSpawnTowerPreview>
    {
        [InjectSharedObject] private MovementPooler _movementPooler;
        
        private EcsFilter _towerPreviewFilter;
        protected override void Initialize()
        {
            _towerPreviewFilter = Pooler.PrototypeMask.Inc<EcsData.TowerPreview>().End();
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
            ref var positionData = ref _movementPooler.Position.Get(entity);
            positionData.Value = currentPos;
        
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
            if (!Pooler.Prototypes.TryGet(data.TowerId, out var prototypeEntity)) return;
            if (!Pooler.TryGetBuildingTilemapEntity(World, out var tilemapEntity)) return;
            ref var attacker = ref Pooler.Attacker.Get(prototypeEntity);
            
            ref var towerView = ref Pooler.TowerView.Get(prototypeEntity);
            towerView.Value.SetTowerPreviewView();
            towerView.Value.SetRadius(attacker.Radius);
            towerView.Value.Show();
            ref var tilemapData = ref Pooler.BuildingTilemap.Get(tilemapEntity);
            ref var towerPreviewData = ref Pooler.TowerPreview.Add(prototypeEntity);
            towerPreviewData.Tilemap = tilemapData.Value;
            towerPreviewData.CachedTiles = tilemapData.CachedTiles;
        }
    }
}