using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems
{
    public class ConfigSystem : EcsGameSystem
    {
        private EcsFilter _configFilter;
        
        protected override void Initialize()
        {
            _configFilter = World.Filter<EcsData.Config>().End();
            Memory.save.OnConfigs += SaveConfigs;
            Memory.load.OnConfigs += LoadConfigs;
        }

        public override void Destroy(IEcsSystems systems)
        {
            Memory.save.OnConfigs -= SaveConfigs;
            Memory.load.OnConfigs -= LoadConfigs;
        }

        private void SaveConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            slot.Configs.SetField(SavePath.Config.FreeEntityID, $"{Configs.FreeID}");
            slot.Configs.SetField(SavePath.Config.MapBounds, $"{SpaceHash.MapBounds}");
            slot.Configs.SetField(SavePath.Config.Routes, Configs.SerializePaths());
        }

        private void LoadConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            if (slot.Configs.TryGetIntField(SavePath.Config.FreeEntityID, out var id)) Configs.SetFreeId(id);
            if (slot.Configs.TryGetVector4Field(SavePath.Config.MapBounds, out var vector4Value)) SpaceHash.ResizeSpaceHash(vector4Value);
            if (slot.Configs.TryGetRoutesField(SavePath.Config.Routes, out var dictionary)) Configs.SetRoutes(dictionary);
        }
    }
}