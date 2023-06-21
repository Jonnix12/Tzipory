using ProjectDawn.Navigation.Hybrid;
using Tzipory.EntitySystem.EntityComponents;
using UnityEngine;

namespace MovementSystem.HerosMovementSystem
{
    public class TempHeroMovement : MonoBehaviour
    {
        [SerializeField] private AgentAuthoring _agentAuthoring;
        [SerializeField] private BasicMoveComponent _moveComponent;
        [SerializeField] Shamans.Shaman _shaman;
        
        private void Start()
        {
            _moveComponent.Init(_shaman.MoveSpeed);
        }
        public void SetTarget(Vector3 pos)
        {
            _moveComponent.SetDestination(pos, MoveType.Free); //MoveType is not really used at all
        }

        private void OnMouseDown()
        {
            TempHeroMovementManager.Instance.SelectTarget(this);
            Debug.Log($"{gameObject.name} has Selected");
        }
    }
}