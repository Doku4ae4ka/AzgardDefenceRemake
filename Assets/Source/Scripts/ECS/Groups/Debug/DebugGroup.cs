using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace Source.Scripts.ECS.Groups.Debug
{
    public class DebugGroup : EcsGroup<DebugPooler>
    {
        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems
                 .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
        }
    }
}