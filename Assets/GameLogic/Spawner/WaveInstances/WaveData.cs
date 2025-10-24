using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/WaveData")]
public class WaveData : ScriptableObject
{
    [Range(0.05f, 2)]
    public float spawnDelay;

    [System.Serializable]
    public class EnemySpawnChance
    {
        public GameObject enemyPrefab;
        [Range(0, 100)]
        public int spawnChance;
    }

    public EnemySpawnChance[] enemyTypes;
    public float timeBetweenWaves;
    public int enemiesPerWave;

    [HideInInspector]
    public int maxChanceValue;

    private void OnValidate()
    {
        ApplyMax();
    }

    [ContextMenu("Apply Max Value")]
    public void ApplyMax()
    {
        maxChanceValue = 0;
        foreach (var enemyType in enemyTypes)
        {
            maxChanceValue += enemyType.spawnChance;
        }
    }

    public GameObject GetRandomEnemy()
    {
        if (enemyTypes.Length == 0) return null;

        int randomValue = Random.Range(0, maxChanceValue);
        int currentSum = 0;

        foreach (var enemyType in enemyTypes)
        {
            currentSum += enemyType.spawnChance;
            if (randomValue < currentSum)
            {
                return enemyType.enemyPrefab;
            }
        }
        return enemyTypes[enemyTypes.Length - 1].enemyPrefab;
    }


}
