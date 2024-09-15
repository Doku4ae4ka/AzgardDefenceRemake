using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Source.Scripts.Core
{
    
#if UNITY_EDITOR
    [Serializable]
#endif
    public class Prototypes
    {
#if UNITY_EDITOR
        [ReadOnly] public List<string> prototypeEditorOnly = new();
#endif
        
        public Dictionary<string, int> _dict = new();

        public void Clear()
        {
            prototypeEditorOnly.Clear();
            _dict.Clear();
        }

        public bool TryGet(string entityID, out int entity)
        {
            if (_dict.TryGetValue(entityID, out var result))
            {
                entity = result;
                return true;
            }

            entity = default;
            return false;
        }

        public int Get(string entityID)
        {
            return _dict[entityID];
        }

        public void Add(string entityID, int entity)
        {
#if UNITY_EDITOR
            prototypeEditorOnly.Add(entityID);
#endif
            _dict[entityID] = entity;
        }
    }
}