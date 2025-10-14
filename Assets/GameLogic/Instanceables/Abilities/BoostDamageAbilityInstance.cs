using UnityEngine;
using System.Collections;

public class BoostDamageAbilityInstance : AbilityInstance
{
     void OnTriggerEnter2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        if (enemy == null) return;
        enemiesInRange.Add(enemy);
        if (!isActive) StartCoroutine(BoostBlood());
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

    IEnumerator BoostBlood()
    {
        isActive = true;
        while (enemiesInRange.Count > 0)
        {
            yield return new WaitForSeconds(ability.cooldown);
            foreach (var enemy in enemiesInRange)
            {
                if (enemy == null || !enemy.isAlive) { CleanQueue(); }
                if (enemy.isSlowed) continue;
                else
                {
                    StartCoroutine(enemy.ApplyBoostBlood(ability.baseValue, ability.howLong));
                }
            }
        }
        if (enemiesInRange.Count == 0) isActive = false;
    }
}
