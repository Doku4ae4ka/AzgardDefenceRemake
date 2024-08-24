using System;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable]
    public class EnemySettings
    {
        public HealthSettings healthSettings = new ();
    }
}