using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class FireSystem : ReactiveSystem<GameEntity>
{
    private readonly  Contexts _contexts;
    
    public FireSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(
            GameMatcher.PlayerTag,
            GameMatcher.FireCmdComp,
            GameMatcher.ViewComp
            ));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var gameEntity in entities)
        {
            var fireCmd = gameEntity.fireCmdComp;
            gameEntity.RemoveFireCmdComp();

            if (gameEntity.timerComp.FireTimer > 0)
                continue;
            
            // todo 暂时将开火CD设置为0.5s
            gameEntity.ReplaceTimerComp(0.5f);

            var playerView = (PlayerView) gameEntity.viewComp.View;

            // todo，子弹飞行速度暂时设置为5
            EntityUtil.CreateBulletEntity(_contexts,
                playerView.Shoot.position,
                fireCmd.Angle.Angle2Vector2D() * 5,
                fireCmd.Angle);
        }
    }
}
