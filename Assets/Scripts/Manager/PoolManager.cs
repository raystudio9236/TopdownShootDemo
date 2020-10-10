using System.Collections.Generic;
using Hybrid.Base;
using Other;
using UnityEngine;

namespace Manager
{
    public class PoolManager : MonoBehaviour
    {
        private const string PATH_PREFIX = "Actors";
        
        public static PoolManager Instance;

        [SerializeField] private ActorTagPathDic ActorTagPathDic;

        private Dictionary<ActorTag, ViewPrefabPool> _viewPrefabPools
            = new Dictionary<ActorTag, ViewPrefabPool>();

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;
        }

        public View Spawn(ActorTag actorTag)
        {
            if (!_viewPrefabPools.TryGetValue(actorTag, out var pool))
            {
                pool = new ViewPrefabPool();
                pool.Prefab = Resources.Load<GameObject>($"{PATH_PREFIX}/{ActorTagPathDic[actorTag]}");
                pool.SpawnRoot = new GameObject($"__SpawnRoot_{actorTag.ToString()}")
                    .transform;
                pool.PoolRoot = new GameObject($"__PoolRoot_{actorTag.ToString()}")
                    .transform;

                pool.SpawnRoot.SetParent(transform);
                pool.SpawnRoot.localPosition = Vector3.zero;
                pool.SpawnRoot.localRotation = Quaternion.identity;
                pool.SpawnRoot.localScale = Vector3.one;

                pool.PoolRoot.SetParent(transform);
                pool.PoolRoot.localPosition = Vector3.zero;
                pool.PoolRoot.localRotation = Quaternion.identity;
                pool.PoolRoot.localScale = Vector3.one;

                _viewPrefabPools.Add(actorTag, pool);
            }

            return pool.Spawn();
        }


        public T Spawn<T>(ActorTag actorTag) where T : class, IView
        {
            return Spawn(actorTag) as T;
        }

        public bool Recycle(View view, ActorTag tag)
        {
            if (!_viewPrefabPools.TryGetValue(tag, out var pool))
            {
                Destroy(view.gameObject);
                return false;
            }

            pool.Recycle(view);
            return true;
        }
    }
}