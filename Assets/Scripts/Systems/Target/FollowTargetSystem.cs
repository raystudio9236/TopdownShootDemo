using Components.Stat;
using Components.Target;
using Entitas;
using Other;

namespace Systems.Target
{
    public class FollowTargetSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _group;

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
                var targetComp = entity.targetComp;

                var targetEntity = _contexts.game.GetEntityWithIdComp(
                    targetComp.TargetId);

                // 丢失目标
                if (!targetEntity.IsValid())
                {
                    // 是否继续寻找下一个目标
                    if (targetComp.LostTargetActionType ==
                        LostTargetActionType.Keep)
                    {
                        entity.AddFindTargetCmdComp(
                            targetComp.TargetTag,
                            targetComp.FindTargetType,
                            targetComp.LostTargetActionType);
                        entity.RemoveTargetComp();
                    }

                    continue;
                }

                var targetPos = targetEntity.posComp.Value;
                var selfPos = entity.posComp.Value;

                var dirVector = (targetPos - selfPos).normalized;

                var vel = entity.GetStat(StatFlag.Velocity);

                // 朝着目标方向
                entity.ReplaceRotComp(dirVector.Vector2Angle2D());
                entity.ReplaceVelComp(dirVector * vel);
            }
        }
    }
}