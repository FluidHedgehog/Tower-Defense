using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    public bool isLastWave;

    List<EnemyInstance> enemies = new List<EnemyInstance>();

    public int enhancements;

    void OnEnable()
    {
        BloodSystemEvents.OnBloodAdded += ValidateEnemies;
        HealthSystemEvents.OnHealthRemoved += ValidateEnemies;
    }

    void OnDisable()
    {
        BloodSystemEvents.OnBloodAdded -= ValidateEnemies;
        HealthSystemEvents.OnHealthRemoved -= ValidateEnemies;
    }

    public void AddEnemies(GameObject enemy)
    {
        enemies.Add(enemy.GetComponent<EnemyInstance>());
    }

    void ValidateEnemies(int dummy)
    {
        StartCoroutine(ValidateEnemies1());
    }

    IEnumerator ValidateEnemies1()
    {
        
        

        yield return new WaitForSeconds(0.1f);
        enemies.RemoveAll(enemy => !enemy.isAlive || enemy == null);

        if (isLastWave && enemies.Count == 0)
        {
            ProgressManager.instance.CompleteLevel(ProgressManager.instance.currentSceneName);

            if (SceneManager.GetSceneByName(ProgressManager.instance.currentSceneName).isLoaded)
            {
                SceneManager.UnloadSceneAsync(ProgressManager.instance.currentSceneName);    
            }
            
            SceneManager.LoadScene("Won", LoadSceneMode.Additive);
        }

        EnhanceEnemies();
    }

    public void EnhanceEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.enhanced < enhancements)
            {
                enemy.Enhance(1);
            }
        }
    }

    public void DamageAll(int damage)
    {
        foreach (var enemy in enemies)
        {
            enemy.ApplyDamage(damage);
        }
    }
}
