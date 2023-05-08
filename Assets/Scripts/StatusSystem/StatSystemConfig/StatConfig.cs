using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem.StatSystemConfig
{
    [CreateAssetMenu(fileName = "NewStat", menuName = "ScriptableObjects/EntitySystem/Stat/New stat", order = 0)]
    public class StatConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _statName;
        [SerializeField] private float _baseValue;

        public int ID => _id;

        public string StatName => _statName;

        public float BaseValue => _baseValue;
    }
}