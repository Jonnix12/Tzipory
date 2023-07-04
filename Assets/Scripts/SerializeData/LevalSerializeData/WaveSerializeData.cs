using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class WaveSerializeData
    {
        [SerializeField] private List<WaveSpawnerSerializeData> waveSpawnerSerializeDatas;
        
        public List<WaveSpawnerSerializeData> WaveSpawnerSerializeDatas => waveSpawnerSerializeDatas;
        
        public void SetWaveSpawners(IEnumerable<WaveSpawner> waveSpawners)
        {
            if (waveSpawnerSerializeDatas == null)
                waveSpawnerSerializeDatas = new List<WaveSpawnerSerializeData>();

            foreach (var waveSpawner in waveSpawners)
            {
                waveSpawnerSerializeDatas.Add(new WaveSpawnerSerializeData(waveSpawner));
            }
        }

        public void AddWaveSpawner(WaveSpawner waveSpawner)
        {
            waveSpawnerSerializeDatas.Add(new WaveSpawnerSerializeData(waveSpawner));
        }
        
        public void RemoveWaveSpawner(WaveSpawner waveSpawner)
        {
            
        }

        [Button("Delete wave")]
        public void DeleteWave()
        {
            
        }
    }
}