using System.Collections.Generic;
using UnityEngine;

public class AbilityInstance : MonoBehaviour
{
    [SerializeField] CircleCollider2D range;
    [SerializeField] Ability ability;

    [SerializeField] List<EnemyInstance> enemiesInRange =new();

    void OnEnable()
    {
        range = gameObject.AddComponent<CircleCollider2D>();
        range.radius = ability.range;
        range.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        enemiesInRange.Add(enemy);
        Debug.Log("Added " + enemy);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EnemyInstance enemy = other.GetComponent<EnemyInstance>();
        enemiesInRange.Remove(enemy);
    }
}
