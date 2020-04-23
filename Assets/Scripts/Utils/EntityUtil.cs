using UnityEngine;

public static class EntityUtil
{
    /// <summary>
    /// 创建 Player Entity
    /// </summary>
    public static GameEntity CreatePlayerEntity(
        Contexts contexts,
        Vector2 pos,
        float angle = 0)
    {
        var playerEntity = contexts.game.CreateEntity();
        playerEntity.isPlayerTag = true;
        playerEntity.isPhysicsTag = true;

        AddStats(playerEntity, 8f);

        playerEntity.AddPosComp(pos);
        playerEntity.AddVelComp(Vector2.zero);
        playerEntity.AddRotComp(angle);
        playerEntity.AddCreateGameObjCmdComp(ActorTag.Player);

        playerEntity.AddTimerComp(0f);

        return playerEntity;
    }

    public static GameEntity CreateEnemyEntity(
        Contexts contexts,
        Vector2 pos,
        float angle = 0)
    {
        var enemyEntity = contexts.game.CreateEntity();
        enemyEntity.isEnemyTag = true;
        enemyEntity.isPhysicsTag = true;
        
        AddStats(enemyEntity, 2f);

        enemyEntity.AddPosComp(pos);
        enemyEntity.AddVelComp(Vector2.zero);
        enemyEntity.AddRotComp(angle);
        enemyEntity.AddCreateGameObjCmdComp(ActorTag.Enemy);

        return enemyEntity;
    }

    public static GameEntity CreateBulletEntity(Contexts contexts,
        Vector2 pos,
        float angle = 0)
    {
        var bulletEntity = contexts.game.CreateEntity();
        bulletEntity.isBulletTag = true;
        bulletEntity.isPhysicsTag = true;

        AddStats(bulletEntity, 12f);

        bulletEntity.AddPosComp(pos);
        bulletEntity.AddVelComp(angle.Angle2Vector2D() * 12f);
        bulletEntity.AddRotComp(angle);
        bulletEntity.AddCreateGameObjCmdComp(ActorTag.Bullet);
        bulletEntity.AddLifetimeComp(1);

        return bulletEntity;
    }
    
    public static GameEntity CreateCoinEntity(
        Contexts contexts,
        Vector2 pos,
        float angle = 0)
    {
        var coinEntity = contexts.game.CreateEntity();
        coinEntity.isCoinTag = true;
        coinEntity.isPhysicsTag = true;

        AddStats(coinEntity, 12f);

        coinEntity.AddPosComp(pos);
        coinEntity.AddVelComp(Vector2.zero);
        coinEntity.AddRotComp(angle);
        coinEntity.AddCreateGameObjCmdComp(ActorTag.Coin);
        coinEntity.AddCloseDestroyComp(0.2f);
        
        coinEntity.AddLifetimeComp(3f);

        return coinEntity;
    }

    private static GameEntity AddStats(GameEntity gameEntity,
        float velocity = 0f)
    {
        var stats = new float[VarFlag.All.ToIdx()];
        stats[VarFlag.Velocity.ToIdx()] = velocity;
        gameEntity.AddStatsComp(stats);

        return gameEntity;
    }
}