using System.Collections;
using System.Collections.Generic;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

public class TEMP_HPBarConnector : MonoBehaviour
{
    [SerializeField]
    TEMP_HP_Bar hP_Bar;

    //IEntityHealthComponent healthComponent; //TBF after IEntityHealthComponent has its own method for subbing to an OnValueChanged
    [SerializeField]
    CoreTemple coreTemple;

    private void Awake()
    {
        hP_Bar.Init(coreTemple.HP.BaseValue);
    }
    private void OnEnable()
    {
        coreTemple.OnHealthChanged += SetBarToHealth;
    }
    private void OnDisable()
    {
        coreTemple.OnHealthChanged -= SetBarToHealth;
    }
    void SetBarToHealth()
    {
        //hP_Bar.SetBarValueSmoothly(coreTemple.HP.CurrentValue);
        hP_Bar.SetBarValue(coreTemple.HP.CurrentValue);
    }


}
