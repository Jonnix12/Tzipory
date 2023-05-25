using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using UnityEngine;

namespace Tzipory.AbilitiesSystem.AbilityEntity
{
    public class ProjectileAbilityEntity : BaseAbilityEntity
    {
        private float _penetrationNumber;
        private float _speed;
        
        public void Init(float speed, float penetrationNumber,IEnumerable<BaseStatusEffect> statusEffect)
        {
            _speed = speed;
            _penetrationNumber = penetrationNumber;
            base.statusEffect  = statusEffect;
        }

        private void Update()
        {
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));

            if (_penetrationNumber <= 0)
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEntityTargetAbleComponent entityTargetAbleComponent))
            {
                _penetrationNumber--;
                Cast(entityTargetAbleComponent);
            }
        }
    }
}