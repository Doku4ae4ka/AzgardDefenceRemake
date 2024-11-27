using Exerussus._1Extensions.SignalSystem;
using Exerussus._1OrganizerUI.Scripts.Ui;
using Source.Scripts.Components;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class ShopButtonUI : CustomMonoSignalListener
    {
        [CustomAttributes.ValueDropdown("Dropdown")] 
        [SerializeField] private string towerId;
        
        private static string[] Dropdown() => Constants.PrototypesId.Towers.All;
        
        public void SpawnTowerPreview()
        {
            Signal.RegistryRaise(new Signals.CommandSpawnTowerPreview
            {
                TowerId = towerId 
            });
        }
        
    }
}