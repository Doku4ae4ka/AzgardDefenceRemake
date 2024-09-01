using System;
using Sirenix.OdinInspector;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts
{
    [Serializable, Toggle("enabled")]
    public class HealthSettings
    {
        public bool enabled;
        public int max;
        public float current;
        
        public void TryLoad(Entity entity)
        {
            if (entity.TryGetField(SavePath.Health.Max, out var maxField))
            {
                enabled = true;
                max = maxField.ParseInt();
                current = max;

                if (entity.TryGetField(SavePath.Health.Current, out var currentField))
                {
                    current = currentField.ParseFloat();
                }
            }
            else enabled = false;
        }
        
        public void TrySave(Entity entity)
        {
            if (!enabled) return;
            
            entity.SetField(SavePath.Health.Max, $"{max}");
            if (current < max) entity.SetField(SavePath.Health.Current, $"{current}");
        }
    }
}