using UnityEngine;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface IEntityInitializationComponent<in T> where T : ScriptableObject
    {
        public void Initialize(T config);
    }
}