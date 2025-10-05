using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[CustomEditor(typeof(TurretInstance))]
public class TurretInstance : MonoBehaviour
{
    [SerializeField] List<Ability> abilities;

    void OnEnable()
    {
        foreach (var ability in abilities)
        {
            var range = gameObject.AddComponent<CircleCollider2D>();
            range.radius = ability.range;
        }
    }

}
