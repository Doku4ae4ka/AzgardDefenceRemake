// using Exerussus._1Extensions.SignalSystem;
// using Source.Scripts.Core;
// using Source.Scripts.ProjectLibraries;
// using UnityEngine;
//
// namespace Source.Scripts.UI
// {
//     public class ShopButtonUI : MonoSignalListener
//     {
//         [SerializeField] private TowerKeys towerID;
//         
//         public void SpawnTowerPreview()
//         {
//             Signal.RegistryRaise(new Signals.CommandSpawnTower
//             {
//                 TowerID = towerID 
//             });
//         }
//     }
// }