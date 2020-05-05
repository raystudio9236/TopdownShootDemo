using Hybrid.Base;

namespace Hybrid
{
    public class CoinView : PhysicsView
    {
        protected override void OnLinkEntityHandler()
        {
            Collider.enabled = true;
        }
    }
}