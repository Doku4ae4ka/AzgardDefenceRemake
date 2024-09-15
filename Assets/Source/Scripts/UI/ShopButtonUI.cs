using Exerussus._1Extensions.SignalSystem;
using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class ShopButtonUI : MonoSignalListener
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