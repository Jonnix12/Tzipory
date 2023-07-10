using UnityEngine;
using Tzipory.EntitySystem;
using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.StatusSystem;
using Sirenix.OdinInspector;

public class CoreTemple : BaseGameEntity, IEntityTargetAbleComponent
{
    [SerializeField]
    float _hp;

    [SerializeField,ReadOnly] private Stat _hpStat;

    public EntityTeamType EntityTeamType => EntityTeamType.Hero;

    public Stat InvincibleTime => throw new System.NotImplementedException();

    public bool IsDamageable => true; //temp

    public Stat HP => _hpStat;
    public bool IsEntityDead => HP.CurrentValue <= 0;

    public StatusHandler StatusHandler => throw new System.NotImplementedException();

    public System.Action OnHealthChanged;

    //SUPER TEMP! this needs to move to the Blackboard if we're really doing it
    public static Transform CoreTrans;


    protected override void Awake()
    {
        CoreTrans = transform;
        _hpStat = new Stat("Health", _hp, int.MaxValue, 0); //TEMP! Requires a config
        base.Awake();
    }

    private void OnDisable() //override OnDestroy() instead?
    {
        CoreTrans = null;
    }

    public void Heal(float amount)
    {
        _hpStat.AddToValue(amount);
        OnHealthChanged?.Invoke();
    }

    public void TakeDamage(float damage, bool isCrit)
    {
        _hpStat.ReduceFromValue(damage);
        OnHealthChanged?.Invoke();

        if (IsEntityDead)
        {
            print("GAME OVER!");
        }
    }
}
