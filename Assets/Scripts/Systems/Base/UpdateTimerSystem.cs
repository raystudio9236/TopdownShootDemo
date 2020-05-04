using Components.Base;
using Entitas;
using UnityEngine;

namespace Systems.Base
{
    public class UpdateTimerSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _group;

        public UpdateTimerSystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.TimerComp);
        }

        public void Execute()
        {
            var dt = Time.deltaTime;
            var timerCnt = TimerFlag.All.ToIdx();

            foreach (var gameEntity in _group.GetEntities())
            {
                var timerArr = gameEntity.timerComp.Timers;
                for (var i = 0; i < timerCnt; i++)
                {
                    var newTime = Mathf.Max(
                        timerArr[i] - dt,
                        0);
                    timerArr[i] = newTime;
                }
            }
        }
    }
}