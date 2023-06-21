using Helpers.Consts;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;
using UnityEngine;

namespace Shamans
{
    public class Shaman : BaseUnitEntity
    {
        [Space]
        [Header("Temps")]
        [SerializeField] private Temp_ShamanShotVisual _shotVisual;//temp!
        private float _currentAttackRate;

        protected override void Awake()
        {
            base.Awake();
            EntityTeamType = EntityTeamType.Hero;
        }

        public override void Attack()
        {
           AbilityHandler.CastAbilityByName("AoeFire",Targeting.AvailableTargets);
            
            bool canAttack = false;

            if (AbilityHandler.IsCasting)//temp!!!
            {
                 return;
            }
            
            _currentAttackRate -= GAME_TIME.GameDeltaTime;
            
            if (_currentAttackRate < 0)
            {
                _currentAttackRate = AttackRate.CurrentValue;
                canAttack = true;
            }
            
            if(!canAttack)
                return;
            
            if (CritChance.CurrentValue > Random.Range(0, 100))
            {
                EffectSequenceHandler.PlaySequenceById(Constant.EffectSequenceIds.OnCritAttack);
                _shotVisual.Shot(Target,CritDamage.CurrentValue,EntityInstanceID,true);
                return;
            }
            EffectSequenceHandler.PlaySequenceById(Constant.EffectSequenceIds.OnAttack);
            _shotVisual.Shot(Target,AttackDamage.CurrentValue,EntityInstanceID,false);
            //EffectSequenceHandler.PlaySequenceById(999);

            //  Debug.Log(
            //    $"{Target.EntityTransform.name} been attacked by {gameObject.name}, hp left {Target.HP.CurrentValue}");
        }
    }
}