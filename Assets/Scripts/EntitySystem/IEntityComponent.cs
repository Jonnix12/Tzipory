using UnityEngine;

namespace Tzipory.EntitySystem
{
    public interface IEntityComponent
    {
        public int EntityInstanceID { get; }
        public Transform EntityTransform { get; }
    }
}