using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbominationInstance : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject space;

    EnemyManager enemyManager;
    [Range(1, 1000)]
    [SerializeField] int damage;

    [Range(0, 120)]
    [SerializeField] int cooldown;
    [SerializeField] bool canShoot;

    [SerializeField] Slider slider;

    void OnEnable()
    {
        Camera mainCamera = Camera.main;

        canvas.worldCamera = mainCamera;
        enemyManager = FindFirstObjectByType<EnemyManager>();
        enemyManager.enhancements += 1;

        slider.maxValue = cooldown;
        StartCoroutine(AbilityCoroutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SuperAbility();
        }
    }

    public void SuperAbility()
    {
        if (canShoot)
        {
            slider.value = 0;
            enemyManager.DamageAll(damage);
            canShoot = false;
            space.SetActive(false);
            StartCoroutine(AbilityCoroutine());
        }
    }

    IEnumerator AbilityCoroutine()
    {
        for (int i = 0; i < cooldown; i++)
        {
            slider.value = i + 1;
            yield return new WaitForSeconds(1f);
        }
        space.SetActive(true);
        canShoot = true;
    }

}
