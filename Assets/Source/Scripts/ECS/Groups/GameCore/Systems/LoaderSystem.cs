using Exerussus._1EasyEcs.Scripts.Core;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore.Systems
{
    public class LoaderSystem : EasySystem<GameCorePooler>
    {
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (!Pooler.Prototypes.TryGet(Constants.PrototypesId.Enemies.SandGolem, out var prototypeEntity)) return;
                Signal.RegistryRaise(new Signals.CommandSpawnEnemy()
                {
                    PrototypeEntity = prototypeEntity,
                    RouteId = 0,
                });
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (!Pooler.Prototypes.TryGet(Constants.PrototypesId.Enemies.SandGolem, out var prototypeEntity)) return;
                Signal.RegistryRaise(new Signals.CommandSpawnEnemy()
                {
                    PrototypeEntity = prototypeEntity,
                    RouteId = 1,
                });
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                if (!Pooler.Prototypes.TryGet(Constants.PrototypesId.Enemies.SandGolem, out var prototypeEntity)) return;
                Signal.RegistryRaise(new Signals.CommandSpawnEnemy()
                {
                    PrototypeEntity = prototypeEntity,
                    RouteId = 2,
                });
            }
        }
    }
}