using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;

namespace Enemes
{
    public class Enemy : BaseUnitEntity
    {
        //TEMP!
        public BasicMoveComponent BasicMoveComponent;
        protected override void Awake()
        {
            base.Awake();
            EntityTeamType = EntityTeamType.Enemy;
        }
    }
}