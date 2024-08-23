using Source.Scripts.ECS.Core.Interfaces;
using Source.Scripts.ProjectLibraries;

namespace Source.Scripts.ECS.Core
{
    
    /// <summary>
    /// Команда на уничтожение Entity 
    /// </summary>
    public struct CommandKillEntitySignal : IEcsSignal
    {
        public int Entity;
        public bool Immediately;
    }

    /// <summary>
    /// MonoBehaviourView проинициализировался.
    /// </summary>
    public struct OnGameEntityInitializedSignal : IEcsSignal
    {
        public IEcsMonoBehavior EcsMonoBehavior;
    }

    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnGameEntityStartDestroySignal : IEcsSignal
    {
        public IEcsMonoBehavior EcsMonoBehavior;
    }

    /// <summary>
    /// </summary>
    public struct CommandSpawnTower : IEcsSignal
    {
        public TowerKeys TowerID;
    }
    
}