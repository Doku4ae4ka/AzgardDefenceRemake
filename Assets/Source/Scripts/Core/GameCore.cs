using Exerussus._1Extensions;
using UnityEngine;

namespace Source.Scripts.Core
{
    public static class GameCore
    {
        public static T TryGetConfig<T>(ref T component) where T : ScriptableObject, IGameCoreConfig
        {
            return ConfigLoader.TryGetConfigIfNull(ref component, "GameCore");
        }
    }

    public interface IGameCoreConfig
    {
        
    }
}