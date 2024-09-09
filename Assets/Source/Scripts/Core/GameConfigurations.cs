using Sirenix.OdinInspector;
using Source.Scripts.SaveSystem;
using UnityEngine;

namespace Source.Scripts.Core
{
[CreateAssetMenu(menuName = "ADR/Global/GameConfigurations", fileName = "GameConfigurations")]
    public class GameConfigurations : ScriptableObject
    {
        public Slot slot;

        [Button]
        public void Validate()
        {
            if (slot.Configs != null)
            {
                slot.Configs.id = SavePath.Config.ID;
                slot.Configs.category = SavePath.EntityCategory.Config;
            }
            if (slot.Prototypes != null) foreach (var entity in slot.Prototypes) entity.category = SavePath.EntityCategory.Prototype;
        }

        [Button]
        public void ExportToJson()
        {
            var json = JsonUtility.ToJson(this, true);
            Debug.Log(json);
        }
    }
}