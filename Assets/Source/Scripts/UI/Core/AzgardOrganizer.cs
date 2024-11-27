using System.Diagnostics;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1OrganizerUI.Scripts.Ui;
using Source.Scripts.Core;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class AzgardOrganizer : OrganizerUI<AzgardModuleUI>
    {
        [SerializeField] private UIContext context;
        [SerializeField] private TMP_Text versionTMP;
        private ShareData _shareData;
        private SignalHandler _signalHandler;

        protected override void SetShareData(ShareData shareData)
        {
            if (_signalHandler == null) _signalHandler = DependenciesContainer.SignalHandler;
            _shareData = shareData;
            shareData.AddObject(_signalHandler.Signal);
            shareData.AddObject(_signalHandler);
            shareData.AddObject(context);
        }

        [Conditional("UNITY_EDITOR")]
        private void ValidateAsset()
        {
            if (_signalHandler as SignalHandler == null)
            {
                _signalHandler = DependenciesContainer.SignalHandler;
            }
        }
    }
}