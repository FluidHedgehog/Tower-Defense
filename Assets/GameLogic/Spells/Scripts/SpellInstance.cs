using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpellInstance : MonoBehaviour
{
    [SerializeField] Spell spell;
    [SerializeField] GameObject showRange;
    CircleCollider2D range;
    bool hasTriggered;

    [HideInInspector] public int cost;

    List<EnemyInstance> enemiesInArea = new List<EnemyInstance>();

    void OnEnable()
    {
        cost = spell.cost;
        range = gameObject.AddComponent<CircleCollider2D>();
        range.radius = spell.range;
        range.isTrigger = true;
        showRange.transform.localScale *= spell.range * 2;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var instance = collision.gameObject.GetComponent<EnemyInstance>();
        if (instance == null) return;

        if (!enemiesInArea.Contains(instance))
        {
            enemiesInArea.Add(instance);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var instance = collision.gameObject.GetComponent<EnemyInstance>();
        if (instance == null) return;

        if (enemiesInArea.Contains(instance))
        {
            enemiesInArea.Remove(instance);
        }
    }

    public void TriggetEffect()
    {
        if (hasTriggered) return;
        hasTriggered = true;
        switch (spell.effectType)
        {
            case EffectType.Damage:
                ApplyDamage();
                return;

            case EffectType.Slow:
                ApplySlow();
                return;

            case EffectType.DamageOverTime:
                ApplyDamageOverTime();
                return;

            default:
                return;
        }
    }

    void ApplyDamage()
    {
        foreach (var enemy in enemiesInArea)
        {
            enemy.ApplyDamage(spell.baseValue);
        }
    }

    void ApplySlow()
    {
        foreach (var enemy in enemiesInArea)
        {
            enemy.ApplySlow(spell.baseValue, spell.howLong);
        }
    }

    void ApplyDamageOverTime()
    {
        foreach (var enemy in enemiesInArea)
        {
            enemy.ApplyPoison(spell.cycles, spell.baseValue, spell.howLong);
        }
    }



}
