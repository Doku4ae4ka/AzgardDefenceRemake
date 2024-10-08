using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace Ecs.Modules.PauldokDev.SlotSaver
{
    public class SlotSaverGroup : EcsGroup<SlotSaverPooler>
    {
        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            base.SetInitSystems(initSystems);
        }
    }
}