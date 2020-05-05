using System.Threading.Tasks;
using Components.Target;
using Events;
using Other;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "BulletFindTargetItem",
        menuName = "Item/BulletFindTargetItem")]
    public class BulletFindTargetItem : ItemData
    {
        protected override void OnPickUp(GameEntity entity)
        {
            entity.eventComp.Dispatcher.AddHandler<EntitySpawnBulletData>(
                EntityEvent.SpawnBullet, OnEntitySpawnBulletHandler);
        }

        protected override void OnRemove(GameEntity entity)
        {
            entity.eventComp.Dispatcher.RemoveHandler<EntitySpawnBulletData>(
                EntityEvent.SpawnBullet, OnEntitySpawnBulletHandler);
        }

        private void OnEntitySpawnBulletHandler(short eventtype,
            EntitySpawnBulletData data)
        {
            if (data.Host.isPlayerTag)
            {
                data.Bullet.AddFindTargetCmdComp(
                    ActorTag.Enemy,
                    FindTargetType.Closet,
                    LostTargetActionType.Keep);
            }
        }
    }
}