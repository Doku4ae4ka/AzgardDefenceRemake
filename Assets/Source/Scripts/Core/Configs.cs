using System;
using UnityEngine;

namespace Source.Scripts.Core
{
    [Serializable]
    public class Configs
    {
        [SerializeField] private int lastID;
        
        public int FreeID => ++lastID;

        public void SetFreeId(int id)
        {
            lastID = id;
        }
    }
}