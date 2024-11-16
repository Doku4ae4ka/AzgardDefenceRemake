using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.SlotSaver
{
    public static class SlotSaverData
    {
        #region SaveSystem

        public struct SlotEntity : IEcsComponent
        {
            public string EntityID;
            public SlotCategory Category;
            public string Type;
        }

        public struct Prototype : IEcsComponent
        {
            public Action<int> DataBuilder;
        }

        /// <summary>
        ///     Сущность не статична (персонаж, предмет, игрок)
        /// </summary>
        public struct DynamicMark : IEcsComponent
        {
        }

        /// <summary>
        ///     Сущность статична (Environment)
        /// </summary>
        public struct StaticMark : IEcsComponent
        {
        }
        
        public struct ConfigMark : IEcsComponent
        {
        }
        
        public struct PlayerMark : IEcsComponent
        {
        }
        
        /// <summary>
        /// Сущность в процессе сохранения
        /// </summary>
        public struct SavingProcess : IEcsComponent
        {
            public Source.Scripts.ECS.Groups.SlotSaver.Core.SlotEntity SlotEntity;
        }
        
        /// <summary>
        /// Сущность в процессе загрузки
        /// </summary>
        public struct LoadingProcess : IEcsComponent
        {
            public Source.Scripts.ECS.Groups.SlotSaver.Core.SlotEntity SlotEntity;
        }

        #endregion
    }
}