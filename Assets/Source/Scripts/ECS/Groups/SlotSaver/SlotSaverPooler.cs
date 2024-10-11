using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.SlotSaver
{
    public class SlotSaverPooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            ConfigMark = new PoolerModule<SlotSaverData.ConfigMark>(world);
            PlayerMark = new PoolerModule<SlotSaverData.PlayerMark>(world);
            SlotEntity = new PoolerModule<SlotSaverData.SlotEntity>(world);
            Prototype = new PoolerModule<SlotSaverData.Prototype>(world);
            DynamicMark = new PoolerModule<SlotSaverData.DynamicMark>(world);
            StaticMark = new PoolerModule<SlotSaverData.StaticMark>(world);

            foreach (var entityBuilder in ConfigDataCreators) entityBuilder.Initialize(GameShare);
            foreach (var entityBuilder in PlayerDataCreators) entityBuilder.Initialize(GameShare);
            foreach (var entityBuilder in DynamicDataCreators) entityBuilder.Initialize(GameShare);
            foreach (var entityBuilder in StaticDataCreators) entityBuilder.Initialize(GameShare);
        }
        
        public GameShare GameShare { get; set; }
        public PoolerModule<SlotSaverData.ConfigMark> ConfigMark { get; private set; }
        public PoolerModule<SlotSaverData.PlayerMark> PlayerMark { get; private set; }
        public PoolerModule<SlotSaverData.SlotEntity> SlotEntity { get; private set; }
        public PoolerModule<SlotSaverData.Prototype> Prototype { get; private set; }
        public PoolerModule<SlotSaverData.DynamicMark> DynamicMark { get; private set; }
        public PoolerModule<SlotSaverData.StaticMark> StaticMark { get; private set; }

        #region DataCreators

        public readonly List<EntityBuilder> ConfigDataCreators = new ();
        public readonly List<EntityBuilder> PlayerDataCreators = new ();
        public readonly List<EntityBuilder> DynamicDataCreators = new ();
        public readonly List<EntityBuilder> StaticDataCreators = new ();

        public void AddDynamicDataCreator(EntityBuilder builder)
        {
            DynamicDataCreators.Add(builder);
        }
        public void AddPlayerDataCreator(EntityBuilder builder)
        {
            PlayerDataCreators.Add(builder);
        }

        public void AddStaticDataCreator(EntityBuilder builder)
        {
            StaticDataCreators.Add(builder);
        }

        public void AddConfigDataCreator(EntityBuilder builder)
        {
            ConfigDataCreators.Add(builder);
        }

        public void ClearDynamicDataCreator()
        {
            DynamicDataCreators.Clear();
        }

        public void ClearPlayerDataCreator()
        {
            PlayerDataCreators.Clear();
        }

        public void ClearStaticDataCreator()
        {
            StaticDataCreators.Clear();
        }


        public void ClearConfigDataCreator()
        {
            ConfigDataCreators.Clear();
        }

        public void ClearAllDataCreator()
        {
            DynamicDataCreators.Clear();
            PlayerDataCreators.Clear();
            StaticDataCreators.Clear();
            ConfigDataCreators.Clear();
        }

        #endregion
    }
}