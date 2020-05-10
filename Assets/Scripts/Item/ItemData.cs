using UnityEngine;

namespace Item
{
    public class ItemData : ScriptableObject
    {
        public string ItemName;
        public string ItemDesc;

        public void PickUp()
        {
            OnPickUp();
        }

        protected virtual void OnPickUp()
        {
        }
    }
}