using Ecs.Modules.PauldokDev.SlotSaver.Core;

namespace Ecs.Modules.PauldokDev.SlotSaver
{
    public static class SlotSaverSignals
    {
        #region SaveSignals
        
        public struct OnStartSaving
        {
            public Slot Slot;
        }

        public struct OnProcessSaving
        {
            public Slot Slot;
        }
        
        public struct OnFinishSaving
        {
            public Slot Slot;
        }
        
        #endregion

        #region LoadSignals

        public struct OnUnloadAllEntities { }
        
        public struct OnStartLoading
        {
            public Slot Slot;
        }

        public struct OnConfigsLoading
        {
            public Slot Slot;
        }

        public struct OnPrototypesLoading
        {
            public Slot Slot;
        }

        public struct OnStaticLoading
        {
            public Slot Slot;
        }

        public struct OnDynamicLoading
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