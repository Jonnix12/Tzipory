using System;
using Tzipory.BaseSystem.TimeSystem;
using UnityEngine;

namespace Tzipory.EntitySystem
{
    public abstract class BaseGameEntity : MonoBehaviour , IEntityComponent
    {
        public int EntityInstanceID { get; private set; }
        public Transform EntityTransform { get; private set; }
        public TimerHandler EntityTimer { get; private set; }
        public BaseGameEntity GameEntity => this;

        protected virtual void Awake()
        {
            EntityTimer = new TimerHandler();
            EntityTransform = transform;
            EntityInstanceID = EntityIDGenerator.GetInstanceID();
        }

        protected virtual void Update()
        {
            EntityTimer.TickAllTimers();
        }

        protected  virtual void OnDestroy()
        {
            
        }
    }
    
}