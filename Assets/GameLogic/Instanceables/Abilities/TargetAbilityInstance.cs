using System.Collections;
using UnityEngine;

public class TargetAbilityInstance : AbilityInstance
{
    public EnemyInstance currentTarget { get; private set; }
    public bool canShoot { get; private set; } = true;

    protected override IEnumerator AbilityCoroutine()
    {
        isActive = true;
        
        while (isActive && enemiesInRange.Count > 0)
        {
            if (currentTarget == null || !currentTarget.isAlive || !enemiesInRange.Contains(currentTarget))
                ChooseTarget();

            if (ShouldShoot())
                yield return StartCoroutine(ShootAtTarget());
            else
                yield return null;
        }
        
        isActive = false;
        currentTarget = null;
    }

    private bool ShouldShoot()
    {
        return canShoot && currentTarget != null && currentTarget.isAlive;
    }

    private IEnumerator ShootAtTarget()
    {
        canShoot = false;
        
        var projectile = Instantiate(ability.projectile, transform);
        projectile.transform.localPosition = Vector3.zero;
        projectile.GetComponent<Projectile>().Initialize(currentTarget, ability.baseValue);

        yield return StartCoroutine(ShootCooldown());
        canShoot = true;
    }

    private void ChooseTarget()
    {
        CleanAndRebuildQueue();
        
        if (prioritizedEnemies.Count == 0)
        {
            currentTarget = null;
            return;
        }

        currentTarget = prioritizedEnemies.Dequeue();
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        
        if (currentTarget != null && other.gameObject == currentTarget.gameObject)
        {
            currentTarget = null;
        }
    }
}