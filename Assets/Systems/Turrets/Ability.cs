using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField] short baseValue;
    [SerializeField] public float range;
    [SerializeField] float cooldown;
    [SerializeField] Sprite sprite;
}
