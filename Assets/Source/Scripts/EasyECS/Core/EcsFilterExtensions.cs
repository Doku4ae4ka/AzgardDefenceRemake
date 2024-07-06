using System;
using Source.Scripts.EasyECS.Core;
using UnityEngine;

namespace Source.EasyECS
{
    public static  class EcsFilterExtensions
    {
        public static void ForeachEntities(this EcsFilter filter, Action<int> action)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity);
            }
        }
        
        public static void ForeachEntities(this EcsFilter filter, Action<int, bool> action, bool boolValue)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity, boolValue);
            }
        }
        
        public static void ForeachEntities(this EcsFilter filter, Action<int, string, bool> action, string stringValue, bool boolValue)
        {
            foreach (var entity in filter)
            {
                action?.Invoke(entity, stringValue, boolValue);
            }
        }
        
        public static int GetFirstEntity(this EcsFilter filter)
        {
            foreach (var entity in filter)
            {
                return entity;
            }

            throw new Exception("Пустой филтр");
        }

        public static bool HasAny(this EcsFilter filter)
        {
            foreach (var _ in filter)
            {
                return true;
            }

            return false;
        }

        public static bool TryGetFirstEntity(this EcsFilter filter, out int entity)
        {
            foreach (var e in filter)
            {
                entity = e;
                return true;
            }

            entity = default;
            return false;
        }
        public static void Foreach(this EcsFilter filter, Action<int> action)
        {
            foreach (var entity in filter)
            {
                action.Invoke(entity);
            }
        }

        public static void Foreach(this EcsFilter filter, Action action)
        {
            foreach (var _ in filter)
            {
                action.Invoke();
            }
        }
        
        public static float GetDistance(this Componenter componenter, int firstEntity, int secondEntity)
        {
            var firstTransform = componenter.Get<TransformData>(firstEntity).Value;
            var secondTransform = componenter.Get<TransformData>(secondEntity).Value;

            return Vector2.Distance(firstTransform.position, secondTransform.position);
        }

        public static bool GetIsDistanceLessThan(this Componenter componenter, int firstEntity, int secondEntity, float distance)
        {
            var firstTransform = componenter.Get<TransformData>(firstEntity).Value;
            var secondTransform = componenter.Get<TransformData>(secondEntity).Value;

            return Vector2.Distance(firstTransform.position, secondTransform.position) > distance;
        }

        public static bool TryGetCloseEntity(this Componenter componenter, int originEntity, EcsFilter lookingFilter, out int foundEntity, out float distance)
        {
            var originTransform = componenter.Get<TransformData>(originEntity).Value;

            var closeEntity = -1;
            var closeDistance = 9999f;
            var hasCloseEntity = false;
            
            foreach (var lookingEntity in lookingFilter)
            {
                var lookingTransform = componenter.Get<TransformData>(lookingEntity).Value;
                
                if (!hasCloseEntity)
                {
                    hasCloseEntity = true;
                    closeEntity = lookingEntity;
                    closeDistance = Vector2.Distance(originTransform.position, lookingTransform.position);
                    continue;
                }
                
                var lookingDistance = Vector2.Distance(originTransform.position, lookingTransform.position);

                if (lookingDistance < closeDistance)
                {
                    closeDistance = lookingDistance;
                    closeEntity = lookingEntity;
                }
            }

            if (hasCloseEntity)
            {
                foundEntity = closeEntity;
                distance = closeDistance;
                return true;
            }
            else
            {
                foundEntity = default;
                distance = default;
                return false;
            }
        }
        
        public static bool TryGetCloseEntity(this Componenter componenter, int originEntity, EcsFilter lookingFilter, out int foundEntity)
        {
            var originTransform = componenter.Get<TransformData>(originEntity).Value;

            var closeEntity = -1;
            var closeDistance = 9999f;
            var hasCloseEntity = false;
            
            foreach (var lookingEntity in lookingFilter)
            {
                var lookingTransform = componenter.Get<TransformData>(lookingEntity).Value;
                
                if (!hasCloseEntity)
                {
                    hasCloseEntity = true;
                    closeEntity = lookingEntity;
                    closeDistance = Vector2.Distance(originTransform.position, lookingTransform.position);
                    continue;
                }
                
                var lookingDistance = Vector2.Distance(originTransform.position, lookingTransform.position);

                if (lookingDistance < closeDistance)
                {
                    closeDistance = lookingDistance;
                    closeEntity = lookingEntity;
                }
            }

            if (hasCloseEntity)
            {
                foundEntity = closeEntity;
                return true;
            }
            else
            {
                foundEntity = default;
                return false;
            }
        }
        
    }
    
}
