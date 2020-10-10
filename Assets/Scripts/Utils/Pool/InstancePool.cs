using System;
using System.Collections.Generic;

namespace Utils.Pool
{
    public interface IPoolItem
    {
        void AwakeFromPool();
        void RecycleToPool();
    }

    public class InstancePool<T> where T : IPoolItem, new()
    {
        private readonly Stack<T> _instances;

        public InstancePool(int preSize = 0)
        {
            _instances = new Stack<T>();
            for (var i = 0; i < preSize; i++)
            {
                var instance = default(T);
                _instances.Push(instance == null ? new T() : instance);
            }
        }

        public T Get()
        {
            T instance;
            if (IsEmpty())
            {
                instance = default;
                instance = instance == null
                    ? OnGetCreateNewInstance()
                    : instance;
            }
            else
            {
                instance = _instances.Pop();
            }

            instance.AwakeFromPool();
            return instance;
        }

        public void Recycle(T instance)
        {
            if (!_instances.Contains(instance))
            {
                instance.RecycleToPool();
                _instances.Push(instance);
            }
        }

        private bool IsEmpty()
        {
            return _instances.Count == 0;
        }

        protected virtual T OnGetCreateNewInstance()
        {
            return new T();
        }
    }

    public class InstancePoolWithSubType<T> : InstancePool<T>
        where T : class, IPoolItem, new()
    {
        private Type _subType;

        public bool SetSubType(Type type)
        {
            var parentT = typeof(T);
            if (type.IsAssignableFrom(parentT) || type.IsSubclassOf(parentT))
            {
                _subType = type;
                return true;
            }

            return false;
        }

        protected override T OnGetCreateNewInstance()
        {
            return Activator.CreateInstance(_subType) as T;
        }
    }
}