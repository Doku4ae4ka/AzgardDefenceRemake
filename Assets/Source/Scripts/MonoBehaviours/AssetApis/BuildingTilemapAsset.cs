using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Scripts.MonoBehaviours.AssetApis
{
    public class BuildingTilemapAsset : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;

        public void SetTile(TileBase tile, Vector3Int position)
        {
            tilemap.SetTile(position, tile);
        }
        
        public void SetTilemap(Tilemap newTilemap)
        {
            tilemap = newTilemap;
        }
        
        public Tilemap GetTilemap()
        {
            return tilemap;
        }
        
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}