using Components.Stat;
using UnityEngine;

namespace Events
{
    public class StatEvent : EventEnumBase
    {
        public static readonly short Change = AutoIndex;
    }

    public struct StatChangeData
    {
        public StatFlag StatFlag;
        public float Value;
    }

    public static class StatEventEx
    {
        public static GameEntity NotifyStatChange(
            this GameEntity gameEntity,
            StatFlag statFlag,
            float value)
        {
#if DEBUG

            if (!gameEntity.hasStatsComp)
            {
                Debug.LogError($"通知Stat变化失败，Entity 没有Stats组件");
                return gameEntity;
            }

#endif

            gameEntity.statsComp.EventDispatcher.Send(StatEvent.Change,
                new StatChangeData
                {
                    StatFlag = statFlag,
                    Value = value
                });

            return gameEntity;
        }

        public static GameEntity AddStatHandler(
            this GameEntity gameEntity,
            Utils.Event.EventHandler<StatChangeData> handler)
        {
#if DEBUG

            if (!gameEntity.hasStatsComp)
            {
                Debug.LogError($"添加Stat事件回调失败，Entity 没有Stats组件");
                return gameEntity;
            }

#endif

            gameEntity.statsComp.EventDispatcher.AddHandler(
                StatEvent.Change,
                handler);
            return gameEntity;
        }

        public static GameEntity RemoveStatHandler(
            this GameEntity gameEntity,
            Utils.Event.EventHandler<StatChangeData> handler)
        {
#if DEBUG

            if (!gameEntity.hasStatsComp)
            {
                Debug.LogError($"移除Stat事件回调失败，Entity 没有Stats组件");
                return gameEntity;
            }

#endif

            gameEntity.statsComp.EventDispatcher.RemoveHandler(
                StatEvent.Change,
                handler);
            return gameEntity;
        }
    }
}