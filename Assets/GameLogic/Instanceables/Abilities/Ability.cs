using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/Ability")]
public class Ability : ScriptableObject
{
    [Header("Damage, Slow Value, Buff Value")]
    [Tooltip("For slowValue use 1-20")]
    [Range(1, 100)]
    [SerializeField] public short baseValue;

    [Header("Range of the effect")]
    [Range(1, 10)]
    [SerializeField] public float range;

    [Header("How long between shots")]
    [Range(0.05f, 5)]
    [SerializeField] public float cooldown;

    [Header("How long will the effect take place")]
    [Range(0.05f, 5)]
    [SerializeField] public float howLong;

    [Header("Only for targeting one enemy at time")]
    [SerializeField] public GameObject projectile;
}
