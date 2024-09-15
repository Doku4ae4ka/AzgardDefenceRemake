using System;

namespace Source.Scripts.Core
{
    [Serializable]
    public class GameStatus
    {
        public State currentState;

        public enum State
        {
            Pause,
            Game,
            Loading
        }
    }
}