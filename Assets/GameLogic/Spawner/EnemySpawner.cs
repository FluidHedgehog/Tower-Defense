using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Assign path reference!")]
    [SerializeField] Path path;

    [Tooltip("Assign waves objects!")]
    [SerializeField] Wave[] wave;

    private int waveIndex;
    private int maxWaveIndex;

    [Range(1, 60)]
    [Tooltip("Set cooldown between waves!")]
    [SerializeField] int cooldown;

    void OnEnable()
    {
        maxWaveIndex = wave.Length - 1;
    }

    void OnDisable()
    {

    }

    public void StartSpawner()
    {
        StartCoroutine(Spawner());
    }

    public IEnumerator  Spawner()
    {
        var currentWave = wave[waveIndex];

        for (int i = currentWave.quantity; i > 0; i--)
        {
            CreateEnemy(currentWave.enemyPrefab);
            yield return new WaitForSeconds(currentWave.cooldown);
        }

        waveIndex++;
        if (waveIndex <= maxWaveIndex)
        {
            yield return new WaitForSeconds(cooldown);
            StartCoroutine(Spawner());
        }
    }


    public void CreateEnemy(GameObject enemy)
    {
        Instantiate(enemy, path.waypoints[0].transform.position, Quaternion.identity);
    }

}
