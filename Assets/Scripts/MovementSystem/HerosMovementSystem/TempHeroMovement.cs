using UnityEngine;

namespace MovementSystem.HerosMovementSystem
{
    public class TempHeroMovement : MonoBehaviour
    {
        private void OnMouseDown()
        {
            TempHeroMovementManager.Instance.SelectTarget(this);
            Debug.Log($"{gameObject.name} has Selected");
        }
    }
}