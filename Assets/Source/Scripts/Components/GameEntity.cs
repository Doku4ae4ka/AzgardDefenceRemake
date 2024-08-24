using Exerussus._1Extensions.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Components
{
    [AddComponentMenu("1Game/GameEntity"), SelectionBase]
    public class GameEntity : MonoBehaviour
    {
        [SerializeField] private int entity;
        
        public int Entity => entity;
        public Signal Signal { get; private set; }

        public void Initialize(int newEntity, Signal signal)
        {
            entity = newEntity;
            Signal = signal;
        }
    }
}