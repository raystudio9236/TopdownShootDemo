using UnityEngine;

public static class EntityUtil
{
    /// <summary>
    /// 创建 Player Entity
    /// </summary>
    /// <param name="contexts"></param>
    /// <param name="pos"></param>
    /// <param name="vel"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static GameEntity CreatePlayerEntity(
        Contexts contexts,
        Vector2 pos,
        Vector2 vel,
        float angle = 0)
    {
        var playerEntity = contexts.game.CreateEntity();
        playerEntity.isPlayerTag = true;
        playerEntity.isPhysicsTag = true;

        playerEntity.AddPosComp(pos);
        playerEntity.AddVelComp(vel);
        playerEntity.AddRotComp(angle);
        playerEntity.AddCreateGameObjCmdComp(ActorTag.Player);
        
        playerEntity.AddTimerComp(0f);

        return playerEntity;
    }
    
        public static GameEntity CreateEnemyEntity(
            Contexts contexts,
            Vector2 pos,
            Vector2 vel,
            float angle = 0)
        {
            var enemyEntity = contexts.game.CreateEntity();
            enemyEntity.isEnemyTag = true;
            enemyEntity.isPhysicsTag = true;

            enemyEntity.AddPosComp(pos);
            enemyEntity.AddVelComp(vel);
            enemyEntity.AddRotComp(angle);
            enemyEntity.AddCreateGameObjCmdComp(ActorTag.Enemy);
    
            return enemyEntity;
        }

    public static GameEntity CreateBulletEntity(Contexts contexts,
        Vector2 pos,
        Vector2 vel,
        float angle = 0)
    {
        var bulletEntity = contexts.game.CreateEntity();
        bulletEntity.isBulletTag = true;
        bulletEntity.isPhysicsTag = true;

        bulletEntity.AddPosComp(pos);
        bulletEntity.AddVelComp(vel);
        bulletEntity.AddRotComp(angle);
        bulletEntity.AddCreateGameObjCmdComp(ActorTag.Bullet);
        bulletEntity.AddLifetimeComp(1);

        return bulletEntity;
    }
}
