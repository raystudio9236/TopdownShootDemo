using System.Collections.Generic;
using Components.Stat;
using Entitas;
using Hybrid;
using Other;
using UnityEngine;
using Utils;
using Utils.Entity;

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
                var playerDamage = gameEntity.GetStat(StatFlag.Damage);

                if (fireCmd.Count == 1)
                {
                    BulletUtil.CreateBulletEntity(
                        _contexts,
                        gameEntity,
                        playerView.Shoot.position,
                        fireCmd.Angle,
                        playerDamage,
                        gameEntity.GetStat(StatFlag.FollowStartTime),
                        gameEntity.GetStat(StatFlag.FollowRotMaxAngle));
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
                        gameEntity.GetStat(StatFlag.BulletSpace);
                    var startPos =
                        shootPos + rightDir * (count - 1) * spacing / 2f;

                    for (var i = 0; i < count; i++)
                    {
                        var spawnPos = startPos - rightDir * (i * spacing);
                        BulletUtil.CreateBulletEntity(
                            _contexts,
                            gameEntity,
                            spawnPos,
                            (spawnPos - playerPos).Vector2Angle2D(),
                            playerDamage,
                            gameEntity.GetStat(StatFlag.FollowStartTime),
                            gameEntity.GetStat(StatFlag.FollowRotMaxAngle));
                    }
                }
            }
        }
    }
}