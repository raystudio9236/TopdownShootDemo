using Entitas;

namespace Systems.Transform
{
    public class SyncPosSystem : IExecuteSystem
    {
        private readonly  IGroup<GameEntity> _group;
    
        public SyncPosSystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.ViewComp,
                GameMatcher.PosComp,
                GameMatcher.PhysicsTag
            ));
        }
    
        public void Execute()
        {
            foreach (var gameEntity in _group.GetEntities())
            {
                var pos = gameEntity.viewComp.View.transform.position;
                gameEntity.ReplacePosComp(pos);
            }
        }
    }
}
