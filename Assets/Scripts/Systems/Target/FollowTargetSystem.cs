using Components.Stat;
using Entitas;
using Other;

namespace Systems.Target
{
    public class FollowTargetSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly  IGroup<GameEntity> _group;
    
        public FollowTargetSystem(Contexts contexts)
        {
            _contexts = contexts;
            _group = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.TargetComp,
                GameMatcher.VelComp,
                GameMatcher.RotComp,
                GameMatcher.StatsComp
            ));
        }
    
        public void Execute()
        {
            foreach (var entity in _group.GetEntities())
            {
                var targetEntity = _contexts.game.GetEntityWithIdComp(
                    entity.targetComp.TargetId);

                var targetPos = targetEntity.posComp.Value;
                var selfPos = entity.posComp.Value;

                var dirVector = (targetPos - selfPos).normalized;

                var vel = entity.GetStat(VarFlag.Velocity);
            
                // 朝着目标方向
                entity.ReplaceRotComp(dirVector.Vector2Angle2D());
                entity.ReplaceVelComp(dirVector * vel);
            }
        }
    }
}
