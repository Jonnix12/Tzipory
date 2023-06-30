using Helpers.Consts;
using Sirenix.OdinInspector;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;
using UnityEngine;

namespace Enemes
{
    public class Enemy : BaseUnitEntity
    {
        [SerializeField,TabGroup("AI")] private float _decisionInterval;
        [SerializeField,TabGroup("AI")] private float _aggroLevel;

        private bool _isAttacking;

        private float _currentDecisionInterval;
        
        //TEMP!
        public BasicMoveComponent BasicMoveComponent;

        float timer;

        protected override void Awake()
        {
            base.Awake();
            EntityTeamType = EntityTeamType.Enemy;
            timer = 0;
            
            _isAttacking  = false;
        }

        protected override void UpdateEntity()
        {
            if (_currentDecisionInterval < 0)
            {
                if (Random.Range(0, 100) < _aggroLevel)
                {
                    _isAttacking  = true;
                }
                
                _currentDecisionInterval = _decisionInterval;
            }

            if (_isAttacking)
            {
                BasicMoveComponent.SetDestination(Target.EntityTransform.position, MoveType.Free);

                if (Vector3.Distance(transform.position, Target.EntityTransform.position) < AttackRange.CurrentValue)
                    Attack();

                if (Target.IsEntityDead)
                {
                    _isAttacking = false;
                }
            }
            else
            {
                
            }
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
            
            if (Target == null)
                return;
            
            if (timer >= StatusHandler.GetStatById((int)Constant.Stats.AttackRate).CurrentValue)
            {
                timer = 0f;
                Target.TakeDamage(StatusHandler.GetStatById((int)Constant.Stats.AttackDamage).CurrentValue, false);
            }
            else
            {
                timer += GAME_TIME.GameDeltaTime;
            }
        }
    }
}