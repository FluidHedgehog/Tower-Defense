using UnityEngine;
using System.Collections;

public class AreaAbilityInstance : AbilityInstance
{
    protected override IEnumerator AbilityCoroutine()
    {
        isActive = true;
        while (enemiesInRange.Count > 0)
        {
            yield return new WaitForSeconds(ability.cooldown);

            CleanAndRebuildQueue();

            ApplyEffectToEnemies();


        }
        if (enemiesInRange.Count == 0) isActive = false;
    }

    private void ApplyEffectToEnemies()
    {
        foreach (var enemy in enemiesInRange)
        {
            if (enemy == null || !enemy.isAlive) { continue; }

            switch (ability.effectType)
            {
                case EffectType.Damage:
                    enemy.ApplyDamage(ability.baseValue);
                    break;

                case EffectType.Slow:
                    if (enemy.isSlowed) break;
                    StartCoroutine(enemy.ApplySlow(ability.baseValue, ability.howLong));
                    break;

                case EffectType.DamageOverTime:
                    StartCoroutine(enemy.ApplyPoison(ability.cycles, ability.baseValue, ability.howLong));
                    break;

                case EffectType.BoostDamage:
                    if (enemy.isDamageBoosted) break;
                    StartCoroutine(enemy.ApplyBoostDamage(ability.baseValue, ability.howLong));
                    break;

                case EffectType.BoostBlood:
                    if (enemy.isBloodBoosted) break;
                    StartCoroutine(enemy.ApplyBoostBlood(ability.baseValue, ability.howLong));
                    break;
            }
        }
    }
}
