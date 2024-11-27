using ECS.Modules.Exerussus.ViewCreator.MonoBehaviours;

namespace Source.Scripts.ECS.Groups.Enemies.MonoBehaviours
{
    public class EnemyViewApi : AssetViewApi
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