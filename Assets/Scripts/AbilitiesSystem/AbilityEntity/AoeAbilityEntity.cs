using System.Collections.Generic;
using Tzipory.AbilitiesSystem.AbilityEntity;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;

public class AoeAbilityEntity : BaseAbilityEntity
{
    private float _duration;

    public void Init(float radius, float duration, IEnumerable<BaseStatusEffect> statusEffect)
    {
        _collider2D.radius = radius;
        _collider2D.isTrigger = true;
        _duration = duration;
        base.statusEffect = statusEffect;

        sprite.localScale = Vector3.one * (radius * 10);

        var colliders = Physics2D.OverlapCircleAll(transform.position, _collider2D.radius);
        
        foreach (var collider in colliders)
        {
            if (collider.isTrigger)
                continue;

            if (collider.TryGetComponent(out IEntityTargetAbleComponent entityTargetAbleComponent))
                Cast(entityTargetAbleComponent);
        }
    }

    private void Update()
    {
        _duration -= Time.deltaTime;
        
        if(_duration <= 0)
            Destroy(gameObject);//temp need to add pool
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IEntityTargetAbleComponent entityTargetAbleComponent))
        {
            // if (entityCasterTargetingComponent.EntityTeamType == entityTargetAbleComponent.EntityTeamType)//temp!!! need to be able to activate status effect on friendly
            //     continue;

            Debug.Log($"Cast on {entityTargetAbleComponent.EntityTransform.name}");
                    
            foreach (var statusEffect in statusEffect)
                entityTargetAbleComponent.StatusHandler.AddStatusEffect(statusEffect);
        }
    }
}
