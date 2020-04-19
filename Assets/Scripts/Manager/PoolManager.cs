using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    
    public ViewPrefabPool BulletPrefabPool;
    public ViewPrefabPool EnemyPrefabPool;
    
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
        
        BulletPrefabPool.Preload(5);
        EnemyPrefabPool.Preload(5);
    }
}
