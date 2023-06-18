namespace Tzipory.EntitySystem.TargetingSystem
{
    public enum TargetingPriorityType
    {
        Default,
        Random,
        ClosesToEntity,
        FarthestFromEntity,
        ClosesToCore,
        FarthestFromCore,
        LowestHpEntity,
        HighestHpEntity,
        LowestHpPercentageEntity,
        HighestHpPercentageEntity,
    }
}