
using Source.Scripts.Components;
using Source.Scripts.ProjectLibraries;

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
            public TowerKeys TowerID;
        }
    }
}