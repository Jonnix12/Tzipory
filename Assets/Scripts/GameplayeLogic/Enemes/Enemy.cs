using Helpers.Consts;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;

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

        protected override void UpdateEntity()
        {
            if (Target != null)//temp
                Attack();
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