using TMPro;
using UnityEngine;

public class EnemySetter : MonoBehaviour
{
    [SerializeField] EnemyType enemyPrefab;

    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI blood;

    void Start()
    {
        speed.text = $"Speed: {enemyPrefab.speed}";
        damage.text = $"Damage: {enemyPrefab.damage}";
        blood.text = $"Blood: {enemyPrefab.blood}";
    }
}
