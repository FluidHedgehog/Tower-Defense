using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Utils;

public abstract class AbilityInstance : MonoBehaviour
{
    [Header("Ability configuration")]
    public Ability ability;
    public GameObject showRange;

    [HideInInspector] public CircleCollider2D range { get; protected set; }
    [HideInInspector] public bool isActive { get; set; }

    [HideInInspector] public List<EnemyInstance> enemiesInRange = new();
    [HideInInspector] public PriorityQueue<EnemyInstance, float> prioritizedEnemies = new();

    protected virtual void OnEnable()
    {
        range = gameObject.AddComponent<CircleCollider2D>();
        range.radius = ability.range;
        range.isTrigger = true;

        showRange.transform.localScale *= ability.range * 2;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.TryGetComponent<EnemyInstance>(out var enemy)) return;

        enemiesInRange.Add(enemy);

        if (!isActive) StartCoroutine(AbilityCoroutine());
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {

        if (!other.TryGetComponent<EnemyInstance>(out var enemy)) return;

        enemiesInRange.Remove(enemy);
    }

    protected void CleanEnemiesList()
    {
        enemiesInRange.RemoveAll(e => e == null || !e.isAlive);

    }

    protected void RebuildQueue()
    {
        prioritizedEnemies.Clear();
        foreach (var enemy in enemiesInRange)
        {
            prioritizedEnemies.Enqueue(enemy, -enemy.totalDistance);
        }
    }

    protected void CleanAndRebuildQueue()
    {
        CleanEnemiesList();
        RebuildQueue();
    }

    public IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(ability.cooldown);
    }

    protected abstract IEnumerator AbilityCoroutine();

}
