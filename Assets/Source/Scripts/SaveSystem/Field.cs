using System;

namespace Source.Scripts.SaveSystem
{
    [Serializable]
    public class Field
    {
        public Field(string fieldKey)
        {
            key = fieldKey;
        }

        public string key;
        public string value;
    }
}