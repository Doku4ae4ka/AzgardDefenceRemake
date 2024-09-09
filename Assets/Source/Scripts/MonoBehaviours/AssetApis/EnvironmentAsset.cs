using UnityEngine;

namespace Source.Scripts.MonoBehaviours.AssetApis
{
    public class EnvironmentAsset : MonoBehaviour
    {
        
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