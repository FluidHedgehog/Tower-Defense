using TMPro;
using UnityEngine;

public class TowerSetter : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;

    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI range;

    void Start()
    {
        cost.text = $"Cost: {towerPrefab.GetComponent<TurretInstance>().cost}";
        damage.text = $"Damage: {towerPrefab.GetComponentInChildren<AbilityInstance>().ability.baseValue}";
        range.text = $"Range: {towerPrefab.GetComponentInChildren<AbilityInstance>().ability.range}";
    }
}
