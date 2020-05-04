using System.Collections.Generic;
using Entitas;

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

            return gameEntity;
        }
    }
}