using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.SaveSystem;

namespace Source.Scripts.Systems
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
        }

        private void LoadConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            if (slot.Configs.TryGetIntField(SavePath.Config.FreeEntityID, out var id)) Configs.SetFreeId(id);
        }
    }
}