using System;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.SlotSaver
{
    public class SlotSaverGroup : EcsGroup<SlotSaverPooler>
    {
        public SlotSaverSettings Settings = new();
        
        protected override void OnBeforePoolInitializing(EcsWorld world, SlotSaverPooler pooler)
        {
            pooler.GameShare = GameShare;
            
            if (Settings?.Builders == null) return;
            
            foreach (var abstractEntityBuilder in Settings.Builders)
            {
                switch (abstractEntityBuilder.Category)
                {
                    case SlotCategory.Config:
                        pooler.AddConfigDataCreator(abstractEntityBuilder);
                        break;
                    case SlotCategory.Player:
                        pooler.AddPlayerDataCreator(abstractEntityBuilder);
                        break;
                    case SlotCategory.Static:
                        pooler.AddStaticDataCreator(abstractEntityBuilder);
                        break;
                    case SlotCategory.Dynamic:
                        pooler.AddDynamicDataCreator(abstractEntityBuilder);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}