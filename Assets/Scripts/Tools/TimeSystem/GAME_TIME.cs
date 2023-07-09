using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.BaseSystem.TimeSystem
{
    public class GAME_TIME : MonoBehaviour
    {
        //[SerializeField, Tooltip("Base duration for one Tick of game time.")]
        private const float BASE_TIME = 1f;
        private static float _timeRate = 1f;
        private static TimerHandler _timerHandler;
        
        public static float GetCurrentTimeRate => _timeRate;
        public static float GameDeltaTime => Time.deltaTime * _timeRate;
        public static TimerHandler TimerHandler => _timerHandler;
        
        public static Action OnTimeRateChange;
        private float _tempTimeData;

        private void Start()
        {
            _timerHandler = new TimerHandler();
        }

        private void Update()
        {
            _timerHandler.TickAllTimers();
        }
        
        [Button("Set time step")]
        public static void SetTimeStep(float time)
        {
            if (time < 0)
            {
                Debug.LogError("Can not set timeStep to less or equal to 0");
                return;
            }
            
            _timeRate = time;
            OnTimeRateChange?.Invoke();
        }

        public void ReduseTime()
        {
            SetTimeStep(_timeRate / 2);
        }

        public void AddTime()
        {
            SetTimeStep(_timeRate * 2);
        }

        public void PlayPause()
        {
            if (_timeRate != 0 )
            {
                _tempTimeData = _timeRate;
                SetTimeStep(0);
            }
            else
            {
                SetTimeStep(_tempTimeData);
                _tempTimeData = 0;
            }
        }
    }
}

