using ECS.Modules.Exerussus.ViewCreator.MonoBehaviours;

namespace Source.Scripts.MonoBehaviours.Views
{
    public class EnvironmentViewApi : AssetViewApi
    {
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void SetName(string newName)
        {
            transform.name = name;
        }
    }
}