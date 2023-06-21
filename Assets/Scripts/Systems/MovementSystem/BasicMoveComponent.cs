using ProjectDawn.Navigation;
using ProjectDawn.Navigation.Hybrid;
using Tzipory.EntitySystem.StatusSystem;
using Tzipory.BaseSystem.TimeSystem;
using UnityEngine;

namespace Tzipory.EntitySystem.EntityComponents
{
    public class BasicMoveComponent : MonoBehaviour, IEntityMovementComponent
    {
        [SerializeField]
        private AgentAuthoring agent;
        
        private Stat _speedStat;

        //Set/init by Unit

        //public float AdjustedSpeed => _speedStat.CurrentValue * GAME_TIME.TimeRate;
        //public float AdjustedSpeed => _speedStat.CurrentValue * GAME_TIME;
        public void Init(Stat newSpeed)
        {
            _speedStat = newSpeed;
            _speedStat.OnCurrentValueChanged += AdjustAgentSpeedToTime;
            AdjustAgentSpeedToTime();
        }

        public Stat MoveSpeed => _speedStat;

        public int EntityInstanceID => throw new System.NotImplementedException();

        public Transform EntityTransform => agent.transform;

        public BaseGameEntity GameEntity => throw new System.NotImplementedException();

        public void SetDestination(Vector3 destination, MoveType moveType)
        {
            agent.SetDestination(destination);
        }

        void AdjustAgentSpeedToTime() //subs to OnTimeRateChange
        {
            //AgentSteering aS = agent.EntitySteering;
            AgentSteering aS = agent.DefaultSteering;
            aS.Speed = _speedStat.CurrentValue * GAME_TIME.GetCurrentTimeRate;
            agent.EntitySteering = aS;
        }

        private void OnEnable()
        {
            GAME_TIME.OnTimeRateChange += AdjustAgentSpeedToTime; //?
        }
        private void OnDisable()
        {
            GAME_TIME.OnTimeRateChange -= AdjustAgentSpeedToTime;
            if(_speedStat != null)
                _speedStat.OnCurrentValueChanged -= AdjustAgentSpeedToTime;
        }

    }
}