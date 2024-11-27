using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.Extensions;

namespace Source.Scripts.ECS.Groups.GameCore.DataBuilder
{
    public class BuildingTilemapBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Dynamic;
        public override EcsWorld.Mask FilterMask => _world.Filter<EcsData.BuildingTileMap>();
        private GameCorePooler _corePooler;
        private EcsWorld _world;
        
        public override void Initialize(GameShare gameShare)
        {
            gameShare.GetSharedObject(ref _corePooler);
            _world = gameShare.GetSharedObject<Componenter>().World;
        }

        public override bool CheckProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.TryGetField(SavePath.BuildingTilemap.Tilemap, out var value);
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            ref var tilemapData = ref _corePooler.BuildingTilemap.Add(entity);
            tilemapData.CachedTiles = TileMapExtensions.CacheAllTiles();
            if (!slotEntity.TryGetTileEntriesField(SavePath.BuildingTilemap.Tilemap, tilemapData.CachedTiles, out var loadedList)) return;
            tilemapData.RawValue = loadedList;
            tilemapData.Value = TileMapExtensions.InstantiateTilemapGameObject();
            tilemapData.Value.FillTilemap(tilemapData.RawValue);
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            ref var buildingTilemapData = ref _corePooler.BuildingTilemap.Get(entity);
            slotEntity.SetField(SavePath.BuildingTilemap.Tilemap, buildingTilemapData.Value.SerializeTilemap());
        }
    }
}