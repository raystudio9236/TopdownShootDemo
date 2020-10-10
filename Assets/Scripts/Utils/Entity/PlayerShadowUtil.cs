using Other;
using UnityEngine;

namespace Utils.Entity
{
    public static class PlayerShadowUtil
    {
        public static GameEntity CreatePlayerShadowEntity(
            Contexts contexts,
            Vector2 pos,
            float angle = 0)
        {
            var playerShadowEntity = contexts.game.CreateEntity();

            playerShadowEntity.AddPosComp(pos);
            playerShadowEntity.AddVelComp(Vector2.zero);
            playerShadowEntity.AddRotComp(angle);
            playerShadowEntity.AddCreateGameObjCmdComp(ActorTag.PlayerShadow);

            playerShadowEntity.AddLifetimeComp(0.2f);

            return playerShadowEntity;
        }
    }
}