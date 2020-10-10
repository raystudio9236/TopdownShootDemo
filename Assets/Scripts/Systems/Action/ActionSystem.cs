using Entitas;

namespace Systems.Action
{
    public class ActionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _group;
    
        public ActionSystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.ActionComp);
        }

        public void Execute()
        {
            foreach (var gameEntity in _group.GetEntities())
            {
                foreach (var graphHost in gameEntity.actionComp.ActionGraphHostArr)
                {
                    graphHost.Execute();
                }
            }
        }
    }
}
