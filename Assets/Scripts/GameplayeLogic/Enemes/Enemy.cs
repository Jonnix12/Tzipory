using System;
using Helpers.Consts;
using Tzipory.BaseSystem.TimeSystem;
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

        private void Start()
        {
            BasicMoveComponent.Init(MoveSpeed);//temp!
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
                timer += GAME_TIME.GameDeltaTime;
            }
        }



        //protected override void Update()
        //{
        //    base.Update();
        //    EffectSequenceHandler.PlaySequenceById(Constant.EffectSequenceIds.OnMove);
        //}
    }
}