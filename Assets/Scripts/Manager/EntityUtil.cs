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
        playerEntity.AddPosComp(pos);
        playerEntity.AddVelComp(vel);
        playerEntity.AddRotComp(angle);
        playerEntity.AddCreateGameObjCmdComp("Player");

        return playerEntity;
    }

    public static GameEntity CreateBulletEntity(Contexts contexts,
        Vector2 pos,
        Vector2 vel,
        float angle = 0)
    {
        var bulletEntity = contexts.game.CreateEntity();
        bulletEntity.AddPosComp(pos);
        bulletEntity.AddVelComp(vel);
        bulletEntity.AddRotComp(angle);
        bulletEntity.AddCreateGameObjCmdComp("Bullet");
        bulletEntity.AddLifetimeComp(1);

        return bulletEntity;
    }
}
