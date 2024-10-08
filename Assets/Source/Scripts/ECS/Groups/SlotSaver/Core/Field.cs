using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Ecs.Modules.PauldokDev.SlotSaver.Core
{
    [Serializable]
    public class Field
    {
        [ValueDropdown("GetKeyOptions")] public string key;

        public string value;

        public Field(string fieldKey)
        {
            key = fieldKey;
        }

        private static IEnumerable<string> GetKeyOptions()
        {
            return SavePath.AllPathFields;
        }
    }
}