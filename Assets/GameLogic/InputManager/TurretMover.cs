using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretMover : MonoBehaviour
{
    [SerializeField] InputsManager inputManager;
    [SerializeField] GridManager gridManager;
  
    [HideInInspector] public GameObject turret;

    void Start()
    {
        PlaceTowerEvents.Initialize(this);
    }

    public void CreateTurret(GameObject currentTurret)
    {
        ChangeStates.ChangeStateNow(1);
        turret = currentTurret;
    }

    public void PlaceTurret(Vector3Int tile)
    {

        Vector3 vector = tile;
        vector.x += 0.5f;
        vector.y += 0.5f;

        Instantiate(turret, vector, Quaternion.identity);
        gridManager.AddTurret(tile, turret);
    }
}
