using System.Collections;
using UnityEngine;

public class SlowAbilityInstance : AbilityInstance
{
    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        if (enemy == null) return;
        enemiesInRange.Add(enemy);
        if (!isActive) StartCoroutine(ApplyAreaSlow());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        if (enemy == null) return;
        enemiesInRange.Remove(enemy);
        
    }

    void CleanQueue()
    {
        enemiesInRange.RemoveAll(e => e == null || !e.isAlive);
        prioritizedEnemies.Clear();

        foreach (var enemy in enemiesInRange)
        {
            prioritizedEnemies.Enqueue(enemy, -enemy.totalDistance);
        }
    }

    IEnumerator ApplyAreaSlow()
    {
        isActive = true;
        while (enemiesInRange.Count > 0)
        {
            yield return new WaitForSeconds(ability.cooldown);

            CleanQueue();

            foreach (var enemy in enemiesInRange)
            {
                if (enemy == null || !enemy.isAlive) { continue; }
                if (enemy.isSlowed) continue;
                else
                {
                    StartCoroutine(enemy.ApplySlow(ability.baseValue, ability.howLong));
                }
            }
        }
        if (enemiesInRange.Count == 0) isActive = false;
    }
}
            