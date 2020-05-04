using System.Collections.Generic;
using Entitas;
using Item;

namespace Components.Item
{
    public sealed class ItemComp : IComponent
    {
        public List<ItemData> Items;
    }
}