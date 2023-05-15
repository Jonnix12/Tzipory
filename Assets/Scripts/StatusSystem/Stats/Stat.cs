

using UnityEngine;

namespace Tzipory.EntitySystem.StatusSystem
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private float _baseValue;
        [SerializeField] private float _maxValue;
        
        public string Name { get; }//may not be neend
        public int Id { get; }
        public float BaseValue { get; }
        public float CurrentValue { get; private set; }
        
        public float MaxValue { get; private set; }
        
        public Stat(string name, float baseValue,float maxValue,int id)
        {
            Name = name;
            Id = id;  
            BaseValue = baseValue;
            MaxValue = maxValue;
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