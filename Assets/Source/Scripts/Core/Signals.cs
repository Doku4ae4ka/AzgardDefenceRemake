
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

        public struct CommandSpawnTowerPreview
        {
            public string TowerId;
        }
        
        public struct CommandSpawnTower
        {
            public int PrototypeEntity;
            public Vector3Int Position;
        }
        
        public struct CommandSpawnEnemy
        {
            public int PrototypeEntity;
            public Vector3Int Position;
        }
        
        public struct OnViewAssetLoaded
        {
            public EcsPackedEntity PackedEntity;
            public Transform Transform;
        }
    }
}