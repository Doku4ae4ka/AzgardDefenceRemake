using System.Collections.Generic;
using Leopotam.SpaceHash;

namespace Exerussus._1Private.SpaceHash
{
    public class SpaceHash
    {
        public SpaceHash(float minX, float maxX, float minY, float maxY)
        {
            _spaceHash = new SpaceHash2<int>(2f, minX, minY, maxX, maxY);
        }
        
        private SpaceHash2<int> _spaceHash;

        public List<SpaceHashHit<int>> Get(float unitPositionX, float unitPositionY, float radius, bool selfIgnore)
        {
            return _spaceHash.Get(unitPositionX, unitPositionY, radius, selfIgnore);
        }

        public void Clear()
        {
            _spaceHash.Clear();
        }

        public void Add(int entity, float valueX, float valueY)
        {
            _spaceHash.Add(entity, valueX, valueY);
        }
    }
}
