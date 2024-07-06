
using Source.Scripts.Data.GamesConfigurations;
using Source.Scripts.EasyECS.Core;

namespace Source.EasyECS
{
    public class EasyNode : BootstrapComponent
    {
        private Componenter _componenter;
        private GameConfiguration _gameConfiguration;
        public static Componenter EcsComponenter { get => Instance._componenter; private set => Instance._componenter = value; }
        public static GameConfiguration GameConfiguration { get => Instance._gameConfiguration; private set => Instance._gameConfiguration = value; }
        public static EasyNode Instance { get; set; }
        
        public override void Initialize()
        {
            if (Instance != null) Destroy(this);
            Instance = this;
            EcsComponenter = GameShare.GetSharedObject<Componenter>();
        }
    }
}