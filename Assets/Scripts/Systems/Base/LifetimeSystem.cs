using Entitas;
using UnityEngine;

namespace Systems.Base
{
    public class LifetimeSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _group;
    
        public LifetimeSystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.LifetimeComp);
        }
    
        public void Execute()
        {
            var dt = Time.deltaTime;
        
            foreach (var gameEntity in _group.GetEntities())
            {
                var newTime = gameEntity.lifetimeComp.Time - dt;
                if (newTime <= 0)
                    gameEntity.isDestroyFlag = true;
                else
                    gameEntity.ReplaceLifetimeComp(newTime);
            }
        }
    }
}
