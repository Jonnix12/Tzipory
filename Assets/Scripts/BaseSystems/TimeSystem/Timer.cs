using System;

namespace Tzipory.BaseSystem.TimeSystem
{
    public class Timer
    {
        public event Action<Timer>  OnTimerComplete;
        
        private float _time;
        private readonly Action _onComplete;
        
        public string TimerName { get; }
        
        public bool IsDone => _time <= 0;

        public Timer(string timerName,float time,Action onComplete = null)
        {
            TimerName = timerName;
            _time = time;
            _onComplete = onComplete;
        }

        public void TickTimer()
        {
            _time -= GAME_TIME.GameTimeDelta;

            if (_time <= 0)
            {
                _onComplete?.Invoke();
                OnTimerComplete?.Invoke(this);
            }
        }
    }
}