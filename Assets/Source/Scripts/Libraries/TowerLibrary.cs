using System;
using Sirenix.OdinInspector;
using Source.Scripts.MonoBehaviors;
using UnityEngine;

namespace Source.Scripts.ProjectLibraries
{
    [CreateAssetMenu(menuName = "Library/TowerLibrary", fileName = "TowerLibrary")]
    public class TowerLibrary : Library<TowerViewPack, TowerKeys>
    {
        
    }
    
    [Serializable]
    public class TowerViewPack : LibraryItem<TowerKeys>
    {
        [ReadOnly] [SerializeField] private TowerKeys towerID;
        [SerializeField] private TowerView tower;
        
        public TowerView Tower => tower;

        public override void SetIDInEditor(TowerKeys id)
        {
            towerID = id;
        }

        public override TowerKeys ID => towerID;
    }
}