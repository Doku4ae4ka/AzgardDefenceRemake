using System;
using Sirenix.OdinInspector;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.Extensions;

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
        
        public void TryLoad(SlotEntity slotEntity)
        {
            if (slotEntity.TryGetField(SavePath.Tower.BaseCost, out var cost))
            {
                enabled = true;
                baseCost = cost.ParseInt();
            }
            else enabled = false;
            
            if (slotEntity.TryGetField(SavePath.Tower.Damage, out var damageValue))
            {
                enabled = true;
                damage = damageValue.ParseFloat();
            }
            else enabled = false;
            
            if (slotEntity.TryGetField(SavePath.Tower.AttackSpeed, out var attackSpeedValue))
            {
                enabled = true;
                attackSpeed = attackSpeedValue.ParseFloat();
            }
            else enabled = false;
            
            if (slotEntity.TryGetField(SavePath.Tower.Radius, out var radiusValue))
            {
                enabled = true;
                radius = radiusValue.ParseFloat();
            }
            else enabled = false;
            
            if (slotEntity.TryGetEnumField(SavePath.Tower.EnemyType, out EnemyType parsedEnemyType))
            {
                enabled = true;
                enemyType = parsedEnemyType;
            }
            else enabled = false;
        }
        
        public void TrySave(SlotEntity slotEntity)
        {
            if (!enabled) return;
            
            slotEntity.SetField(SavePath.Tower.BaseCost, $"{baseCost}");
            slotEntity.SetField(SavePath.Tower.Damage, $"{damage}");
            slotEntity.SetField(SavePath.Tower.AttackSpeed, $"{attackSpeed}");
            slotEntity.SetField(SavePath.Tower.Radius, $"{radius}");
            slotEntity.SetField(SavePath.Tower.EnemyType, $"{enemyType}");
        }
    }
}