using ProjectDawn.Navigation.Hybrid;
using UnityEngine;

namespace MovementSystem.HerosMovementSystem
{
    public class TempHeroMovement : MonoBehaviour
    {
        [SerializeField] private AgentAuthoring _agentAuthoring;

        public void SetTarget(Vector3 pos)
        {
            _agentAuthoring.SetDestination(pos);
        }

        private void OnMouseDown()
        {
            TempHeroMovementManager.Instance.SelectTarget(this);
            Debug.Log($"{gameObject.name} has Selected");
        }
    }
}