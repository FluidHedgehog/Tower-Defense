using System;
using UnityEngine;

public enum EffectType { Damage, Slow, DamageOverTime, BoostDamage, BoostBlood, Stun, Single, }

public enum DPSCalculator { Target, Area, DamageOverTime }

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/Ability")]
public class Ability : ScriptableObject
{
   float probableEnemies;

    public EffectType effectType;
    public DPSCalculator calculator;

    [Header("DPS Calculator")]
    [SerializeField] private float regularDPS;
    [Range(0.5f, 2)]
    [SerializeField] private float enemyDensity;
    [Range(0, 1)]
    [SerializeField] private float uptime;

    [Header("Damage, Slow Value, Buff Value")]
    [Tooltip("For slowValue use 1-20")]
    [Range(1, 100)]
    [SerializeField] public int baseValue;

    [Header("Range of the effect")]
    [Range(1, 10)]
    [SerializeField] public float range;

    [Header("For Damage Over Time")]
    [Tooltip("How many times the baseValue damage will be applied")]
    [Range(1, 20)]
    [SerializeField] public int cycles;

    [Header("How long between shots")]
    [Range(0.05f, 5)]
    [SerializeField] public float cooldown;

    [Header("How long will the effect take place")]
    [Range(0.05f, 5)]
    [SerializeField] public float howLong;

    [Header("Only for targeting one enemy at time")]
    [SerializeField] public GameObject projectile;


    public float RegularDPS
    {
        
        get
        {
            return regularDPS;
            
        }
    }

    private void OnValidate()
    {
        probableEnemies = Mathf.PI * range * range * enemyDensity;

        switch (calculator)
            {
                case DPSCalculator.Target:
                    regularDPS = (baseValue / cooldown) * uptime;
                break;
                case DPSCalculator.Area:
                    regularDPS = ((baseValue * probableEnemies) / cooldown) * uptime;
                    break;
                case DPSCalculator.DamageOverTime:
                regularDPS = ((baseValue * cycles) / howLong) * uptime;
                break;
                case DPSCalculator:
                    regularDPS = baseValue;
                    break;
            }
#if UNITY_EDITOR
UnityEditor.EditorUtility.SetDirty(this);
#endif

    }
}
