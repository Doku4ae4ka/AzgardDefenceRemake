using System;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.SaveSystem;

namespace MapMaker.Scripts.EntitySettings.Tower
{
    [Serializable, Toggle("enabled")]
    public class BaseSettings
    {
        public bool enabled;
        public int baseCost;
        public float damage;
        public float attackSpeed;
        public float radius;
        public EnemyType enemyType;
        
        public void TryLoad(Entity entity)
        {
            if (entity.TryGetField(SavePath.Tower.BaseCost, out var cost))
            {
                enabled = true;
                baseCost = cost.ParseInt();
            }
            else enabled = false;
            
            if (entity.TryGetField(SavePath.Tower.Damage, out var damageValue))
            {
                enabled = true;
                damage = damageValue.ParseFloat();
            }
            else enabled = false;
            
            if (entity.TryGetField(SavePath.Tower.AttackSpeed, out var attackSpeedValue))
            {
                enabled = true;
                attackSpeed = attackSpeedValue.ParseFloat();
            }
            else enabled = false;
            
            if (entity.TryGetField(SavePath.Tower.Radius, out var radiusValue))
            {
                enabled = true;
                radius = radiusValue.ParseFloat();
            }
            else enabled = false;
            
            if (entity.TryGetEnumField(SavePath.Tower.EnemyType, out EnemyType parsedEnemyType))
            {
                enabled = true;
                enemyType = parsedEnemyType;
            }
            else enabled = false;
        }
        
        public void TrySave(Entity entity)
        {
            if (!enabled) return;
            
            entity.SetField(SavePath.Tower.BaseCost, $"{baseCost}");
            entity.SetField(SavePath.Tower.Damage, $"{damage}");
            entity.SetField(SavePath.Tower.AttackSpeed, $"{attackSpeed}");
            entity.SetField(SavePath.Tower.Radius, $"{radius}");
            entity.SetField(SavePath.Tower.EnemyType, $"{enemyType}");
        }
    }
}