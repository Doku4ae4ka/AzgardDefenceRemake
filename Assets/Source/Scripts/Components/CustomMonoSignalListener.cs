using Exerussus._1Extensions.SignalSystem;
using Source.Scripts.Core;

namespace Source.Scripts.Components
{
    public class CustomMonoSignalListener : MonoSignalListener
    {
        public override Signal Signal => DependenciesContainer.SignalHandler.Signal;
    }
}