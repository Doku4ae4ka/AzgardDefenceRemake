
using Source.Scripts.EasyECS.Core;
using Source.SignalSystem;

namespace Source.Scripts.SignalSystem
{
    /// <summary>
    /// MonoBehaviourView проинициализировался.
    /// </summary>
    public struct OnEcsMonoBehaviorInitializedSignal : ISignal
    {
        public EcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnEcsMonoBehaviorStartDestroySignal : ISignal
    {
        public EcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnEcsMonoBehaviorDestroyedSignal : ISignal
    {
        public EcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// Entity потеряло всё здоровье
    /// </summary>
    public struct CommandKillEntitySignal : ISignal
    {
        public int Entity;
    }

}