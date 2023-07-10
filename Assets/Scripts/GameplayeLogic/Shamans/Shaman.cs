﻿using Helpers.Consts;
using MovementSystem.HerosMovementSystem;
using Tzipory.BaseSystem.TimeSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;
using Tzipory.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shamans
{
    public class Shaman : BaseUnitEntity
    {

        [SerializeField, TabGroup("Proximity Indicator")] private ProximityIndicatorHandler _proximityHandler;

        [Space]
        [Header("Temps")]
        [SerializeField] private Temp_ShamanShotVisual _shotVisual;
        [SerializeField] private ClickHelper _clickHelper;
        [SerializeField] private Temp_HeroMovement _tempHeroMovement;
        
        private float _currentAttackRate;

        protected override void Awake()
        {
            base.Awake();
            EntityTeamType = EntityTeamType.Hero;
            _clickHelper.OnClick += _tempHeroMovement.SelectHero;

            //_proximityConfig
        }
        protected override void Start()
        {
            base.Start();
            _proximityHandler.Init(AttackRange.CurrentValue *2f);//MAY need to move to OnEnable - especially if we use ObjectPooling instead of instantiate
        }
        private void OnDisable()
        {
            _proximityHandler.Disable();
        }

        protected override void UpdateEntity()
        {
             if (Targeting.CurrentTarget != null)//temp
                 Attack();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _clickHelper.OnClick -= _tempHeroMovement.SelectHero;
        }

        public override void Attack()
        {
            if (_tempHeroMovement.IsMoveing)
                return;
            
            AbilityHandler.CastAbility(Targeting.AvailableTargets);
            
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
                _shotVisual.Shot(Targeting.CurrentTarget,CritDamage.CurrentValue,true);
                return;
            }
            EffectSequenceHandler.PlaySequenceById(Constant.EffectSequenceIds.OnAttack);
            _shotVisual.Shot(Targeting.CurrentTarget,AttackDamage.CurrentValue,false);
        }
    }
}