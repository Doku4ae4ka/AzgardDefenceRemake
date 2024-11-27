using ECS.Modules.Exerussus.SpaceHash;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;


namespace Source.Scripts.ECS.Groups.GameCore.DataBuilder
{
    public class ConfigBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Config;
        public override EcsWorld.Mask FilterMask => _world.Filter<EcsData.Config>();
        private GameCorePooler _gameCorePooler;
        private SpaceHashPooler _spaceHashPooler;
        private EcsWorld _world;
        
        public override void Initialize(GameShare gameShare)
        {
            _world = gameShare.GetSharedObject<Componenter>().World;
            gameShare.GetSharedObject(ref _gameCorePooler);
            gameShare.GetSharedObject(ref _spaceHashPooler);
        }

        public override bool CheckProcess(int entity, SlotEntity slotEntity)
        {
            return slotEntity.type == SavePath.EntityType.Config;
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if (slotEntity.TryGetIntField(SavePath.Config.FreeEntityID, out var id)) _gameCorePooler.Configs.SetFreeId(id);
            if (slotEntity.TryGetVector4Field(SavePath.Config.MapBounds, out var vector4Value)) _spaceHashPooler.Resize(vector4Value, 4);
            if (slotEntity.TryGetRoutesField(SavePath.Config.Routes, out var dictionary)) _gameCorePooler.Configs.SetRoutes(dictionary);
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            slotEntity.SetField(SavePath.Config.FreeEntityID, $"{_gameCorePooler.Configs.FreeID}");
            slotEntity.SetField(SavePath.Config.MapBounds, $"{_spaceHashPooler.MapBounds}");
            slotEntity.SetField(SavePath.Config.Routes, _gameCorePooler.Configs.SerializePaths());
        }
    }
}