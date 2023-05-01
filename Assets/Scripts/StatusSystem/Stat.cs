
namespace Tzipory.EntitySystem.StatusSystem
{
    public class Stat
    {
        public string Name { get; }//may not be neend
        public int Id { get; }
        public float BaseValue { get; }
        public float CurrentValue { get; private set; }
        
        public Stat(string name, float baseValue,int id)
        {
            Name = name;
            Id = id;  
            BaseValue = baseValue;
            CurrentValue = BaseValue;
        }

        public void MultiplyValue(float amount)=>
            CurrentValue *= amount;
        
        
        public void AddToValue(float amount)=>
            CurrentValue += amount;
        

        public void ReduceFromValue(float amount)=>
            CurrentValue -= amount;
        

        public void ResetValue()=>
            CurrentValue = BaseValue;
    }
}