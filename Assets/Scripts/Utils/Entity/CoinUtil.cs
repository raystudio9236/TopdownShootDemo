using Other;
using UnityEngine;

namespace Utils.Entity
{
    public static class CoinUtil
    {
        public static GameEntity CreateCoinEntity(
            Contexts contexts,
            Vector2 pos,
            float angle = 0)
        {
            var coinEntity = contexts.game.CreateEntity();
            coinEntity.isCoinTag = true;
            coinEntity.isPhysicsTag = true;

            coinEntity.AddStats(
                velocity: 16f,
                followStartTime: 0f,
                followRotMaxAngle: 900f);

            coinEntity.AddPosComp(pos);
            coinEntity.AddVelComp(Vector2.zero);
            coinEntity.AddRotComp(angle);
            coinEntity.AddCreateGameObjCmdComp(ActorTag.Coin);
            coinEntity.AddCloseDestroyComp(0.4f);

            coinEntity.AddLifetimeComp(3f);

            return coinEntity;
        }
    }
}