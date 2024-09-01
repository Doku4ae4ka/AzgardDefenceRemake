using Source.Scripts.SaveSystem;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class GameConfigurations : ScriptableObject, IGameCoreConfig
    {
        public Slot slot;
    }
}