using Components.Base;
using Components.Item;
using Components.Stat;
using Utils.Event;

namespace Utils.Entity
{
    public static class EntityUtil
    {
        public static GameEntity AddStats(this GameEntity gameEntity,
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

        public static GameEntity AddTimer(this GameEntity gameEntity)
        {
            gameEntity.AddTimerComp(new float[TimerFlag.All.ToIdx()]);
            return gameEntity;
        }

        public static GameEntity AddItem(this GameEntity gameEntity,
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