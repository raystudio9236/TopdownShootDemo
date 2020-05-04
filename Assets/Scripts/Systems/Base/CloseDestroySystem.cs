using Entitas;
using Manager;

namespace Systems.Base
{
    public class CloseDestroySystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _group;
    
        public CloseDestroySystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TargetComp,
                GameMatcher.PosComp,
                GameMatcher.CloseDestroyComp));
        }
    
        public void Execute()
        {
            foreach (var gameEntity in _group.GetEntities())
            {
                var targetPos = GameManager.GetEntity(
                    gameEntity.targetComp.TargetId).posComp.Value;
                var selfPos = gameEntity.posComp.Value;

                var sqrDistance = gameEntity.closeDestroyComp.Distance 
                                  * gameEntity.closeDestroyComp.Distance;

                if ((targetPos - selfPos).sqrMagnitude <= sqrDistance)
                {
                    gameEntity.isDestroyFlag = true;
                }
            }
        }
    }
}