using Manager;
using Other;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "New Graph Item",
        menuName = "Item/AddGraphItem")]
    public class AddGraphItem : ItemData
    {
        public string ActionGraphName;

        protected override void OnPickUp(GameEntity entity)
        {
            ActionManager.Instance.AddGraph(entity, ActionGraphName);
        }

        protected override void OnRemove(GameEntity entity)
        {
            ActionManager.Instance.RemoveGraph(entity, ActionGraphName);
        }
    }
}