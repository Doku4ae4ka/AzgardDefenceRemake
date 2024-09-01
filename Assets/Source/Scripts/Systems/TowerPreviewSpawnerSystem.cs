using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public class TowerPreviewSpawnerSystem : EcsGameSystem
    {
        protected override void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     foreach (var entity in PrototypeMask.End())
            //     {
            //         ref var prototypeData = ref Pooler.Prototype.Get(entity);
            //
            //         var newEntity = World.NewEntity();
            //         ref var entityData = ref Pooler.Entity.Add(newEntity);
            //         entityData.EntityID = Random.Range(0, 1111111111);
            //         entityData.Category = prototypeData.Category;
            //
            //         foreach (var buildAction in prototypeData.DataBuilder)
            //         {
            //             buildAction.Invoke(newEntity);
            //         }
            //     }
            // }
        }
    }
}