using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Scripts.ProjectLibraries
{
    public class Library<T, TE> : ScriptableObject, ILibrary 
        where TE : Enum
        where T : LibraryItem<TE> , new()
    {
        [SerializeField] protected T[] items;
        private Dictionary<TE, T> _itemByKey;
        
        public T GetByID(TE id)
        {
            return _itemByKey[id];
        }

        public T TryGetById(TE id)
        {
            if (_itemByKey.ContainsKey(id))
            {
                return _itemByKey[id];
            }
            throw new Exception($"Cannot find library item in library : <<{GetType().Name}>> by index : <<{id}>>.");
        }

        public virtual void Initialize()
        {
            _itemByKey = new Dictionary<TE, T>();
            foreach (var item in items)
            {
                _itemByKey[item.ID] = item;
            }
        }
        
        protected virtual void OnValidation() {}
        
        private void OnValidate()
        {
            AddAllKeys();
            OnValidation();
            foreach (var item in items) item.OnValidation();
        }

        private void AddAllKeys()
        {
            var listToAdd = new List<TE>();
            var allEnums = (TE[])Enum.GetValues(typeof(TE));
            
            foreach (var te in allEnums)
            {
                var hasTe = false;

                if (items != null && items.Length > 0)
                {
                    foreach (var item in items)
                    {
                        if (item.ID.Equals(te))
                        {
                            hasTe = true;
                            break;
                        }
                    }
                }
                
                if (!hasTe)
                {
                    listToAdd.Add(te);
                }
            }

            var itemList = items != null && items.Length > 0 ? items.ToList() : new List<T>();
            foreach (var te in listToAdd)
            {
                var newItem = new T();
                newItem.SetIDInEditor(te);
                itemList.Add(newItem);
            }

            items = itemList.ToArray();
        }
    }

    public interface ILibrary
    {
        public void Initialize();
    }
}