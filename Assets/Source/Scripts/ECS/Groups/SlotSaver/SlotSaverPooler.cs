using System;
using System.Collections.Generic;
using Ecs.Modules.PauldokDev.SlotSaver.Core;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace Ecs.Modules.PauldokDev.SlotSaver
{
    public class SlotSaverPooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            SlotEntity = new PoolerModule<SlotSaverData.SlotEntity>(world);
            Prototype = new PoolerModule<SlotSaverData.Prototype>(world);
            DynamicMark = new PoolerModule<SlotSaverData.DynamicMark>(world);
            StaticMark = new PoolerModule<SlotSaverData.StaticMark>(world);
            ConfigMark = new PoolerModule<SlotSaverData.ConfigMark>(world);
        }
        
        public PoolerModule<SlotSaverData.SlotEntity> SlotEntity { get; private set; }
        public PoolerModule<SlotSaverData.Prototype> Prototype { get; private set; }
        public PoolerModule<SlotSaverData.DynamicMark> DynamicMark { get; private set; }
        public PoolerModule<SlotSaverData.StaticMark> StaticMark { get; private set; }
        public PoolerModule<SlotSaverData.ConfigMark> ConfigMark { get; private set; }

        #region DataCreators

        public readonly List<Action<int, SlotEntity>> PrototypeDataCreators = new ();
        public readonly List<Action<int, SlotEntity>> DynamicDataCreators = new ();
        public readonly List<Action<int, SlotEntity>> StaticDataCreators = new ();
        public readonly List<Action<int, SlotEntity>> ConfigDataCreators = new ();

        public void AddDynamicDataCreator(Action<int, SlotEntity> builder)
        {
            DynamicDataCreators.Add(builder);
        }

        public void AddStaticDataCreator(Action<int, SlotEntity> builder)
        {
            StaticDataCreators.Add(builder);
        }

        public void AddPrototypeDataCreator(Action<int, SlotEntity> builder)
        {
            PrototypeDataCreators.Add(builder);
        }

        public void AddConfigDataCreator(Action<int, SlotEntity> builder)
        {
            ConfigDataCreators.Add(builder);
        }

        public void ClearDynamicDataCreator()
        {
            DynamicDataCreators.Clear();
        }

        public void ClearStaticDataCreator()
        {
            StaticDataCreators.Clear();
        }

        public void ClearPrototypeDataCreator()
        {
            PrototypeDataCreators.Clear();
        }

        public void ClearConfigDataCreator()
        {
            ConfigDataCreators.Clear();
        }

        public void ClearAllDataCreator()
        {
            DynamicDataCreators.Clear();
            StaticDataCreators.Clear();
            PrototypeDataCreators.Clear();
            ConfigDataCreators.Clear();
        }

        #endregion
    }
}