using Manager;
using Other;
using UnityEngine;

namespace Utils.Entity
{
    public static class EnemyUtil
    {
        public static GameEntity CreateEnemyEntity(
            Contexts contexts,
            Vector2 pos,
            float angle = 0)
        {
            var enemyEntity = contexts.game.CreateEntity();
            enemyEntity.isEnemyTag = true;
            enemyEntity.isPhysicsTag = true;

            enemyEntity.AddStats(
                velocity: 2f,
                attackSpeed: 0.5f,
                bulletCount: 1,
                bulletSpace: 0.2f,
                Hp: 50f,
                maxHp: 50f,
                damage: 30f,
                followStartTime: 0.1f,
                followRotMaxAngle: 80f);

            enemyEntity.AddPosComp(pos);
            enemyEntity.AddVelComp(Vector2.zero);
            enemyEntity.AddRotComp(angle);
            enemyEntity.AddCreateGameObjCmdComp(ActorTag.Enemy);

            return enemyEntity;
        }

        public static void CreateCoin(GameEntity entity)
        {
            int count = Random.Range(3, 5);
            var pos = entity.posComp.Value;

            for (var i = 0; i < count; i++)
            {
                var x = Random.Range(-1f, 1f);
                var y = Random.Range(-1f, 1f);

                CoinUtil.CreateCoinEntity(GameManager.Contexts,
                    pos + new Vector2(x, y),
                    Random.Range(0, 360));
            }
        }
    }
}