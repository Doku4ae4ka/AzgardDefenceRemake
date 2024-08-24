// using System;
// using Sirenix.OdinInspector;
// using Source.Scripts.ProjectLibraries;
// using UnityEngine;
//
// namespace MapMaker.Scripts
// {
//     [AddComponentMenu("EditorOnly/TowerCreator")]
//     public class TowerCreator : EntityCreator
//     {
//         [PropertySpace(SpaceBefore = 15)]
//         [SerializeField] private TowerSettings towerSettings;
//
//         protected override void UpdateCategory()
//         {
//             base.UpdateCategory();
//             if (prototype)
//                 prototypeSettings.category = EntityCategory.Tower;
//             else
//                 category = EntityCategory.Tower;
//             
//         }
//     }
//     
//     [Serializable]
//     public class TowerSettings
//     {
//         public ViewSettings view;
//         public HealthSettings health;
//         public DamageSettings damage;
//         
//         [Serializable, Toggle("enabled")]
//         public class ViewSettings
//         {
//             public bool enabled;
//             
//             [SerializableField("view"), EnumPaging]
//             public TowerKeys towerID;
//         }
//         
//         [Serializable, Toggle("enabled")]
//         public class HealthSettings
//         {
//             public bool enabled;
//             
//             [SerializableField("health.max")]
//             public int max;
//             
//             [SerializableField("health.current")]
//             public float current;
//         }   
//         
//         [Serializable, Toggle("enabled")]
//         public class DamageSettings
//         {
//             public bool enabled;
//             
//             [SerializableField("damage")]
//             public int damage;
//         }   
//     }
// }