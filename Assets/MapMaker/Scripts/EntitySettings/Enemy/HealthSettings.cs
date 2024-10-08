using System;
using Sirenix.OdinInspector;
using Ecs.Modules.PauldokDev.SlotSaver.Core;
using Source.Scripts.Extensions;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable, Toggle("enabled")]
    public class HealthSettings
    {
        public bool enabled;
        public int max;
        public float current;
        
        public void TryLoad(SlotEntity slotEntity)
        {
            if (slotEntity.TryGetField(SavePath.Health.Max, out var maxField))
            {
                enabled = true;
                max = maxField.ParseInt();
                current = max;

                if (slotEntity.TryGetField(SavePath.Health.Current, out var currentField))
                {
                    current = currentField.ParseFloat();
                }
            }
            else enabled = false;
        }
        
        public void TrySave(SlotEntity slotEntity)
        {
            if (!enabled) return;
            
            slotEntity.SetField(SavePath.Health.Max, $"{max}");
            if (current < max) slotEntity.SetField(SavePath.Health.Current, $"{current}");
        }
    }
}