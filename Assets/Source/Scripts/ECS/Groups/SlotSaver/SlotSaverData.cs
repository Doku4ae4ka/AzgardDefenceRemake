using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.SlotSaver
{
    public static class SlotSaverData
    {
        #region SaveSystem

        /// <summary> Основная дата SlotSaver, хранит данные о сущности в сохранении </summary>
        public struct SlotEntity : IEcsComponent
        {
            public string EntityID;
            public SlotCategory Category;
            public string Type;
        }

        /// <summary> Сущность является прототипом, хранит метод для создания данных при клонировании </summary>
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
        
        /// <summary> Сущность конфиг (Config) </summary>
        public struct ConfigMark : IEcsComponent
        {
        }
        
        /// <summary> Сущность игрок (Player) </summary>
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