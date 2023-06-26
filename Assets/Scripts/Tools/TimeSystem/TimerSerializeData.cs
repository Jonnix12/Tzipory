using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.BaseSystem.TimeSystem
{
    [Serializable]
    public class TimerSerializeData
    {
        [SerializeField, ReadOnly] private string _timerName;
        [SerializeField, ReadOnly] private float _timeRemain;
        [SerializeField, ReadOnly] private string _onCompleted;
        
        private ITimer _timer;

        public ITimer Timer => _timer;

        public TimerSerializeData(ITimer timer)
        {
            _timerName = timer.TimerName;
            _onCompleted = timer.OnCompleteMethodName;
            _timer  = timer;
        }

        public void Update()=>
            _timeRemain = _timer.TimeRemaining;
    }
}