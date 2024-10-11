using Sirenix.OdinInspector;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
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
                slot.Configs.category = SlotCategory.Config;
            }
        }

        [Button]
        public void ExportToJson()
        {
            var json = JsonUtility.ToJson(this, true);
            Debug.Log(json);
        }
    }
}