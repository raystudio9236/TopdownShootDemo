using UnityEngine;
using Utils;

namespace Item
{
    [CreateAssetMenu(fileName = "New Item",
        menuName = "Item/Test")]
    public class TestItem : ItemData
    {
        public float NewVelocity;
        
        protected override void OnPickUp()
        {
            CommandUtil.ChangePlayerVelocityTo(NewVelocity);
        }
    }
}