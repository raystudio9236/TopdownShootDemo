using System.Collections.Generic;
using Entitas;
using Hybrid.Base;
using Manager;

namespace Systems.Base
{
    public class AddViewSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;
    
        public AddViewSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }
    
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CreateGameObjCmdComp);
        }
    
        protected override bool Filter(GameEntity entity)
        {
            return true;
        }
    
        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var view = SpawnObj(entity);
                entity.AddViewComp(view);
                entity.RemoveCreateGameObjCmdComp();
            }
        }

        private View SpawnObj(GameEntity gameEntity)
        {
            var tag = gameEntity.createGameObjCmdComp.Tag;
            View view = PoolManager.Instance.Spawn(tag);
            view.Link(_contexts, gameEntity);
            return view;
        }
    }
}
