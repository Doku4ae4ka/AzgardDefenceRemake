using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Source.Scripts.ECS.Groups.GameCore
{
    [Serializable]
    public class Configs
    {
        [SerializeField] private int lastID;
        
        private Dictionary<int, List<Vector2>> _routes = new();

        public Dictionary<int, List<Vector2>> Routes => _routes;

        public int FreeID => ++lastID;

        public void SetFreeId(int id)
        {
            lastID = id;
        }
        
        public void SetRoutes(Dictionary<int, List<Vector2>> dictionary)
        {
            _routes = dictionary;
        }
        
        public string SerializePaths()
        {
            StringBuilder serializedData = new StringBuilder();

            foreach (var entry in _routes)
            {
                serializedData.Append(entry.Key);
                serializedData.Append(":");

                for (int i = 0; i < entry.Value.Count; i++)
                {
                    var point = entry.Value[i];
                    serializedData.Append($"({point.x},{point.y})");

                    if (i < entry.Value.Count - 1)
                    {
                        serializedData.Append("/");
                    }
                }

                serializedData.Append(";");
            }

            return serializedData.ToString();
        }
    }
}