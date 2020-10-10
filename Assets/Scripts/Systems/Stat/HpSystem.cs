using System.Collections.Generic;
using Components.Stat;
using Entitas;
using UnityEngine;

namespace Systems.Stat
{
    public class HpSystem : ReactiveSystem<GameEntity>
    {
        public HpSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(
            IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ChangeHpComp);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasStatsComp;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var gameEntity in entities)
            {
                var changeHpList = gameEntity.changeHpComp.ChangeList;

                var currentValue = gameEntity.GetStat(StatFlag.Hp);
                var maxValue = gameEntity.GetStat(StatFlag.MaxHp);

                foreach (var f in changeHpList)
                    currentValue += f;

                currentValue = Mathf.Clamp(currentValue, 0, maxValue);

                if (currentValue <= 0)
                    gameEntity.isDestroyFlag = true;
                else
                    gameEntity.SetStat(StatFlag.Hp, currentValue);

                changeHpList.Clear();
            }
        }
    }
}