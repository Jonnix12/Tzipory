[System.Serializable]

public class WaveGroup 
{
    public EnemyGroup[] enemyGroups;

    public bool isLive;

    public void SetAllTickers()
    {
        foreach (var item in enemyGroups)
        {
            item.ticker.Init(item.spawnRate.y, item.spawnRate.x);
        }
    }
}
