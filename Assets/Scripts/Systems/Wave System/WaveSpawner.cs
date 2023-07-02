using UnityEngine;
using PathCreation;
using Sirenix.OdinInspector;
using Tzipory.SerializeData.LevalSerializeData;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private PathCreator myPathCreator;

    [SerializeField,ReadOnly] private bool _isSpawnning;

    private WaveSpawnerSerializeData _spawnerSerializeData;
    
    private Color _waveSpawnerColor = Color.white;

    public Color WaveSpawnerColor => _waveSpawnerColor;

    public bool IsSpawnning => _isSpawnning;
    
    public void Init()
    {
        _waveSpawnerColor = LevelManager.GetSpawnerColor();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = _waveSpawnerColor;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void OnDisable()
    {
        LevelManager.ReturnColor(_waveSpawnerColor);
        Debug.Log("return color");
    }

    private void OnDestroy()
    {
        LevelManager.ReturnColor(_waveSpawnerColor);
        Debug.Log("return color");
    }
}