using System.Collections.Generic;
using Actions.Core;
using Components.Base;
using Components.Item;
using Components.Stat;
using Item;
using UnityEngine;

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
                damage: 50f);

            playerEntity.AddPosComp(pos);
            playerEntity.AddVelComp(Vector2.zero);
            playerEntity.AddRotComp(angle);
            playerEntity.AddCreateGameObjCmdComp(ActorTag.Player);

            playerEntity.AddTimer();

            playerEntity.AddActionComp(new List<ActionGraphHost>(3));
            playerEntity.AddItemComp(new List<ItemData>());

            playerEntity.AddItem("NormalFire", "Dash");

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
                damage: 30f);

            enemyEntity.AddPosComp(pos);
            enemyEntity.AddVelComp(Vector2.zero);
            enemyEntity.AddRotComp(angle);
            enemyEntity.AddCreateGameObjCmdComp(ActorTag.Enemy);

            return enemyEntity;
        }

        public static GameEntity CreateBulletEntity(Contexts contexts,
            Vector2 pos,
            float angle = 0,
            float damage = 50f)
        {
            var bulletEntity = contexts.game.CreateEntity();
            bulletEntity.isBulletTag = true;
            bulletEntity.isPhysicsTag = true;

            bulletEntity.AddStats(
                velocity: 15f,
                // attackSpeed: 0.5f,
                // bulletCount: 1,
                // bulletSpace: 0.2f,
                // Hp: 50f,
                // maxHp: 50f,
                damage: damage);

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

            coinEntity.AddStats(12f);

            coinEntity.AddPosComp(pos);
            coinEntity.AddVelComp(Vector2.zero);
            coinEntity.AddRotComp(angle);
            coinEntity.AddCreateGameObjCmdComp(ActorTag.Coin);
            coinEntity.AddCloseDestroyComp(0.2f);

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
            float damage = 0f)
        {
            var stats = new float[VarFlag.All.ToIdx()];
            stats[VarFlag.Velocity.ToIdx()] = velocity;
            stats[VarFlag.AttackSpeed.ToIdx()] = attackSpeed;
            stats[VarFlag.BulletCount.ToIdx()] = bulletCount;
            stats[VarFlag.BulletSpace.ToIdx()] = bulletSpace;
            stats[VarFlag.Hp.ToIdx()] = Hp;
            stats[VarFlag.MaxHp.ToIdx()] = maxHp;
            stats[VarFlag.Damage.ToIdx()] = damage;
            gameEntity.AddStatsComp(stats);

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