using UnityEngine;

namespace Source.Scripts.MonoBehaviours.AssetApis
{
    public class EnemyAsset : MonoBehaviour
    {
        
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