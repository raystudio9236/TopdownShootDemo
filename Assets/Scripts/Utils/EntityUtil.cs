using System.Collections.Generic;
using Actions.Core;
using Components.Base;
using Components.Item;
using Components.Stat;
using Events;
using Item;
using Manager;
using Other;
using UnityEngine;
using Utils.Event;

namespace Utils
{
    public static class EntityUtil
    {
        /// <summary>
        /// 创建player
        /// </summary>
        public static GameEntity CreatePlayerEntity(
            Contexts contexts,
            Vector2 pos,
            float angle = 0)
        {
            var playerEntity = contexts.game.CreateEntity();
            playerEntity.isPlayerTag = true;
            playerEntity.isPhysicsTag = true;

            playerEntity.AddStats(
                velocity: 8f,
                attackSpeed: 1.5f,
                bulletCount: 1,
                bulletSpace: 0.2f,
                Hp: 100f,
                maxHp: 100f,
                damage: 50f,
                followStartTime: 0.2f,
                followRotMaxAngle: 180f);

            playerEntity.AddPosComp(pos);
            playerEntity.AddVelComp(Vector2.zero);
            playerEntity.AddRotComp(angle);
            playerEntity.AddCreateGameObjCmdComp(ActorTag.Player);

            playerEntity.AddTimer();

            playerEntity.AddActionComp(new List<ActionGraphHost>(3));
            playerEntity.AddItemComp(new List<ItemData>());

            playerEntity.AddItem("NormalFire", "Dash");

            playerEntity.AddEventComp(new EventDispatcher());

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

        private static GameEntity AddStats(this GameEntity gameEntity,
            float velocity = 0f,
            float attackSpeed = 1f,
            float bulletCount = 1f,
            float bulletSpace = 0.2f,
            float Hp = 1f,
            float maxHp = 1f,
            float damage = 0f,
            float followStartTime = 0f,
            float followRotMaxAngle = 360f)
        {
            var stats = new float[StatFlag.All.ToIdx()];
            stats[StatFlag.Velocity.ToIdx()] = velocity;
            stats[StatFlag.AttackSpeed.ToIdx()] = attackSpeed;
            stats[StatFlag.BulletCount.ToIdx()] = bulletCount;
            stats[StatFlag.BulletSpace.ToIdx()] = bulletSpace;
            stats[StatFlag.Hp.ToIdx()] = Hp;
            stats[StatFlag.MaxHp.ToIdx()] = maxHp;
            stats[StatFlag.Damage.ToIdx()] = damage;
            stats[StatFlag.FollowStartTime.ToIdx()] = followStartTime;
            stats[StatFlag.FollowRotMaxAngle.ToIdx()] = followRotMaxAngle;
            gameEntity.AddStatsComp(stats, new EventDispatcher());

            return gameEntity;
        }

        private static GameEntity AddTimer(this GameEntity gameEntity)
        {
            gameEntity.AddTimerComp(new float[TimerFlag.All.ToIdx()]);
            return gameEntity;
        }

        private static GameEntity AddItem(this GameEntity gameEntity,
            params string[] itemNames)
        {
            foreach (var itemName in itemNames)
            {
                gameEntity.ChangeItem(new ChangeItemPair
                {
                    ItemName = itemName,
                    Type = ChangeItemType.Add
                });
            }

            return gameEntity;
        }
    }
}