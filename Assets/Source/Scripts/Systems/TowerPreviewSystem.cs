using System;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class TowerPreviewSystem : EcsGameSystem
    {

        private const string Goblin = "prototype.goblin";
        protected override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!Prototypes.TryGet(Goblin, out var prototypeEntity)) return;
                
                ref var prototypeData = ref Pooler.Prototype.Get(prototypeEntity);
            
                var newEntity = World.NewEntity();
                
                ref var entityData = ref Pooler.Entity.Add(newEntity);
                entityData.EntityID = $"{prototypeData.Category}.{Configs.FreeID}";
                entityData.Category = prototypeData.Category;
                
                foreach (var buildAction in prototypeData.DataBuilder)
                {
                    buildAction.Invoke(newEntity);
                }
            }
        }
    }
}