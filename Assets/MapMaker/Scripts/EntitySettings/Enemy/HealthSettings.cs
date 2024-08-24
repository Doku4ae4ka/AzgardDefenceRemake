using System;
using Sirenix.OdinInspector;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable, Toggle("enabled")]
    public class HealthSettings
    {
        public bool enabled;
        public int max;
        public float current;
        
        public void TryGet(Entity entity)
        {
            if (entity.TryGetField(SavePath.HealthMax, out var maxField))
            {
                enabled = true;
                max = maxField.ParseInt();
                current = max;

                if (entity.TryGetField(SavePath.HealthCurrent, out var currentField))
                {
                    current = currentField.ParseFloat();
                }
            }
            else enabled = false;
        }
        
        public void Set(Entity entity)
        {
            entity.SetField(SavePath.HealthMax, $"{max}");
            if (current < max) entity.SetField(SavePath.HealthCurrent, $"{current}");
        }
    }
}