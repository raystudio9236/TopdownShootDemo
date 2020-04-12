using UnityEngine;

public class PlayerView : View
{
    public Transform Shoot;

    protected override void OnDestroyEntityHandler()
    {
        base.OnDestroyEntityHandler();
        
        Destroy(gameObject);
    }
}
