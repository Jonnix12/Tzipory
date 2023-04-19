using Tzipory.EntitySystem.TargetingSystem;

namespace Tzipory.EntitySystem.EntityComponents
{
    public interface ITargetingComponent : IEntityComponent
    {
        public ITargetingHandler TargetingHandler { get;}

        public void SetTargetingHandler(ITargetingHandler targetingHandler);
    }
}