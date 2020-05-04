using System.Collections.Generic;
using Entitas;
using UnityEngine;
using Utils;

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

            var playerView = (PlayerView) gameEntity.viewComp.View;

            EntityUtil.CreateBulletEntity(_contexts,
                playerView.Shoot.position,
                fireCmd.Angle);
        }
    }
}
