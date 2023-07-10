using System;
using UnityEngine;

namespace Tzipory.Systems.PoolSystem
{
    public abstract class MonoPoolable<T> : MonoBehaviour , IPoolable<T>
    {
        public event Action<T> OnDispose;
        public abstract void Dispose();
        public abstract void Reset();
        public abstract void Free();
    }
    
    public abstract class MonoPoolable<T1,T2> : MonoBehaviour , IPoolable<T1,T2>
    {
        public event Action<T1> OnDispose;
        public abstract void Dispose();
        public abstract void Reset(T2 t);

        public abstract void Free();
    }
    
    public abstract class MonoPoolable<T1,T2,T3> : MonoBehaviour , IPoolable<T1,T2,T3>
    {
        public event Action<T1> OnDispose;
        public abstract void Dispose();
        public abstract void Reset(T2 t1, T3 t2);

        public abstract void Free();
    }
    
    public abstract class MonoPoolable<T1,T2,T3,T4> : MonoBehaviour , IPoolable<T1,T2,T3,T4>
    {
        public event Action<T1> OnDispose;
        public abstract void Dispose();
        public abstract void Reset(T2 t1, T3 t2, T4 t3);

        public abstract void Free();
    }
}