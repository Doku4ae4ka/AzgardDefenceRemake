namespace Source.Scripts.Core
{
    public static class Constants
    {
        public static class PrototypesId
        {
            public static class Towers
            {
                public const string TowerArcher = "tower_archer";
                public const string TowerGun = "tower_gun";      
                public const string TowerWind = "tower_wind";      
                
                public static readonly string[] All = new string[] 
                    { TowerArcher, TowerGun, TowerWind };
            }
            
            public static class Enemies
            {
                public const string SandGolem = "enemy_sand_golem";
                
                public static readonly string[] All = new string[] 
                    { SandGolem };
            }
        }
        public static class Resources
        {
            public static class Tiles
            {
                public const string Empty = "Tiles/CyanEmpty";
                public const string Exclude = "Tiles/PurpleExclusion";
                public const string Path = "Tiles/Path";
                public const string Castle = "Tiles/Castle";
                public const string Portal = "Tiles/Portal";
            }
        }
    }
}