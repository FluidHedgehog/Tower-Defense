using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;

    [SerializeField] TextMeshProUGUI text;

    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject skipWaveButton;
 
    [Tooltip("Assign path reference!")]
    [SerializeField] Path[] paths;

    [Tooltip("Assign waves objects!")]
    [SerializeField] WaveData[] waves;

    [Range(1, 60)]
    [Tooltip("Set cooldown between waves!")]
    [SerializeField] int cooldown;

    Coroutine delay;

    int currentWave;


    void Start()
    {
        text.text = $" {0} / {waves.Length}";
        //waves.Length;
    }

    void UpdateText()
    {
        var waveText = currentWave;
        text.text = $" {waveText + 1} / {waves.Length}";
    }

    Path GetRandomPath(int random)
    {
        return paths[random];
    }

    void CreateEnemy()
    {
        var randomPath = Random.Range(0, paths.Length);
        var enemy = Instantiate(waves[currentWave].GetRandomEnemy(), (Vector2)GetRandomPath(randomPath).waypoints[0].transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyInstance>().Initialize(paths[randomPath]);
        enemyManager.AddEnemies(enemy);
    }

    public void StartGame()
    {
        
        startGameButton.SetActive(false);
        skipWaveButton.SetActive(true);
        StartCoroutine(StartSpawn());

    }

    IEnumerator StartSpawn()
    {
        UpdateText();
        for (int i = 0; i < waves[currentWave].enemiesPerWave; i++)
        {
            CreateEnemy();
            yield return new WaitForSeconds(waves[currentWave].spawnDelay);
        }

        delay = StartCoroutine(WaveDelay());
    }

    IEnumerator WaveDelay()
    {
        currentWave += 1;
        if (currentWave == waves.Length)
        {
            CheckIfWon();
            if (delay != null)
            {
                StopCoroutine(delay);
            }
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(cooldown);
            StartCoroutine(StartSpawn());
        }
    }

    public void SkipDelay()
    {
        if (delay != null)
        {
            StopCoroutine(delay);
            delay = null;
            //currentWave += 1;
            if (currentWave == waves.Length)
            {
                CheckIfWon();
            }
            else
            {
                StartCoroutine(StartSpawn());
            }
        }
    }

    public void CheckIfWon()
    {
        enemyManager.isLastWave = currentWave == waves.Length;
    }























    // void OnEnable()
    // {
    //     maxWaveIndex = wave.Length - 1;
    // }

    // void OnDisable()
    // {

    // }

    // public void StartSpawner()
    // {
    //     StartCoroutine(Spawner());
    // }

    // public IEnumerator  Spawner()
    // {
    //     var currentWave = wave[waveIndex];

    //     for (int i = currentWave.quantity; i > 0; i--)
    //     {
    //         CreateEnemy(currentWave.enemyPrefab);
    //         yield return new WaitForSeconds(currentWave.cooldown);
    //     }

    //     waveIndex++;
    //     if (waveIndex <= maxWaveIndex)
    //     {
    //         yield return new WaitForSeconds(cooldown);
    //         StartCoroutine(Spawner());
    //     }
    // }




}
