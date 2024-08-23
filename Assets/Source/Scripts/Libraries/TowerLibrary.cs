using System;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Components;
using UnityEngine;

namespace Source.Scripts.ProjectLibraries
{
    [CreateAssetMenu(menuName = "Library/TowerLibrary", fileName = "TowerLibrary")]
    public class TowerLibrary : Library<TowerPack, TowerKeys>
    {
        
    }
    
    [Serializable]
    public class TowerPack : LibraryItem<TowerKeys>
    {
        [ReadOnly] [SerializeField] private TowerKeys towerID;
        [SerializeField] private Tower tower;
        
        public Tower Tower => tower;

        public override void SetIDInEditor(TowerKeys id)
        {
            towerID = id;
        }

        public override TowerKeys ID => towerID;
    }
}