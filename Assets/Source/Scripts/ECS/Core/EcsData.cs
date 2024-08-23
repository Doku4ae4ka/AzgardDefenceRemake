using Source.Scripts.ECS.Core.Interfaces;
using Source.Scripts.ProjectLibraries;
using UnityEngine;

namespace Source.Scripts.ECS.Core
{
    public struct TransformData : IEcsData
    {
        public UnityEngine.Transform Value;
        public void InitializeValues(UnityEngine.Transform value)
        {
            Value = value;
        }
    }

    public struct OnDestroyData : IEcsData
    {
        public float TimeRemaining;
        public GameObject ObjectToDelete;
        public void InitializeValues(GameObject objectToDelete, float value)
        {
            TimeRemaining = value;
            ObjectToDelete = objectToDelete;
        }
    }

    public struct EcsMonoBehaviorData : IEcsData
    {
        public IEcsMonoBehavior Value;
    
        public void InitializeValues(IEcsMonoBehavior value)
        {
            Value = value;
        }
    }
    
    
    public struct TowerData : IEcsData
    {
        public TowerKeys TowerID;
    }
}