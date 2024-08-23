using System;
using System.Collections.Generic;
using Source.Scripts.Extensions;

namespace Source.Scripts.ProjectLibraries
{
    public class KeysHolder
    {
        private Dictionary<string, TowerKeys> _towerKeys = new();
        private Dictionary<string, EnemyKeys> _enemyKeys = new();
        private Dictionary<string, VfxKeys> _vfxKeys = new();

        public TowerKeys GetTowerKeyByID(string id) => GetItemByString(_towerKeys, id);

        public EnemyKeys GetEnemyKeyByID(string id) => GetItemByString(_enemyKeys, id);

        public VfxKeys GetVfxKeyByID(string id) => GetItemByString(_vfxKeys, id);

        public T GetItemByString<T>(Dictionary<string, T> collection, string id)
        {
            return collection[id];
        }

        public void InitEnum<T>(Dictionary<string, T> collection) where T : Enum
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                collection[value.GetDescription()] = value;
            }
        }

        public void Initialize()
        {
            InitEnum(_towerKeys);
            InitEnum(_enemyKeys);
            InitEnum(_vfxKeys);
        }
    }

    public enum TowerKeys
    {
        Archer,
        Wind,
        Gun,
    }

    public enum EnemyKeys
    {
        YellowGolem,
        
    }

    public enum MapKeys
    {

    }

    public enum LanguageKeys
    {
        
    }

    public enum DifficultyKeys
    {
        
    }

    public enum VfxKeys
    {
        
    }
    
    public enum AudioKeys 
    {
    
    }
    
}