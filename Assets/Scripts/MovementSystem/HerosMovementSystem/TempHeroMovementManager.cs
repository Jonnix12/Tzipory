using System;
using Tzipory.BaseScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem.HerosMovementSystem
{
    public class TempHeroMovementManager : MonoSingleton<TempHeroMovementManager>
    {
        public event Action<Vector3> OnMove;

        private TempHeroMovement _currentTarget;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void SelectTarget(TempHeroMovement  target)
        {
            _currentTarget = target;
        }

        public void ClearTarget()
        {
            _currentTarget = null;
        }

        private void Update()
        {
            if (_currentTarget == null)
                return;

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                var screenPos = Mouse.current.position.ReadValue();
                var worldPos = _camera.ScreenToWorldPoint(screenPos);
                
                _currentTarget.transform.position = worldPos;
                OnMove?.Invoke(worldPos);

                _currentTarget = null;
            }
        }
    }
}