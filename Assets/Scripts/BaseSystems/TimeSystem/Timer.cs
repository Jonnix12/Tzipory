using System;

namespace Tzipory.BaseSystem.TimeSystem
{
    public interface ITimer
    {
        public event Action<ITimer>  OnTimerComplete;
        
        public string TimerName { get; }
        
        public bool IsDone { get; }
        
        public void TickTimer();
    }

    public class Timer : ITimer
    {
        public event Action<ITimer> OnTimerComplete;
        
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
            _time -= GAME_TIME.GameDeltaTime;

            if (_time <= 0)
            {
                _onComplete?.Invoke();
                OnTimerComplete?.Invoke(this);
            }
        }
    }
    
    public class Timer<T> : ITimer
    {
        public event Action<ITimer> OnTimerComplete;
        
        private float _time;
        private readonly Action<T> _onComplete;

        private T _parameter;
        
        public string TimerName { get; }
        
        public bool IsDone => _time <= 0;

        public Timer(string timerName,float time,Action<T> onComplete,ref T parameter)
        {
            TimerName = timerName;
            _time = time;
            _onComplete = onComplete;

            _parameter = parameter;
        }

        public void TickTimer()
        {
            _time -= GAME_TIME.GameDeltaTime;

            if (_time <= 0)
            {
                _onComplete?.Invoke(_parameter);
                OnTimerComplete?.Invoke(this);
            }
        }
    }
    
    public class Timer<T1,T2> : ITimer
    {
        public event Action<ITimer>  OnTimerComplete;
        
        private float _time;
        private readonly Action<T1,T2> _onComplete;

        private T1 _parameter1;
        private T2 _parameter2;
        
        public string TimerName { get; }
        
        public bool IsDone => _time <= 0;

        public Timer(string timerName,float time,Action<T1,T2> onComplete,ref T1 parameter1,ref T2 parameter2)
        {
            TimerName = timerName;
            _time = time;
            _onComplete = onComplete;

            _parameter1 = parameter1;
            _parameter2 = parameter2;
        }

        public void TickTimer()
        {
            _time -= GAME_TIME.GameDeltaTime;

            if (_time <= 0)
            {
                _onComplete?.Invoke(_parameter1,_parameter2);
                OnTimerComplete?.Invoke(this);
            }
        }
    }
    
    public class Timer<T1,T2,T3> : ITimer
    {
        public event Action<ITimer> OnTimerComplete;
        
        private float _time;
        private readonly Action<T1,T2,T3> _onComplete;

        private T1 _parameter1;
        private T2 _parameter2;
        private T3 _parameter3;
        
        public string TimerName { get; }
        
        public bool IsDone => _time <= 0;

        public Timer(string timerName,float time,Action<T1,T2,T3> onComplete,ref T1 parameter1,ref T2 parameter2,ref T3 parameter3)
        {
            TimerName = timerName;
            _time = time;
            _onComplete = onComplete;

            _parameter1 = parameter1;
            _parameter2 = parameter2;
            _parameter3 = parameter3;
        }

        public void TickTimer()
        {
            _time -= GAME_TIME.GameDeltaTime;

            if (_time <= 0)
            {
                _onComplete?.Invoke(_parameter1,_parameter2,_parameter3);
                OnTimerComplete?.Invoke(this);
            }
        }
    }
    
    public class Timer<T1,T2,T3,T4> : ITimer
    {
        public event Action<ITimer> OnTimerComplete;
        
        private float _time;
        private readonly Action<T1,T2,T3,T4> _onComplete;

        private T1 _parameter1;
        private T2 _parameter2;
        private T3 _parameter3;
        private T4 _parameter4;
        
        public string TimerName { get; }
        
        public bool IsDone => _time <= 0;

        public Timer(string timerName,float time,Action<T1,T2,T3,T4> onComplete,ref T1 parameter1,ref T2 parameter2,ref T3 parameter3,ref T4 parameter4)
        {
            TimerName = timerName;
            _time = time;
            _onComplete = onComplete;

            _parameter1 = parameter1;
            _parameter2 = parameter2;
            _parameter3 = parameter3;
            _parameter4 = parameter4;
        }

        public void TickTimer()
        {
            _time -= GAME_TIME.GameDeltaTime;

            if (_time <= 0)
            {
                _onComplete?.Invoke(_parameter1,_parameter2,_parameter3,_parameter4);
                OnTimerComplete?.Invoke(this);
            }
        }
    }
    
    public class Timer<T1,T2,T3,T4,T5> : ITimer
    {
        public event Action<ITimer> OnTimerComplete;
        
        private float _time;
        private readonly Action<T1,T2,T3,T4,T5> _onComplete;

        private T1 _parameter1;
        private T2 _parameter2;
        private T3 _parameter3;
        private T4 _parameter4;
        private T5 _parameter5;
        
        public string TimerName { get; }
        
        public bool IsDone => _time <= 0;

        public Timer(string timerName,float time,Action<T1,T2,T3,T4,T5> onComplete,ref T1 parameter1,ref T2 parameter2,ref T3 parameter3, ref T4 parameter4,ref T5 parameter5)
        {
            TimerName = timerName;
            _time = time;
            _onComplete = onComplete;

            _parameter1 = parameter1;
            _parameter2 = parameter2;
            _parameter3 = parameter3;
            _parameter4 = parameter4;
            _parameter5 = parameter5;
        }

        public void TickTimer()
        {
            _time -= GAME_TIME.GameDeltaTime;

            if (_time <= 0)
            {
                _onComplete?.Invoke(_parameter1,_parameter2,_parameter3,_parameter4,_parameter5);
                OnTimerComplete?.Invoke(this);
            }
        }
    }
}