using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class TargetAbilityInstance : AbilityInstance
{
    #nullable enable
    public EnemyInstance? currentTarget { get; set; }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        if (enemy == null) return;

        enemiesInRange.Add(enemy);

        if (!isActive)
        {
            currentTarget = enemy;
            isActive = true;
            StartCoroutine(ShootingCoroutine());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        enemiesInRange.Remove(enemy);

        if (currentTarget == enemy)
        {
            currentTarget = null; 
            ChooseTarget();
        }
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

    IEnumerator ShootingCoroutine()
    {
        while (isActive)
        {
            if (ShouldShootAtCurrentTarget())
            {

                canShoot = false;
                var projectile = Instantiate(ability.projectile, transform, false);
                projectile.transform.localPosition = Vector3.zero;
                projectile.GetComponent<Projectile>().Initialize(currentTarget, ability.baseValue);

                yield return StartCoroutine(ShootCooldown());
            }
            else
            {
                ChooseTarget();
                yield return null;
            }
        }
    }
    
    bool ShouldShootAtCurrentTarget()
    {
        return canShoot && currentTarget != null && currentTarget.isAlive && enemiesInRange.Contains(currentTarget);
    }

    void ChooseTarget()
    {
        CleanQueue();
        if (enemiesInRange.Count == 0)
        {
            isActive = false;
            currentTarget = null;
            return;
        }

        currentTarget = prioritizedEnemies.Dequeue();
    }
}
