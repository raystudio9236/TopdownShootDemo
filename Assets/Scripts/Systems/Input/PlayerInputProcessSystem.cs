using System.Collections.Generic;
using Components.Stat;
using Entitas;
using Other;
using UnityEngine;

namespace Systems.Input
{
    public class PlayerInputProcessSystem : ReactiveSystem<InputEntity>
    {
        private readonly Contexts _contexts;
        private readonly Camera _mainCamera;

        public PlayerInputProcessSystem(Contexts contexts) : base(contexts.input)
        {
            _mainCamera = Camera.main;
            _contexts = contexts;
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override ICollector<InputEntity> GetTrigger(
            IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.InputComp);
        }

        protected override void Execute(List<InputEntity> entities)
        {
            var playerEntity = _contexts.game.playerTagEntity;
            var inputComp = _contexts.input.inputComp;

            // 处理玩家移动
            var velocity =
                playerEntity.GetStat(StatFlag.Velocity);
            playerEntity.ReplaceVelComp(
                new Vector2(
                    inputComp.Dir.x * velocity,
                    inputComp.Dir.y * velocity
                )
            );

            // 处理玩家旋转
            var mousePos = inputComp.MousePos;
            var worldPos = _mainCamera.ScreenToWorldPoint(mousePos);
            var dir = new Vector2(worldPos.x, worldPos.y)
                      - playerEntity.posComp.Value;
            var angle = dir.Vector2Angle2D();
            playerEntity.ReplaceRotComp(angle);
        }
    }
}