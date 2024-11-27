using ECS.Modules.Exerussus.Health;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver;

namespace Source.Scripts.ECS.Groups.GameCore
{
    public class GameCorePooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            TowerView = new PoolerModule<EcsData.TowerView>(world);
            EnemyView = new PoolerModule<EcsData.EnemyView>(world);
            EnvironmentView = new PoolerModule<EcsData.EnvironmentView>(world);
            
            TowerPreview = new PoolerModule<EcsData.TowerPreview>(world);
            TowerMark = new PoolerModule<EcsData.TowerMark>(world);
            Attacker = new PoolerModule<EcsData.Attacker>(world);
            TowerLevel = new PoolerModule<EcsData.TowerLevel>(world);
            TowerPrices = new PoolerModule<EcsData.TowerPrices>(world);
            Upgradable = new PoolerModule<EcsData.Upgradable>(world);
            Target = new PoolerModule<EcsData.Target>(world);
            TilePosition = new PoolerModule<EcsData.TilePosition>(world);
            
            Enemy = new PoolerModule<EcsData.Enemy>(world);
            PathFollower = new PoolerModule<EcsData.PathFollower>(world);
            Route = new PoolerModule<EcsData.Route>(world);
            
            Config = new PoolerModule<EcsData.Config>(world);
            Environment = new PoolerModule<EcsData.Environment>(world);
            Camera = new PoolerModule<EcsData.Camera>(world);
            Level = new PoolerModule<EcsData.Level>(world);
            
            BuildValidMark = new PoolerModule<EcsData.BuildValidMark>(world);
            BuildingTilemap = new PoolerModule<EcsData.BuildingTileMap>(world);
        }
     
        public EcsWorld.Mask InGameMask => _world.Filter<SlotSaverData.SlotEntity>().Exc<SlotSaverData.Prototype>().Exc<HealthData.DeadMark>();
        public EcsWorld.Mask PrototypeMask => _world.Filter<SlotSaverData.SlotEntity>().Inc<SlotSaverData.Prototype>().Exc<HealthData.DeadMark>();
        public EcsWorld.Mask AllEntitiesMask => _world.Filter<SlotSaverData.SlotEntity>();
        
        [InjectSharedObject] private EcsWorld _world;
        [InjectSharedObject] public Configs Configs { get; private set; }
        [InjectSharedObject] public Prototypes Prototypes { get; private set; }
        
        public PoolerModule<EcsData.TowerView> TowerView { get; private set; }
        public PoolerModule<EcsData.EnemyView> EnemyView { get; private set; }
        public PoolerModule<EcsData.EnvironmentView> EnvironmentView { get; private set; }

        public PoolerModule<EcsData.TowerMark> TowerMark { get; private set; }
        public PoolerModule<EcsData.Attacker> Attacker { get; private set; }
        public PoolerModule<EcsData.TowerPreview> TowerPreview { get; private set; }
        public PoolerModule<EcsData.TowerLevel> TowerLevel { get; private set; }
        public PoolerModule<EcsData.Upgradable> Upgradable { get; private set; }
        public PoolerModule<EcsData.TowerPrices> TowerPrices { get; private set; }
        public PoolerModule<EcsData.Target> Target { get; private set; }
        public PoolerModule<EcsData.TilePosition> TilePosition { get; private set; }
        
        public PoolerModule<EcsData.Enemy> Enemy { get; private set; }
        public PoolerModule<EcsData.PathFollower> PathFollower { get; private set; }
        public PoolerModule<EcsData.Route> Route { get; private set; }
        
        public PoolerModule<EcsData.Config> Config { get; private set; }
        public PoolerModule<EcsData.Environment> Environment { get; private set; }
        public PoolerModule<EcsData.Camera> Camera { get; private set; }
        public PoolerModule<EcsData.Level> Level { get; private set; }
        
        public PoolerModule<EcsData.BuildValidMark> BuildValidMark { get; private set; }
        public PoolerModule<EcsData.BuildingTileMap> BuildingTilemap { get; private set; }
        
    }
}