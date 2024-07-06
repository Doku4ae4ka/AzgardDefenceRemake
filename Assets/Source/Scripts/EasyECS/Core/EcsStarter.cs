
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.EasyECS
{
    public class EcsStarter : Starter
    {
        [SerializeField] private bool updateSystemsOnValidate;
        [SerializeField] private bool autoTransition;
        [Space]
        [SerializeField, GUIColor("@NoneColor")] private List<SystemPack> none = new List<SystemPack>();
        [Space]
        [SerializeField, GUIColor("@StartColor")] private List<SystemPack> start = new List<SystemPack>();
        [Space]
        [SerializeField, GUIColor("@FixedUpdateColor")] private List<SystemPack> fixedUpdate = new List<SystemPack>();
        [Space]
        [SerializeField, GUIColor("@TickColor")] private List<SystemPack> tick = new List<SystemPack>();
        [Space]
        [Tooltip("Перенесите сюда систему, чтобы автоматически поставить её в дефолтную группу.")]
        [SerializeField, GUIColor("@DefaultColor")] private List<SystemPack> setDefault = new List<SystemPack>();
        
        private Color NoneColor => new Color(1, .9f, .9f); 
        private Color StartColor => new Color(.9f, .9f, 1f); 
        private Color UpdateColor => new Color(.9f, .9f, .7f); 
        private Color FixedUpdateColor => new Color(.9f, 1f, .9f); 
        private Color TickColor => new Color(.8f, .8f, .7f);
        private Color DefaultColor => new Color(.8f, .3f, .7f); 
        private List<List<SystemPack>> AllSystems => new List<List<SystemPack>>
        {
            none,
            start,
            fixedUpdate,
            tick
        };
        
        [Button]
        private void UpdateSystems()
        {
            DelNullSystems();
            SetDefault();
            UpdateSettings();
            FindEasySystemClasses();
        }
        

        private void SetDefault()
        {
            for (var index = setDefault.Count - 1; index >= 0; index--)
            {
                var systemPack = setDefault[index];
                systemPack.initializeType = systemPack.CreateSystem().DefaultInitializeType();
                DetermineAndAdd(systemPack);
                setDefault.Remove(systemPack);
            }
        }
        
        private void UpdateSettings()
        {
            foreach (var systemPackList in AllSystems)
            {
                foreach (var systemPack in systemPackList)
                {
                    systemPack.UpdateEditorSettings();
                }
            }
        }
        
        private bool TryGetSystemPack(Type type, out SystemPack foundSystemPack)
        {
            foundSystemPack = null;

            foreach (var systemPackList in AllSystems)
            {
                var systemPack = systemPackList.FirstOrDefault(sp => sp.Name == type.Name);
                if (systemPack != null)
                {
                    foundSystemPack = systemPack;
                    return true;
                }
            }

            return false;
        }

        private void DelNullSystems()
        {            
            foreach (var systemPackList in AllSystems)
            {
                for (var index = systemPackList.Count - 1; index >= 0; index--)
                {
                    var systemPack = systemPackList[index];
                    if (Type.GetType(systemPack.fullName) == null) systemPackList.Remove(systemPack);
                }
            }
        }
        
        private void FindEasySystemClasses()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<string> foundTypeNames = new List<string>();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(EasySystem)) && !type.IsAbstract)
                    {
                        foundTypeNames.Add(type.Name);
                        if (TryGetSystemPack(type, out SystemPack systemPack))
                        {
                            if (autoTransition) DetermineAndAdd(systemPack);
                            continue;
                        }
                        
                        DetermineAndAdd(type);
                    }
                }
            }

            RemoveObsoleteSystemPacks(foundTypeNames);
            if (!autoTransition) UpdateTypeByGroup();
        }
        
        private void DetermineAndAdd(Type type)
        {
            var systemPack = new SystemPack(type);
            switch (systemPack.initializeType)
            {
                case InitializeType.None:
                    CheckContainAndSet(systemPack, none);
                    break;
                case InitializeType.Start:
                    CheckContainAndSet(systemPack, start);
                    break;
                case InitializeType.FixedUpdate:
                    CheckContainAndSet(systemPack, fixedUpdate);
                    break;
                case InitializeType.Tick:
                    CheckContainAndSet(systemPack, tick);
                    break;
            }
        }

        private void UpdateTypeByGroup()
        {
            foreach (var systemPack in none) systemPack.initializeType = InitializeType.None;
            foreach (var systemPack in start) systemPack.initializeType = InitializeType.Start;
            foreach (var systemPack in fixedUpdate) systemPack.initializeType = InitializeType.FixedUpdate;
            foreach (var systemPack in tick) systemPack.initializeType = InitializeType.Tick;
        }
        
        /// <summary>
        /// Нужен для автоматической форсированной отправки в выбранный тип
        /// </summary>
        private void DetermineAndAdd(SystemPack systemPack)
        {
            
            switch (systemPack.initializeType)
            {
                case InitializeType.None:
                    CheckContainAndSet(systemPack, none);
                    break;
                case InitializeType.Start:
                    CheckContainAndSet(systemPack, start);
                    break;
                case InitializeType.FixedUpdate:
                    CheckContainAndSet(systemPack, fixedUpdate);
                    break;
                case InitializeType.Tick:
                    CheckContainAndSet(systemPack, tick);
                    break;
            }
        }

        private void CheckContainAndSet(SystemPack systemPack, List<SystemPack> packCollection)
        {
            if (!packCollection.Contains(systemPack))
            {
                RemoteFromAll(systemPack, packCollection);
                packCollection.Add(systemPack);
            }
        }

        private void RemoteFromAll(SystemPack systemPack, List<SystemPack> excludeCollection)
        {
            if (excludeCollection != none) TryRemove(systemPack, none);
            if (excludeCollection != start) TryRemove(systemPack, start);
            if (excludeCollection != fixedUpdate) TryRemove(systemPack, fixedUpdate);
            if (excludeCollection != tick) TryRemove(systemPack, tick);
        }

        private void TryRemove(SystemPack systemPack, List<SystemPack> packCollection)
        {
            for (var index = packCollection.Count - 1; index >= 0; index--)
            {
                var lookingSystemPack = packCollection[index];
                if (lookingSystemPack == systemPack)
                {
                    packCollection.Remove(lookingSystemPack);
                }
            }
        }
        
        private void RemoveObsoleteSystemPacks(List<string> foundTypeNames)
        {
            foreach (var systemPackList in AllSystems)
            {
                for (int i = systemPackList.Count - 1; i >= 0; i--)
                {
                    if (!foundTypeNames.Contains(systemPackList[i].Name))
                    {
                        systemPackList.RemoveAt(i);
                    }
                }
            }
        }

        private void OnValidate()
        {
            if (updateSystemsOnValidate) UpdateSystems();
        }
        
        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            if (start.Count > 0) foreach (var systemPack in start) initSystems.Add(systemPack.CreateSystem());
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            if (fixedUpdate.Count > 0) foreach (var systemPack in fixedUpdate) fixedUpdateSystems.Add(systemPack.CreateSystem());
        }

        protected override void SetTickUpdateSystems(IEcsSystems tickUpdateSystems)
        {
            if (tick.Count > 0) foreach (var systemPack in tick) tickUpdateSystems.Add(systemPack.CreateSystem());
        }
    }
    
    [Serializable]
    public class SystemPack
    {
        public SystemPack(Type type)
        {
            Type = type;
            name = type.Name;
            fullName = type.FullName;
            var system = CreateSystem();
            initializeType = system.DefaultInitializeType();
            description = system.Description();
        }
        
        [SerializeField, HideInInspector] public string name;
        [SerializeField, HideInInspector] public string fullName;
        [Tooltip("@description")]
        [LabelText("@name")]
        [HorizontalGroup("Split")]
        [HorizontalGroup("Split/Left")]
        [HideLabel, GUIColor("@NormalColor")]
        [SerializeField] public InitializeType initializeType;

        [SerializeField, HideInInspector] public string description;
        private Color NormalColor => new Color(1, 1f, 1f); 
        public string Name => name;
        public Type Type { get; private set; }

        public void UpdateEditorSettings()
        {
            var system = CreateSystem();
            initializeType = system.DefaultInitializeType();
            description = $"{system.Description()}\n\nDefault: {initializeType}";
        }
        
        public EasySystem CreateSystem()
        {
            var result = Type.GetType(fullName);
            if (result == null) Debug.Log($"{fullName} is null.");
            return Activator.CreateInstance(result) as EasySystem;
        }
    }
}
