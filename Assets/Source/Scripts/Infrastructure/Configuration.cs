using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu]
    sealed class Configuration : ScriptableObject
    {
        public TowerData[] TowerDatas;
    }
}