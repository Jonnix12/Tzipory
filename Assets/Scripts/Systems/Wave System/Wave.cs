using System;
using UnityEngine;

namespace Systems.Wave_System
{
    [Serializable]
    public class Wave
    {
        [SerializeField] WaveSpawner[] _waveSpawners;
        
        public void Init(WaveSpawner[] waveSpawners)
        {
            _waveSpawners = waveSpawners;
            
        }
    }
}