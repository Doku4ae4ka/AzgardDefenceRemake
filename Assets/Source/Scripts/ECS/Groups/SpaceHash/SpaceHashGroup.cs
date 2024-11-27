
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SmallFeatures;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.SpaceHash
{
    public class SpaceHashGroup : EcsGroup<SpaceHashPooler>
    {
        private SpaceHashSettings _settings = new();

        protected override void SetSharingData(EcsWorld world, GameShare gameShare)
        {
            gameShare.AddSharedObject(_settings);
        }
        
        public SpaceHashGroup SetCellSize(float value)
        {
            _settings.CellSize = value;
            return this;
        }

        public SpaceHashGroup SetMinMaxPoints(Vector2 min, Vector2 max)
        {
            _settings.MinPoint = min;
            _settings.MaxPoint = max;
            return this;
        }

        public SpaceHashGroup SetMask(EcsWorld.Mask mask)
        {
            _settings.AdditionalMask = mask;
            return this;
        }
    }
}