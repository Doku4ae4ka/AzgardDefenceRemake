using Source.Scripts.Core;

namespace Source.Scripts.ECS.Systems
{
    public class EnemySpawnSystem : EcsGameSystem<Signals.CommandSpawnEnemy>
    {
        protected override void OnSignal(Signals.CommandSpawnEnemy data)
        {
            ref var prototypeData = ref Pooler.Prototype.Get(data.PrototypeEntity);
            
            var newEntity = World.NewEntity();
            ref var entityData = ref Pooler.Entity.Add(newEntity);
            entityData.EntityID = $"{prototypeData.Category}.{Configs.FreeID}";
            entityData.Category = prototypeData.Category;

            foreach (var action in prototypeData.DataBuilder) action.Invoke(newEntity);
            
            ref var positionData = ref Pooler.Position.Get(newEntity);
            positionData.Value = data.Position;
            if (Pooler.Transform.Has(newEntity))
            {
                ref var transformData = ref Pooler.Transform.Get(newEntity);
                transformData.Value.position = data.Position;
            }
            ref var viewData = ref Pooler.EnemyView.Get(newEntity);
            viewData.Value.Show();
        }
    }
}