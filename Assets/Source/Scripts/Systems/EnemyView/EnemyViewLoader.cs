using Leopotam.EcsLite;
using Source.Scripts.Core;
using Source.Scripts.Extensions;
using Source.Scripts.ProjectLibraries;
using Source.Scripts.SaveSystem;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Source.Scripts.Systems.View
{
    public static class EnemyViewLoader
    {
        public static void LoadViewDynamic(EcsWorld world, Pooler pooler, Slot slot)
        {
            foreach (var entity in world.Filter<EcsData.Entity>().Inc<EcsData.DynamicMark>().Exc<EcsData.Prototype>().End())
            {
                ref var entityData = ref pooler.Entity.Get(entity);
                var savingEntity = slot.GetEntity(entityData.EntityID);
                
                if (savingEntity.TryGetField(SavePath.View, out var viewValue))
                {
                    ref var viewData = ref pooler.View.AddOrGet(entity);
                    ref var transformData = ref pooler.Transform.AddOrGet(entity);
                    viewData.Value = Object.Instantiate
                            (Libraries.EnemyLibrary.GetByID(viewValue.ParseEnum<EnemyKeys>()).Enemy.gameObject)
                            .GetComponent<MonoBehaviours.View>();
                    viewData.Value.gameObject.name = $"{entityData.Category} : {entityData.EntityID}";
                    transformData.Value = viewData.Value.transform;
                }
                else continue;
                
                if (savingEntity.TryGetField(SavePath.Position, out var positionValue)) 
                {
                    ref var transformData = ref pooler.Transform.Get(entity);
                    transformData.Value.position = positionValue.ParseVector3();
                }
                
                if (savingEntity.TryGetField(SavePath.Rotation, out var rotationValue))
                {
                    ref var transformData = ref pooler.Transform.Get(entity);
                    transformData.Value.rotation = rotationValue.ParseQuaternion();
                }
            }
        }
    }
}