using System;
using System.Collections.Generic;
using Entitas;

namespace Components.Item
{
    [Serializable]
    public enum ChangeItemType
    {
        Add,
        Remove,
    }

    [Serializable]
    public struct ChangeItemPair
    {
        public ChangeItemType Type;
        public string ItemName;
    }

    public sealed class ChangeItemCmdComp : IComponent
    {
        public List<ChangeItemPair> ChangeItemList;
    }

    public static class ChangeItemCmdCompEx
    {
        public static void ChangeItem(this GameEntity entity,
            ChangeItemPair changeItemPair)
        {
            if (!entity.hasChangeItemCmdComp)
            {
                entity.AddChangeItemCmdComp(new List<ChangeItemPair>());
            }

            var list = entity.changeItemCmdComp.ChangeItemList;
            list.Add(changeItemPair);
            entity.ReplaceChangeItemCmdComp(list);
        }
    }
}