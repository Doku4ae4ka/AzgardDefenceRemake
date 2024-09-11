using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class LoaderSystem : EcsGameSystem
    {
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.F4)) GameStarter.Save();
            if (Input.GetKeyDown(KeyCode.F9)) GameStarter.Load();
        }
    }
}