using System;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.Extensions;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable, Toggle("enabled")]
    public class PathFollowerSettings
    {
        public bool enabled;
        public float speed;
        
        public void TryLoad(SlotEntity slotEntity)
        {
            if (slotEntity.TryGetField(SavePath.PathFollower.Speed, out var field))
            {
                enabled = true;
                speed = field.ParseFloat();
            }
            else enabled = false;
        }
        
        public void TrySave(SlotEntity slotEntity)
        {
            if (!enabled) return;
            slotEntity.SetField(SavePath.PathFollower.Speed, $"{speed}");
        }
    }
}