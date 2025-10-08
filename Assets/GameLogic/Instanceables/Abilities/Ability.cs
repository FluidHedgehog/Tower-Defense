using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField] public short baseValue;
    [SerializeField] public float range;
    [SerializeField] public float cooldown;
    [SerializeField] public GameObject projectile;
}
