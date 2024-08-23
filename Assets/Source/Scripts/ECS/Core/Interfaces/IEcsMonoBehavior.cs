namespace Source.Scripts.ECS.Core.Interfaces
{
    public interface IEcsMonoBehavior
    {
        public int Entity { get; }
        public void DestroyEcsMonoBehavior(float delay);
    }
}