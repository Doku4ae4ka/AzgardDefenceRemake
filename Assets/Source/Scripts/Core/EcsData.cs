
using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;

namespace Source.Scripts.Core
{
    public static class EcsData
    {
        public struct Transform : IGameEcsComponent
        {
            public UnityEngine.Transform Value;
        }

        public struct View : IGameEcsComponent
        {
            public MonoBehaviours.View Value;
        }

        public struct Entity : IGameEcsComponent
        {
            public string EntityID;
            public EntityCategory Category;
        }

        public struct Prototype : IGameEcsComponent
        {
            public EntityCategory Category;
            public List<Action<int>> DataBuilder;
        }

        public struct DeadMark : IGameEcsComponent
        {
            
        }

        public struct Movable : IGameEcsComponent
        {
            
        }

        public struct Config : IGameEcsComponent
        {
            
        }

        public struct Health : IGameEcsComponent
        {
            public int Max;
            public float Current;
        }

        public struct PlayerInfo : IGameEcsComponent
        {
            
        }

        public struct Environment : IGameEcsComponent
        {
            
        }
        
        public struct Tower : IGameEcsComponent
        {
            
        }
        
        public struct Enemy : IGameEcsComponent
        {
            
        }
        
        /// <summary>
        /// Сущность не статична (персонаж, предмет, игрок)
        /// </summary>
        public struct DynamicMark : IGameEcsComponent
        {
            
        }
        
        /// <summary>
        /// Сущность статична (Environment)
        /// </summary>
        public struct StaticMark : IGameEcsComponent
        {
            
        }
        
        /// <summary>
        /// Сущность глобальная
        /// </summary>
        public struct GlobalMark : IGameEcsComponent
        {
            
        }
    }

    public enum EntityCategory
    {
        Tower,
        Enemy,
        Prototype,
        Config,
        Environment,
        Trigger
    }

    public interface IGameEcsComponent : IEcsComponent
    {
        
    }
}