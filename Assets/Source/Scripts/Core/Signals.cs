
using Source.Scripts.Components;

namespace Source.Scripts.Core
{
    public static class Signals
    {
        public struct OnGameEntityInitialized
        {
            public GameEntity GameEntity;
        }
        
        public struct CommandSaveGame
        {
            public string SlotName;
        }
        
        public struct CommandLoadGame
        {
            public string SlotName;
        }

        public struct CommandSpawnTower
        {
            
        }
    }
}