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
        
        private static float _tickStep;
        private static WaitForSeconds _waitTimeStep;
        
        private static TimerHandler _timerHandler;

        //SACLED DELTA TIME (smoothStep)
        public static float GameTimeDelta => Time.deltaTime * _timeRate;

        public static TimerHandler TimerHandler => _timerHandler;
        
        public static Action OnGameTimeTick; //tbd static or private and static un/sub methods? - no added funtionality is really required to un/subbing that I can think of... 
        
        private void Start()
        {
            _timerHandler = new TimerHandler();
            _waitTimeStep = new WaitForSeconds(_timeRate);
            StartCoroutine(nameof(RunTime));
        }

        private void Update()
        {
            _timerHandler.TickAllTimers();
        }
        
        [Button("Set time step")]
        public static void SetTimeStep(float time)
        {
            if (time <= 0)
            {
                Debug.LogError("Can not set timeStep to less or equal to 0");
                return;
            }
            
            _timeRate = time;
            _tickStep = 1f / _timeRate;
            _waitTimeStep = new WaitForSeconds(_tickStep);
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

