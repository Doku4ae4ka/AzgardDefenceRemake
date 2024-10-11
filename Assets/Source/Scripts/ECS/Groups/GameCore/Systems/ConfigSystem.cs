using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.GameCore.Systems
{
    public class ConfigSystem : EasySystem<GameCorePooler>
    {
        private EcsFilter _configFilter;
        
        private void SaveConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            // slot.Configs.SetField(SavePath.Config.FreeEntityID, $"{Configs.FreeID}");
            // slot.Configs.SetField(SavePath.Config.MapBounds, $"{SpaceHash.MapBounds}");
            // slot.Configs.SetField(SavePath.Config.Routes, Configs.SerializePaths());
        }

        private void LoadConfigs(EcsWorld world, Pooler pooler, Slot slot)
        {
            // if (slot.Configs.TryGetIntField(SavePath.Config.FreeEntityID, out var id)) Configs.SetFreeId(id);
            // if (slot.Configs.TryGetVector4Field(SavePath.Config.MapBounds, out var vector4Value)) SpaceHash.ResizeSpaceHash(vector4Value);
            // if (slot.Configs.TryGetRoutesField(SavePath.Config.Routes, out var dictionary)) Configs.SetRoutes(dictionary);
        }
    }
}