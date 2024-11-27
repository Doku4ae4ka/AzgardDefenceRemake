using ECS.Modules.Exerussus.ViewCreator.MonoBehaviours;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.Towers.MonoBehaviours
{
    public class TowerViewApi : AssetViewApi
    {
        private const int RangeSpritePixelOffset = 14;
        
        [PropertySpace(SpaceBefore = 15)]
        [SerializeField] private SpriteRenderer towerSprite;
        
        [PropertySpace(SpaceBefore = 15)]
        [SerializeField] private SpriteRenderer stvolSprite;
        [SerializeField] private Transform stvolTransform;
        
        [PropertySpace(SpaceBefore = 15)]
        [SerializeField] private Transform projectileSpawnPosition;
        
        [PropertySpace(SpaceBefore = 15)]
        [SerializeField] private SpriteRenderer rangeSprite;
        
        [PropertySpace(SpaceBefore = 15)]
        [SerializeField] private SpriteRenderer towerSelectSprite;

        [PropertySpace(SpaceBefore = 15)]
        [SerializeField, ReadOnly] private float currentRadius;

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


        public void SetTarget(Vector3 targetPosition)
        {
            var stvolPosition = stvolTransform.position;
            var direction = targetPosition - stvolPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            stvolTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        
        
        public void SetTowerSelectValid()
        {
            towerSelectSprite.color = new Color(0f, 1f, 0f, 0.6f);
        }
        
        public void SetTowerSelectInvalid()
        {
            towerSelectSprite.color = new Color(1f, 0f, 0f, 0.6f);
        }
        
        public void SetTowerSelectSelected()
        {
            towerSelectSprite.color = Color.cyan;
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
            currentRadius = radius;
            
            var offsetInUnits = RangeSpritePixelOffset / rangeSprite.sprite.pixelsPerUnit;
            
            var spriteWidth = rangeSprite.sprite.bounds.size.x - offsetInUnits;
            var spriteHeight = rangeSprite.sprite.bounds.size.y - offsetInUnits;
            
            var scaleX = (radius * 2) / spriteWidth;
            var scaleY = (radius * 2) / spriteHeight;
            
            rangeSprite.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
        
        public void SetName(string newName)
        {
            transform.name = name;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, currentRadius);
        }
    }
}