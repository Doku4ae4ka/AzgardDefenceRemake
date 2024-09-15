using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Systems.SaveLoadSystems
{
    public class LoaderSystem : EcsGameSystem
    {
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.F4)) GameStarter.Save();
            if (Input.GetKeyDown(KeyCode.F9)) GameStarter.Load();
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (!Prototypes.TryGet(Constants.PrototypesId.Enemies.SandGolem, out var prototypeEntity)) return;
                Signal.RegistryRaise(new Signals.CommandSpawnEnemy()
                {
                    PrototypeEntity = prototypeEntity,
                    RouteId = 0,
                });
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                if (!Prototypes.TryGet(Constants.PrototypesId.Enemies.SandGolem, out var prototypeEntity)) return;
                Signal.RegistryRaise(new Signals.CommandSpawnEnemy()
                {
                    PrototypeEntity = prototypeEntity,
                    RouteId = 1,
                });
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                if (!Prototypes.TryGet(Constants.PrototypesId.Enemies.SandGolem, out var prototypeEntity)) return;
                Signal.RegistryRaise(new Signals.CommandSpawnEnemy()
                {
                    PrototypeEntity = prototypeEntity,
                    RouteId = 2,
                });
            }
        }
    }
}