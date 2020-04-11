using Entitas;
using UnityEngine;

public class MoveSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _group;

    public MoveSystem(Contexts contexts)
    {
        _group = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.PosComp,
            GameMatcher.VelComp
        ));
    }

    public void Execute()
    {
        var dt = Time.deltaTime;

        foreach (var entity in _group.GetEntities())
        {
            var posComp = entity.posComp;
            var velComp = entity.velComp;
            entity.ReplacePosComp(new Vector2(
                posComp.Value.x + dt * velComp.Value.x,
                posComp.Value.y + dt * velComp.Value.y
            ));
        }
    }
}