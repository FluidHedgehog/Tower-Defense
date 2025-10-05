using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Scriptable Objects/Turret")]
public class Turret : ScriptableObject
{
    [SerializeField] public byte level;

    [SerializeField] public List<Ability> abilities;
}
