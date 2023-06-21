using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.BaseSystem.TimeSystem
{
    public class GAME_TIME : MonoBehaviour
    {
        //[SerializeField, Tooltip("Base duration for one Tick of game time.")]
        private static float _timeRate = 1f;
        private static float _tickBase = 1f;

        private float _tempTimeData; // temp

        private static float _tickStep;


        //private static WaitForSeconds _waitTimeStep;
        private static WaitForSecondsRealtime _waitTimeStep;
        
        private static TimerHandler _timerHandler;


        public static float GetCurrentTimeRate => _timeRate;
        //SACLED DELTA TIME (smoothStep)
        public static float GameDeltaTime => Time.deltaTime * _timeRate;

        public static TimerHandler TimerHandler => _timerHandler;
        
        public static Action OnGameTimeTick; //tbd static or private and static un/sub methods? - no added funtionality is really required to un/subbing that I can think of... 
        public static Action OnTimeRateChange;

        private void Start()
        {
            _timerHandler = new TimerHandler();
            _tickStep = _tickBase / _timeRate;
            //_waitTimeStep = new WaitForSeconds(_timeRate);
            _waitTimeStep = new WaitForSecondsRealtime(_tickStep);
            StartCoroutine(nameof(RunTime));
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
            //_tickStep = 1f / _timeRate;
            _tickStep = _tickBase / _timeRate;
            //_waitTimeStep = new WaitForSeconds(_tickStep);
            _waitTimeStep = new WaitForSecondsRealtime(_tickStep);
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

        private IEnumerator RunTime()
        {
            while(true)
            {
                if (_timeRate == 0)
                    yield return  null;
                
                yield return _waitTimeStep;
                OnGameTimeTick?.Invoke();
            }
        }
    }
}

