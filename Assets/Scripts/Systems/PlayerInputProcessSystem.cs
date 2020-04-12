using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerInputProcessSystem : ReactiveSystem<InputEntity>
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _playerGroup;
    private readonly Camera _mainCamera;

    public PlayerInputProcessSystem(Contexts contexts) : base(contexts.input)
    {
        _mainCamera = Camera.main;
        _contexts = contexts;
        _playerGroup = _contexts.game.GetGroup(GameMatcher.PlayerTag);
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var playerEntity = _playerGroup.GetSingleEntity();

        foreach (var inputEntity in entities)
        {
            // 处理玩家移动
            playerEntity.ReplaceVelComp(
                new Vector2(
                    inputEntity.inputComp.Dir.x * 10,
                    inputEntity.inputComp.Dir.y * 10
                    )
                );
            
            // 处理玩家旋转
            var mousePos = inputEntity.inputComp.MousePos;
            var worldPos = _mainCamera.ScreenToWorldPoint(mousePos);
            var dir = new Vector2(worldPos.x, worldPos.y)
                      - playerEntity.posComp.Value;
            var angle = Vector2.SignedAngle(Vector2.up, dir);
            playerEntity.ReplaceRotComp(angle);

            // 处理玩家开火
            if (inputEntity.inputComp.Fire)
            {
                playerEntity.AddFireCmdComp(angle);
            }
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
