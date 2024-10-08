using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;

namespace Ecs.Modules.PauldokDev.SlotSaver
{
    public static class SlotSaverData
    {
        #region SaveSystem

        public struct SlotEntity : IEcsComponent
        {
            public string EntityID;
            public string Category;
        }

        public struct Prototype : IEcsComponent
        {
            public string Category;
            public List<Action<int>> DataBuilder;
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

        #endregion
    }
}