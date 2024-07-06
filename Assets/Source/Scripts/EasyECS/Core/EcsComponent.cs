using Source.EasyECS;
using Source.Scripts.SignalSystem;
using UnityEngine;

namespace Source.Scripts.EasyECS.Core
{
    [RequireComponent(typeof(EcsMonoBehavior))]
    public abstract class EcsComponent : MonoSignalListener, IEcsComponentInitialize, IEcsComponentDestroy
    {
        public virtual void Initialize(int entity, Componenter componenter)
        {
            
        }

        public virtual void Destroy(int entity, Componenter componenter)
        {
            
        }
    }
}