
using UnityEngine;

namespace Components
{
    internal struct Tower
    {
        // public enum Type
        // {
        //     TowerArcher,
        //     TowerGun,
        //     TowerWind
        // }

        public Transform Transform;
        public string Type;
        public int Level;
        
        public float Damage;
        public float RateOfFire;
        public float Range;

        public int PlaceCost;
        public int BaseUpgradeCost;
        public int BaseLevelUpCost;

        public string Category;
        public string[] DamageType;

    
    }
}