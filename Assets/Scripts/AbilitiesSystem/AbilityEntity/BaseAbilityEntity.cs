using System.Collections.Generic;
using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Tzipory.AbilitiesSystem.AbilityEntity
{
    public abstract class BaseAbilityEntity : BaseGameEntity
    {
        [FormerlySerializedAs("_range")] [SerializeField] protected CircleCollider2D _collider2D;
        [SerializeField] protected Transform sprite;
        
        protected IEnumerable<BaseStatusEffect> statusEffect;

        protected virtual void Cast(params IEntityTargetAbleComponent[] targets)
        {
            foreach (var target in targets)
            {
                Debug.Log($"ExecuteAbility on {target.EntityTransform.name}");
                    
                foreach (var statusEffect in statusEffect)
                    target.StatusHandler.AddStatusEffect(statusEffect);
            }
        }
    }
}