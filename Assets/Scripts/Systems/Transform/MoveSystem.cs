using Entitas;
using Hybrid.Base;

namespace Systems.Transform
{
    public class MoveSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _group;

        public MoveSystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.VelComp,
                GameMatcher.PhysicsTag,
                GameMatcher.ViewComp
            ));
        }

        public void Execute()
        {
            foreach (var entity in _group.GetEntities())
            {
                var velComp = entity.velComp;
                var rigidbody = ((IPhysicsView) entity.viewComp.View).Rigidbody;
                rigidbody.velocity = velComp.Value;
            }
        }
    }
}