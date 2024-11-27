using System;

namespace MapMaker.Scripts.EntitySettings.Enemy
{
    [Serializable]
    public class EnemySettings
    {
        public BaseSettings baseSettings = new();
        public HealthSettings health = new();
        public PathFollowerSettings pathFollower = new();
    }
}