using System;
using Sirenix.OdinInspector;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable, Toggle("enabled")]
    public class MovableSettings
    {
        public bool enabled;
        public float speed;
        
        public void TryLoad(Entity entity)
        {
            if (entity.TryGetField(SavePath.Movable.Speed, out var field))
            {
                enabled = true;
                speed = field.ParseFloat();
            }
            else enabled = false;
        }
        
        public void TrySave(Entity entity)
        {
            if (!enabled) return;
            entity.SetField(SavePath.Movable.Speed, $"{speed}");
        }
    }
}