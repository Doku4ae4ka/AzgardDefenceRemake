using Source.EasyECS;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.EasyECS.Core
{
    public abstract class BootstrapComponent : MonoBehaviour, IGameShareItem
    {
        protected GameShare GameShare;
        private DataPack _dataPack;
        public Signal Signal { get; private set; }

        public void PreInit(GameShare gameShare, DataPack dataPack)
        {
            GameShare = gameShare;
            _dataPack = dataPack;
            Signal = gameShare.GetSharedObject<Signal>();
            OnPreInit();
        }
        
        protected virtual void OnPreInit() {}
        
        public abstract void Initialize();
    }
}