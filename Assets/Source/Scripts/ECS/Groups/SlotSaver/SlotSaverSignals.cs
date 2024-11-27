using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.SlotSaver
{
    public static class SlotSaverSignals
    {
        #region SaveSignals
        
        public struct OnStartSaving
        {
            public Slot Slot;
        }
        
        public struct OnFinishSaving
        {
            public Slot Slot;
        }
        
        #endregion

        #region LoadSignals
        
        public struct OnStartLoading
        {
            public Slot Slot;
        }

        public struct OnFinishLoading
        {
            public Slot Slot;
        }

        #endregion
    }
}