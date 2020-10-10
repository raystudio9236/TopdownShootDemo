using Entitas;

namespace Systems.Base
{
    public class DestroySystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _group;
    
        public DestroySystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.DestroyFlag);
        }
    
        public void Cleanup()
        {
            foreach (var gameEntity in _group.GetEntities())
            {
                gameEntity.Destroy();
            }
        }
    }
}