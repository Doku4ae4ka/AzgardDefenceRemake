using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.ECS.Groups.SlotSaver.Core;

namespace Source.Scripts.ECS.Groups.AzgardView.Systems
{
    public class ViewSystem : EasySystem<AzgardViewPooler>
    {
        private EcsFilter _viewFilter;
        
        protected override void Initialize()
        {
           
        }

        public override void Destroy(IEcsSystems systems)
        {
         
        }

        // protected override void OnSignal(Signals.OnViewAssetLoaded data)
        // {
        //     if (!data.PackedEntity.Unpack(World, out var unpackedEntity))
        //     {
        //         // сделать возврат в пул вместо уничтожения
        //         ProjectTask.TestCode(() => { Object.Destroy(data.Transform.gameObject); });
        //         return;
        //     }
        //
        //     if (Pooler.Tower.Has(unpackedEntity) ||
        //         Pooler.Enemy.Has(unpackedEntity) ||
        //         Pooler.Level.Has(unpackedEntity) ||
        //         Pooler.Environment.Has(unpackedEntity))
        //     {
        //         ref var transformData = ref Pooler.Transform.Add(unpackedEntity);
        //         transformData.Value = data.Transform;
        //     
        //         ref var position = ref Pooler.Position.Get(unpackedEntity);
        //         ref var rotation = ref Pooler.Rotation.Get(unpackedEntity);
        //         transformData.Value.position = position.Value;
        //         transformData.Value.rotation = rotation.Value;
        //     }
        // }

        // private void TryLoadDynamic(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        // {
        //     ViewLoader.LoadViewDynamic(ecsWorld, pooler, slot, Signal);
        // }
        //
        // private void TryLoadStatic(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        // {
        //     ViewLoader.LoadViewStatic(ecsWorld, pooler, slot, Signal);
        // }
        //
        // private void TryLoadPrototypes(EcsWorld ecsWorld, Pooler pooler, Slot slot)
        // {
        //     ViewLoader.LoadViewPrototype(ecsWorld, pooler, slot, Signal);
        // }
    }
}