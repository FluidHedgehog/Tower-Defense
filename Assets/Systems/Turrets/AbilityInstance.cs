using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Utils;

public class AbilityInstance : MonoBehaviour
{
    [Header("Assign Scriptable Object 'Ability' here.")]
    [Space(5)]

    public Ability ability;

    public GameObject showRange;

    [HideInInspector] public CircleCollider2D range { get; protected set; }

    [HideInInspector] public bool isActive { get; set; }

    [HideInInspector] public bool canShoot { get; set; }

    [HideInInspector] public List<EnemyInstance> enemiesInRange = new();
    [HideInInspector] public PriorityQueue<EnemyInstance, float> prioritizedEnemies = new();

    void OnEnable()
    {
        range = gameObject.AddComponent<CircleCollider2D>();
        range.radius = ability.range;
        range.isTrigger = true;
        canShoot = true;

        showRange.transform.localScale *= ability.range * 2;

    }

    public IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(ability.cooldown);
        canShoot = true;
    }
}
