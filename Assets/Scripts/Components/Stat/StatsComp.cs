using System;
using Entitas;
using Events;
using Utils.Event;

namespace Components.Stat
{
    [Serializable]
    public enum StatFlag : short
    {
        Velocity, // 速度

        AttackSpeed, // 攻击速度

        BulletCount, // 子弹数量
        BulletSpace, // 子弹之间的间隔

        Hp, // 血量
        MaxHp, // 最大血量

        Damage, // 伤害

        FollowStartTime, // 开始跟踪时间
        FollowRotMaxAngle, // 跟踪旋转最大角度

        All,
    }

    public sealed class StatsComp : IComponent
    {
        public float[] Vars;
        public EventDispatcher EventDispatcher;
    }

    public static class VarFlagExtension
    {
        public static short ToIdx(this StatFlag flag)
        {
            return (short) flag;
        }
    }

    public static class StatsCompEx
    {
        public static GameEntity SetStat(
            this GameEntity gameEntity,
            StatFlag statFlag,
            float value)
        {
            gameEntity.statsComp.Vars[statFlag.ToIdx()] = value;
            gameEntity.NotifyStatChange(statFlag, value);
            return gameEntity;
        }

        public static float GetStat(
            this GameEntity gameEntity,
            StatFlag statFlag)
        {
            return gameEntity.statsComp.Vars[statFlag.ToIdx()];
        }
    }
}