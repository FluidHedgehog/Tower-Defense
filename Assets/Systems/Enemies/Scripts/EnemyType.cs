using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Scriptable Objects/EnemyType")]
public class EnemyType : ScriptableObject
{
    [Range(0, 1000)]
    public short health;

    [Range(0, 10)]
    public float speed;

    [Range(0, 10)]
    public byte damage;

    [Range(0, 10)]
    public byte blood;

    //[Range(0, 10)]
    // Chance to additional blood

    //[Range(0, 10)]
    // Additional blood
}
