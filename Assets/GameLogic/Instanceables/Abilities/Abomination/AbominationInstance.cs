using System.Collections;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.UI;

public class AbominationInstance : MonoBehaviour
{


    EnemyManager enemyManager;
    [Range(1, 1000)]
    [SerializeField] int damage;

    [Range(0, 120)]
    [SerializeField] int cooldown;
    [SerializeField] bool canShoot;

    [SerializeField] Slider slider;

    void OnEnable()
    {
        enemyManager = FindFirstObjectByType<EnemyManager>();
        enemyManager.enhancements += 1;

        slider.maxValue = cooldown;
        StartCoroutine(AbilityCoroutine());
    }

    public void SuperAbility()
    {
        if (canShoot)
        {
            enemyManager.DamageAll(damage);
            canShoot = false;
            StartCoroutine(AbilityCoroutine());
        }
    }

    IEnumerator AbilityCoroutine()
    {
        for (int i = cooldown; i > 0; i--)
        {
            slider.value += 1;
            yield return new WaitForSeconds(i);
        }

        canShoot = true;
    }

}
