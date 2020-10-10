using Events;
using Manager;
using Other;
using UnityEngine;

namespace Utils.Entity
{
    public static class BulletUtil
    {
        public static GameEntity CreateBulletEntity(
            Contexts contexts,
            GameEntity host,
            Vector2 pos,
            float angle = 0,
            float damage = 50f,
            float followStartTime = 0f,
            float followRotMaxAngle = 360f)
        {
            var bulletEntity = contexts.game.CreateEntity();
            bulletEntity.isBulletTag = true;
            bulletEntity.isPhysicsTag = true;

            bulletEntity.AddStats(
                velocity: 18f,
                // attackSpeed: 0.5f,
                // bulletCount: 1,
                // bulletSpace: 0.2f,
                // Hp: 50f,
                // maxHp: 50f,
                damage: damage,
                followStartTime: followStartTime,
                followRotMaxAngle: followRotMaxAngle);

            bulletEntity.AddPosComp(pos);
            bulletEntity.AddVelComp(angle.Angle2Vector2D() * 12f);
            bulletEntity.AddRotComp(angle);
            bulletEntity.AddCreateGameObjCmdComp(ActorTag.Bullet);
            bulletEntity.AddLifetimeComp(1);

            GameManager.Send(GlobalEvent.SpawnBullet, new SpawnBulletData
            {
                Host = host,
                Bullet = bulletEntity
            });

            if (host.hasEventComp)
            {
                host.eventComp.Dispatcher.Send(EntityEvent.SpawnBullet,
                    new EntitySpawnBulletData
                    {
                        Host = host,
                        Bullet = bulletEntity
                    });
            }

            return bulletEntity;
        }
    }
}