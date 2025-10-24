using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretMover : MonoBehaviour
{
    [SerializeField] InputsManager inputManager;
    [SerializeField] GridManager gridManager;
    [SerializeField] BloodSystem bloodSystem;

    [HideInInspector] public GameObject turret;
    [HideInInspector] int cost;

    void Start()
    {
        PlaceTowerEvents.Initialize(this);
        MoveTowerEvents.Initialize(this);
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
        var vector = Align(tile);

        BloodSystemEvents.TriggerBloodRemoved(cost);

        GameObject placedTurret = Instantiate(turret, vector, Quaternion.identity);
        gridManager.AddTurret(tile, placedTurret);
    }

    public void MoveTurret(Vector3Int tile)
    {
        var vector = Align(tile);

        if (TurretMerger.turret == null)
        {
            Debug.LogWarning("No turret in turret merger!");
        }
        GameObject placedTurret = TurretMerger.turret;
        placedTurret.transform.position = vector;
        gridManager.AddTurret(tile, placedTurret);
    }

    Vector3 Align(Vector3Int tile)
    {
        Vector3 vector = tile;
        vector.x += 0.5f;
        vector.y += 0.5f;

        return vector;
    }

    bool CanBuy(GameObject turret)
    {
        cost = turret.GetComponent<TurretInstance>().cost;
        if (cost < bloodSystem.currentBlood)
        {
            return true;
        }
        else return false;
    }
}
