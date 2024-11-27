using System;
using System.Collections.Generic;
using Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Source.Scripts.ECS.Groups.SlotSaver.Core
{
    [Serializable]
    public class SlotEntity
    {
        /// <summary> айди сохраненной сущности </summary>
        public string id;
        /// <summary> является ли оно конфигом, прототипом и дайнемик </summary>
        public SlotCategory category;
        public string type;
        public List<Field> fields;
        private Dictionary<string, Field> _fieldsDict;

        public SlotEntity(string entityID, SlotCategory category, string type)
        {
            id = entityID;
            this.category = category;
            this.type = type;
            _fieldsDict = new Dictionary<string, Field>();
            fields = new List<Field>();
        }

        public static string[] Dropdown()
        {
            return SavePath.EntityType.All;
        }

        public void Initialize()
        {
            _fieldsDict = new Dictionary<string, Field>();
            if (fields == null) fields = new List<Field>();
            foreach (var field in fields) _fieldsDict[field.key] = field;
        }

        public void SetField(string key, string value)
        {
            if (_fieldsDict.TryGetValue(key, out var result))
            {
                result.value = value;
            }
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
            foreach (var field in fields)
                if (field.key == fieldKey)
                    return field;

            var newField = new Field(fieldKey);
            fields.Add(newField);
            return newField;
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

        public bool TryGetFloatField(string key, out float value)
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (float.TryParse(resultString.value, out var resultFloat))
                {
                    value = resultFloat;
                    return true;
                }

            value = default;
            return false;
        }

        public bool TryGetIntField(string key, out int value)
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (int.TryParse(resultString.value, out var resultInt))
                {
                    value = resultInt;
                    return true;
                }

            value = default;
            return false;
        }

        public bool TryGetEnumField<T>(string key, out T value) where T : struct, Enum
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (Enum.TryParse(resultString.value, true, out T parsedValue))
                {
                    value = parsedValue;
                    return true;
                }

            value = default;
            return false;
        }

        public bool TryGetVector2Field(string key, out Vector2 value)
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (resultString.value.TryParseVector2(out var resultVector2))
                {
                    value = resultVector2;
                    return true;
                }

            value = default;
            return false;
        }

        public bool TryGetVector3Field(string key, out Vector3 value)
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (resultString.value.TryParseVector3(out var resultVector3))
                {
                    value = resultVector3;
                    return true;
                }

            value = default;
            return false;
        }

        public bool TryGetVector4Field(string key, out Vector4 value)
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (resultString.value.TryParseVector4(out var resultVector4))
                {
                    value = resultVector4;
                    return true;
                }

            value = default;
            return false;
        }

        public bool TryGetQuaternionField(string key, out Quaternion value)
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (resultString.value.TryParseQuaternion(out var resultQuaternion))
                {
                    value = resultQuaternion;
                    return true;
                }

            value = default;
            return false;
        }

        public bool TryGetRoutesField(string key, out Dictionary<int, List<Vector2>> result)
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (resultString.value.TryParseRoutes(out var value))
                {
                    result = value;
                    return true;
                }

            result = default;
            return false;
        }

        public bool TryGetTileEntriesField(string key, Dictionary<string, TileBase> tileDictionary,
            out List<KeyValuePair<Vector3Int, TileBase>> value)
        {
            if (_fieldsDict.TryGetValue(key, out var resultString))
                if (resultString.value.TryParseTileEntries(tileDictionary, out var resultList))
                {
                    value = resultList;
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
            foreach (var field in fields)
                if (field.key == key)
                    return field;

            return default;
        }
    }
}