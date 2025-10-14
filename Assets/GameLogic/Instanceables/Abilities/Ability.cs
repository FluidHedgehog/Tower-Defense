using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/Ability")]
public class Ability : ScriptableObject
{
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
}
