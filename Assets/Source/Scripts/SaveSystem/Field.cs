using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public class Field
    {
        public Field(string fieldKey)
        {
            key = fieldKey;
        }
        
        [ValueDropdown("GetKeyOptions")]
        public string key;
        public string value;
        
        private static IEnumerable<string> GetKeyOptions()
        {
            return SavePath.GetKeyOptions();
        }
    }
}