using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.Pool
{
    [Serializable]
    public class PrefabPool
    {
        public Transform SpawnRoot;
        public Transform PoolRoot;

        public GameObject Prefab;

        protected readonly Stack<GameObject> _inPoolObjs = new Stack<GameObject>();
        protected readonly HashSet<GameObject> _outPoolObjs = new HashSet<GameObject>();

        /// <summary>
        /// 预加载
        /// </summary>
        public void Preload(int count)
        {
            if (Prefab == null)
                return;

            if (_inPoolObjs.Count + _outPoolObjs.Count >= count)
                return;

            var n = count - _inPoolObjs.Count - _outPoolObjs.Count;
            for (var i = 0; i < n; i++)
            {
                var obj = Object.Instantiate(Prefab,
                    Vector3.zero,
                    Quaternion.identity);

                obj.SetActive(false);
                _inPoolObjs.Push(obj);
            }
        }

        /// <summary>
        /// 生成对象
        /// </summary>
        public GameObject Spawn()
        {
            GameObject obj;

            if (_inPoolObjs.Count > 0)
            {
                obj = _inPoolObjs.Pop();
            }
            else
            {
                obj = Object.Instantiate(Prefab,
                    Vector3.zero,
                    Quaternion.identity);
            }

            _outPoolObjs.Add(obj);
            obj.SetActive(true);
            obj.transform.SetParent(SpawnRoot);
            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public bool Recycle(GameObject obj)
        {
            if (!_outPoolObjs.Contains(obj))
            {
                Object.Destroy(obj);
                return false;
            }

            _outPoolObjs.Remove(obj);
            obj.SetActive(false);
            obj.transform.SetParent(PoolRoot);
            _inPoolObjs.Push(obj);

            return true;
        }
    }

    [Serializable]
    public class PrefabPool<T> where T : Component
    {
        public Transform SpawnRoot;
        public Transform PoolRoot;

        public GameObject Prefab;

        protected readonly Stack<T> _inPoolObjs = new Stack<T>();
        protected readonly HashSet<T> _outPoolObjs = new HashSet<T>();

        /// <summary>
        /// 预加载
        /// </summary>
        public void Preload(int count)
        {
            if (Prefab == null)
                return;

            if (_inPoolObjs.Count + _outPoolObjs.Count >= count)
                return;

            var n = count - _inPoolObjs.Count - _outPoolObjs.Count;
            for (var i = 0; i < n; i++)
            {
                var obj = Object.Instantiate(Prefab,
                    Vector3.zero,
                    Quaternion.identity);

                obj.SetActive(false);
                obj.transform.SetParent(PoolRoot);
                _inPoolObjs.Push(obj.GetComponent<T>());
            }
        }

        /// <summary>
        /// 生成对象
        /// </summary>
        public T Spawn()
        {
            T obj;

            if (_inPoolObjs.Count > 0)
            {
                obj = _inPoolObjs.Pop();
            }
            else
            {
                obj = Object.Instantiate(Prefab,
                    Vector3.zero,
                    Quaternion.identity).GetComponent<T>();
            }

            _outPoolObjs.Add(obj);
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(SpawnRoot);
            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public bool Recycle(T comp)
        {
            if (!_outPoolObjs.Contains(comp))
            {
                Object.Destroy(comp.gameObject);
                return false;
            }

            _outPoolObjs.Remove(comp);
            comp.gameObject.SetActive(false);
            comp.transform.SetParent(PoolRoot);
            _inPoolObjs.Push(comp);

            return true;
        }
    }
}