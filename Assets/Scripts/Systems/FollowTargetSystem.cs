using Entitas;

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
            GameMatcher.RotComp
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
            
            // 朝着目标方向
            // todo 敌人跟踪主角的速度，暂时设置为5
            entity.ReplaceRotComp(dirVector.Vector2Angle2D());
            entity.ReplaceVelComp(dirVector * 5);
        }
    }
}
