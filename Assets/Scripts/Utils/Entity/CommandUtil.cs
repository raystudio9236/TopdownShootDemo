using Components.Stat;
using Manager;

namespace Utils.Entity
{
    public static class CommandUtil
    {
        /// <summary>
        /// 改变玩家的移送速度
        /// </summary>
        public static void ChangePlayerVelocity(float offset)
        {
            ChangeEntityVelocity(GameManager.Contexts.game.playerTagEntity,
                offset);
        }

        /// <summary>
        /// 改变玩家的移送速度到新的速度值
        /// </summary>
        public static void ChangePlayerVelocityTo(float newVelocity)
        {
            ChangeEntityVelocityTo(GameManager.Contexts.game.playerTagEntity,
                newVelocity);
        }

        /// <summary>
        /// 改变Entity的移送速度
        /// </summary>
        public static void ChangeEntityVelocity(GameEntity entity,
            float offset)
        {
            if (entity.hasStatsComp)
            {
                var newValue = entity.GetStat(StatFlag.Velocity) +
                               offset;
                newValue = newValue < 0 ? 0 : newValue;
                entity.SetStat(StatFlag.Velocity, newValue);
            }
        }

        /// <summary>
        /// 改变Entity的移送速度到新的速度值
        /// </summary>
        public static void ChangeEntityVelocityTo(GameEntity entity,
            float newVelocity)
        {
            if (newVelocity < 0)
                return;

            if (entity.hasStatsComp)
            {
                entity.SetStat(StatFlag.Velocity, newVelocity);
            }
        }
    }
}