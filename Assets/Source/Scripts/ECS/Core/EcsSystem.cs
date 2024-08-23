using Exerussus._1EasyEcs.Scripts.Core;

namespace Source.Scripts.ECS.Core
{
    public class EcsSystem : EasySystem<Pooler>
    {
        protected GameConfiguration Configuration;
        
        public override void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Configuration);
        }
    }
}