using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerInputProcessSystem : ReactiveSystem<InputEntity>
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _playerGroup;

    public PlayerInputProcessSystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
        _playerGroup = _contexts.game.GetGroup(GameMatcher.PlayerTag);
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var playerEntity = _playerGroup.GetSingleEntity();

        foreach (var inputEntity in entities)
        {
            playerEntity.ReplaceVelComp(
                new Vector2(
                    inputEntity.inputComp.Dir.x * 1,
                    inputEntity.inputComp.Dir.y * 1
                    )
                );
        }
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.InputComp);
    }
}
