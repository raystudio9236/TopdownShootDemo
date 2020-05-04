using Entitas;
using UnityEngine;

namespace Systems.Transform
{
    public class RotationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _group;
    
        public RotationSystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.RotComp,
                GameMatcher.ViewComp
            ));
        }
    
        public void Execute()
        {
            foreach (var gameEntity in _group.GetEntities())
            {
                var angle = gameEntity.rotComp.Angle;
                gameEntity.viewComp.View.transform.rotation 
                    = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}