using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretMover : MonoBehaviour
{
    [SerializeField] InputsManager inputManager;
    [SerializeField] GridManager gridManager;
    [SerializeField] BloodSystem bloodSystem;

    [HideInInspector] public GameObject turret;

    void Start()
    {
        PlaceTowerEvents.Initialize(this);
    }

    public void CreateTurret(GameObject currentTurret)
    {
        if (CanBuy(currentTurret))
        {
            ChangeStates.ChangeStateNow(1);
            turret = currentTurret;
        }
        else return;
    }

    public void PlaceTurret(Vector3Int tile)
    {
        Vector3 vector = tile;
        vector.x += 0.5f;
        vector.y += 0.5f;

        GameObject placedTurret = Instantiate(turret, vector, Quaternion.identity);
        gridManager.AddTurret(tile, placedTurret);
    }

    bool CanBuy(GameObject turret)
    {
        var cost = turret.GetComponent<TurretInstance>().cost;
        if (cost < bloodSystem.currentBlood)
        {
            BloodSystemEvents.TriggerBloodRemoved(cost);
            return true;
        }
        else return false;
    }
}
