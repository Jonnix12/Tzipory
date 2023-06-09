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

        private Vector2 _destination = Vector2.zero;

        //Set/init by Unit

        //public float AdjustedSpeed => _speedStat.CurrentValue * GAME_TIME.TimeRate;
        //public float AdjustedSpeed => _speedStat.CurrentValue * GAME_TIME;
        public void Init(Stat newSpeed)
        {
            _speedStat = newSpeed;
            _speedStat.OnCurrentValueChanged += AdjustAgentSpeed;
            AdjustAgentSpeed(_speedStat.CurrentValue);
        }

        public Stat MoveSpeed => _speedStat;

        public int EntityInstanceID => throw new System.NotImplementedException();

        public Transform EntityTransform => agent.transform;

        public BaseGameEntity GameEntity => throw new System.NotImplementedException();
        
        public bool IsMoveing { get; private set; }

        public void SetDestination(Vector3 destination, MoveType moveType)
        {
            agent.SetDestination(destination);
            _destination  = destination;
            IsMoveing = true;
        }

        private void Update()
        {
            if (_destination == Vector2.zero) return;
            
            if (Vector2.Distance(_destination, transform.position) > 0.2f) return;
            
            _destination = Vector2.zero;
            IsMoveing = false;
        }

        void AdjustAgentSpeed(float currentSpeed) //subs to OnTimeRateChange
        {
            //AgentSteering aS = agent.EntitySteering;
            AgentSteering aS = agent.DefaultSteering;
            aS.Speed = currentSpeed * GAME_TIME.GetCurrentTimeRate;
            agent.EntitySteering = aS;
        }

        private void AdjustAgentSpeedTime()
        {
            if (_speedStat == null)
                return;
            AdjustAgentSpeed(_speedStat.CurrentValue);
        }

        private void OnEnable()
        {
            GAME_TIME.OnTimeRateChange += AdjustAgentSpeedTime; //?
        }
        private void OnDisable()
        {
            GAME_TIME.OnTimeRateChange -= AdjustAgentSpeedTime;
            if(_speedStat != null)
                _speedStat.OnCurrentValueChanged -= AdjustAgentSpeed;
        }

    }
}