using System.Collections.Generic;
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
    [SerializeField] //temp
    StatusEffectConfigSo myOppositeEffectSO;

    List<Shamans.Shaman> _currentShamans; //not really used, but good to have
    private void OnEnable()
    {
        _currentShamans = new List<Shamans.Shaman>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shaman"))
        {
            Shamans.Shaman s = collision.GetComponentInParent<Shamans.Shaman>();
            s.StatusHandler.AddStatusEffect(myEffectSO);
            _currentShamans.Add(s);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shaman"))
        {
            Shamans.Shaman s = collision.GetComponentInParent<Shamans.Shaman>();
            
            if (s is null)
                return;
            
            s.StatusHandler.AddStatusEffect(myOppositeEffectSO);
            _currentShamans.Remove(s);
        }
    }

}
