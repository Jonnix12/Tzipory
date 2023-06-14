using Tzipory.EntitySystem.EntityComponents;
using Tzipory.EntitySystem.Entitys;

namespace Enemes
{
    public class Enemy : BaseUnitEntity
    {
        protected override void Awake()
        {
            base.Awake();
            EntityTeamType = EntityTeamType.Enemy;
        }
    }
}