using UnityEngine;

public class PlaceTower : IState
{
    InputsManager inputManager;

    public PlaceTower(InputsManager input) { inputManager = input; }

    public void Enter()
    {
        Debug.Log("Entered PlaceTower");
        inputManager.pointEvent.AddListener(PlaceTowerEvents.OnPoint);
        inputManager.clickEvent.AddListener(PlaceTowerEvents.OnInteract);
        inputManager.holdStartEvent.AddListener(PlaceTowerEvents.OnHold);
        inputManager.holdCancelEvent.AddListener(PlaceTowerEvents.OnRelease);
    }

    public void Update()
    {

    }

    public void Exit()
    {
        inputManager.pointEvent.RemoveListener(PlaceTowerEvents.OnPoint);
        inputManager.clickEvent.RemoveListener(PlaceTowerEvents.OnInteract);
        inputManager.holdStartEvent.RemoveListener(PlaceTowerEvents.OnHold);
        inputManager.holdCancelEvent.RemoveListener(PlaceTowerEvents.OnRelease);
    }
}
