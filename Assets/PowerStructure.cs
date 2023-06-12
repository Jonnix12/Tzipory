using System.Collections;
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
    [SerializeField]
    LayerMask shamanLayer;

    Collider2D[] _cols;
    //TEMP! This should probably sub to game-tick instead, fixedupdated is still too often IMO
    private void FixedUpdate()
    {
        _cols = Physics2D.OverlapCircleAll((Vector2)transform.position + effectArea.offset, effectArea.radius, shamanLayer);

        foreach (var item in _cols)
        {
            item.GetComponent<Shamans.Shaman>().StatusHandler.AddStatusEffect(new InstantStatusEffect(myEffectSO));
        }
    }

}
