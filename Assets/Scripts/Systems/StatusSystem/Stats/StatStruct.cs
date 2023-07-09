using Helpers.Consts;

namespace Tzipory.EntitySystem.StatusSystem
{
    public struct StatStruct
    {
        public readonly string Name;
        public readonly int ID;
        public readonly float BaseValue;
        public readonly float MaxValue;
        public readonly float CurrentValue;
        
        private readonly Constant.Stats _statsType;
        
        public StatStruct(Constant.Stats stats,float baseValue,float maxValue)
        {
            Name = stats.ToString();
            ID = (int)stats;
            BaseValue = baseValue;
            CurrentValue = BaseValue;
            MaxValue = maxValue;
            _statsType = stats;
        }
        public StatStruct(Constant.Stats stats,float value)
        {
            Name = stats.ToString();
            ID = (int)stats;
            BaseValue = 0;
            MaxValue = 0;
            CurrentValue = value;
            _statsType = stats;
        }

        public static StatStruct operator + (StatStruct a,StatStruct b)
        {
            return new StatStruct(a._statsType,a.CurrentValue + b.CurrentValue);
        }
        
        public static StatStruct operator + (StatStruct a,float b)
        {
            return new StatStruct(a._statsType,a.CurrentValue + b);
        }
    }
}