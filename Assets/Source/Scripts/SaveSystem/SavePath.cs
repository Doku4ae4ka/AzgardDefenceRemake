using System.Collections.Generic;
using Source.Scripts.Core;

namespace Source.Scripts.SaveSystem
{
    public static class SavePath
    {
        private static readonly string[] AllPaths = new string[] {
            "view", 
            "position", 
            "rotation", 
            "prototype.category", 
            "stamina.max", 
            "stamina.current", 
            "health.max", 
            "health.current", 
            "speed.default", 
            "speed.base", 
            "speed.current", 
            "speed.sprint.multiply", 
            "speed.crouch.multiply", 
            "global.player.info.location",
        };

        public static class Config
        {
            public const string ID = "global.configs";
            public const string FreeEntityID = "config.free.entity.id";
        }
        
        public static class Health
        {
            public const string Max = "health.max";
            public const string Current = "health.current";
        }

        public static class Speed
        {
            public const string Default = "speed.default";
            public const string Base = "speed.base";
            public const string Current = "speed.current";
            public const string SprintMultiply = "speed.sprint.multiply";
            public const string CrouchMultiply = "speed.crouch.multiply";
        }

        public static class Stamina
        {
            public const string Max = "stamina.max";
            public const string Current = "stamina.current";
        }
        
        public static class Prototype
        {
            public const string Category = "prototype.category";
        }
        
        public static class Player
        {
            public static class Info
            {
                public static string Location = "global.player.info.location";
            }
        }
        
        public const string View = "view";
        public const string Position = "position";
        public const string Rotation = "rotation";
        
        public static IEnumerable<string> GetKeyOptions()
        {
            return AllPaths;
        }
    }
}