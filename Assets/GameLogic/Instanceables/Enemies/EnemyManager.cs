using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] EnemySpawner enemySpawner;
    public bool isLastWave;
    List<EnemyInstance> enemies = new List<EnemyInstance>();

    public int enhancements;

    void OnEnable()
    {
        BloodSystemEvents.OnBloodAdded += ValidateEnemies;
    }

    void OnDisable()
    {
        BloodSystemEvents.OnBloodAdded -= ValidateEnemies;
    }

    public void AddEnemies(GameObject enemy)
    {
        enemies.Add(enemy.GetComponent<EnemyInstance>());
    }

    void ValidateEnemies(int i)
    {
        enemies.RemoveAll(enemy => !enemy.isAlive || enemy == null);

        if (isLastWave && enemies.Count == 0)
        {
            SceneManager.LoadScene("Won");
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
