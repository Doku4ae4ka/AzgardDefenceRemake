using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.GameCore.DataBuilder
{
    public class ConfigBuilder : EntityBuilder
    {
        public override SlotCategory Category => SlotCategory.Config;

        public override void Initialize(GameShare gameShare)
        {
            
        }

        public override bool CheckProcess(int entity, SlotEntity slotEntity)
        {
            return true;
        }

        public override Action<int> GetDataBuilderProcess(int entity, SlotEntity slotEntity)
        {
            return null;
        }

        public override void TrySetDataProcess(int entity, SlotEntity slotEntity)
        {
            
        }
    }
}