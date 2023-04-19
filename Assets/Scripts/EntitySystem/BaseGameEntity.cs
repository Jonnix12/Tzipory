using UnityEngine;

namespace Tzipory.EntitySystem
{
    public abstract class BaseGameEntity : MonoBehaviour , IEntityComponent
    {
        public int EntityInstanceID { get; private set; }
        public Transform EntityTransform { get; private set; }

        protected virtual void Awake()
        {
            EntityTransform = transform;
            EntityInstanceID = EntityIDGenerator.GetInstanceID();
        }
    }
    
}