using System.Collections.Generic;

namespace Tzipory.StatusSystem
{
    public class Status
    {
        public string Name { get; }//may not be neend
        public float BaseValue { get; }
        public float CurrentValue { get; private set; }

        public List<StatusModifier> StatusModifiers { get; private set; }


        public Status(string name, float baseValue)
        {
            Name = name;
            BaseValue = baseValue;
            CurrentValue = BaseValue;
            StatusModifiers = new List<StatusModifier>();
        }
        
        public void AddModifier(StatusModifier modifier)
        {
            StatusModifiers.Add(modifier);
        }
        
        public void RemoveModifier(StatusModifier modifier)
        {
            StatusModifiers.Remove(modifier);
        }
        
        public void AddToValue(float amount)
        {
            CurrentValue += amount;
        }

        public void ReduceFromValue(float amount)
        {
            CurrentValue -= amount;
        }

        public void ResetValue()
        {
            StatusModifiers.Clear();
            CurrentValue = BaseValue;
        }
    }
}