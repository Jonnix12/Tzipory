using System.Collections.Generic;

namespace Tzipory.AI.WeightSystem
{
    public interface IActor 
    {
        void CalculateActionVariations();
        List<ActionVariation> GetActionVariations(); //maybe limit access to this, we must have a listalways needed 

        void PerformAction();
    }
}


