using System;
using ECS.Modules.Exerussus.Health;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.GameCore.DataBuilder
{
    public class ConfigBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Config;
        public override EcsWorld.Mask FilterMask => _world.Filter<GameCoreData.MainConfigMark>();
        private GameCorePooler _gameCorePooler;
        private EcsWorld _world;
        
        public override void Initialize(GameShare gameShare)
        {
            _world = gameShare.GetSharedObject<Componenter>().World;
            gameShare.GetSharedObject(ref _gameCorePooler);
        }

        public override void TrySetDataForStandardEntity(int entity, SlotEntity slotEntity)
        {
            if (slotEntity.type != SavePath.EntityType.Config) return;
            
            
        }

        public override void TrySaveDataProcess(int entity, SlotEntity slotEntity)
        {
            
        }
    }
}