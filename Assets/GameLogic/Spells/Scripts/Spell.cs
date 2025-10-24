using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spells/Spell")]
public class Spell : ScriptableObject
{
    public EffectType effectType;

    [Header("Cost")]
    public int cost;

    [Header("Range of the effect")]
    [Range(1, 10)]
    [SerializeField] public float range;

    [Header("Damage, Slow Value, Buff Value")]
    [Tooltip("For slowValue use 1-20")]
    [Range(1, 100)]
    [SerializeField] public int baseValue;

    [Header("How long will the effect take place")]
    [Range(0.05f, 5)]
    [SerializeField] public float howLong;

    [Header("How long between shots")]
    [Range(0.05f, 5)]
    [SerializeField] public float cooldown;

    [Header("For Damage Over Time")]
    [Tooltip("How many times the baseValue damage will be applied")]
    [Range(1, 20)]
    [SerializeField] public int cycles;


}
