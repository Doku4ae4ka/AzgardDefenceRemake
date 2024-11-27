using System;
using Sirenix.OdinInspector;
using Source.Scripts.ECS.Groups.GameCore;
using Source.Scripts.ECS.Groups.SlotSaver.Core;
using Source.Scripts.Extensions;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable, Toggle("enabled")]
    public class BaseSettings
    {
        public bool enabled;
        public float damageToCastle;
        public EnemyType enemyType;
        
        public void TryLoad(SlotEntity slotEntity)
        {
            if (slotEntity.TryGetField(SavePath.Enemy.DamageToCastle, out var damageValue))
            {
                enabled = true;
                damageToCastle = damageValue.ParseInt();
            }
            else enabled = false;
            
            if (slotEntity.TryGetEnumField(SavePath.Enemy.EnemyType, out EnemyType parsedEnemyType))
            {
                enabled = true;
                enemyType = parsedEnemyType;
            }
            else enabled = false;
        }
        
        public void TrySave(SlotEntity slotEntity)
        {
            if (!enabled) return;
            
            slotEntity.SetField(SavePath.Enemy.DamageToCastle, $"{damageToCastle}");
            slotEntity.SetField(SavePath.Enemy.EnemyType, $"{enemyType}");
        }
    }
}