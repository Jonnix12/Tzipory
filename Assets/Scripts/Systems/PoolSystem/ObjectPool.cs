using System;
using System.Collections.Generic;

namespace Tzipory.Systems.PoolSystem
{
    public abstract class BaseObjectPool<T>
    {
        protected Action<T> OnGet;//may need to make readOnly
        protected Action<T> OnReturn;//may need to make readOnly
        
        protected readonly bool IsDynamic;
        protected readonly int MaxPoolSize;
        protected readonly int MinPoolSize;
        private readonly Queue<T> _objectPool;
        private readonly List<T> _aliveObjects;

        protected int NumberOfObjectInPool => _objectPool.Count;
        
        private BaseObjectPool(Action<T> onGet, Action<T> onReturn)
        {
            OnGet = onGet;
            OnReturn = onReturn;
            _aliveObjects  = new List<T>();
            _objectPool = new Queue<T>();
            
            OnGet += AddObject;
            OnReturn += RemoveObject;
        }

        protected BaseObjectPool(Action<T> onGet, Action<T> onReturn, int maxPoolSize, int minPoolSize) : this(onGet,onReturn)
        {
            IsDynamic = true;
            MaxPoolSize = maxPoolSize;
            MinPoolSize = minPoolSize;
        }
        
        protected BaseObjectPool(Action<T> onGet, Action<T> onReturn, bool isDynamic,int initialPool) : this(onGet,onReturn)
        {
            IsDynamic = isDynamic;
            MaxPoolSize = int.MaxValue;
            MinPoolSize = 0;
        }
        
        protected BaseObjectPool(Action<T> onGet, Action<T> onReturn,int minPoolSize,int initialPool,int maxPoolSize) : this(onGet,onReturn)
        {
            IsDynamic = true;
            MaxPoolSize = maxPoolSize;
            MinPoolSize = minPoolSize;
        }

        protected bool TryGetObjectFromPool(out T t)
        {
            if (_objectPool.Count > 0)
            {
                t = _objectPool.Dequeue();
                return true;
            }

            t = default;
            return false;
        }
        
        private void AddObject(T t) => _aliveObjects.Add(t);
        private void RemoveObject(T t) => _aliveObjects.Remove(t);

        ~BaseObjectPool()
        {
            OnGet -= AddObject;
            OnReturn -= RemoveObject;
        }
    }
    
    public class ObjectPool<T> : BaseObjectPool<T> where T : class, IPoolable<T>
    {
        public ObjectPool(Action<T> onGet, Action<T> onReturn, int maxPoolSize, int minPoolSize) : base(onGet, onReturn, maxPoolSize, minPoolSize)
        {
        }

        public ObjectPool(Action<T> onGet, Action<T> onReturn, bool isDynamic, int initialPool) : base(onGet, onReturn, isDynamic, initialPool)
        {
        }

        public ObjectPool(Action<T> onGet, Action<T> onReturn, int minPoolSize, int initialPool, int maxPoolSize) : base(onGet, onReturn, minPoolSize, initialPool, maxPoolSize)
        {
        }
    }

    public class ObjectPool<T1, T2> : BaseObjectPool<T1> where T1 : class, IPoolable<T1,T2>
    {
        public ObjectPool(Action<T1> onGet, Action<T1> onReturn, int maxPoolSize, int minPoolSize) : base(onGet, onReturn, maxPoolSize, minPoolSize)
        {
        }

        public ObjectPool(Action<T1> onGet, Action<T1> onReturn, bool isDynamic, int initialPool) : base(onGet, onReturn, isDynamic, initialPool)
        {
        }

        public ObjectPool(Action<T1> onGet, Action<T1> onReturn, int minPoolSize, int initialPool, int maxPoolSize) : base(onGet, onReturn, minPoolSize, initialPool, maxPoolSize)
        {
        }
        
        private Func<T2, T1> _factoryMethod;

        public T1 GetObject(T2 t2)
        {
            if (TryGetObjectFromPool(out var t))
            {
                t.Reset(t2);
                OnGet?.Invoke(t);
                return t;
            }

            if (IsDynamic && NumberOfObjectInPool < MaxPoolSize)
            {
                var newObject = _factoryMethod(t2);
                OnGet?.Invoke(newObject);
                return newObject;
            }
            
            return null;
        }

        public void Return(T1 t1)
        {
            OnReturn?.Invoke(t1);
        }
    }
}