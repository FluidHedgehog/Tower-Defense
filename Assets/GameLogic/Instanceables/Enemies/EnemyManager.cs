using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<EnemyInstance> enemies = new List<EnemyInstance>();

    int enhancements;

    void OnEnable()
    {
        BloodSystemEvents.OnBloodAdded += ValidateEnemies;
    }

    void OnDisable()
    {

    }

    public void AddEnemies(GameObject enemy)
    {
        enemies.Add(enemy.GetComponent<EnemyInstance>());
    }

    void ValidateEnemies(int i)
    {
        foreach (var enemy in enemies)
        {
            if (enemy.isAlive || enemy == null)
            {
                enemies.Remove(enemy);
            }
        }
    }
    
    void EnhanceEnemies()
    {   
        foreach (var enemy in enemies)
        {
            if (enemy.enhanced < enhancements)
            {
                enemy.Enhance(1);
            }
        }
    }
}
