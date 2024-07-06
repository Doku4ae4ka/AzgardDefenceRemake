using UnityEngine;

namespace Services
{
    sealed class TowerUtils
    {
        private readonly TowerData[] _towerDatas;
        public TowerUtils(TowerData[] towerDatas)
        {
            _towerDatas = towerDatas;
        }
        
        public TowerData GetTowerData(string towerType)
        {
            foreach (var data in _towerDatas)
            {
                if (data.name == towerType + "Data")
                {
                    return data;
                }
            }
            Debug.LogError("Tower Data not found: " + towerType);
            return null;
        }
    }
}