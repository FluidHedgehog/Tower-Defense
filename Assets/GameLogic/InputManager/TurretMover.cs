using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretMover : MonoBehaviour
{
    [SerializeField] InputsManager inputManager;
    [SerializeField] StateMachine stateMachine;
 
    public GameObject turret;

    void Start()
    {
        PlaceTowerEvents.Initialize(this);
    }

    public void CreateTurret(GameObject currentTurret)
    {
        stateMachine.ChangeState(stateMachine.placeTowerState);
        turret = currentTurret;
    }

    public void PlaceTurret(Vector3Int tile)
    {
        Instantiate(turret, tile, Quaternion.identity);
    }
}
