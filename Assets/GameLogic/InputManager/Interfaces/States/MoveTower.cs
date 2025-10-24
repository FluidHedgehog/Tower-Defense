using UnityEngine;

public class MoveTower : IState
{
    InputsManager inputManager;

    public MoveTower(InputsManager input) { inputManager = input; }

    public void Enter()
    {
        inputManager.pointEvent.AddListener(MoveTowerEvents.OnPoint);
        inputManager.clickEvent.AddListener(MoveTowerEvents.OnInteract);
        inputManager.holdStartEvent.AddListener(MoveTowerEvents.OnHold);
        inputManager.holdCancelEvent.AddListener(MoveTowerEvents.OnRelease);
    }

    public void Update()
    {

    }

    public void Exit()
    {
        inputManager.pointEvent.RemoveListener(MoveTowerEvents.OnPoint);
        inputManager.clickEvent.RemoveListener(MoveTowerEvents.OnInteract);
        inputManager.holdStartEvent.RemoveListener(MoveTowerEvents.OnHold);
        inputManager.holdCancelEvent.RemoveListener(MoveTowerEvents.OnRelease);
    }
    
}