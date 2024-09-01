// using Leopotam.EcsLite;
// using Source.Scripts.Core;
// using Source.Scripts.SaveSystem;
//
// namespace Source.Scripts.Systems
// {
//     public class HealthSystem : EcsGameSystem
//     {
//         private EcsFilter _healthFilter;
//
//         protected override void Initialize()
//         {
//             _healthFilter = InGameMask.Inc<EcsData.Health>().End();
//             Memory.save.OnDynamic += TryAddData;
//             Memory.load.OnDynamic += TryLoadData;
//         }
//
//         public override void Destroy(IEcsSystems systems)
//         {
//             base.Destroy(systems);
//             Memory.save.OnDynamic -= TryAddData;
//             Memory.load.OnDynamic -= TryLoadData;
//         }
//     }
// }