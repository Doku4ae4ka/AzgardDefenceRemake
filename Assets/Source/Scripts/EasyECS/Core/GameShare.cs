using System;
using System.Collections.Generic;
using UnityEngine;


namespace Source.EasyECS
{
    [Serializable]
    public class GameShare
    {
        public GameShare(Dictionary<Type, DataPack> sharedObjects)
        {
            _sharedObjects = sharedObjects;
        }

        [SerializeField] private List<string> sharedEcsSystems = new ();
        private Dictionary<Type, DataPack> _sharedObjects;
        
        public T GetSharedObject<T>() where T : IGameShareItem
        {
            var classPack = _sharedObjects[typeof(T)];
            var sharedObject = classPack.Object;
            return (T)sharedObject;
        }

        public void AddSharedObject<T>(Type type, T sharedObject) where T : IGameShareItem
        {
            sharedEcsSystems.Add(type.Name);
            _sharedObjects[type] = new DataPack(type, sharedObject);
        }
    }

    public interface IGameShareItem
    {
        
    }
    
    [Serializable]
    public class DataPack
    {
        public DataPack(Type type, IGameShareItem sharedObject)
        {
            _object = sharedObject;
            _type = type;
            name = _type.Name;
        }

        [SerializeField] private string name;
        private Type _type;
        private IGameShareItem _object;
        
        public string Name => name; 
        public IGameShareItem Object => _object;
    }
}