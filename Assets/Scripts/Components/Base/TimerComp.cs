using System;
using Components.Stat;
using Entitas;

namespace Components.Base
{
    [Serializable]
    public enum TimerFlag : short
    {
        Fire,
        All,
    }

    public static class TimerFlagEx
    {
        public static short ToIdx(this TimerFlag flag)
        {
            return (short) flag;
        }
    }

    public sealed class TimerComp : IComponent
    {
        public float[] Timers;
    }

    public static class TimerCompEx
    {
        public static void ResetTimer(this GameEntity gameEntity,
            TimerFlag flag,
            float value = -1f)
        {
            if (value < 0)
            {
                switch (flag)
                {
                    case TimerFlag.Fire:
                        value = 1.0f / gameEntity.GetStat(StatFlag.AttackSpeed);
                        break;

                    default:
                        value = 0f;
                        break;
                }
            }

            gameEntity.timerComp.Timers[flag.ToIdx()] = value;
        }

        public static bool CheckTimer(this GameEntity gameEntity, TimerFlag flag)
        {
            if (!gameEntity.hasTimerComp)
                return false;

            if (gameEntity.timerComp.Timers[flag.ToIdx()] <= 0)
                return true;

            return false;
        }
    }
}