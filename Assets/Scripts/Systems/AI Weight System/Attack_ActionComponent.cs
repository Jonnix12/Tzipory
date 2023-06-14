using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tzipory.AI.WeightSystem
{
    public class Attack_ActionComponent : ActionComponent
    {
        //ViableTargets - this will probaly be a static list somewhere

        //this is a temp Viable Target list
        [SerializeField]
        List<GameObject> viableTargets;

        public override List<ActionVariation> CalculateVariations()
        {
            return base.CalculateVariations();
        }
    }

}

