using UnityEngine;
using UnityEngine.Tilemaps;

namespace Infrastructure
{
    sealed class SceneData : MonoBehaviour
    {
        public Tilemap exclusionTilemap;
        public TileBase exclusionTile;
        public TileBase emptyTile;
        
        public Transform TowersParent;

    }
}