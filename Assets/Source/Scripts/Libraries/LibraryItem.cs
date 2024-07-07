using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Libraries
{
    [Serializable]
    public abstract class LibraryItem<TEnum> where TEnum : Enum
    {
        public abstract void SetIDInEditor(TEnum id);
        public abstract TEnum ID { get; }
        public virtual void OnValidation() {}
        
        public bool TryLoad<T>(string folderPath, out T lookingObject) where T : Object
        {
            T[] lookingObjects = Resources.LoadAll<T>(folderPath);

            if (lookingObjects.Length > 0)
            {
                T firstObject = lookingObjects[0];

                lookingObject = firstObject;
                return true;
            }

            Debug.Log($"Отсутствует {typeof(T).Name} по пути {folderPath}");
            lookingObject = null;
            return false;
        }
        public bool TryGetPath<T>(string folderPath, out string path) where T : Object
        {
            T[] lookingObjects = Resources.LoadAll<T>(folderPath);

            if (lookingObjects.Length > 0)
            {
                T firstObject = lookingObjects[0];

                path = firstObject.name;
                return true;
            }

            Debug.Log($"Отсутствует {typeof(T).Name} по пути {folderPath}");
            path = null;
            return false;
        }
        
        public void Check<T>(string folderPath) where T : Object
        {
            T[] lookingObjects = Resources.LoadAll<T>(folderPath);
            
            if (lookingObjects.Length == 0)
            {
                Debug.LogError($"Отсуствует {typeof(T).Name} по пути {folderPath}");
            }
        }
        
        public bool TryGetRequirementPath<TRequiring>(string folderPath, out string objectPath) where TRequiring : Component
        {
            GameObject[] lookingObjects = Resources.LoadAll<GameObject>(folderPath);

            if (lookingObjects.Length > 0)
            {
                foreach (var looking in lookingObjects)
                {
                    var rObject = looking.GetComponent<TRequiring>();
                    if (rObject != null)
                    {
                        objectPath = rObject.name;
                        return true;
                    }
                }
            }
            
            Debug.LogError($"Отсуствует {typeof(TRequiring).Name} по пути {folderPath}");
            objectPath = default;
            return false;
        }
    }
}