using System;
using Exerussus._1OrganizerUI.Scripts.Ui;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.UI
{
    [Serializable]
    public class AzgardModuleUI : UiModule
    {
        [SerializeField, ]//ValueDropdown("DropdownNames")]
        private string name;
        
        [SerializeField, ]//ValueDropdown("DropdownPaths")]
        private string resourcePath;

        [SerializeField, ]//ValueDropdown("DropdownGroups")]
        private string group;

        [SerializeField] private int order;
        
        public override int Order { get => order; protected set => order = value; }

        public override string Name
        {
            get => name;
            protected set => name = value;
        }

        public override string ResourcePath
        {
            get => resourcePath;
            protected set => resourcePath = value;
        }

        public override string Group
        {
            get => group;
            protected set => group = value;
        }


        public static string[] DropdownNames() => Constants.UI.Name.All;
        public static string[] DropdownPaths() => Constants.UI.Path.All;
        public static string[] DropdownGroups() => Constants.UI.Group.All;
        
    }
}