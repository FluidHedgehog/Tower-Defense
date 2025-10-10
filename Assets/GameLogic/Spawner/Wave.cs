using UnityEngine;

public class Wave : MonoBehaviour
{
    [Tooltip("This enemy will be generated!")]
    [SerializeField] public GameObject enemyPrefab;

    [Range(0.1f, 2)]
    [Tooltip("Cooldown between enemies!")]
    [SerializeField] public float cooldown;

    [Range(1, 500)]
    [Tooltip("How much enemies will spawn!")]
    [SerializeField] public int quantity;
}
