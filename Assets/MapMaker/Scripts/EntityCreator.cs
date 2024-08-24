// using System;
// using Exerussus._1Extensions;
// using Sirenix.OdinInspector;
// using Source.Scripts.ECS.Core;
// using Source.Scripts.ECS.Core.Enums;
// using Source.Scripts.ECS.Core.SaveManager;
// using UnityEngine;
//
// namespace MapMaker.Scripts
// {
//     public abstract class EntityCreator : MonoBehaviour
//     {
//         [SerializeField]
//         protected int entityID;
//         [SerializeField]
//         protected GameConfiguration gameConfiguration;
//         [SerializeField, OnValueChanged("UpdateCategory")]
//         protected bool prototype;
//         [SerializeField, HideInInspector]
//         protected EntityCategory category;
//         [SerializeField, HideInInspector]
//         protected PrototypeSettings prototypeSettings;
//
//         public int EntityID => entityID;
//
//         public EntityCategory Category => category;
//
//         [Button]
//         public void Save()
//         {
//             this.SerializeEntity(gameConfiguration.saver, "default_slot");
//         }
//
//         [Button]
//         public void Validate()
//         {
//             ConfigLoader.TryGetConfigIfNull(ref gameConfiguration, "Configs");
//         }
//
//         protected virtual void UpdateCategory()
//         {
//             if (prototype)
//             {
//                 category = EntityCategory.Prototype;
//                 prototypeSettings.enabled = true;
//             }
//             else
//             {
//                 prototypeSettings.enabled = false;
//             }
//         }
//         
//         [Serializable]
//         public class PrototypeSettings
//         {
//             public bool enabled;
//             
//             [SerializableField("category"), EnumPaging]
//             public EntityCategory category;
//         }
//     }
// }