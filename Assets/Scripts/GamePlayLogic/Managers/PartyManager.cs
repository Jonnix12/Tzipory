using System.Collections.Generic;
using Shamans;

namespace GameplayeLogic.Managers
{
    public class PartyManager
    {
        public IEnumerable<Shaman> Party { get; private set; }
        
        public PartyManager(IEnumerable<Shaman> party)
        {
            Party = party;
        }
    }
}