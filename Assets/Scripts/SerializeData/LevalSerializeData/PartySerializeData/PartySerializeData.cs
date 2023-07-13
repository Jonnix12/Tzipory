using Shamans;
using Tzipory.EntitySystem.EntityConfigSystem;
using UnityEngine;

namespace SerializeData.LevalSerializeData.PartySerializeData
{
    [System.Serializable]
    public class PartySerializeData
    {
        [SerializeField] private Shaman  _shamanPrefab;
        [SerializeField] private Transform _partyParent;
        [SerializeField] private BaseUnitEntityConfig[] _entityConfigs;

        public Shaman ShamanPrefab => _shamanPrefab;

        public Transform PartyParent => _partyParent;

        public BaseUnitEntityConfig[] EntityConfigs => _entityConfigs;
    }
}