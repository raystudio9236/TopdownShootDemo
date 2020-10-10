using UnityEngine;

namespace Item
{
    public class ItemData : ScriptableObject
    {
        public string ItemName;
        public string ItemDesc;

        public void PickUp(GameEntity entity)
        {
            OnPickUp(entity);
        }

        public void Remove(GameEntity entity)
        {
            OnRemove(entity);
        }

        protected virtual void OnPickUp(GameEntity entity)
        {
        }

        protected virtual void OnRemove(GameEntity entity)
        {
        }
    }
}