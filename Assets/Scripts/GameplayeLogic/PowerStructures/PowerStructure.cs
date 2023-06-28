using System;
using System.Collections.Generic;
using Shamans;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.EntitySystem;
using UnityEngine;

//TEMP NAME! BAD NAME!
public class PowerStructure : BaseGameEntity
{
    //This is actually not temp, it's a good gizmo to help move the area of influence a bit, as needed
    [SerializeField]
    CircleCollider2D effectArea;
    [SerializeField]
    StatusEffectConfigSo myEffectSO;
    [SerializeField]
    ProximityIndicatorHandler _proximityIndicatorHandler;

    private Dictionary<int, IDisposable> _activeStatusEffectOnShaman;

    protected override void Awake()
    {
        base.Awake();
        _activeStatusEffectOnShaman = new Dictionary<int, IDisposable>();
        _proximityIndicatorHandler.Init(effectArea.radius*2f); //MAY need to move to OnEnable - especially if we use ObjectPooling instead of instantiate
    }

    private void OnDisable()
    {
        _proximityIndicatorHandler.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shaman"))
        {
            Shaman shaman = collision.GetComponentInParent<Shaman>();
            Debug.Log($"{shaman.name} entered the area of influence of {name}");
            IDisposable disposable = shaman.StatusHandler.AddStatusEffect(myEffectSO);
            _activeStatusEffectOnShaman.Add(shaman.EntityInstanceID, disposable);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shaman"))
        {
            Shaman shaman = collision.GetComponentInParent<Shaman>();
            
            if (shaman is null)
                return;

            if (_activeStatusEffectOnShaman.TryGetValue(shaman.EntityInstanceID,out IDisposable disposable))
            {
                disposable.Dispose();
                _activeStatusEffectOnShaman.Remove(shaman.EntityInstanceID);
            }
        }
    }

}
