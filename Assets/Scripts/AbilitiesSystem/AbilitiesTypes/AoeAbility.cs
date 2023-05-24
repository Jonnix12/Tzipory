using Tzipory.AbilitiesSystem.AbilityConfigSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Tzipory.AbilitiesSystem
{
    public class AoeAbility : BaseAbility
    {
        public Stat Radius { get; private set; }
        public Stat Duration { get; private set; }


        public AoeAbility(IEntityTargetingComponent entityCasterTargetingComponent ,AbilityConfig config) : base(entityCasterTargetingComponent ,config)
        {
            if(AbilityParameter.TryGetValue("Radius", out Stat radius))
                Radius = radius;
            else
                throw new System.Exception($"{nameof(AoeAbility)} Radius not found");
            
            if(AbilityParameter.TryGetValue("Duration", out Stat duration))
                Duration = duration;
            else
                throw new System.Exception($"{nameof(AoeAbility)} Duration not found");
        }

        protected override void Cast(IEntityTargetAbleComponent target)
        {
            var colliders = Physics2D.OverlapCircleAll(target.EntityTransform.position, Radius.CurrentValue);
            foreach (var collider in colliders)
            {
                if(collider.isTrigger)
                    continue;

                if (collider.TryGetComponent(out IEntityTargetAbleComponent entityTargetAbleComponent))
                {
                    if (entityCasterTargetingComponent.EntityTeamType == entityTargetAbleComponent.EntityTeamType)//temp!!! need to be able to activate status effect on friendly
                        continue;
#if UNITY_EDITOR
                    //UnityEditor.Handles.DrawSolidDisc(target.EntityTransform.position,-Vector3.forward, Radius.CurrentValue);
#endif
                    Debug.Log($"Cast on {entityTargetAbleComponent.EntityTransform.name}");
                    
                    foreach (var statusEffect in StatusEffects)
                        entityTargetAbleComponent.StatusHandler.AddStatusEffect(statusEffect);
                }
            }
        }
    }
    
    #if UNITY_EDITOR

    internal class AOEEditor : Editor
    {
        
    }
    
    #endif
}