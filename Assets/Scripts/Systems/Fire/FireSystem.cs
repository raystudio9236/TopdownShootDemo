using System.Collections.Generic;
using Components.Stat;
using Entitas;
using Hybrid;
using UnityEngine;
using Utils;

namespace Systems.Fire
{
    public class FireSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;

        public FireSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(
            IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.FireCmdComp);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isPlayerTag;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var gameEntity in entities)
            {
                var fireCmd = gameEntity.fireCmdComp;
                gameEntity.RemoveFireCmdComp();
                if (fireCmd.Count == 0)
                    continue;

                var playerView = (PlayerView) gameEntity.viewComp.View;
                var playerDamage = gameEntity.GetStat(VarFlag.Damage);

                if (fireCmd.Count == 1)
                {
                    EntityUtil.CreateBulletEntity(_contexts,
                        playerView.Shoot.position,
                        fireCmd.Angle,
                        playerDamage);
                }
                else
                {
                    var shootPos = playerView.Shoot.position;
                    var playerPos = playerView.transform.position;

                    var count = fireCmd.Count;

                    var rightDir = Vector3
                        .Cross(fireCmd.Angle.Angle2Vector2D(), Vector3.forward)
                        .normalized;
                    var spacing =
                        gameEntity.GetStat(VarFlag.BulletSpace);
                    var startPos =
                        shootPos + rightDir * (count - 1) * spacing / 2f;

                    EntityUtil.CreateBulletEntity(_contexts,
                        startPos,
                        (startPos - playerPos).Vector2Angle2D(),
                        playerDamage);

                    for (var i = 1; i < count; i++)
                    {
                        var spawnPos = startPos - rightDir * (i * spacing);
                        EntityUtil.CreateBulletEntity(_contexts,
                            spawnPos,
                            (spawnPos - playerPos).Vector2Angle2D(),
                            playerDamage);
                    }
                }
            }
        }
    }
}