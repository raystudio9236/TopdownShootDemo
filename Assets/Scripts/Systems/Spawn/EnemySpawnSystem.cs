using System.Threading.Tasks;
using Components.Stat;
using Components.Target;
using Entitas;
using Other;
using UnityEngine;
using Utils;
using Utils.Entity;

namespace Systems.Spawn
{
    public class EnemySpawnSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private float _timer = 0f;

        public EnemySpawnSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public async void Execute()
        {
            var dt = Time.deltaTime;
            _timer += dt;

            if (_timer >= 1.5f)
            {
                var playerEntity = _contexts.game.playerTagEntity;

                _timer = 0;

                var x = Random.Range(-9f, 9f);
                var y = Random.Range(-5f, 5f);
                var pos = new Vector2(x, y) + playerEntity.posComp.Value;

                var enemyEntity = EnemyUtil.CreateEnemyEntity(_contexts,
                    new Vector2(x, y),
                    Random.Range(0, 360f));

                await Task.Delay(
                    (int) (enemyEntity.GetStat(StatFlag.FollowStartTime) *
                           1000));

                if (!enemyEntity.IsValid())
                    return;

                enemyEntity.AddTargetComp(
                    playerEntity.idComp.Value,
                    ActorTag.Player,
                    FindTargetType.Given,
                    LostTargetActionType.None);
            }
        }
    }
}