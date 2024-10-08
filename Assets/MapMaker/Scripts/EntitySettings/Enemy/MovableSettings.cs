using System;
using Sirenix.OdinInspector;
using Ecs.Modules.PauldokDev.SlotSaver.Core;
using Source.Scripts.Extensions;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable, Toggle("enabled")]
    public class MovableSettings
    {
        public bool enabled;
        public float speed;
        
        public void TryLoad(SlotEntity slotEntity)
        {
            if (slotEntity.TryGetField(SavePath.Movable.Speed, out var field))
            {
                enabled = true;
                speed = field.ParseFloat();
            }
            else enabled = false;
        }
        
        public void TrySave(SlotEntity slotEntity)
        {
            if (!enabled) return;
            slotEntity.SetField(SavePath.Movable.Speed, $"{speed}");
        }
    }
}