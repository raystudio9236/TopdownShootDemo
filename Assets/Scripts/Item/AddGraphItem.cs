using Manager;
using Other;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "AddGraphItem",
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