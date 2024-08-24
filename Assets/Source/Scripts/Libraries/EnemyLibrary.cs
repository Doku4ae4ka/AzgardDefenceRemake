using System;
using Sirenix.OdinInspector;
using Source.Scripts.MonoBehaviors;
using UnityEngine;

namespace Source.Scripts.ProjectLibraries
{
    [CreateAssetMenu(menuName = "Library/EnemyLibrary", fileName = "EnemyLibrary")]
    public class EnemyLibrary : Library<EnemyViewPack, EnemyKeys>
    {
        
    }
    
    [Serializable]
    public class EnemyViewPack : LibraryItem<EnemyKeys>
    {
        [ReadOnly] [SerializeField] private EnemyKeys enemyID;
        [SerializeField] private EnemyView enemy;
        
        public EnemyView Enemy => enemy;

        public override void SetIDInEditor(EnemyKeys id)
        {
            enemyID = id;
        }

        public override EnemyKeys ID => enemyID;
    }
}