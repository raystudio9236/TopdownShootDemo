using System.Collections.Generic;
using Actions.Core;
using Item;
using Other;
using UnityEngine;
using Utils.Event;

namespace Utils.Entity
{
    public static class PlayerUtil
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
    }
}