using System;
using System.Collections.Generic;
using Source.Scripts.Core;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public class Entity
    {
        public Entity(string entityID, EntityCategory category)
        {
            id = entityID;
            this.category = category;
            _fieldsDict = new();
            fields = new();
        }

        public string id;
        public EntityCategory category;
        public List<Field> fields;
        private Dictionary<string, Field> _fieldsDict;

        public void Initialize()
        {
            _fieldsDict = new Dictionary<string, Field>();
            if (fields == null) fields = new();
            foreach (var field in fields) _fieldsDict[field.key] = field;
        }

        public void SetField(string key, string value)
        {
            if (_fieldsDict.TryGetValue(key, out var result)) result.value = value;
            else
            {
                var field = new Field(key)
                {
                    value = value
                };
                
                fields.Add(field);
                _fieldsDict[field.key] = field;
            }
        }
        
        public Field GetOrCreateFieldIteration(string fieldKey)
        {
            foreach (var field in fields) if (field.key == fieldKey) return field;
            
            var newField = new Field(fieldKey);
            fields.Add(newField);
            return newField;
        }

        public bool TryGetFieldIteration(string key, out string value)
        {
            if (_fieldsDict.TryGetValue(key, out var result))
            {
                value = result.value;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetField(string key, out string value)
        {
            if (_fieldsDict.TryGetValue(key, out var result))
            {
                value = result.value;
                return true;
            }

            value = default;
            return false;
        }

        public string GetField(string key)
        {
            if (_fieldsDict.TryGetValue(key, out var result)) return result.value;

            return default;
        }

        public Field GetFieldIteration(string key)
        {
            foreach (var field in fields) if (field.key == key)
            {
                return field;
            }

            return default;
        }
    }
}