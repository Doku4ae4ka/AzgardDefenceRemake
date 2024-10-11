
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.GameCore.Systems
{
    public class ConfigInitializing : EasySystem<GameCorePooler>
    {
        private SlotSaverPooler _slotSaverPooler;
        
        protected override void Initialize()
        {
            GameShare.GetSharedObject(ref _slotSaverPooler);
        }
    }
}