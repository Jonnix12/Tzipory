using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tzipory.SerializeData.LevalSerializeData
{
    [System.Serializable]
    public class WaveSerializeData
    {
        [Title("$_name",bold:true,titleAlignment:TitleAlignments.Centered)] 
        [SerializeField,ReadOnly] private string _name;
        [SerializeField,ListDrawerSettings(HideAddButton = true,HideRemoveButton = true)] private List<WaveSpawnerSerializeData> waveSpawnerSerializeDatas;
        private LevelSerializeData  _levelSerializeData;
        
        public List<WaveSpawnerSerializeData> WaveSpawnerSerializeDatas => waveSpawnerSerializeDatas;

        public WaveSerializeData(IEnumerable<WaveSpawner> waveSpawners,LevelSerializeData levelSerializeData)
        {
            _levelSerializeData = levelSerializeData;
            waveSpawnerSerializeDatas = new List<WaveSpawnerSerializeData>();

            foreach (var waveSpawner in waveSpawners)
            {
                waveSpawnerSerializeDatas.Add(new WaveSpawnerSerializeData(waveSpawner));
            }
        }
        
        public void SetWaveSpawners(IEnumerable<WaveSpawner> waveSpawners)
        {
            if (waveSpawnerSerializeDatas == null)
                waveSpawnerSerializeDatas = new List<WaveSpawnerSerializeData>();

            foreach (var waveSpawner in waveSpawners)
            {
                waveSpawnerSerializeDatas.Add(new WaveSpawnerSerializeData(waveSpawner));
            }
        }
        
        public void SetName(string  name)=>
            _name = name;

        public void AddWaveSpawner(WaveSpawner waveSpawner)
        {
            waveSpawnerSerializeDatas.Add(new WaveSpawnerSerializeData(waveSpawner));
        }
        
        public void RemoveWaveSpawner(WaveSpawner waveSpawner)
        {
        }

        [Button("Delete wave")]
        public void DeleteWave()=>
            _levelSerializeData.RemoveWave(this);
    }
}