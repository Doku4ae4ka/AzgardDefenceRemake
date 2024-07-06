using UnityEngine;

namespace Components
{
    internal struct BuildRequest
    {
        public Transform Parent;
        public string TowerType;
        public Vector3Int Position;
    }
}