

using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private string _name;
        [SerializeField] private int _id;
        [SerializeField] private float _baseValue;

        public string Name => _name;
        public int Id => _id;
        public float BaseValue => _baseValue;
        public float CurrentValue { get; private set; }
        
        public float MaxValue { get; private set; }
        
        public Stat(string name, float baseValue,float maxValue,int id)
        {
            _name = name;
            _id = id;  
            _baseValue = baseValue;
            MaxValue = StatLimiters.MaxStatValue;
            CurrentValue = BaseValue;
        }

        public void SetValue(float amount) =>
            CurrentValue = amount;

        public void MultiplyValue(float amount)=>
            CurrentValue *= amount;
        
        public void DivideValue(float amount)=>
            CurrentValue *= amount;
        
        public void AddToValue(float amount)=>
            CurrentValue += amount;
        

        public void ReduceFromValue(float amount)=>
            CurrentValue -= amount;
        
        public void ResetValue()=>
            CurrentValue = BaseValue;
    }
}