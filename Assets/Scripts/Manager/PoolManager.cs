using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    
    public ViewPrefabPool BulletPrefabPool;
    
    private void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance.gameObject);

        Instance = this;
        
        BulletPrefabPool.Preload(5);
    }
}
