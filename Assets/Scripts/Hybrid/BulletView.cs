public class BulletView : View
{
    protected override void OnDestroyEntityHandler()
    {
        PoolManager.Instance.BulletPrefabPool.Recycle(this);
    }
}
