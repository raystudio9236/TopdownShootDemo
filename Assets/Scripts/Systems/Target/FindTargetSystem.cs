using System.Collections.Generic;
using Components.Target;
using Entitas;
using Manager;
using Other;

namespace Systems.Target
{
    public class FindTargetSystem : ReactiveSystem<GameEntity>
    {
        private List<GameEntity> _entities;
        private ActorTag _currentTag;

        public FindTargetSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(
            IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.FindTargetCmdComp);
        }

        protected override bool Filter(GameEntity entity)
        {
            return !entity.hasTargetComp && entity.hasFindTargetCmdComp;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _entities?.Clear();
            _currentTag = ActorTag.None;

            entities.Sort(Compare);

            foreach (var gameEntity in entities)
            {
                var findTargetComp = gameEntity.findTargetCmdComp;

                if (_entities == null
                    || _currentTag != findTargetComp.TargetTag)
                {
                    _entities = GameManager.GetEntities(
                        findTargetComp.TargetTag,
                        ref _entities);

                    _currentTag = findTargetComp.TargetTag;
                }

                var targetId = -1;

                if (findTargetComp.FindTargetType == FindTargetType.Closet)
                {
                    var selfPos = gameEntity.posComp.Value;
                    var closeDis = float.MaxValue;
                    foreach (var entity in _entities)
                    {
                        if (!entity.IsValid() || !entity.hasPosComp)
                            continue;

                        var dis = (entity.posComp.Value - selfPos).sqrMagnitude;
                        if (closeDis > dis)
                        {
                            closeDis = dis;
                            targetId = entity.idComp.Value;
                        }
                    }
                }

                if (targetId != -1)
                {
                    gameEntity.RemoveFindTargetCmdComp();
                    gameEntity.AddTargetComp(
                        targetId,
                        findTargetComp.TargetTag,
                        findTargetComp.FindTargetType,
                        findTargetComp.LostTargetActionType);
                }
            }
        }

        private int Compare(GameEntity a, GameEntity b)
        {
            return a.findTargetCmdComp.TargetTag.CompareTo(
                b.findTargetCmdComp.TargetTag);
        }
    }
}