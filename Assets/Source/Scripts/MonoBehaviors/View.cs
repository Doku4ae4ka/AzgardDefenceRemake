using Exerussus._1Extensions.SignalSystem;
using Source.Scripts.Core;

namespace Source.Scripts.MonoBehaviors
{
    public abstract class View : MonoSignalListener
    {
        public string viewId;
        public EntityCategory category;
    }
}