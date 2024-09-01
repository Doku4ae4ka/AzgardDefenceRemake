//
// using Source.Scripts.Core;
// using Source.Scripts.MonoBehaviors;
// using Source.Scripts.ProjectLibraries;
// using UnityEngine;
// using UnityEngine.Scripting;
//
// namespace Source.Scripts.ECS.Systems
// {
//     /// <summary>
//     /// создает превью башни получая сигнал CommandSpawnTowerPreview.
//     /// </summary>
//     [Preserve]
//     public class TowerPreviewSpawnSystem : EcsGameSystem<Signals.CommandSpawnTower>
//     {
//         private TowerView GetTowerByID(TowerKeys towerID)
//         {
//             return Libraries.TowerLibrary.GetByID(towerID).Tower;
//         }
//         
//         protected override void OnSignal(Signals.CommandSpawnTower data)
//         {
//             var entity = World.NewEntity();
//             ref var transformData = ref Pooler.Transform.Add(entity);
//             ref var towerData = ref Pooler.Tower.Add(entity);
//             towerData.TowerID = data.TowerID;
//
//             var tower = GetTowerByID(data.TowerID);
//             
//             var towerGo =
//                 Object.Instantiate(tower.gameObject, Vector2.zero, Quaternion.identity);
//             transformData.Value = towerGo.transform;
//
//             SetTowerPreviewView(transformData.Value);
//         }
//
//         private void SetTowerPreviewView(Transform transform)
//         {
//             Transform towerSelectTransform = transform.Find("TowerSelect");
//             if (towerSelectTransform != null)
//             {
//                 towerSelectTransform.gameObject.SetActive(true);
//             }
//             else { Debug.LogError("TowerSelect not found"); }
//             Transform towerRangeTransform = transform.Find("Range");
//             if (towerRangeTransform != null)
//             {
//                 towerRangeTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
//             }
//             else { Debug.LogError("Range object not found"); }
//             Transform towerModelTransform = transform.Find("TowerModel");
//             if (towerModelTransform != null)
//             {
//                 towerModelTransform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.45f, 0.6f);
//             }
//             else { Debug.LogError("TowerModel not found"); }
//         }
//         
//     }
//     
// }