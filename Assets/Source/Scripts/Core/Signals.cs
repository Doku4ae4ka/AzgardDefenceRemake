
using Leopotam.EcsLite;
using Source.Scripts.Components;
using UnityEngine;

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
        
        public struct OnViewAssetLoaded
        {
            public EcsPackedEntity PackedEntity;
            public Transform Transform;
        }
    }
}