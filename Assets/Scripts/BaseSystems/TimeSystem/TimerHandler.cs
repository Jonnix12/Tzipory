using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tzipory.BaseSystem.TimeSystem
{
    public class TimerHandler
    {
        private Dictionary<string, Timer> _timersDictionary;
        private List<Timer> _timersList;

        public TimerHandler()
        {
            _timersDictionary = new Dictionary<string, Timer>();
            _timersList = new List<Timer>();
        }

        public Timer StartNewTimer(float time,Action onComplete)
        {
            return StartNewTimer(time, null, onComplete);
        }
        
        public Timer StartNewTimer(float time,string timerName)
        {
            return  StartNewTimer(time, timerName, null);
        }
        
        public Timer StartNewTimer(float time, string timerName = null, Action onComplete = null)
        {
            Timer timer = null;
            
            if (!string.IsNullOrEmpty(timerName))
            {
                if(_timersDictionary.ContainsKey(timerName))
                    throw new Exception("Timer with name " + timerName + " already exists");
                
                timer = new Timer(timerName,time,onComplete);
                timer.OnTimerComplete  += TimeComplete;
                _timersDictionary.Add(timerName,timer);
                
                return  timer;
            }
            
            timer = new Timer(timerName,time,onComplete);
            timer.OnTimerComplete  += TimeComplete;
            _timersList.Add(timer);
            
            return timer;
        }
        
        public void TickAllTimers()
        {
            foreach (var keyValuePair in _timersDictionary)
                keyValuePair.Value.TickTimer();

            for (int i = 0; i < _timersList.Count; i++)
                _timersList[i].TickTimer();
        }
        
        private void TimeComplete(Timer timer)
        {
            if (!string.IsNullOrEmpty(timer.TimerName))
            {
                if (_timersDictionary.TryGetValue(timer.TimerName, out var value))
                {
                    _timersDictionary.Remove(timer.TimerName);
                    return;
                }

                Debug.LogError($"can not find time by name : {timer.TimerName}");
            }
            
            if (_timersList.Contains(timer))
            {
                _timersList.Remove(timer);
                return;
            }

            Debug.LogError("Con not find time ro remove");
        }
    }
}