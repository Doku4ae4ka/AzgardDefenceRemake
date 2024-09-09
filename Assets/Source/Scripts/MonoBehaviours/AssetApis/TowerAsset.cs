using UnityEngine;

namespace Source.Scripts.MonoBehaviours.AssetApis
{
    public class TowerAsset : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer towerSprite;
        [SerializeField] private SpriteRenderer stvolSprite;
        
        [SerializeField] private Transform projectileSpawnPosition;
        
        [SerializeField] private SpriteRenderer rangeSprite;
        
        [SerializeField] private SpriteRenderer towerSelectSprite;

        public void SetTowerPreviewView()
        {
            towerSelectSprite.enabled = true;
            rangeSprite.enabled = true;
            towerSprite.color = new Color(1f, 1f, 0.45f, 0.6f);
            stvolSprite.color = new Color(1f, 1f, 0.45f, 0.6f);
        }
        
        public void SetDefault()
        {
            towerSelectSprite.enabled = false;
            rangeSprite.enabled = false;
            towerSprite.color = Color.white;
            stvolSprite.color = Color.white;
        }
        
        
        public void SetTowerAndStvolColors(Color color)
        {
            towerSprite.color = color;
            stvolSprite.color = color;
        }
        
        public void SetTowerColor(Color color)
        {
            towerSprite.color = color;
        }

        public void SetStvolColor(Color color)
        {
            stvolSprite.color = color;
        }
        
        public void SetTowerSelectColor(Color color)
        {
            towerSelectSprite.color = color;
        }
        
        
        public void SetRangeActive(bool isActive)
        {
            rangeSprite.enabled = isActive;
        }
        
        public void SetTowerSelectActive(bool isActive)
        {
            towerSelectSprite.enabled = isActive;
        }
        
        public void SetRadius(float radius)
        {
            float spriteWidth = rangeSprite.sprite.bounds.size.x;
            float spriteHeight = rangeSprite.sprite.bounds.size.y;
            
            float scaleX = (radius * 2) / spriteWidth;
            float scaleY = (radius * 2) / spriteHeight;
            
            rangeSprite.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
        
        public void Activate()
        {
            gameObject.SetActive(false);
        }
    }
}