using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem.StatSystemConfig
{
    [CreateAssetMenu(fileName = "NewStat", menuName = "ScriptableObjects/EntitySystem/StatusSystem/New statName", order = 0)]
    public class StatConfig : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _statName;
        [SerializeField] private float _baseValue;
        [SerializeField] private float _maxValue;

        public int ID => _id;

        public string StatName => _statName;

        public float BaseValue => _baseValue;

        public float MaxValue => _maxValue;
    }
}