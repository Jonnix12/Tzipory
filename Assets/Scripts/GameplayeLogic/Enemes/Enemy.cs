using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;
using UnityEngine;

namespace Enemes
{
    public class Enemy : BaseUnitEntity
    {
        //TEMP!
        public BasicMoveComponent BasicMoveComponent;

        float timer;

        protected override void Awake()
        {
            base.Awake();
            EntityTeamType = EntityTeamType.Enemy;
            timer = 0;
        }

        public void TakeTarget(IEntityTargetAbleComponent target)
        {
            SetAttackTarget(target);
        }

        public override void Attack()
        {
            base.Attack(); //empty
            if (timer >= StatusHandler.GetStatByName("AttackRate").CurrentValue)
            {
                timer = 0f;
                Target.TakeDamage(StatusHandler.GetStatByName("AttackDamage").CurrentValue, false);
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}