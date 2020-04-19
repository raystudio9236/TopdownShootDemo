using Entitas;
using UnityEngine;

public class UpdateTimerSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _group;

    public UpdateTimerSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.TimerComp);
    }

    public void Execute()
    {
        var dt = Time.deltaTime;

        foreach (var gameEntity in _group.GetEntities())
        {
            var newTime = Mathf.Max(
                gameEntity.timerComp.FireTimer - dt,
                0);
            gameEntity.ReplaceTimerComp(newTime);
        }
    }
}