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
            // todo 玩家的移动速度暂时设置为10
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
            var angle = dir.Vector2Angle2D();
            playerEntity.ReplaceRotComp(angle);

            // 处理玩家开火
            if (inputEntity.inputComp.MainButton)
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
