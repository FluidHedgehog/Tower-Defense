using UnityEngine;
using System.Collections;

public class AreaAbilityInstance : AbilityInstance
{
    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        enemiesInRange.Add(enemy);

        if (!isActive) StartCoroutine(ApplyAreaDamage());
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        enemiesInRange.Remove(enemy);
    }

    IEnumerator ApplyAreaDamage()
    {
        isActive = true;
        while (enemiesInRange.Count > 0)
        {
            yield return new WaitForSeconds(ability.cooldown);

            foreach (var enemy in enemiesInRange)
            {
                if (enemy == null || !enemy.isAlive) { continue; }
                enemy.ApplyDamage(ability.baseValue);
            }
        }
        if (enemiesInRange.Count == 0) isActive = false;
    }
}
