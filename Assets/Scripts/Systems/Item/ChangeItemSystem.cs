using System.Collections.Generic;
using Components.Item;
using Entitas;
using Manager;

namespace Systems.Item
{
    public class ChangeItemSystem : ReactiveSystem<GameEntity>
    {
        public ChangeItemSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(
            IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.ChangeItemCmdComp);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasItemComp;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var gameEntity in entities)
            {
                var changeList = gameEntity.changeItemCmdComp.ChangeItemList;
                var itemlist = gameEntity.itemComp.Items;
                var newIndex = itemlist.Count;

                foreach (var changeItemPair in changeList)
                {
                    switch (changeItemPair.Type)
                    {
                        case ChangeItemType.Add:
                            itemlist.Add(
                                ItemManager.Instance.GetItem(changeItemPair
                                    .ItemName));
                            break;

                        case ChangeItemType.Remove:
                            for (var i = itemlist.Count - 1; i >= 0; i--)
                            {
                                if (itemlist[i].ItemName ==
                                    changeItemPair.ItemName)
                                {
                                    if (i < newIndex)
                                    {
                                        newIndex--;
                                        itemlist[i].Remove(gameEntity);
                                    }

                                    itemlist.RemoveAt(i);
                                }
                            }

                            break;
                    }
                }

                for (var i = newIndex; i < itemlist.Count; i++)
                {
                    itemlist[i].PickUp(gameEntity);
                }
                
                changeList.Clear();
            }
        }
    }
}