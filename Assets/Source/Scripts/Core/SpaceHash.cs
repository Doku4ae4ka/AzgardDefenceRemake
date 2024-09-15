using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.SpaceHash;
using UnityEngine;

namespace Source.Scripts.Core
{
    public class SpaceHash<TTransform, TExclude> 
        where TTransform : struct, ISpaceHashTransform
        where TExclude : struct
    {
        public SpaceHash(EcsWorld world, Vector4 mapBounds, int frameDelay)
        {
            _world = world;
            _lastFrame = 0;
            _frameDelay = frameDelay;
            _filter = _world.Filter<TTransform>().Exc<TExclude>().Exc<EcsData.DeadMark>().End();
            _pool = world.GetPool<TTransform>();
            ResizeSpaceHash(mapBounds);
        }

        private readonly EcsWorld _world;
        private readonly EcsFilter _filter;
        private readonly EcsPool<TTransform> _pool;
        private readonly int _frameDelay;
        private SpaceHash2<int> _spaceHash;
        private List<SpaceHashHit<int>> _result = new(15);
        private int _lastFrame;
        public Vector4 MapBounds;
        
        public void ResizeSpaceHash(Vector4 mapBounds)
        {
            MapBounds = mapBounds;
            _spaceHash = new(4f, mapBounds.x, mapBounds.y, mapBounds.z, mapBounds.w);
            _lastFrame = 0;
        }
        
        private void UpdateSpaceHash()
        {
            _spaceHash.Clear();
            foreach (var entity in _filter)
            {
                ref var transformData = ref _pool.Get(entity);
                _spaceHash.Add(entity, transformData.Transform.position.x, transformData.Transform.position.y);
            }
        }
        
        public List<SpaceHashHit<int>> GetAllInRadiusCustomFilter(Vector2 originPosition, float radius, EcsFilter ecsFilter)
        {
            _spaceHash.Clear();
            foreach (var entity in ecsFilter)
            {
                ref var transformData = ref _pool.Get(entity);
                _spaceHash.Add(entity, transformData.Transform.position.x, transformData.Transform.position.y);
            }
            
            _spaceHash.Get(originPosition.x, originPosition.y, radius, false, _result);
            _lastFrame = 0;
            return _result;
        }

        public List<SpaceHashHit<int>> GetAllInRadius(Vector2 originPosition, float radius)
        {
            _spaceHash = TryRefresh();
            _spaceHash.Get(originPosition.x, originPosition.y, radius, false, _result);
            return _result;
        }
        
        public void SetSpaceHashObsolete()
        {
            _lastFrame = 0;
        }
        
        public void ForceUpdateSpaceHash()
        {
            UpdateSpaceHash();
        }
        
        private SpaceHash2<int> TryRefresh()
        {
            var currentFrame = Time.frameCount;
            if (_lastFrame < currentFrame)
            {
                _lastFrame = currentFrame + _frameDelay;
                UpdateSpaceHash();
            }
            return _spaceHash;
        }
    }

    public interface ISpaceHashTransform
    {
        public Transform Transform { get; }
    } 
}
