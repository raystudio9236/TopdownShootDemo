using Hybrid.Base;
using Utils;
using Utils.Entity;

namespace Hybrid
{
    public class EnemyView : PhysicsView
    {
        protected override void OnDestroyEntityHandler()
        {
            EnemyUtil.CreateCoin(_selfEntity);
            base.OnDestroyEntityHandler();
        }
    }
}