using Ecs.Modules.PauldokDev.SlotSaver;
using Ecs.Modules.PauldokDev.SlotSaver.Core;
using Exerussus._1EasyEcs.Scripts.Core;

namespace Source.Scripts.ECS.Groups.GameCore.Systems
{
    public class Initializing : EasySystem<GameCorePooler>
    {
        private SlotSaverPooler _slotSaverPooler;
        
        protected override void Initialize()
        {
            GameShare.GetSharedObject(ref _slotSaverPooler);
            _slotSaverPooler.AddDynamicDataCreator(CheckAndCreateData);
        }

        private void CheckAndCreateData(int entity, SlotEntity slotEntity)
        {
            if (slotEntity.category == SavePath.EntityCategory.Tower)
            {
                Pooler.Tower.Add(entity);
            }
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}