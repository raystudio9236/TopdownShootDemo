using System.Collections.Generic;
using Entitas;
using Events;
using Manager;

namespace Components.Stat
{
    public sealed class ChangeHpComp : IComponent
    {
        public bool Dirty;
        public List<float> ChangeList;
    }

    public static class ChangeHpCompEx
    {
        public static GameEntity ChangeHp(this GameEntity gameEntity,
            float changeValue)
        {
            if (!gameEntity.hasChangeHpComp)
                gameEntity.AddChangeHpComp(true, new List<float>());

            var list = gameEntity.changeHpComp.ChangeList;
            list.Add(changeValue);
            gameEntity.ReplaceChangeHpComp(true, list);

            GameManager.Send(GlobalEvent.ChangeHp, new ChangeHpData()
            {
                Target = gameEntity,
                ChangeValue = changeValue
            });

            return gameEntity;
        }
    }
}