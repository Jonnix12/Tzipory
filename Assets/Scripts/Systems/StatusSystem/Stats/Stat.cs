

using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private string _name;
        [SerializeField] private int _id;
        [SerializeField] private float _baseValue;
        [SerializeField,ReadOnly] float _currentValue;

        public string Name => _name;
        public int Id => _id;
        public float BaseValue => _baseValue;
        public float CurrentValue => _currentValue;
        
        public float MaxValue { get; private set; }
        
        public Stat(string name, float baseValue,float maxValue,int id)
        {
            _name = name;
            _id = id;  
            _baseValue = baseValue;
            MaxValue = StatLimiters.MaxStatValue;
            _currentValue = _baseValue;
        }

        public void SetValue(float amount) =>
            _currentValue = amount;

        public void MultiplyValue(float amount)=>
            _currentValue *= amount;
        
        public void DivideValue(float amount)=>
            _currentValue *= amount;
        
        public void AddToValue(float amount)=>
            _currentValue += amount;
        
        public void ReduceFromValue(float amount)=>
            _currentValue -= amount;
        
        public void ResetValue()=>
            _currentValue = _baseValue;
    }
}