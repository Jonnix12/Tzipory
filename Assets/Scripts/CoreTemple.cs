using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tzipory.EntitySystem;
using Tzipory.EntitySystem.TargetingSystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;

public class CoreTemple : BaseGameEntity, IEntityTargetAbleComponent
{
    [SerializeField]
    Stat _hp;

    public EntityTeamType EntityTeamType => EntityTeamType.Hero;

    public Stat InvincibleTime => throw new System.NotImplementedException();

    public bool IsDamageable => true; //temp

    public Stat HP => _hp;

    public bool IsEntityDead => _hp.CurrentValue <= 0;

    public StatusHandler StatusHandler => throw new System.NotImplementedException();

    private void Awake()
    {
        _hp = new Stat("Health", 100, 100, 0);
        base.Awake();
    }

    public void Heal(float amount)
    {
        _hp.AddToValue(amount);
    }

    public void TakeDamage(float damage, bool isCrit)
    {
        _hp.ReduceFromValue(damage);
        if(IsEntityDead)
        {
            print("DEAD");
        }
    }
}
